using FCG_Users.API.Requests.Auth;
using FCG_Users.Domain.DTO.Auth;

namespace FCG_Users.API.Extensions.Converters;

public static class AuthConverter
{
    public static LoginDto ToDto(this LoginRequest request)
        => new(request.Email, request.Password);
}
