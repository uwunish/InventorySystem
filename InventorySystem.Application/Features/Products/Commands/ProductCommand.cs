using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Enums;

namespace InventorySystem.Application.Features.Products.Commands
{
    public class CreateProductCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Active;
        public int UnitOfMeasureId { get; set; }
        public int ProductGroupId { get; set; }
        public int UserId { get; set; }
    }

    public class CreateProductCommandHandler
    {
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductDto>> Handle(CreateProductCommand command)
        {
            if (await _repository.NameExistsAsync(command.Name))
                return Result<ProductDto>.Failure(
                    "A product with this name already exists.");

            var entity = new Product
            {
                Name = command.Name.Trim(),
                Description = command.Description.Trim(),
                Status = command.Status,
                UnitOfMeasureId = command.UnitOfMeasureId,
                ProductGroupId = command.ProductGroupId,
                UserId = command.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(entity);
            return Result<ProductDto>.Success(ProductMapper.MapToDto(created));
        }
    }

    public class UpdateProductCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; }
        public int UnitOfMeasureId { get; set; }
        public int ProductGroupId { get; set; }
    }

    public class UpdateProductCommandHandler
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductDto>> Handle(UpdateProductCommand command)
        {
            var entity = await _repository.GetByIdAsync(command.Id);
            if (entity == null)
                return Result<ProductDto>.Failure("Product not found.");

            if (await _repository.NameExistsAsync(command.Name, command.Id))
                return Result<ProductDto>.Failure(
                    "A product with this name already exists.");

            entity.Name = command.Name.Trim();
            entity.Description = command.Description.Trim();
            entity.Status = command.Status;
            entity.UnitOfMeasureId = command.UnitOfMeasureId;
            entity.ProductGroupId = command.ProductGroupId;
            entity.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(entity);
            return Result<ProductDto>.Success(ProductMapper.MapToDto(updated));
        }
    }

    internal static class ProductMapper
    {
        internal static ProductDto MapToDto(Product e) => new()
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            Status = e.Status,
            UnitOfMeasureId = e.UnitOfMeasureId,
            UnitOfMeasureName = e.UnitOfMeasure?.Name ?? string.Empty,
            ProductGroupId = e.ProductGroupId,
            ProductGroupName = e.ProductGroup?.Name ?? string.Empty,
            UserId = e.UserId,
            CreatedAt = e.CreatedAt
        };
    }
}
