using FCG_Users.Domain.DTO.User;
using FCG_Users.Domain.Entities;

namespace FCG_Users.Application.Converters;

public static class UserConverter
{
	public static User ToUser(this CreateUserDto dto, string hashedPassword)
		=> new()
		{
			UserName = dto.UserName,
			Email = dto.Email,
			Password = hashedPassword,
			Role = Domain.Enums.UserRole.Common
		};

	public static UserDto ToDto(this User user)
		=> new(user.Id, user.UserName, user.Email);

	public static ICollection<UserDto> ToDtoList(this IEnumerable<User> users)
		=> [.. users.Select(u => u.ToDto())];
}
