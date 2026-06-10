using InventorySystem.Application.Features.Users.Commands;
using InventorySystem.Application.Features.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly CreateUserCommandHandler _createHandler;
        private readonly UpdateUserCommandHandler _updateHandler;
        private readonly GetUsersQueryHandler _getUsersHandler;
        private readonly GetUserByIdQueryHandler _getUserByIdHandler;
        private readonly ChangePasswordCommandHandler _changePasswordHandler;

        public UsersController(
            CreateUserCommandHandler createHandler,
            UpdateUserCommandHandler updateHandler,
            GetUsersQueryHandler getUsersHandler,
            GetUserByIdQueryHandler getUserByIdHandler,
            ChangePasswordCommandHandler changePasswordHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getUsersHandler = getUsersHandler;
            _getUserByIdHandler = getUserByIdHandler;
            _changePasswordHandler = changePasswordHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getUsersHandler.Handle(new GetUsersQuery());
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getUserByIdHandler
                .Handle(new GetUserByIdQuery { Id = id });

            if (!result.Succeeded)
                return NotFound(new { message = result.Error });

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await _createHandler.Handle(command);

            if (!result.Succeeded)
                return BadRequest(new { message = result.Error });

            return CreatedAtAction(nameof(GetById),
                new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,
            [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            var result = await _updateHandler.Handle(command);

            if (!result.Succeeded)
                return NotFound(new { message = result.Error });

            return Ok(result.Data);
        }

        [HttpPatch("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(int id,
            [FromBody] ChangePasswordCommand command)
        {
            command.UserId = id;
            var result = await _changePasswordHandler.Handle(command);
            if (!result.Succeeded)
                return NotFound(new { message = result.Error });
            return Ok(new { message = "Password changed successfully." });
        }
    }
}
