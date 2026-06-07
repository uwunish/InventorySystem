using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Users.Commands
{
    public class UpdateUserCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public Status Status { get; set; }
    }

    public class UpdateUserCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> Handle(UpdateUserCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.Id);
            if (user == null)
            {
                return Result<UserDto>.Failure("User not found");
            }

            user.Name = command.Name;
            user.MobileNumber = command.MobileNumber;
            user.Status = command.Status;
            user.UpdatedAt = DateTime.UtcNow;

            var updated = await _userRepository.UpdateAsync(user);

            return Result<UserDto>.Success(new UserDto
            {
                Id = updated.Id,
                Name = updated.Name,
                Email = updated.Email,
                MobileNumber = updated.MobileNumber,
                Status = updated.Status,
                CreatedAt = updated.CreatedAt
            });
        }
    }
}
