using System.Collections.Generic;
using App.Core.Enums;
using App.Core.Models;

namespace App.Core.Services
{
    public interface IAccountService
    {
        List<Account> GetAll();

        /// <summary>
        /// Returns the current (cleared) balance for every account, keyed by account Id.
        ///
        /// CurrentBalance = OpeningBalance + Σ(Income amounts) − Σ(Expense amounts),
        /// counting only transactions with status 'Cleared'. Income vs. expense is
        /// determined by the linked category's type, since Transaction.Amount itself
        /// is unsigned. Computed server-side so it can never drift out of sync.
        /// </summary>
        Dictionary<string, decimal> GetCurrentBalances();

        Account? GetById(string id);
        Account Add(Account account);
        bool Update(Account account);
        bool Delete(string id);
        List<Account> Search(string text, AccountTypeEnum? type, AccountStatusEnum? status);
    }
}