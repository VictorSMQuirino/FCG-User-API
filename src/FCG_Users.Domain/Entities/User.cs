using FCG_Users.Domain.Enums;

namespace FCG_Users.Domain.Entities;

public class User : BaseEntity
{
	public required string UserName { get; set; }
	public required string Email { get; set; }
	public required string Password { get; set; }
	public UserRole Role { get; set; }
}
