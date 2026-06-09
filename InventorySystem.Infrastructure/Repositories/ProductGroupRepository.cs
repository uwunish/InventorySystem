using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Repositories
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly AppDbContext _context;

        public ProductGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductGroup?> GetByIdAsync(int id) =>
            await _context.ProductGroups.FindAsync(id);

        public async Task<IEnumerable<ProductGroup>> GetAllAsync() =>
            await _context.ProductGroups
                .OrderBy(p => p.Name)
                .ToListAsync();

        public async Task<ProductGroup> CreateAsync(ProductGroup entity)
        {
            _context.ProductGroups.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ProductGroup> UpdateAsync(ProductGroup entity)
        {
            _context.ProductGroups.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> NameExistsAsync(string name, int? excludeId = null) =>
            await _context.ProductGroups.AnyAsync(p =>
                p.Name == name && (excludeId == null || p.Id != excludeId));
    }
}
