using InventorySystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
