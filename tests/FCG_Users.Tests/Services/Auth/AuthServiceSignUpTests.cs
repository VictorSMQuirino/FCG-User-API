using FCG_Common.Domain.Exceptions;
using FCG_Users.Domain.Entities;
using FCG_Users.Tests.Fixtures;
using Moq;
using System.Linq.Expressions;

namespace FCG_Users.Tests.Services.Auth;

[Collection(nameof(AuthServiceCollection))]
public class AuthServiceSignUpTests : AuthServiceTests
{
	private void OnValidationAssert(
		Func<Times> timesVerifyIfUserExists,
		Func<Times> timesHashPassword,
		Func<Times> timesCreated
		)
	{
		_userRepositoryMock
			.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<User, bool>>>()), timesVerifyIfUserExists);
		_passwordServiceMock
			.Verify(s => s.HashPassword(It.IsAny<string>()), timesHashPassword);
		_userRepositoryMock
			.Verify(r => r.CreateAsync(It.IsAny<User>()), timesCreated);
	}

	[Fact]
	public async Task ValidDto_SignUp_MustCreateANewUser()
	{
		//Arrange
		var dto = _serviceFixture.GetValidCreateUserDto();

		_userRepositoryMock
			.Setup(r => r.ExistsBy(It.IsAny<Expression<Func<User, bool>>>()))
			.ReturnsAsync(false);

		//Act
		await _service.SignUp(dto, _configuration);

		//Assert
		OnValidationAssert(Times.Once, Times.Once, Times.Once);
	}

	[Theory]
	[InlineData("", "teste@email.com", "Teste@321/")]
	[InlineData("A", "teste@email.com", "Teste@321/")]
	[InlineData("abcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabc", "teste@emaikl.com", "Teste@321/")]
	[InlineData("Fulano", "", "Teste@321/")]
	[InlineData("Fulano", "invalidEmail", "Teste@321/")]
	[InlineData("Fulano", "teste@email.com", "")]
	[InlineData("Fulano", "teste@email.com", "invalidPassword")]
	public async Task InvalidDto_SignUp_MustFailBecauseValidationFailed(string username, string email, string password)
	{
		//Arrange
		var dto = _serviceFixture.GetValidCreateUserDto(username, email, password);

		//Act
		var act = async () => await _service.SignUp(dto, _configuration);

		//Assert
		await Assert.ThrowsAsync<ValidationErrorsException>(act);
		OnValidationAssert(Times.Never, Times.Never, Times.Never);
	}

	[Fact]
	public async Task ValidDto_SignUp_MustFailBecauseAlreadyExistsAnUserWithSameEmail()
	{
		//Arrange
		var dto = _serviceFixture.GetValidCreateUserDto();

		_userRepositoryMock
			.Setup(r => r.ExistsBy(It.IsAny<Expression<Func<User, bool>>>()))
			.ReturnsAsync(true);

		//Act
		var act = async () => await _service.SignUp(dto, _configuration);

		//Assert
		await Assert.ThrowsAsync<DuplicatedEntityException>(act);
		OnValidationAssert(Times.Once, Times.Never, Times.Never);
	}
}
