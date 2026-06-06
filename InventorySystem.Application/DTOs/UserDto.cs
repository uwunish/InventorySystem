using InventorySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
