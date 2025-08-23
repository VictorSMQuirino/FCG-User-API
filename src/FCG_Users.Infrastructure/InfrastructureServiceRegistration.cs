using FCG_Users.Domain.Interfaces.Repositories;
using FCG_Users.Infrastructure.Data;
using FCG_Users.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCG_Users.Infrastructure;

public static class InfrastructureServiceRegistration
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<FCGUsersDbContext>(options => options.UseNpgsql(
			configuration.GetConnectionString("DefaultConnection")
			));

		services.AddScoped<IUserRepository, UserRepository>();

		return services;
	}
}
