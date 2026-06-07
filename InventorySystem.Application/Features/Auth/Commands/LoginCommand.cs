using BCrypt.Net;
using InventorySystem.Application.Common;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Auth.Commands
{
    public class LoginCommand
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResult
    {
        public string Token { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

    public class LoginCommandHandler
    {
        public readonly IUserRepository _userRepository;
        public readonly IAuthService _authService;
        
        public LoginCommandHandler(IUserRepository userRepository,IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }
        public async Task<Result<LoginResult>> Handle(LoginCommand command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email.ToLower().Trim());

            if (user == null)
            {
                return Result<LoginResult>.Failure("Invalid email or password");
            }

            if (user.Status == Status.Inactive)
            {
                return Result<LoginResult>.Failure("Your account is inactive. Please contact admin");
            }

            if (!BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash))
            {
                return Result<LoginResult>.Failure("Invalid email or password");
            }

            var token = _authService.GenerateToken(user);

            return Result<LoginResult>.Success(new LoginResult
            {
                Token = token,
                Name = user.Name,
                Email = user.Email,
                UserId = user.Id
            });

        }
    }
}
