using System;
using System.Collections.Generic;
using System.Text;
using InventorySystem.Domain.Common;
using InventorySystem.Domain.Enums;

namespace InventorySystem.Domain.Entities
{
	public class ProductGroup : BaseEntity
	{
		public string Name { get; private set; } = string.Empty;
		public string? Description { get; private set; }
		public Status Status { get; private set; }
		public long CreatedByUserId { get; private set; }
		public User CreatedByUser { get; private set; } = null!;
		public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
	}
}
