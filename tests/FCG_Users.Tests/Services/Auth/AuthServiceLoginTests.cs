using FCG_Users.Domain.Entities;
using FCG_Users.Domain.Exceptions;
using FCG_Users.Tests.Fixtures;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace FCG_Users.Tests.Services.Auth;

[Collection(nameof(AuthServiceCollection))]
public class AuthServiceLoginTests : AuthServiceTests
{
	private void OnValidationAssert(
		Func<Times> timesVerifyPassword, 
		Func<Times> timesGenerateToken
		)
	{
		_userRepositoryMock
			.Verify(r => r.GetBy(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);

		_passwordServiceMock
			.Verify(r => r.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()), timesVerifyPassword);

		_tokenServiceMock
			.Verify(r => r.GenerateToken(It.IsAny<User>()), timesGenerateToken);
	}

	[Fact]
	public async Task ValidDto_Login_MustReturnToken()
	{
		//Arrange
		var dto = _serviceFixture.GetValidLoginDto();
		var user = _serviceFixture.GetValidUser();
		var token = _serviceFixture.GetValidToken();

		_userRepositoryMock
			.Setup(r => r.GetBy(It.IsAny<Expression<Func<User, bool>>>()))
			.ReturnsAsync(user);

		_passwordServiceMock
			.Setup(s => s.VerifyHashedPassword(user.Password, dto.Password))
			.Returns(true);

		_tokenServiceMock
			.Setup(s => s.GenerateToken(user))
			.Returns(token);

		//Act
		var result = await _service.Login(dto);

		//Assert
		result.Should().Be(token);
		OnValidationAssert(Times.Once, Times.Once);
	}

	[Fact]
	public async Task ValidDto_Login_MustFailBecauseUserNotFound()
	{
		//Arrange
		var dto = _serviceFixture.GetValidLoginDto();

		_userRepositoryMock
			.Setup(r => r.GetBy(It.IsAny<Expression<Func<User, bool>>>()))
			.ReturnsAsync(null as User);

		//Act
		var act = async () => await _service.Login(dto);

		//Assert
		await Assert.ThrowsAsync<InvalidCredentialsException>(act);
		OnValidationAssert(Times.Never, Times.Never);
	}

	[Fact]
	public async Task ValidDto_Login_MustFailBecausePasswordVerificationFailed()
	{
		//Arrange
		var dto = _serviceFixture.GetValidLoginDto();
		var user = _serviceFixture.GetValidUser();

		_userRepositoryMock
			.Setup(r => r.GetBy(It.IsAny<Expression<Func<User, bool>>>()))
			.ReturnsAsync(user);

		_passwordServiceMock
			.Setup(s => s.VerifyHashedPassword(user.Password, dto.Password))
			.Returns(false);

		//Act
		var act = async () => await _service.Login(dto);

		//Assert
		await Assert.ThrowsAsync<InvalidCredentialsException>(act);
		OnValidationAssert(Times.Once, Times.Never);
	}
}
