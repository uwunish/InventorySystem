using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Features.ProductGroups.Commands;
using InventorySystem.Application.Interfaces;

namespace InventorySystem.Application.Features.ProductGroups.Queries
{
    public class GetProductGroupsQuery { }

    public class GetProductGroupsQueryHandler
    {
        private readonly IProductGroupRepository _repository;

        public GetProductGroupsQueryHandler(IProductGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<ProductGroupDto>>> Handle(
            GetProductGroupsQuery query)
        {
            var entities = await _repository.GetAllAsync();
            var dtos = entities.Select(ProductGroupMapper.MapToDto);
            return Result<IEnumerable<ProductGroupDto>>.Success(dtos);
        }
    }

    public class GetProductGroupByIdQuery
    {
        public int Id { get; set; }
    }

    public class GetProductGroupByIdQueryHandler
    {
        private readonly IProductGroupRepository _repository;

        public GetProductGroupByIdQueryHandler(IProductGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductGroupDto>> Handle(
            GetProductGroupByIdQuery query)
        {
            var entity = await _repository.GetByIdAsync(query.Id);
            if (entity == null)
                return Result<ProductGroupDto>.Failure("Product group not found.");

            return Result<ProductGroupDto>.Success(
                ProductGroupMapper.MapToDto(entity));
        }
    }
}
