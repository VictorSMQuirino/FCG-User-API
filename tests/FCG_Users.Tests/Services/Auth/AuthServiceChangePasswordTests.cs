using FCG_Users.Domain.DTO.Auth;
using FCG_Users.Domain.Entities;
using FCG_Users.Domain.Exceptions;
using FCG_Users.Tests.Fixtures;
using Moq;

namespace FCG_Users.Tests.Services.Auth;

[Collection(nameof(AuthServiceCollection))]
public class AuthServiceChangePasswordTests : AuthServiceTests
{
	private (User user, ChangePasswordDto dto) ArrangeSetup(string? password = null)
	{
		var user = _serviceFixture.GetValidUser();
		var dto = _serviceFixture.GetValidChangePasswordDto(password);

		return (user, dto);
	}

	private void OnValidationAssert(
		Func<Times> timesVerifyPassword,
		Func<Times> timesHashPassword,
		Func<Times> timesUpdate
		)
	{
		_userRepositoryMock
			.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);

		_passwordServiceMock
			.Verify(s => s.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()), timesVerifyPassword);

		_passwordServiceMock
			.Verify(s => s.HashPassword(It.IsAny<string>()), timesHashPassword);

		_userRepositoryMock
			.Verify(r => r.UpdateAsync(It.IsAny<User>()), timesUpdate);
	}

	[Fact]
	public async Task ValidDto_ChangePassword_MustChangeUserPassword()
	{
		//Arrange
		var (user, dto) = ArrangeSetup();

		_userRepositoryMock
			.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(user);

		_passwordServiceMock
			.Setup(s => s.VerifyHashedPassword(user.Password, dto.CurrentPassword))
			.Returns(true);

		//Act
		await _service.ChangePassword(dto);

		//Assert
		OnValidationAssert(Times.Once, Times.Once, Times.Once);
	}

	[Fact]
	public async Task ValidDto_ChangePassword_MustFailBecausePasswordVerificationFailed()
	{
		//Arrange
		var (user, dto) = ArrangeSetup();

		_userRepositoryMock
			.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(user);

		_passwordServiceMock
			.Setup(s => s.VerifyHashedPassword(user.Password, dto.CurrentPassword))
			.Returns(false);

		//Act
		var act = async () => await _service.ChangePassword(dto);

		//Assert
		await Assert.ThrowsAsync<DomainException>(act);
		OnValidationAssert(Times.Once, Times.Never, Times.Never);
	}

	[Fact]
	public async Task InvalidDto_ChangePassword_MustFailBecausePasswordValidationFailed()
	{
		//Arrange
		var (user, dto) = ArrangeSetup("invalid");

		_userRepositoryMock
			.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(user);

		_passwordServiceMock
			.Setup(s => s.VerifyHashedPassword(user.Password, dto.CurrentPassword))
			.Returns(true);

		//Act
		var act = async () => await _service.ChangePassword(dto);

		//Assert
		await Assert.ThrowsAsync<DomainException>(act);
		OnValidationAssert(Times.Once, Times.Never, Times.Never);
	}
}
