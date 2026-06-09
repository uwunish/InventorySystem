using InventorySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.DTOs
{
    public class UnitOfMeasureDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
