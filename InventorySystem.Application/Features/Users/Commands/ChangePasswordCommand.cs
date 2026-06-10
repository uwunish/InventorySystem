using InventorySystem.Application.Common;
using InventorySystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Users.Commands
{
    public class ChangePasswordCommand
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; } = string.Empty;
    }

    public class ChangePasswordCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> Handle(ChangePasswordCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user == null)
                return Result<bool>.Failure("User not found.");

            var newHash = BCrypt.Net.BCrypt.HashPassword(command.NewPassword);
            await _userRepository.UpdatePasswordAsync(command.UserId, newHash);

            return Result<bool>.Success(true);
        }
    }
}
