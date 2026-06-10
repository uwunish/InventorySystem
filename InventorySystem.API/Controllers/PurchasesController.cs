using InventorySystem.Application.DTOs;
using InventorySystem.Application.Features.Purchases.Commands;
using InventorySystem.Application.Features.Purchases.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PurchasesController : ControllerBase
    {
        private readonly CreatePurchaseCommandHandler _createHandler;
        private readonly GetPurchasesQueryHandler _getHandler;
        private readonly GetPurchaseByIdQueryHandler _getByIdHandler;

        public PurchasesController(
            CreatePurchaseCommandHandler createHandler,
            GetPurchasesQueryHandler getHandler,
            GetPurchaseByIdQueryHandler getByIdHandler)
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
            _getByIdHandler = getByIdHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getHandler.Handle(new GetPurchasesQuery());
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getByIdHandler
                .Handle(new GetPurchaseByIdQuery { Id = id });
            if (!result.Succeeded)
                return NotFound(new { message = result.Error });
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreatePurchaseRequest request)
        {
            var command = new CreatePurchaseCommand
            {
                VendorId = request.VendorId,
                UserId = GetCurrentUserId(),
                Items = request.Items
            };

            var result = await _createHandler.Handle(command);
            if (!result.Succeeded)
                return BadRequest(new { message = result.Error });

            return CreatedAtAction(nameof(GetById),
                new { id = result.Data!.Id }, result.Data);
        }

        // No PUT endpoint — purchases are immutable by design

        private int GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)
                ?? User.FindFirst("sub");
            return int.Parse(claim!.Value);
        }
    }
}
