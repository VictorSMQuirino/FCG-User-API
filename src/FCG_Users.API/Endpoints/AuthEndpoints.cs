using FCG_Users.API.Extensions.Converters;
using FCG_Users.API.Requests.Auth;
using FCG_Users.API.Requests.User;
using FCG_Users.API.Responses;
using FCG_Users.API.Responses.Auth;
using FCG_Users.Domain.Interfaces.Services;

namespace FCG_Users.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/auth")
            .WithTags("Auth")
            .WithOpenApi();

        group.MapPost("/login", async (LoginRequest request, IAuthService authService) =>
        {
            var dto = request.ToDto();
            var token = await authService.Login(dto);

            return Results.Ok(new TokenResponse(token));
        })
        .Produces<TokenResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/signup", async (CreateUserRequest request, IAuthService authService, IConfiguration configuration) =>
        {
            var dto = request.ToDto();

            await authService.SignUp(dto, configuration);

            return Results.Created();
        })
        .Produces(StatusCodes.Status201Created)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest);

        group.MapPatch("/change-password", async (ChangePasswordRequest request, IAuthService authService) =>
        {
            var dto = request.ToDto();

            await authService.ChangePassword(dto);

            return Results.NoContent();
        })
        .RequireAuthorization()
        .Produces(StatusCodes.Status204NoContent)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}
