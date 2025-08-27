using FCG_Users.API.Extensions.Converters;
using FCG_Users.API.Responses.User;
using FCG_Users.Domain.Enums;
using FCG_Users.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace FCG_Users.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/users")
            .RequireAuthorization(new AuthorizeAttribute { Roles = nameof(UserRole.Admin) })
            .WithTags("Users")
            .WithOpenApi();

        group.MapGet("{id:guid}", async (Guid id, IUserService userService) =>
        {
            var dto = await userService.GetByIdAsync(id);

            return Results.Ok(dto?.ToResponse());
        })
        .Produces<GetUserByIdResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status403Forbidden);

        group.MapGet("", async (IUserService userService) =>
        {
            var dtoList = await userService.GetAllAsync();

            return Results.Ok(dtoList.ToResponse());
        })
        .Produces<ICollection<GetUserByIdResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status403Forbidden);

        group.MapPatch("{id:guid}/change-role", async (Guid id, IUserService userService) =>
        {
            await userService.ChangeRoleAsync(id);

            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status403Forbidden)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
