using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Features.UnitOfMeasures.Commands;
using InventorySystem.Application.Interfaces;

namespace InventorySystem.Application.Features.UnitOfMeasures.Queries
{
    public class GetUnitOfMeasuresQuery { }

    public class GetUnitOfMeasuresQueryHandler
    {
        private readonly IUnitOfMeasureRepository _repository;

        public GetUnitOfMeasuresQueryHandler(IUnitOfMeasureRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<UnitOfMeasureDto>>> Handle(
            GetUnitOfMeasuresQuery query)
        {
            var entities = await _repository.GetAllAsync();
            return Result<IEnumerable<UnitOfMeasureDto>>.Success(
                entities.Select(UnitOfMeasureMapper.MapToDto));
        }
    }

    public class GetUnitOfMeasureByIdQuery { public int Id { get; set; } }

    public class GetUnitOfMeasureByIdQueryHandler
    {
        private readonly IUnitOfMeasureRepository _repository;

        public GetUnitOfMeasureByIdQueryHandler(
            IUnitOfMeasureRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<UnitOfMeasureDto>> Handle(
            GetUnitOfMeasureByIdQuery query)
        {
            var entity = await _repository.GetByIdAsync(query.Id);
            if (entity == null)
                return Result<UnitOfMeasureDto>.Failure(
                    "Unit of measure not found.");

            return Result<UnitOfMeasureDto>.Success(
                UnitOfMeasureMapper.MapToDto(entity));
        }
    }
}
