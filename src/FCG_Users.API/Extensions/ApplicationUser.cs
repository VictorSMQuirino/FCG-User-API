using System.Security.Claims;
using FCG_Users.Domain.Interfaces.Services;

namespace FCG_Users.API.Extensions;

public class ApplicationUser : IApplicationUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(id, out var userId) ? userId : Guid.Empty;
    }
}
