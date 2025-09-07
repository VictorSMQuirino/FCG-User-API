using FCG_Users.Application.Auth;
using FCG_Users.Domain.Interfaces.Services;
using FCG_Users.Tests.Fixtures;

namespace FCG_Users.Tests.Services.Token;

public class TokenServiceTests
{
	protected readonly TokenServiceFixture _serviceFixture;
	protected readonly JwtSettings _jwtSettings;
	protected readonly ITokenService _service;

	public TokenServiceTests()
	{
		_serviceFixture = new();
		_jwtSettings = TokenServiceFixture.GetJwtSettings();
		_service = _serviceFixture.GetService(_jwtSettings);
	}
}
