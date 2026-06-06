using System;
using System.Collections.Generic;
using System.Text;
using InventorySystem.Domain.Common;

namespace InventorySystem.Domain.Entities
{
	public class Purchase : BaseEntity
	{
		public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
		public int VendorId { get; set; }
		public Vendor? Vendor { get; set; }
		public int UserId { get; set; }
		public User? User { get; set; }
		public ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
	}
}
