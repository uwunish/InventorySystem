using InventorySystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Interfaces
{
    public interface IStockRepository
    {
        Task<IEnumerable<StockItemDto>> GetCurrentStockAsync();
        Task<StockItemDto?> GetStockForProductAsync(int productId);
    }
}
