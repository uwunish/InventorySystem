using InventorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
