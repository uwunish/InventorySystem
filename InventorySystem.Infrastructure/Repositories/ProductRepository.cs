using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products
                .Include(p => p.UnitOfMeasure)
                .Include(p => p.ProductGroup)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products
                .Include(p => p.UnitOfMeasure)
                .Include(p => p.ProductGroup)
                .OrderBy(p => p.Name)
                .ToListAsync();

        public async Task<Product> CreateAsync(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(entity.Id) ?? entity;
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(entity.Id) ?? entity;
        }

        public async Task<bool> NameExistsAsync(string name, int? excludeId = null) =>
            await _context.Products.AnyAsync(p =>
                p.Name == name && (excludeId == null || p.Id != excludeId));
    }
}
