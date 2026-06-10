using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Sale?> GetByIdAsync(int id) =>
            await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.User)
                .Include(s => s.SaleItems)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<IEnumerable<Sale>> GetAllAsync() =>
            await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.User)
                .Include(s => s.SaleItems)
                    .ThenInclude(i => i.Product)
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();

        public async Task<Sale> CreateAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(sale.Id) ?? sale;
        }
    }
}
