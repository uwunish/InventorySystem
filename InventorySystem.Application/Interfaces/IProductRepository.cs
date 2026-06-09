using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> NameExistsAsync(string name, int? excludeId = null);
    }
}
