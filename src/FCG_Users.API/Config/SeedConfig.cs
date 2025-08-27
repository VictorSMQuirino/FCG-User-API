using FCG_Users.Domain.Interfaces.Services;
using FCG_Users.Infrastructure.Data;
using FCG_Users.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace FCG_Users.API.Config;

public static class SeedConfig
{
	public static async Task SeedDatabase(IServiceProvider serviceProvider, IConfiguration configuration)
	{
		using var scope = serviceProvider.CreateScope();
		var services = scope.ServiceProvider;

		var dbContext = services.GetRequiredService<FCGUsersDbContext>();
		var passwordService = services.GetRequiredService<IPasswordService>();
		var seeder = new DatabaseSeeder(dbContext, passwordService);

		await EnsureMigrationsAplied(dbContext, services);

		await seeder.SeedAsync(configuration);
	}

	private static async Task EnsureMigrationsAplied(FCGUsersDbContext dbContext, IServiceProvider services)
	{
		var enviroment = services.GetRequiredService<IWebHostEnvironment>();
		var logger = services.GetRequiredService<ILogger<FCGUsersDbContext>>();

		if (!enviroment.IsDevelopment())
		{
			try
			{
				logger.LogInformation("Verifying and applying database migrations...");
				await dbContext.Database.MigrateAsync();
				logger.LogInformation("Database migrations successfully applied.");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while applying database migrations.");
			}

		}
		else
		{
			logger.LogInformation("Development environment detected. Skipping automatic database migration.");
		}
	}
}
