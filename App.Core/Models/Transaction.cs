using App.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Models
{
    internal class Transaction
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
        public bool IsRecurring { get; set; } = false;
        public RecurringFrequencyEnum? RecurringFrequency { get; set; }
        public TransactionStatusEnum TransactionStatus { get; set; } = TransactionStatusEnum.Cleared; 
    }
}
