using System;
using System.Collections.Generic;
using System.Text;
using InventorySystem.Domain.Common;
using InventorySystem.Domain.Enums;

namespace InventorySystem.Domain.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; private set; } = string.Empty;
		public string Email { get; private set; } = string.Empty;
		public string MobileNumber { get; private set; } = string.Empty;
		public Status Status { get; private set; } = Status.Active;
		public string PasswordHash { get; private set; } = string.Empty;
	}
}
