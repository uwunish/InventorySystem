using BCrypt.Net;
using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Users.Commands
{
    public class CreateUserCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Active;
    }

    public class CreateUserCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand command)
        {
            if(await _userRepository.EmailExistsAsync(command.Email))
            {
                return Result<UserDto>.Failure("A user with this email already exists.");
            }

            var user = new User
            {
                Name = command.Name,
                Email = command.Email.ToLower().Trim(),
                MobileNumber = command.MobileNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password),
                Status = command.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _userRepository.CreateAsync(user);
            return Result<UserDto>.Success(new UserDto
            {
                Id = created.Id,
                Name = created.Name,
                Email = created.Email,
                MobileNumber = created.MobileNumber,
                Status = created.Status,
                CreatedAt = created.CreatedAt
            });
        }
    }
}
