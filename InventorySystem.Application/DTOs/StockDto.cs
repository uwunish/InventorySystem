using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.DTOs
{
    public class StockItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductGroupName { get; set; } = string.Empty;
        public string UnitOfMeasureName { get; set; } = string.Empty;
        public string UnitOfMeasureCode { get; set; } = string.Empty;
        public decimal TotalPurchased { get; set; }
        public decimal TotalSold { get; set; }
        public decimal StockQuantity => TotalPurchased - TotalSold;
        public decimal AverageRate { get; set; }
        public decimal StockValue => StockQuantity * AverageRate;
    }
}
