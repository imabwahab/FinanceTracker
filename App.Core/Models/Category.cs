using App.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Models
{
    internal class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CategoryTypeEnum CategoryType { get; set; }
        public decimal? MonthlyBudget { get; set; }
        public CategoryStatusEnum CategoryStatus { get; set; } = CategoryStatusEnum.Active;

    }
}
