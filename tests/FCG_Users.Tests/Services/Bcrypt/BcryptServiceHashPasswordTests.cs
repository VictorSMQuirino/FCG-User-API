using FCG_Users.Tests.Fixtures;
using FluentAssertions;

namespace FCG_Users.Tests.Services.Bcrypt;

[Collection(nameof(BcryptServiceCollection))]
public class BcryptServiceHashPasswordTests : BcryptServiceTests
{
	[Fact]
	public Task ValidPassword_HashPassword_MustHashPassoword()
	{
		//Arrange
		var password = _serviceFixture.GetValidPassword();

		//Act
		var result = _service.HashPassword(password);

		//Assert
		var assert = _service.VerifyHashedPassword(result, password);
		assert.Should().BeTrue();

		return Task.CompletedTask;
	}
}
