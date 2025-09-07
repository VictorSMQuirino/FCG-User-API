using FCG_Common.Domain.Exceptions;
using FCG_Users.Domain.Entities;
using FCG_Users.Tests.Fixtures;
using Moq;

namespace FCG_Users.Tests.Services;

[Collection(nameof(UserServiceCollection))]
public class UserServiceChangeRoleTests : UserServiceTests
{
	private void OnValidationAssert(
		Func<Times> timesGetById,
		Func<Times> timesUpdated
		)
	{
		_applicationUserServiceMock
			.Verify(s => s.GetUserId(), Times.Once);
		_repositoryMock
			.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), timesGetById);
		_repositoryMock
			.Verify(r => r.UpdateAsync(It.IsAny<User>()), timesUpdated);
	}

	[Fact]
	public async Task ValidId_ChangeRoleAsync_MustUpdateUser()
	{
		//Arrange
		var user = _serviceFixture.GetValidUser();

		_applicationUserServiceMock
			.Setup(s => s.GetUserId())
			.Returns(Guid.NewGuid());

		_repositoryMock
			.Setup(r => r.GetByIdAsync(user.Id))
			.ReturnsAsync(user);

		//Act
		await _service.ChangeRoleAsync(user.Id);

		//Assert
		OnValidationAssert(Times.Once, Times.Once);
	}

	[Fact]
	public async Task ValidId_ChangeRoleAsync_MustFailBecauseLogggedUserIsTryingToChangeOwnRole()
	{
		//Arrange
		var id = Guid.NewGuid();

		_applicationUserServiceMock
			.Setup (s => s.GetUserId())
			.Returns(id);

		//Act
		var act = async () => await _service.ChangeRoleAsync(id);

		//Assert
		await Assert.ThrowsAsync<DomainException>(act);
		OnValidationAssert(Times.Never, Times.Never);
	}

	[Fact]
	public async Task ValidId_ChangeRoleAsync_MustFailBecauseUserNotFound()
	{
		//Arrange
		var id = Guid.NewGuid();

		_applicationUserServiceMock
			.Setup(s => s.GetUserId())
			.Returns(Guid.NewGuid());

		_repositoryMock
			.Setup(r => r.GetByIdAsync(id))
			.ReturnsAsync(null as User);

		//Act
		var act = async () => await _service.ChangeRoleAsync(id);

		//Assert
		await Assert.ThrowsAsync<NotFoundException>(act);
		OnValidationAssert(Times.Once, Times.Never);
	}
}
