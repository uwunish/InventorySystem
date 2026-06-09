using InventorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Interfaces
{
    public interface IProductGroupRepository
    {
        Task<ProductGroup> GetByIdAsync(int id);
        Task<IEnumerable<ProductGroup>> GetAllAsync();
        Task<ProductGroup> CreateAsync(ProductGroup productGroup);
        Task<ProductGroup> UpdateAsync(ProductGroup productGroup);
        Task<bool> NameExistsAsync(string name, int? excludeId = null);
    }
}
