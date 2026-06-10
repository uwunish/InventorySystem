using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StockItemDto>> GetCurrentStockAsync()
        {
            // Step 1: Get all products with their relationships
            var products = await _context.Products
                .Include(p => p.ProductGroup)
                .Include(p => p.UnitOfMeasure)
                .Where(p => p.Status == Domain.Enums.Status.Active)
                .ToListAsync();

            // Step 2: Get all purchase totals grouped by product
            var purchaseTotals = await _context.PurchaseItems
                .GroupBy(pi => pi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(pi => pi.Quantity),
                    TotalValue = g.Sum(pi => pi.Quantity * pi.PurchasePrice)
                })
                .ToListAsync();

            // Step 3: Get all sale totals grouped by product
            var saleTotals = await _context.SaleItems
                .GroupBy(si => si.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(si => si.Quantity)
                })
                .ToListAsync();

            // Step 4: Combine everything in memory
            var stock = products.Select(product =>
            {
                var purchased = purchaseTotals
                    .FirstOrDefault(p => p.ProductId == product.Id);
                var sold = saleTotals
                    .FirstOrDefault(s => s.ProductId == product.Id);

                var totalPurchasedQty = purchased?.TotalQuantity ?? 0;
                var totalPurchaseValue = purchased?.TotalValue ?? 0;
                var totalSoldQty = sold?.TotalQuantity ?? 0;

                // Average rate = total purchase value / total purchase quantity
                // Guard against division by zero when no purchases exist
                var averageRate = totalPurchasedQty > 0
                    ? totalPurchaseValue / totalPurchasedQty
                    : 0;

                return new StockItemDto
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductGroupName = product.ProductGroup?.Name
                        ?? string.Empty,
                    UnitOfMeasureName = product.UnitOfMeasure?.Name
                        ?? string.Empty,
                    UnitOfMeasureCode = product.UnitOfMeasure?.Code
                        ?? string.Empty,
                    TotalPurchased = totalPurchasedQty,
                    TotalSold = totalSoldQty,
                    AverageRate = Math.Round(averageRate, 4)
                };
            });

            return stock.OrderBy(s => s.ProductName).ToList();
        }

        public async Task<StockItemDto?> GetStockForProductAsync(int productId)
        {
            var product = await _context.Products
                .Include(p => p.ProductGroup)
                .Include(p => p.UnitOfMeasure)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null) return null;

            var purchased = await _context.PurchaseItems
                .Where(pi => pi.ProductId == productId)
                .GroupBy(pi => pi.ProductId)
                .Select(g => new
                {
                    TotalQuantity = g.Sum(pi => pi.Quantity),
                    TotalValue = g.Sum(pi => pi.Quantity * pi.PurchasePrice)
                })
                .FirstOrDefaultAsync();

            var soldQty = await _context.SaleItems
                .Where(si => si.ProductId == productId)
                .SumAsync(si => si.Quantity);

            var totalPurchasedQty = purchased?.TotalQuantity ?? 0;
            var totalPurchaseValue = purchased?.TotalValue ?? 0;

            var averageRate = totalPurchasedQty > 0
                ? totalPurchaseValue / totalPurchasedQty
                : 0;

            return new StockItemDto
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductGroupName = product.ProductGroup?.Name ?? string.Empty,
                UnitOfMeasureName = product.UnitOfMeasure?.Name ?? string.Empty,
                UnitOfMeasureCode = product.UnitOfMeasure?.Code ?? string.Empty,
                TotalPurchased = totalPurchasedQty,
                TotalSold = soldQty,
                AverageRate = Math.Round(averageRate, 4)
            };
        }
    }
}
