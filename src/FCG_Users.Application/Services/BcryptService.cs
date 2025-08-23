using FCG_Users.Domain.Interfaces.Services;

namespace FCG_Users.Application.Services;

public class BcryptService : IPasswordService
{
	public string HashPassword(string password)
		=> BCrypt.Net.BCrypt.HashPassword(password);

	public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
		=> BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
}
