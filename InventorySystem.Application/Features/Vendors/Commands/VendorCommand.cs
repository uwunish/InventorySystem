using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Vendors.Commands
{
    public class CreateVendorCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Active;
        public int UserId { get; set; }
    }

    public class CreateVendorCommandHandler
    {
        private readonly IVendorRepository _repository;

        public CreateVendorCommandHandler(IVendorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<VendorDto>> Handle(CreateVendorCommand command)
        {
            if (await _repository.NameExistsAsync(command.Name))
                return Result<VendorDto>.Failure(
                    "A vendor with this name already exists.");

            var entity = new Vendor
            {
                Name = command.Name.Trim(),
                Description = command.Description.Trim(),
                Status = command.Status,
                UserId = command.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(entity);
            return Result<VendorDto>.Success(VendorMapper.MapToDto(created));
        }
    }

    public class UpdateVendorCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; }
    }

    public class UpdateVendorCommandHandler
    {
        private readonly IVendorRepository _repository;

        public UpdateVendorCommandHandler(IVendorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<VendorDto>> Handle(UpdateVendorCommand command)
        {
            var entity = await _repository.GetByIdAsync(command.Id);
            if (entity == null)
                return Result<VendorDto>.Failure("Vendor not found.");

            if (await _repository.NameExistsAsync(command.Name, command.Id))
                return Result<VendorDto>.Failure(
                    "A vendor with this name already exists.");

            entity.Name = command.Name.Trim();
            entity.Description = command.Description.Trim();
            entity.Status = command.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(entity);
            return Result<VendorDto>.Success(VendorMapper.MapToDto(updated));
        }
    }

    internal static class VendorMapper
    {
        internal static VendorDto MapToDto(Vendor e) => new()
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            Status = e.Status,
            UserId = e.UserId,
            CreatedAt = e.CreatedAt
        };
    }
}
