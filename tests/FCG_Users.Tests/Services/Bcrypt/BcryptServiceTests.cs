using FCG_Users.Domain.Interfaces.Services;
using FCG_Users.Tests.Fixtures;

namespace FCG_Users.Tests.Services.Bcrypt;

public class BcryptServiceTests
{
	protected readonly BcryptServiceFixture _serviceFixture;
	protected readonly IPasswordService _service;

	public BcryptServiceTests()
	{
		_serviceFixture = new();
		_service = _serviceFixture.GetService();
	}
}
