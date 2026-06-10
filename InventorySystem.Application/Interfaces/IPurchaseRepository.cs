using InventorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<Purchase?> GetByIdAsync(int id);
        Task<IEnumerable<Purchase>> GetAllAsync();
        Task<Purchase> CreateAsync(Purchase purchase);
    }
}
