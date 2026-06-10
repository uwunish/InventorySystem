using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Customers.Commands
{
    public class CreateCustomerCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Active;
        public int UserId { get; set; }
    }

    public class CreateCustomerCommandHandler
    {
        private readonly ICustomerRepository _repository;

        public CreateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<CustomerDto>> Handle(
            CreateCustomerCommand command)
        {
            if (await _repository.NameExistsAsync(command.Name))
                return Result<CustomerDto>.Failure(
                    "A customer with this name already exists.");

            var entity = new Customer
            {
                Name = command.Name.Trim(),
                Description = command.Description.Trim(),
                Status = command.Status,
                UserId = command.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(entity);
            return Result<CustomerDto>.Success(CustomerMapper.MapToDto(created));
        }
    }

    public class UpdateCustomerCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; }
    }

    public class UpdateCustomerCommandHandler
    {
        private readonly ICustomerRepository _repository;

        public UpdateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<CustomerDto>> Handle(
            UpdateCustomerCommand command)
        {
            var entity = await _repository.GetByIdAsync(command.Id);
            if (entity == null)
                return Result<CustomerDto>.Failure("Customer not found.");

            if (await _repository.NameExistsAsync(command.Name, command.Id))
                return Result<CustomerDto>.Failure(
                    "A customer with this name already exists.");

            entity.Name = command.Name.Trim();
            entity.Description = command.Description.Trim();
            entity.Status = command.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(entity);
            return Result<CustomerDto>.Success(CustomerMapper.MapToDto(updated));
        }
    }

    internal static class CustomerMapper
    {
        internal static CustomerDto MapToDto(Customer e) => new()
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
