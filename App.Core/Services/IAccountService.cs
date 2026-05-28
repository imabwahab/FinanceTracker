using System.Collections.Generic;
using App.Core.Enums;
using App.Core.Models;

namespace App.Core.Services
{
    public interface IAccountService
    {
        List<Account> GetAll();
        Account GetById(string id);
        Account Add(Account account);
        bool Update(Account account);
        bool Delete(string id);
        List<Account> Search(string text, AccountTypeEnum? type, AccountStatusEnum? status);
    }
}