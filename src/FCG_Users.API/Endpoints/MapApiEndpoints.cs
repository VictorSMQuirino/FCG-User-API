namespace FCG_Users.API.Endpoints;

public static class MapApiEndpoints
{
    public static void MapAllApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAuthEndpoints();
        app.MapUserEndpoints();
    }
}
