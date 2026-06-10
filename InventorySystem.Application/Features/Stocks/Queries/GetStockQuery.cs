using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Stocks.Queries
{
    public class GetCurrentStockQuery { }

    public class GetCurrentStockQueryHandler
    {
        private readonly IStockRepository _repository;

        public GetCurrentStockQueryHandler(IStockRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<StockItemDto>>> Handle(
            GetCurrentStockQuery query)
        {
            var stock = await _repository.GetCurrentStockAsync();
            return Result<IEnumerable<StockItemDto>>.Success(stock);
        }
    }
}
