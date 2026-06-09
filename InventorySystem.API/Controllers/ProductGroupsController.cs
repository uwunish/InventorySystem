using InventorySystem.Application.Features.ProductGroups.Commands;
using InventorySystem.Application.Features.ProductGroups.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductGroupsController : ControllerBase
    {
        private readonly CreateProductGroupCommandHandler _createHandler;
        private readonly UpdateProductGroupCommandHandler _updateHandler;
        private readonly GetProductGroupsQueryHandler _getHandler;
        private readonly GetProductGroupByIdQueryHandler _getByIdHandler;

        public ProductGroupsController(
            CreateProductGroupCommandHandler createHandler,
            UpdateProductGroupCommandHandler updateHandler,
            GetProductGroupsQueryHandler getHandler,
            GetProductGroupByIdQueryHandler getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getHandler = getHandler;
            _getByIdHandler = getByIdHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getHandler.Handle(new GetProductGroupsQuery());
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getByIdHandler
                .Handle(new GetProductGroupByIdQuery { Id = id });
            if (!result.Succeeded)
                return NotFound(new { message = result.Error });
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProductGroupCommand command)
        {
            // Get userId from JWT token claims
            command.UserId = GetCurrentUserId();
            var result = await _createHandler.Handle(command);
            if (!result.Succeeded)
                return BadRequest(new { message = result.Error });
            return CreatedAtAction(nameof(GetById),
                new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,
            [FromBody] UpdateProductGroupCommand command)
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
