using System;
using System.Collections.Generic;
using App.Core.Enums;
using App.Core.Models;
using Microsoft.Data.SqlClient;

namespace App.Core.Services
{
    // ADO.NET implementation of IAccountService.
    //
    // This file is your REFERENCE TEMPLATE.
    // DbCategoryService and DbTransactionService follow the exact same shape:
    //   1. Constructor receives the connection string (constructor injection).
    //   2. Every method opens a connection in a `using` block, runs SQL, closes.
    //   3. All user values flow through SqlParameter - never string concatenation.
    //   4. A private MapRow helper builds the model object from a reader row (DRY).
    //   5. Enums are stored as their ToString() name; parsed back with Enum.Parse<T>.
    //   6. Ids are generated in C# using a short prefix + part of a GUID.
    public class DbAccountService : IAccountService
    {
        private readonly string _connectionString;

        public DbAccountService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // -------------------------------------------------------------
        // GetAll: returns every account, sorted by name.
        // -------------------------------------------------------------
        public List<Account> GetAll()
        {
            List<Account> accounts = new List<Account>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT Id, Name, AccountType, OpeningBalance, Currency, Status, CreatedAt " +
                    "FROM Accounts ORDER BY Name", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(MapRow(reader));
                    }
                }
            }

            return accounts;
        }

        // -------------------------------------------------------------
        // GetById: returns one account, or null if not found.
        // -------------------------------------------------------------
        public Account? GetById(string id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT Id, Name, AccountType, OpeningBalance, Currency, Status, CreatedAt " +
                    "FROM Accounts WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapRow(reader);
                    }
                }
            }

            return null;
        }

        // -------------------------------------------------------------
        // Add: generates an Id, inserts, returns the saved object.
        // -------------------------------------------------------------
        public Account Add(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            // Generate a short readable Id in C# (no IDENTITY column in the DB).
            account.Id = "A-" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            account.CreatedAt = DateTime.UtcNow;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Accounts (Id, Name, AccountType, OpeningBalance, Currency, Status, CreatedAt) " +
                    "VALUES (@Id, @Name, @AccountType, @OpeningBalance, @Currency, @Status, @CreatedAt)", conn);

                cmd.Parameters.AddWithValue("@Id", account.Id);
                cmd.Parameters.AddWithValue("@Name", account.Name);
                cmd.Parameters.AddWithValue("@AccountType", account.AccountType.ToString());
                cmd.Parameters.AddWithValue("@OpeningBalance", account.OpeningBalance);
                cmd.Parameters.AddWithValue("@Currency", account.Currency ?? "PKR");
                cmd.Parameters.AddWithValue("@Status", account.AccountStatus.ToString());
                cmd.Parameters.AddWithValue("@CreatedAt", account.CreatedAt);

                cmd.ExecuteNonQuery();
            }

            return account;
        }

        // -------------------------------------------------------------
        // Update: returns true only if an account with this Id existed.
        // -------------------------------------------------------------
        public bool Update(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Accounts SET " +
                    "  Name = @Name, " +
                    "  AccountType = @AccountType, " +
                    "  OpeningBalance = @OpeningBalance, " +
                    "  Currency = @Currency, " +
                    "  Status = @Status " +
                    "WHERE Id = @Id", conn);

                cmd.Parameters.AddWithValue("@Id", account.Id);
                cmd.Parameters.AddWithValue("@Name", account.Name);
                cmd.Parameters.AddWithValue("@AccountType", account.AccountType.ToString());
                cmd.Parameters.AddWithValue("@OpeningBalance", account.OpeningBalance);
                cmd.Parameters.AddWithValue("@Currency", account.Currency ?? "PKR");
                cmd.Parameters.AddWithValue("@Status", account.AccountStatus.ToString());

                // ExecuteNonQuery returns rows affected.
                // 0 means the WHERE clause matched nothing - the Id did not exist.
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // -------------------------------------------------------------
        // Delete: returns true only if a row was actually removed.
        // -------------------------------------------------------------
        public bool Delete(string id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Accounts WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // -------------------------------------------------------------
        // Search: text + optional filters. This is the dynamic SQL pattern
        // from the Lec 16 handout - build the WHERE clause based on which
        // filters are actually provided.
        // -------------------------------------------------------------
        public List<Account> Search(string text, AccountTypeEnum? type, AccountStatusEnum? status)
        {
            List<Account> accounts = new List<Account>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Always-present filter: name LIKE '%text%'
                string sql =
                    "SELECT Id, Name, AccountType, OpeningBalance, Currency, Status, CreatedAt " +
                    "FROM Accounts WHERE Name LIKE @Text";

                if (type != null) sql += " AND AccountType = @AccountType";
                if (status != null) sql += " AND Status = @Status";
                sql += " ORDER BY Name";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Text", "%" + (text ?? "") + "%");

                // Only add parameters that are actually referenced in the SQL string.
                // Adding an unreferenced parameter would make SQL Server complain.
                if (type != null)
                    cmd.Parameters.AddWithValue("@AccountType", type.Value.ToString());
                if (status != null)
                    cmd.Parameters.AddWithValue("@Status", status.Value.ToString());

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(MapRow(reader));
                    }
                }
            }

            return accounts;
        }

        // =============================================================
        // PRIVATE HELPER
        // =============================================================
        // MapRow builds one Account object from the current row of the reader.
        // Called by GetAll, GetById, and Search - write it once, fix bugs once.
        // This is the DRY principle in action.
        private Account MapRow(SqlDataReader reader)
        {
            return new Account
            {
                Id = (reader["Id"]?.ToString()) ?? throw new InvalidOperationException("Id cannot be null"),
                Name = (reader["Name"]?.ToString()) ?? throw new InvalidOperationException("Name cannot be null"),
                AccountType = Enum.Parse<AccountTypeEnum>(reader["AccountType"]?.ToString() ?? throw new InvalidOperationException("AccountType cannot be null")),
                OpeningBalance = Convert.ToDecimal(reader["OpeningBalance"]),
                Currency = reader["Currency"]?.ToString() ?? "PKR",
                AccountStatus = Enum.Parse<AccountStatusEnum>(reader["Status"]?.ToString() ?? throw new InvalidOperationException("Status cannot be null")),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }
    }
}