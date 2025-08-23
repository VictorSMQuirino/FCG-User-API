using FCG_Users.Domain.Entities;

namespace FCG_Users.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
