using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Domain.Entities
{
    public class SaleItem
    {
        public int SaleId { get; set; }
        public Sale? Sale { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal SalesPrice { get; set; }
    }
}
