using InventorySystem.Application.Interfaces;
using InventorySystem.Infrastructure.Persistence;
using InventorySystem.Infrastructure.Repositories;
using InventorySystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastucture(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            // database
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
                )
            );

            // repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // services
            services.AddScoped<IAuthService, AuthService>();

            // jwt
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]!;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

            return services;
        }
    }
}
