using FCG_Users.API.Extensions;
using FCG_Users.Domain.Interfaces.Services;

namespace FCG_Users.API;

public static class ApiServicesRegistration
{
	public static IServiceCollection AddApiServices(this IServiceCollection services)
	{
		services.AddScoped<IApplicationUserService, ApplicationUser>();

		return services;
	}
}
