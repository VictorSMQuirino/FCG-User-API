using FCG_Users.Application.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;

namespace FCG_Users.API.Config;

public static class ApiConfiguration
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        services.AddHttpContextAccessor();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings?.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings?.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings!.Key)),
                ValidateLifetime = true
            };
        });

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection ConfigureHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
		services.AddHealthChecks()
	    .AddNpgSql(
		    configuration.GetConnectionString("DefaultConnection")!,
		    name: "PostgreSQL",
		    failureStatus: HealthStatus.Unhealthy,
		    tags: ["database", "postgres"]
	    );

        return services;
	}
}
