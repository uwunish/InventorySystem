using InventorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Interfaces
{
    public interface IVendorRepository
    {
        Task<Vendor?> GetByIdAsync(int id);
        Task<IEnumerable<Vendor>> GetAllAsync();
        Task<Vendor> CreateAsync(Vendor vendor);
        Task<Vendor> UpdateAsync(Vendor vendor);
        Task<bool> NameExistsAsync(string name, int? excludeId = null);
    }
}
