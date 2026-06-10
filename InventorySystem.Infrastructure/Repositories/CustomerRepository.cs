using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByIdAsync(int id) =>
            await _context.Customers.FindAsync(id);

        public async Task<IEnumerable<Customer>> GetAllAsync() =>
            await _context.Customers
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<Customer> CreateAsync(Customer entity)
        {
            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Customer> UpdateAsync(Customer entity)
        {
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> NameExistsAsync(string name,
            int? excludeId = null) =>
            await _context.Customers.AnyAsync(c =>
                c.Name == name && (excludeId == null || c.Id != excludeId));
    }
}
