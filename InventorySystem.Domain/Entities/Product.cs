using System;
using System.Collections.Generic;
using System.Text;
using InventorySystem.Domain.Common;
using InventorySystem.Domain.Enums;

namespace InventorySystem.Domain.Entities
{
	public class Product : BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public Status Status { get; set; } = Status.Active;
		public int ProductGroupId { get; set; }
		public ProductGroup? ProductGroup { get; set; }
		public int UnitOfMeasureId { get; set; }
		public UnitOfMeasure? UnitOfMeasure { get; set; }
		public int UserId { get; set; }
		public User? User { get; set; }
	}
}
