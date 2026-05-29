using System;
using System.Collections.Generic;
using App.Core.Enums;
using App.Core.Models;
using Microsoft.Data.SqlClient;

namespace App.Core.Services
{
    // ADO.NET implementation of ICategoryService.
    //
    // Same pattern as DbAccountService, with ONE new wrinkle:
    // The MonthlyBudget column is nullable (decimal? in C#, NULL in SQL).
    // That means reading and writing it requires the DBNull.Value dance.
    public class DbCategoryService : ICategoryService
    {
        private readonly string _connectionString;

        public DbCategoryService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // -------------------------------------------------------------
        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Id, Name, CategoryType, MonthlyBudget, Status " +
                    "FROM Categories ORDER BY Name", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(MapRow(reader));
                        }
                    }
                }
            }

            return categories;
        }

        // -------------------------------------------------------------
        public Category? GetById(string id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Id, Name, CategoryType, MonthlyBudget, Status " +
                    "FROM Categories WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return MapRow(reader);
                    }
                }
            }

            return null;
        }

        // -------------------------------------------------------------
        public Category Add(Category category)
        {
            ArgumentNullException.ThrowIfNull(category);

            const int maxAttempts = 5;
            SqlException? lastDuplicateKeyException = null;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                category.Id = GenerateCategoryId();

                try
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO Categories (Id, Name, CategoryType, MonthlyBudget, Status) " +
                            "VALUES (@Id, @Name, @CategoryType, @MonthlyBudget, @Status)", conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", category.Id);
                            cmd.Parameters.AddWithValue("@Name", category.Name);
                            cmd.Parameters.AddWithValue("@CategoryType", category.CategoryType.ToString());
                            cmd.Parameters.AddWithValue("@MonthlyBudget",
                                (object?)category.MonthlyBudget ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Status", category.CategoryStatus.ToString());

                            cmd.ExecuteNonQuery();
                        }
                    }

                    return category;
                }
                catch (SqlException ex) when (IsDuplicateKeyException(ex))
                {
                    lastDuplicateKeyException = ex;
                }
            }

            throw new InvalidOperationException(
                "Unable to generate a unique category ID after multiple attempts.",
                lastDuplicateKeyException);
        }

        // -------------------------------------------------------------
        public bool Update(Category category)
        {
            ArgumentNullException.ThrowIfNull(category);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Categories SET " +
                    "  Name = @Name, " +
                    "  CategoryType = @CategoryType, " +
                    "  MonthlyBudget = @MonthlyBudget, " +
                    "  Status = @Status " +
                    "WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", category.Id);
                    cmd.Parameters.AddWithValue("@Name", category.Name);
                    cmd.Parameters.AddWithValue("@CategoryType", category.CategoryType.ToString());
                    cmd.Parameters.AddWithValue("@MonthlyBudget",
                        (object?)category.MonthlyBudget ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", category.CategoryStatus.ToString());

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // -------------------------------------------------------------
        public bool Delete(string id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Categories WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // -------------------------------------------------------------
        public List<Category> Search(string text, CategoryTypeEnum? type, CategoryStatusEnum? status)
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Escape LIKE wildcards in user input
                string escapedText = (text ?? "").Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");

                string sql =
                    "SELECT Id, Name, CategoryType, MonthlyBudget, Status " +
                    "FROM Categories WHERE Name LIKE @Text ESCAPE '\\'";

                if (type != null) sql += " AND CategoryType = @CategoryType";
                if (status != null) sql += " AND Status = @Status";
                sql += " ORDER BY Name";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Text", "%" + escapedText + "%");

                    if (type != null)
                        cmd.Parameters.AddWithValue("@CategoryType", type.Value.ToString());
                    if (status != null)
                        cmd.Parameters.AddWithValue("@Status", status.Value.ToString());

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(MapRow(reader));
                        }
                    }
                }
            }

            return categories;
        }

        // =============================================================
        // PRIVATE HELPERS
        // =============================================================
        private static string GenerateCategoryId()
        {
            return "C-" + Guid.NewGuid().ToString("N");
        }

        private static bool IsDuplicateKeyException(SqlException ex)
        {
            return ex.Number == 2627 || ex.Number == 2601;
        }

        private static Category MapRow(SqlDataReader reader)
        {
            return new Category
            {
                Id = (reader["Id"]?.ToString()) ?? throw new InvalidOperationException("Id cannot be null"),
                Name = (reader["Name"]?.ToString()) ?? throw new InvalidOperationException("Name cannot be null"),
                CategoryType = Enum.Parse<CategoryTypeEnum>(
                    reader["CategoryType"]?.ToString() ?? throw new InvalidOperationException("CategoryType cannot be null")),
                MonthlyBudget = reader["MonthlyBudget"] == DBNull.Value
                    ? null
                    : Convert.ToDecimal(reader["MonthlyBudget"]),
                CategoryStatus = Enum.Parse<CategoryStatusEnum>(
                    reader["Status"]?.ToString() ?? throw new InvalidOperationException("Status cannot be null"))
            };
        }
    }
}