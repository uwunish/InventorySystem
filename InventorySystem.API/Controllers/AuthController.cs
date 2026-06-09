using InventorySystem.Application.Features.Auth.Commands;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginCommandHandler _loginHandler;

        public AuthController(LoginCommandHandler loginHandler)
        {
            _loginHandler = loginHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _loginHandler.Handle(command);

            if (!result.Succeeded)
                return Unauthorized(new { message = result.Error });

            return Ok(result.Data);
        }
    }
}
