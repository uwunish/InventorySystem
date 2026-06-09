using InventorySystem.Domain.Entities;

namespace InventorySystem.Application.Interfaces
{
    public interface IUnitOfMeasureRepository
    {
        Task<UnitOfMeasure?> GetByIdAsync(int id);
        Task<IEnumerable<UnitOfMeasure>> GetAllAsync();
        Task<UnitOfMeasure> CreateAsync(UnitOfMeasure uom);
        Task<UnitOfMeasure> UpdateAsync(UnitOfMeasure uom);
        Task<bool> NameExistsAsync(string name, int? excludeId = null);
        Task<bool> CodeExistsAsync(string code, int? excludeId = null);
    }
}
