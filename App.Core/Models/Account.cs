using App.Core.Enums;

namespace App.Core.Models
{
    internal class Account
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public AccountTypeEnum AccountType { get; set; }
        public decimal OpeningBalance { get; set; }
        public string Currency { get; set; } = "PKR";
        public AccountStatusEnum AccountStatus { get; set; } = AccountStatusEnum.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
