using InventorySystem.Domain.Common;
using InventorySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Status Status { get; set; } = Status.Active;
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
