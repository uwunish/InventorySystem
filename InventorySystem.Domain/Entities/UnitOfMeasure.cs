using System;
using System.Collections.Generic;
using System.Text;
using InventorySystem.Domain.Common;
using InventorySystem.Domain.Enums;

namespace InventorySystem.Domain.Entities
{
	public class UnitOfMeasure : BaseEntity
	{
		public string Name { get; private set; } = string.Empty;
		public string Code { get; private set; } = string.Empty;
		public string? Description { get; private set; }
		public Status Status { get; private set; } = Status.Active;
		public long CreatedByUserId { get; private set; }
		public User CreatedByUser { get; private set; }
		private readonly List<Product> _products = new();
		public IReadOnlyCollection<Product> Products => _products;

		public UnitOfMeasure
			(
				string name,
				string code,
				string? description,
				long createdByUserId
				)
		{
			Name = name;
			Code = code;
			Description = description;
			CreatedByUserId = createdByUserId;
			CreatedAt = DateTime.UtcNow;
		}

		public void Update(string name, string code, string? description)
		{
			Name = name;
			Code = code;
			Description = description;
		}

		public void Activate()
		{
			Status = Status.Active;
		}

		public void Deactivate()
		{
			Status = Status.Inactive;
		}

	}
}
