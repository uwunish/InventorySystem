using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Purchases.Commands
{
    public class CreatePurchaseCommand
    {
        public int VendorId { get; set; }
        public int UserId { get; set; }
        public List<CreatePurchaseItemRequest> Items { get; set; } = new();
    }

    public class CreatePurchaseCommandHandler
    {
        private readonly IPurchaseRepository _repository;

        public CreatePurchaseCommandHandler(IPurchaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PurchaseDto>> Handle(
            CreatePurchaseCommand command)
        {
            // Validate that at least one item exists
            if (command.Items == null || command.Items.Count == 0)
                return Result<PurchaseDto>.Failure(
                    "A purchase must have at least one item.");

            // Validate each line item
            foreach (var item in command.Items)
            {
                if (item.Quantity <= 0)
                    return Result<PurchaseDto>.Failure(
                        "Quantity must be greater than zero.");
                if (item.PurchasePrice <= 0)
                    return Result<PurchaseDto>.Failure(
                        "Purchase price must be greater than zero.");
                if (item.SalesPrice <= 0)
                    return Result<PurchaseDto>.Failure(
                        "Sales price must be greater than zero.");
                if (item.SalesPrice < item.PurchasePrice)
                    return Result<PurchaseDto>.Failure(
                        "Sales price cannot be less than purchase price.");
            }

            // Build the aggregate — Purchase header + all items together
            var purchase = new Purchase
            {
                VendorId = command.VendorId,
                UserId = command.UserId,
                PurchaseDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PurchaseItems = command.Items.Select(i => new PurchaseItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    PurchasePrice = i.PurchasePrice,
                    SalesPrice = i.SalesPrice,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }).ToList()
            };

            var created = await _repository.CreateAsync(purchase);
            return Result<PurchaseDto>.Success(MapToDtoPublic(created));
        }

        public static PurchaseDto MapToDtoPublic(Purchase p) => new()
        {
            Id = p.Id,
            PurchaseDate = p.PurchaseDate,
            VendorId = p.VendorId,
            VendorName = p.Vendor?.Name ?? string.Empty,
            UserId = p.UserId,
            CreatedByName = p.User?.Name ?? string.Empty,
            CreatedAt = p.CreatedAt,
            Items = p.PurchaseItems.Select(i => new PurchaseItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ProductName = i.Product?.Name ?? string.Empty,
                Quantity = i.Quantity,
                PurchasePrice = i.PurchasePrice,
                SalesPrice = i.SalesPrice
            }).ToList()
        };
    }
}
