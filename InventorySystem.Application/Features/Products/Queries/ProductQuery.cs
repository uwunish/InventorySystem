using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Features.Products.Commands;
using InventorySystem.Application.Interfaces;

namespace InventorySystem.Application.Features.Products.Queries
{
    public class GetProductsQuery { }

    public class GetProductsQueryHandler
    {
        private readonly IProductRepository _repository;

        public GetProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<ProductDto>>> Handle(
            GetProductsQuery query)
        {
            var entities = await _repository.GetAllAsync();
            return Result<IEnumerable<ProductDto>>.Success(
                entities.Select(ProductMapper.MapToDto));
        }
    }

    public class GetProductByIdQuery { public int Id { get; set; } }

    public class GetProductByIdQueryHandler
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery query)
        {
            var entity = await _repository.GetByIdAsync(query.Id);
            if (entity == null)
                return Result<ProductDto>.Failure("Product not found.");

            return Result<ProductDto>.Success(ProductMapper.MapToDto(entity));
        }
    }
}
