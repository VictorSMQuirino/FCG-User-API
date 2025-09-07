using FCG_Users.Tests.Fixtures;
using FluentAssertions;

namespace FCG_Users.Tests.Services.Bcrypt;

[Collection(nameof(BcryptServiceCollection))]
public class BcryptServiceVerifyHashedPasswordTests : BcryptServiceTests
{
	[Fact]
	public Task ValidProveidedPassword_VerifyHashedPassword_MustReturnTrue()
	{
		//Arrange
		var password = _serviceFixture.GetValidPassword();
		var hashedPassword = _service.HashPassword(password);

		//Act
		var result = _service.VerifyHashedPassword(hashedPassword, password);

		//Assert
		result.Should().BeTrue();

		return Task.CompletedTask;
	}

	[Fact]
	public Task InvalidProvidedPassword_VerifyHashedPassword_MustReturnFalse()
	{
		//Arrange
		var password = _serviceFixture.GetValidPassword();
		const string invalidPassword = "invalidPassword";
		var hashedPassword = _service.HashPassword(password);

		//Act
		var result = _service.VerifyHashedPassword(hashedPassword, invalidPassword);

		//Assert
		result.Should().BeFalse();

		return Task.CompletedTask;
	}
}
