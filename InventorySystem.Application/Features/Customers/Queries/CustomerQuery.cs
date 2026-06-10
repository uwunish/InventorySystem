using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Features.Customers.Commands;
using InventorySystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Customers.Queries
{
    public class GetCustomersQuery { }

    public class GetCustomersQueryHandler
    {
        private readonly ICustomerRepository _repository;

        public GetCustomersQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<CustomerDto>>> Handle(
            GetCustomersQuery query)
        {
            var entities = await _repository.GetAllAsync();
            return Result<IEnumerable<CustomerDto>>.Success(
                entities.Select(CustomerMapper.MapToDto));
        }
    }

    public class GetCustomerByIdQuery { public int Id { get; set; } }

    public class GetCustomerByIdQueryHandler
    {
        private readonly ICustomerRepository _repository;

        public GetCustomerByIdQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<CustomerDto>> Handle(
            GetCustomerByIdQuery query)
        {
            var entity = await _repository.GetByIdAsync(query.Id);
            if (entity == null)
                return Result<CustomerDto>.Failure("Customer not found.");

            return Result<CustomerDto>.Success(
                CustomerMapper.MapToDto(entity));
        }
    }
}
