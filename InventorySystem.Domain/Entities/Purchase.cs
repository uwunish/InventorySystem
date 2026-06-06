using System;
using System.Collections.Generic;
using System.Text;
using InventorySystem.Domain.Common;

namespace InventorySystem.Domain.Entities
{
	public class Purchase : BaseEntity
	{
		public long VendorId { get; private set; }
		public DateTime PurchaseDate { get; private set; }
		public long CreatedByUserId { get; private set; }
	}
}
