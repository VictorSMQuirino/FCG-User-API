using FCG_Users.Domain.DTO.User;

namespace FCG_Users.Domain.Interfaces.Services;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(Guid id);
    Task<ICollection<UserDto>> GetAllAsync();
    Task ChangeRoleAsync(Guid id);
}
