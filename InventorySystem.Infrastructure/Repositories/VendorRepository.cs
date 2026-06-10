using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Infrastructure.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly AppDbContext _context;

        public VendorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Vendor?> GetByIdAsync(int id) =>
            await _context.Vendors.FindAsync(id);

        public async Task<IEnumerable<Vendor>> GetAllAsync()
        {
            return await _context.Vendors.OrderBy(v => v.Name).ToListAsync();
        } 

        public async Task<Vendor> CreateAsync(Vendor entity)
        {
            _context.Vendors.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Vendor> UpdateAsync(Vendor entity)
        {
            _context.Vendors.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> NameExistsAsync(string name,
            int? excludeId = null) =>
            await _context.Vendors.AnyAsync(v =>
                v.Name == name && (excludeId == null || v.Id != excludeId));
    }
}
