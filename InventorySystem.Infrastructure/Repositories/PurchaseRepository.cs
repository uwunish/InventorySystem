using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDbContext _context;

        public PurchaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Purchase?> GetByIdAsync(int id) =>
            await _context.Purchases
                .Include(p => p.Vendor)
                .Include(p => p.User)
                .Include(p => p.PurchaseItems)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Purchase>> GetAllAsync() =>
            await _context.Purchases
                .Include(p => p.Vendor)
                .Include(p => p.User)
                .Include(p => p.PurchaseItems)
                    .ThenInclude(i => i.Product)
                .OrderByDescending(p => p.PurchaseDate)
                .ToListAsync();

        public async Task<Purchase> CreateAsync(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            // Reload with all relationships populated for the response
            return await GetByIdAsync(purchase.Id) ?? purchase;
        }
    }
}
