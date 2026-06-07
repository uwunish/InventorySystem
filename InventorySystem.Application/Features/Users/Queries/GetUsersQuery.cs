using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application.Features.Users.Queries
{
    public class GetUsersQuery
    {
    }

    public class GetUsersQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<IEnumerable<UserDto>>> Handle(GetUsersQuery query)
        {
            var users = await _userRepository.GetAllUsersAsync();

            var userDtos = users.Select(x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                MobileNumber = x.MobileNumber,
                Status = x.Status,
                CreatedAt = x.CreatedAt
            });

            return Result<IEnumerable<UserDto>>.Success(userDtos);
        }
    }
}
