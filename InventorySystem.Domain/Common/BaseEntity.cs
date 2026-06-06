using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Domain.Common
{
	public abstract class BaseEntity
	{
		public long Id { get; protected set; }
		public DateTime CreatedAt { get; protected set; }
	}
}
