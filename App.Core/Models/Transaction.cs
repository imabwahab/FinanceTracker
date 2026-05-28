using App.Core.Enums;

namespace App.Core.Models
{
    public class Transaction
    {
        public required string Id { get; set; }
        public required string AccountId { get; set; }
        public required string CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
        public bool IsRecurring { get; set; } = false;
        public RecurringFrequencyEnum? RecurringFrequency { get; set; }
        public TransactionStatusEnum TransactionStatus { get; set; } = TransactionStatusEnum.Cleared;
    }
}
