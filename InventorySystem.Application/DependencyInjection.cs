using InventorySystem.Application.Features.Auth.Commands;
using InventorySystem.Application.Features.ProductGroups.Commands;
using InventorySystem.Application.Features.ProductGroups.Queries;
using InventorySystem.Application.Features.Products.Commands;
using InventorySystem.Application.Features.Products.Queries;
using InventorySystem.Application.Features.UnitOfMeasures.Commands;
using InventorySystem.Application.Features.UnitOfMeasures.Queries;
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
            // auth
            services.AddScoped<LoginCommandHandler>();

            // users
            services.AddScoped<CreateUserCommandHandler>();
            services.AddScoped<UpdateUserCommandHandler>();
            services.AddScoped<GetUsersQueryHandler>();
            services.AddScoped<GetUserByIdQueryHandler>();

            // product groups
            services.AddScoped<CreateProductGroupCommandHandler>();
            services.AddScoped<UpdateProductGroupCommandHandler>();
            services.AddScoped<GetProductGroupsQueryHandler>();
            services.AddScoped<GetProductGroupByIdQueryHandler>();

            // unit of measures
            services.AddScoped<CreateUnitOfMeasureCommandHandler>();
            services.AddScoped<UpdateUnitOfMeasureCommandHandler>();
            services.AddScoped<GetUnitOfMeasuresQueryHandler>();
            services.AddScoped<GetUnitOfMeasureByIdQueryHandler>();

            // products
            services.AddScoped<CreateProductCommandHandler>();
            services.AddScoped<UpdateProductCommandHandler>();
            services.AddScoped<GetProductsQueryHandler>();
            services.AddScoped<GetProductByIdQueryHandler>();

            return services;
        }
    }
}
