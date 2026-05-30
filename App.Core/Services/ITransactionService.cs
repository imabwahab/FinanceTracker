using System;
using System.Collections.Generic;
using App.Core.Enums;
using App.Core.Models;

namespace App.Core.Services
{
    public interface ITransactionService
    {
        List<Transaction> GetAll();
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