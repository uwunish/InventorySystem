using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace InventorySystem.Application.Features.Users.Queries
{
    public class GetUserByIdQuery
    {
        public int Id { get; set; }
    }

    public class GetUserByIdQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto>> Handle (GetUserByIdQuery query)
        {
            var user = await _userRepository.GetByIdAsync(query.Id);
            if(user == null)
            {
                return Result<UserDto>.Failure("User not found");
            }

            return Result<UserDto>.Success(new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                MobileNumber = user.MobileNumber,
                Status = user.Status,
                CreatedAt = user.CreatedAt
            });
        }
    }
}
