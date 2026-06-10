using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Features.Purchases.Commands;
using InventorySystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Purchases.Queries
{
    public class GetPurchasesQuery { }

    public class GetPurchasesQueryHandler
    {
        private readonly IPurchaseRepository _repository;

        public GetPurchasesQueryHandler(IPurchaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<PurchaseDto>>> Handle(
            GetPurchasesQuery query)
        {
            var entities = await _repository.GetAllAsync();
            return Result<IEnumerable<PurchaseDto>>.Success(
                entities.Select(CreatePurchaseCommandHandler.MapToDtoPublic));
        }
    }

    public class GetPurchaseByIdQuery { public int Id { get; set; } }

    public class GetPurchaseByIdQueryHandler
    {
        private readonly IPurchaseRepository _repository;

        public GetPurchaseByIdQueryHandler(IPurchaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PurchaseDto>> Handle(
            GetPurchaseByIdQuery query)
        {
            var entity = await _repository.GetByIdAsync(query.Id);
            if (entity == null)
                return Result<PurchaseDto>.Failure("Purchase not found.");

            return Result<PurchaseDto>.Success(
                CreatePurchaseCommandHandler.MapToDtoPublic(entity));
        }
    }
}
