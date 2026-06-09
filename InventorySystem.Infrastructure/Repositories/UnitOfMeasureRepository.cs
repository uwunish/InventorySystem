using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Repositories
{
    public class UnitOfMeasureRepository : IUnitOfMeasureRepository
    {
        private readonly AppDbContext _context;

        public UnitOfMeasureRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UnitOfMeasure?> GetByIdAsync(int id) =>
            await _context.UnitOfMeasures.FindAsync(id);

        public async Task<IEnumerable<UnitOfMeasure>> GetAllAsync() =>
            await _context.UnitOfMeasures
                .OrderBy(u => u.Name)
                .ToListAsync();

        public async Task<UnitOfMeasure> CreateAsync(UnitOfMeasure entity)
        {
            _context.UnitOfMeasures.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UnitOfMeasure> UpdateAsync(UnitOfMeasure entity)
        {
            _context.UnitOfMeasures.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> NameExistsAsync(string name, int? excludeId = null) =>
            await _context.UnitOfMeasures.AnyAsync(u =>
                u.Name == name && (excludeId == null || u.Id != excludeId));

        public async Task<bool> CodeExistsAsync(string code, int? excludeId = null) =>
            await _context.UnitOfMeasures.AnyAsync(u =>
                u.Code == code && (excludeId == null || u.Id != excludeId));
    }
}
