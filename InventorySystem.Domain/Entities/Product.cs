using System;
using System.Collections.Generic;
using System.Text;
using InventorySystem.Domain.Common;
using InventorySystem.Domain.Enums;

namespace InventorySystem.Domain.Entities
{
	public class Product : BaseEntity
	{
		public string Name { get; private set; } = string.Empty;
		public string? Description { get; private set; }
		public Status Status { get; private set; } = Status.Active;
		public long ProductGroupId { get; private set; }
		public long UnitOfMeasureId { get; private set; }
		public long CreatedByUserId { get; private set; }
		public ProductGroup ProductGroup { get; private set; }
		public UnitOfMeasure UnitOfMeasure { get; private set; }
		public User CreatedByUser { get; private set; }
	}
}
