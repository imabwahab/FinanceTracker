using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Enums;
using App.Core.Models;

namespace App.Core.Services
{
    public interface ITransactionService
    {
        List<Transaction> GetAll();

        /// <summary>
        /// Asynchronous version of <see cref="GetAll"/>. Keeps the UI thread
        /// responsive while the (potentially large) transaction list loads.
        /// </summary>
        Task<List<Transaction>> GetAllAsync();

        Transaction? GetById(string id);
        Transaction Add(Transaction transaction);
        bool Update(Transaction transaction);
        bool Delete(string id);

        List<Transaction> Search(
            string text,
            string accountId,
            string categoryId,
            DateTime? fromDate,
            DateTime? toDate,
            TransactionStatusEnum? status);
    }
}