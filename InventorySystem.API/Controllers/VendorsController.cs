using InventorySystem.Application.Features.Vendors.Commands;
using InventorySystem.Application.Features.Vendors.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VendorsController : ControllerBase
    {
        private readonly CreateVendorCommandHandler _createHandler;
        private readonly UpdateVendorCommandHandler _updateHandler;
        private readonly GetVendorsQueryHandler _getHandler;
        private readonly GetVendorByIdQueryHandler _getByIdHandler;

        public VendorsController(
            CreateVendorCommandHandler createHandler,
            UpdateVendorCommandHandler updateHandler,
            GetVendorsQueryHandler getHandler,
            GetVendorByIdQueryHandler getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getHandler = getHandler;
            _getByIdHandler = getByIdHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getHandler.Handle(new GetVendorsQuery());
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getByIdHandler
                .Handle(new GetVendorByIdQuery { Id = id });
            if (!result.Succeeded)
                return NotFound(new { message = result.Error });
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateVendorCommand command)
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
            [FromBody] UpdateVendorCommand command)
        {
            command.Id = id;
            var result = await _updateHandler.Handle(command);
            if (!result.Succeeded)
                return NotFound(new { message = result.Error });
            return Ok(result.Data);
        }

        private int GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)
                ?? User.FindFirst("sub");
            return int.Parse(claim!.Value);
        }
    }
}
