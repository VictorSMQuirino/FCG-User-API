using FCG_Users.Domain.DTO.Auth;
using FCG_Users.Domain.DTO.User;
using Microsoft.Extensions.Configuration;

namespace FCG_Users.Domain.Interfaces.Services;

public interface IAuthService
{
    Task SignUp(CreateUserDto dto, IConfiguration configuration);
    Task<string> Login(LoginDto dto);
}
