using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Infrastructure.Persistence
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider
                .GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider
                .GetRequiredService<ILogger<AppDbContext>>();

            try
            {
                await context.Database.MigrateAsync();

                if(!await context.Users.AnyAsync())
                {
                    var adminUser = new User
                    {
                        Name = "Admin",
                        Email = "admin@inventory.com",
                        MobileNumber = "9812345678",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        Status = Status.Active,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    context.Users.Add(adminUser);
                    await context.SaveChangesAsync();

                    logger.LogInformation("Database seeded with admin user");
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while seeding the database");
                throw;
            }
        }
    }
}
