using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Sales.Commands
{
    public class CreateSaleCommand
    {
        public int? CustomerId { get; set; }
        public int UserId { get; set; }
        public List<CreateSaleItemRequest> Items { get; set; } = new();
    }

    public class CreateSaleCommandHandler
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IStockRepository _stockRepository;

        public CreateSaleCommandHandler(
            ISaleRepository saleRepository,
            IStockRepository stockRepository)
        {
            _saleRepository = saleRepository;
            _stockRepository = stockRepository;
        }

        public async Task<Result<SaleDto>> Handle(CreateSaleCommand command)
        {
            // Validate items exist
            if (command.Items == null || command.Items.Count == 0)
                return Result<SaleDto>.Failure(
                    "A sale must have at least one item.");

            // Validate each line item
            foreach (var item in command.Items)
            {
                if (item.Quantity <= 0)
                    return Result<SaleDto>.Failure(
                        "Quantity must be greater than zero.");

                if (item.SalesPrice <= 0)
                    return Result<SaleDto>.Failure(
                        "Sales price must be greater than zero.");

                // Check sufficient stock exists before selling
                var stock = await _stockRepository
                    .GetStockForProductAsync(item.ProductId);

                if (stock == null || stock.StockQuantity < item.Quantity)
                    return Result<SaleDto>.Failure(
                        $"Insufficient stock for product " +
                        $"'{stock?.ProductName ?? item.ProductId.ToString()}'. " +
                        $"Available: {stock?.StockQuantity ?? 0}, " +
                        $"Requested: {item.Quantity}");
            }

            // Build the sale aggregate
            var sale = new Sale
            {
                CustomerId = command.CustomerId,
                UserId = command.UserId,
                SaleDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SaleItems = command.Items.Select(i => new SaleItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    SalesPrice = i.SalesPrice,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }).ToList()
            };

            var created = await _saleRepository.CreateAsync(sale);
            return Result<SaleDto>.Success(MapToDtoPublic(created));
        }

        public static SaleDto MapToDtoPublic(Sale s) => new()
        {
            Id = s.Id,
            SaleDate = s.SaleDate,
            CustomerId = s.CustomerId,
            CustomerName = s.Customer?.Name ?? "Walk-in Customer",
            UserId = s.UserId,
            CreatedByName = s.User?.Name ?? string.Empty,
            CreatedAt = s.CreatedAt,
            Items = s.SaleItems.Select(i => new SaleItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ProductName = i.Product?.Name ?? string.Empty,
                Quantity = i.Quantity,
                SalesPrice = i.SalesPrice
            }).ToList()
        };
    }
}
