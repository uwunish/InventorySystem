using InventorySystem.Application.Features.UnitOfMeasures.Commands;
using InventorySystem.Application.Features.UnitOfMeasures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UnitOfMeasuresController : ControllerBase
    {
        private readonly CreateUnitOfMeasureCommandHandler _createHandler;
        private readonly UpdateUnitOfMeasureCommandHandler _updateHandler;
        private readonly GetUnitOfMeasuresQueryHandler _getHandler;
        private readonly GetUnitOfMeasureByIdQueryHandler _getByIdHandler;

        public UnitOfMeasuresController(
            CreateUnitOfMeasureCommandHandler createHandler,
            UpdateUnitOfMeasureCommandHandler updateHandler,
            GetUnitOfMeasuresQueryHandler getHandler,
            GetUnitOfMeasureByIdQueryHandler getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getHandler = getHandler;
            _getByIdHandler = getByIdHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getHandler.Handle(new GetUnitOfMeasuresQuery());
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getByIdHandler
                .Handle(new GetUnitOfMeasureByIdQuery { Id = id });
            if (!result.Succeeded)
                return NotFound(new { message = result.Error });
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateUnitOfMeasureCommand command)
        {
            command.UserId = GetCurrentUserId();
            var result = await _createHandler.Handle(command);
            if (!result.Succeeded)
                return BadRequest(new { message = result.Error });
            return CreatedAtAction(nameof(GetById),
                new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,
            [FromBody] UpdateUnitOfMeasureCommand command)
        {
            command.Id = id;
            var result = await _updateHandler.Handle(command);
            if (!result.Succeeded)
                return NotFound(new { message = result.Error });
            return Ok(result.Data);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                ?? User.FindFirst("sub");
            return int.Parse(userIdClaim!.Value);
        }
    }
}
