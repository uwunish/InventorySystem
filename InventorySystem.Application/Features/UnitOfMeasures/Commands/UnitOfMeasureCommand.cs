using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Enums;

namespace InventorySystem.Application.Features.UnitOfMeasures.Commands
{
    public class CreateUnitOfMeasureCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Active;
        public int UserId { get; set; }
    }

    public class CreateUnitOfMeasureCommandHandler
    {
        private readonly IUnitOfMeasureRepository _repository;

        public CreateUnitOfMeasureCommandHandler(
            IUnitOfMeasureRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<UnitOfMeasureDto>> Handle(
            CreateUnitOfMeasureCommand command)
        {
            if (await _repository.NameExistsAsync(command.Name))
                return Result<UnitOfMeasureDto>.Failure(
                    "A unit of measure with this name already exists.");

            if (await _repository.CodeExistsAsync(command.Code))
                return Result<UnitOfMeasureDto>.Failure(
                    "A unit of measure with this code already exists.");

            var entity = new UnitOfMeasure
            {
                Name = command.Name.Trim(),
                Code = command.Code.Trim().ToUpper(),
                Description = command.Description.Trim(),
                Status = command.Status,
                UserId = command.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(entity);
            return Result<UnitOfMeasureDto>.Success(UnitOfMeasureMapper.MapToDto(created));
        }
    }

    public class UpdateUnitOfMeasureCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; }
    }

    public class UpdateUnitOfMeasureCommandHandler
    {
        private readonly IUnitOfMeasureRepository _repository;

        public UpdateUnitOfMeasureCommandHandler(
            IUnitOfMeasureRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<UnitOfMeasureDto>> Handle(
            UpdateUnitOfMeasureCommand command)
        {
            var entity = await _repository.GetByIdAsync(command.Id);
            if (entity == null)
                return Result<UnitOfMeasureDto>.Failure(
                    "Unit of measure not found.");

            if (await _repository.NameExistsAsync(command.Name, command.Id))
                return Result<UnitOfMeasureDto>.Failure(
                    "A unit of measure with this name already exists.");

            if (await _repository.CodeExistsAsync(command.Code, command.Id))
                return Result<UnitOfMeasureDto>.Failure(
                    "A unit of measure with this code already exists.");

            entity.Name = command.Name.Trim();
            entity.Code = command.Code.Trim().ToUpper();
            entity.Description = command.Description.Trim();
            entity.Status = command.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(entity);
            return Result<UnitOfMeasureDto>.Success(
                UnitOfMeasureMapper.MapToDto(updated));
        }
    }

    internal static class UnitOfMeasureMapper
    {
        internal static UnitOfMeasureDto MapToDto(UnitOfMeasure e) => new()
        {
            Id = e.Id,
            Name = e.Name,
            Code = e.Code,
            Description = e.Description,
            Status = e.Status,
            UserId = e.UserId,
            CreatedAt = e.CreatedAt
        };
    }
}
