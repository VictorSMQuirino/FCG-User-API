using FCG_Users.Application.Services;
using FCG_Users.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FCG_Users.Application;

public static class ApplicationServiceRegistration
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IPasswordService, BcryptService>();
		services.AddScoped<ITokenService, TokenService>();
		services.AddScoped<IUserService, UserService>();

		return services;
	}
}
