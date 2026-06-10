using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Features.Sales.Commands;
using InventorySystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Sales.Queries
{
    public class GetSalesQuery { }

    public class GetSalesQueryHandler
    {
        private readonly ISaleRepository _repository;

        public GetSalesQueryHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<SaleDto>>> Handle(
            GetSalesQuery query)
        {
            var entities = await _repository.GetAllAsync();
            return Result<IEnumerable<SaleDto>>.Success(
                entities.Select(CreateSaleCommandHandler.MapToDtoPublic));
        }
    }

    public class GetSaleByIdQuery { public int Id { get; set; } }

    public class GetSaleByIdQueryHandler
    {
        private readonly ISaleRepository _repository;

        public GetSaleByIdQueryHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<SaleDto>> Handle(GetSaleByIdQuery query)
        {
            var entity = await _repository.GetByIdAsync(query.Id);
            if (entity == null)
                return Result<SaleDto>.Failure("Sale not found.");

            return Result<SaleDto>.Success(
                CreateSaleCommandHandler.MapToDtoPublic(entity));
        }
    }
}
