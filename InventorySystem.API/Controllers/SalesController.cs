using InventorySystem.Application.DTOs;
using InventorySystem.Application.Features.Sales.Commands;
using InventorySystem.Application.Features.Sales.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly CreateSaleCommandHandler _createHandler;
        private readonly GetSalesQueryHandler _getHandler;
        private readonly GetSaleByIdQueryHandler _getByIdHandler;

        public SalesController(
            CreateSaleCommandHandler createHandler,
            GetSalesQueryHandler getHandler,
            GetSaleByIdQueryHandler getByIdHandler)
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
            _getByIdHandler = getByIdHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getHandler.Handle(new GetSalesQuery());
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getByIdHandler
                .Handle(new GetSaleByIdQuery { Id = id });
            if (!result.Succeeded)
                return NotFound(new { message = result.Error });
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateSaleRequest request)
        {
            var command = new CreateSaleCommand
            {
                CustomerId = request.CustomerId,
                UserId = GetCurrentUserId(),
                Items = request.Items
            };

            var result = await _createHandler.Handle(command);
            if (!result.Succeeded)
                return BadRequest(new { message = result.Error });

            return CreatedAtAction(nameof(GetById),
                new { id = result.Data!.Id }, result.Data);
        }

        // No PUT — sales are immutable by design

        private int GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)
                ?? User.FindFirst("sub");
            return int.Parse(claim!.Value);
        }
    }
}
