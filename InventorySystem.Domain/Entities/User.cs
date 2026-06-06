using System;
using System.Collections.Generic;
using System.Text;
using InventorySystem.Domain.Common;
using InventorySystem.Domain.Enums;

namespace InventorySystem.Domain.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string MobileNumber { get; set; } = string.Empty;
		public Status Status { get; set; } = Status.Active;
		public string PasswordHash { get; set; } = string.Empty;
	}
}
