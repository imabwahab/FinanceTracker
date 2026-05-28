using App.Core.Enums;

namespace App.Core.Models
{
    public class Category
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public CategoryTypeEnum CategoryType { get; set; }
        public decimal? MonthlyBudget { get; set; }
        public CategoryStatusEnum CategoryStatus { get; set; } = CategoryStatusEnum.Active;
    }
}
