using System;
using System.Collections.Generic;
using System.Text;
using InventorySystem.Domain.Common;
using InventorySystem.Domain.Enums;

namespace InventorySystem.Domain.Entities
{
	public class ProductGroup : BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public Status Status { get; set; }
		public int UserId { get; set; }
		public User? User { get; set; } 
	}
}
