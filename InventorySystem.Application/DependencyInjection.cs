using InventorySystem.Application.Features.Auth.Commands;
using InventorySystem.Application.Features.Users.Commands;
using InventorySystem.Application.Features.Users.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateUserCommandHandler>();
            services.AddScoped<UpdateUserCommandHandler>();
            services.AddScoped<LoginCommandHandler>();

            services.AddScoped<GetUsersQueryHandler>();
            services.AddScoped<GetUserByIdQueryHandler>();

            return services;
        }
    }
}
