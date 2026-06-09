using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Enums;

namespace InventorySystem.Application.Features.ProductGroups.Commands
{
    // --- CREATE ---
    public class CreateProductGroupCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Active;
        public int UserId { get; set; }
    }

    public class CreateProductGroupCommandHandler
    {
        private readonly IProductGroupRepository _repository;

        public CreateProductGroupCommandHandler(IProductGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductGroupDto>> Handle(
            CreateProductGroupCommand command)
        {
            if (await _repository.NameExistsAsync(command.Name))
                return Result<ProductGroupDto>.Failure(
                    "A product group with this name already exists.");

            var entity = new ProductGroup
            {
                Name = command.Name.Trim(),
                Description = command.Description.Trim(),
                Status = command.Status,
                UserId = command.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(entity);
            return Result<ProductGroupDto>.Success(ProductGroupMapper.MapToDto(created));
        }
    }

    public class UpdateProductGroupCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; }
    }

    public class UpdateProductGroupCommandHandler
    {
        private readonly IProductGroupRepository _repository;

        public UpdateProductGroupCommandHandler(IProductGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductGroupDto>> Handle(
            UpdateProductGroupCommand command)
        {
            var entity = await _repository.GetByIdAsync(command.Id);
            if (entity == null)
                return Result<ProductGroupDto>.Failure("Product group not found.");

            if (await _repository.NameExistsAsync(command.Name, command.Id))
                return Result<ProductGroupDto>.Failure(
                    "A product group with this name already exists.");

            entity.Name = command.Name.Trim();
            entity.Description = command.Description.Trim();
            entity.Status = command.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(entity);
            return Result<ProductGroupDto>.Success(ProductGroupMapper.MapToDto(updated));
        }
    }

    internal static class ProductGroupMapper
    {
        internal static ProductGroupDto MapToDto(ProductGroup entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Status = entity.Status,
            UserId = entity.UserId,
            CreatedAt = entity.CreatedAt
        };
    }
}
