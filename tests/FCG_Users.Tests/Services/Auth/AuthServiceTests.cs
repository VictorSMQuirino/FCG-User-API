using FCG_Users.Domain.Interfaces.Repositories;
using FCG_Users.Domain.Interfaces.Services;
using FCG_Users.Tests.Fixtures;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FCG_Users.Tests.Services.Auth;

public class AuthServiceTests
{
	protected readonly AuthServiceFixture _serviceFixture;
	protected readonly Mock<IUserRepository> _userRepositoryMock;
	protected readonly Mock<ITokenService> _tokenServiceMock;
	protected readonly Mock<IPasswordService> _passwordServiceMock;
	protected readonly Mock<IApplicationUserService> _applicationUserServiceMock;
	protected readonly IAuthService _service;
	protected readonly IConfiguration _configuration;

	public AuthServiceTests()
	{
		_serviceFixture = new();
		_userRepositoryMock = AuthServiceFixture.GetUserRepositoryMock();
		_tokenServiceMock = AuthServiceFixture.GetTokenServiceMock();
		_passwordServiceMock = AuthServiceFixture.GetPasswordServiceMock();
		_applicationUserServiceMock = AuthServiceFixture.GetApplicationUserServiceMock();
		_configuration = AuthServiceFixture.GetConfiguration();
		_service = _serviceFixture.GetService(
			_userRepositoryMock,
			_tokenServiceMock,
			_passwordServiceMock,
			_applicationUserServiceMock
			);
	}
}
