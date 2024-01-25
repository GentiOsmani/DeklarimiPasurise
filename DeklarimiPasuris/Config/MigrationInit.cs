using DeklarimiPasuris.Data;
using DeklarimiPasuris.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeklarimiPasuris.Config;

public static class MigrationInit
{
    public static async Task ApplyMigrationsAsync(WebApplication app, IConfiguration config)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            await context.Database.MigrateAsync();

            if (!context.AppUser.Any())
            {
                var dataSeeder = new DataSeeder(context);
                await dataSeeder.Seed();

                var adminInit = new AdminInit(context, config);
                await adminInit.Initialize();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error has occurred during migration.");
            throw;
        }
    }
}