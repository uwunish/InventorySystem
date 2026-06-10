using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Features.Vendors.Commands;
using InventorySystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Vendors.Queries
{
    public class GetVendorsQuery { }

    public class GetVendorsQueryHandler
    {
        private readonly IVendorRepository _repository;

        public GetVendorsQueryHandler(IVendorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<VendorDto>>> Handle(
            GetVendorsQuery query)
        {
            var entities = await _repository.GetAllAsync();
            return Result<IEnumerable<VendorDto>>.Success(
                entities.Select(VendorMapper.MapToDto));
        }
    }

    public class GetVendorByIdQuery { public int Id { get; set; } }

    public class GetVendorByIdQueryHandler
    {
        private readonly IVendorRepository _repository;

        public GetVendorByIdQueryHandler(IVendorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<VendorDto>> Handle(GetVendorByIdQuery query)
        {
            var entity = await _repository.GetByIdAsync(query.Id);
            if (entity == null)
                return Result<VendorDto>.Failure("Vendor not found.");

            return Result<VendorDto>.Success(VendorMapper.MapToDto(entity));
        }
    }
}
