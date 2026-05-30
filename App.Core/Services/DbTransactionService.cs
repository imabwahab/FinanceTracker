using System;
using System.Collections.Generic;
using App.Core.Enums;
using App.Core.Models;
using Microsoft.Data.SqlClient;

namespace App.Core.Services
{
    // ADO.NET implementation of ITransactionService.
    //
    // Two new wrinkles in this file:
    //   1. TWO nullable columns (Description, RecurringFrequency) instead of one.
    //   2. Search supports 6 optional filters - uses the WHERE 1=1 trick to keep
    //      the dynamic SQL clean.
    public class DbTransactionService : ITransactionService
    {
        private readonly string _connectionString;

        public DbTransactionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // -------------------------------------------------------------
        public List<Transaction> GetAll()
        {
            List<Transaction> transactions = new List<Transaction>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Id, AccountId, CategoryId, Amount, TransactionDate, " +
                    "       Description, IsRecurring, RecurringFrequency, Status " +
                    "FROM Transactions ORDER BY TransactionDate DESC", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactions.Add(MapRow(reader));
                        }
                    }
                }
            }

            return transactions;
        }

        // -------------------------------------------------------------
        public Transaction? GetById(string id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Id, AccountId, CategoryId, Amount, TransactionDate, " +
                    "       Description, IsRecurring, RecurringFrequency, Status " +
                    "FROM Transactions WHERE Id = @Id", conn))
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
        public Transaction Add(Transaction transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction);

            const int maxAttempts = 5;
            SqlException? lastDuplicateKeyException = null;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                transaction.Id = GenerateTransactionId();

                try
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO Transactions " +
                            "  (Id, AccountId, CategoryId, Amount, TransactionDate, " +
                            "   Description, IsRecurring, RecurringFrequency, Status) " +
                            "VALUES " +
                            "  (@Id, @AccountId, @CategoryId, @Amount, @TransactionDate, " +
                            "   @Description, @IsRecurring, @RecurringFrequency, @Status)", conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", transaction.Id);
                            cmd.Parameters.AddWithValue("@AccountId", transaction.AccountId);
                            cmd.Parameters.AddWithValue("@CategoryId", transaction.CategoryId);
                            cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
                            cmd.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
                            cmd.Parameters.AddWithValue("@Description",
                                (object?)transaction.Description ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@IsRecurring", transaction.IsRecurring);
                            cmd.Parameters.AddWithValue("@RecurringFrequency",
                                (object?)transaction.RecurringFrequency?.ToString() ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Status", transaction.TransactionStatus.ToString());

                            cmd.ExecuteNonQuery();
                        }
                    }

                    return transaction;
                }
                catch (SqlException ex) when (IsDuplicateKeyException(ex))
                {
                    lastDuplicateKeyException = ex;
                }
            }

            throw new InvalidOperationException(
                "Unable to generate a unique transaction ID after multiple attempts.",
                lastDuplicateKeyException);
        }

        // -------------------------------------------------------------
        public bool Update(Transaction transaction)
        {
            ArgumentNullException.ThrowIfNull(transaction);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Transactions SET " +
                    "  AccountId = @AccountId, " +
                    "  CategoryId = @CategoryId, " +
                    "  Amount = @Amount, " +
                    "  TransactionDate = @TransactionDate, " +
                    "  Description = @Description, " +
                    "  IsRecurring = @IsRecurring, " +
                    "  RecurringFrequency = @RecurringFrequency, " +
                    "  Status = @Status " +
                    "WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", transaction.Id);
                    cmd.Parameters.AddWithValue("@AccountId", transaction.AccountId);
                    cmd.Parameters.AddWithValue("@CategoryId", transaction.CategoryId);
                    cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
                    cmd.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
                    cmd.Parameters.AddWithValue("@Description",
                        (object?)transaction.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsRecurring", transaction.IsRecurring);
                    cmd.Parameters.AddWithValue("@RecurringFrequency",
                        (object?)transaction.RecurringFrequency?.ToString() ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", transaction.TransactionStatus.ToString());

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
                    "DELETE FROM Transactions WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // -------------------------------------------------------------
        // Search: 6 optional filters with LIKE wildcard escaping.
        // Uses "WHERE 1=1" so every conditional clause can start with " AND ".
        // -------------------------------------------------------------
        public List<Transaction> Search(
            string text,
            string accountId,
            string categoryId,
            DateTime? fromDate,
            DateTime? toDate,
            TransactionStatusEnum? status)
        {
            List<Transaction> transactions = new List<Transaction>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql =
                    "SELECT Id, AccountId, CategoryId, Amount, TransactionDate, " +
                    "       Description, IsRecurring, RecurringFrequency, Status " +
                    "FROM Transactions WHERE 1=1";

                if (!string.IsNullOrEmpty(text)) sql += " AND Description LIKE @Text ESCAPE '\\'";
                if (!string.IsNullOrEmpty(accountId)) sql += " AND AccountId = @AccountId";
                if (!string.IsNullOrEmpty(categoryId)) sql += " AND CategoryId = @CategoryId";
                if (fromDate != null) sql += " AND TransactionDate >= @FromDate";
                if (toDate != null) sql += " AND TransactionDate <= @ToDate";
                if (status != null) sql += " AND Status = @Status";

                sql += " ORDER BY TransactionDate DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(text))
                    {
                        string escapedText = text.Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");
                        cmd.Parameters.AddWithValue("@Text", "%" + escapedText + "%");
                    }
                    if (!string.IsNullOrEmpty(accountId))
                        cmd.Parameters.AddWithValue("@AccountId", accountId);
                    if (!string.IsNullOrEmpty(categoryId))
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    if (fromDate != null)
                        cmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
                    if (toDate != null)
                        cmd.Parameters.AddWithValue("@ToDate", toDate.Value);
                    if (status != null)
                        cmd.Parameters.AddWithValue("@Status", status.Value.ToString());

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactions.Add(MapRow(reader));
                        }
                    }
                }
            }

            return transactions;
        }

        // =============================================================
        // PRIVATE HELPERS
        // =============================================================
        private static string GenerateTransactionId()
        {
            // "T-" (2 chars) + 18 chars from a GUID = 20 chars total to fit NVARCHAR(20)
            return "T-" + Guid.NewGuid().ToString("N").Substring(0, 18);
        }

        private static bool IsDuplicateKeyException(SqlException ex)
        {
            return ex.Number == 2627 || ex.Number == 2601;
        }

        private static Transaction MapRow(SqlDataReader reader)
        {
            // Check for DBNull.Value BEFORE calling ToString() to properly detect SQL NULLs
            object idValue = reader["Id"];
            if (idValue == DBNull.Value)
                throw new InvalidOperationException("Id cannot be null");

            object accountIdValue = reader["AccountId"];
            if (accountIdValue == DBNull.Value)
                throw new InvalidOperationException("AccountId cannot be null");

            object categoryIdValue = reader["CategoryId"];
            if (categoryIdValue == DBNull.Value)
                throw new InvalidOperationException("CategoryId cannot be null");

            object statusValue = reader["Status"];
            if (statusValue == DBNull.Value)
                throw new InvalidOperationException("Status cannot be null");

            return new Transaction
            {
                Id = idValue.ToString()!,
                AccountId = accountIdValue.ToString()!,
                CategoryId = categoryIdValue.ToString()!,
                Amount = Convert.ToDecimal(reader["Amount"]),
                TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
                Description = reader["Description"] == DBNull.Value
                    ? null
                    : reader["Description"].ToString(),
                IsRecurring = Convert.ToBoolean(reader["IsRecurring"]),
                RecurringFrequency = reader["RecurringFrequency"] == DBNull.Value
                    ? null
                    : Enum.Parse<RecurringFrequencyEnum>(reader["RecurringFrequency"].ToString()!),
                TransactionStatus = Enum.Parse<TransactionStatusEnum>(statusValue.ToString()!)
            };
        }
    }
}