using FCG_Users.Application.Converters;
using FCG_Users.Domain.Entities;
using FCG_Users.Domain.Exceptions;
using FCG_Users.Tests.Fixtures;
using FluentAssertions;
using Moq;

namespace FCG_Users.Tests.Services;

[Collection(nameof(UserServiceCollection))]
public class UserServiceGetTests : UserServiceTests
{
	[Fact]
	public async Task ValidId_GetByIdAsync_MustReturnUserDto()
	{
		//Arrange
		var user = _serviceFixture.GetValidUser();

		_repositoryMock
			.Setup(r => r.GetByIdAsync(user.Id))
			.ReturnsAsync(user);

		//Act
		var result = await _service.GetByIdAsync(user.Id);

		//Assert
		result.Should().Be(user.ToDto());
		_repositoryMock
			.Verify(r => r.GetByIdAsync(user.Id), Times.Once);
	}

	[Fact]
	public async Task ValidId_getByIdAsync_MustFailBecauseUserNotFound()
	{
		//Arrange
		_repositoryMock
			.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(null as User);

		//Act
		var act = async () => await _service.GetByIdAsync(Guid.NewGuid());

		//Assert
		await Assert.ThrowsAsync<NotFoundException>(act);
		_repositoryMock
			.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
	}

	[Fact]
	public async Task ValidRequest_GetAllAsync_MustReturnAnUserDtoList()
	{
		//Arrange
		var userList = _serviceFixture.GetValidUserList();

		_repositoryMock
			.Setup(r => r.GetAllAsync())
			.ReturnsAsync(userList);

		//Act
		var result = await _service.GetAllAsync();

		//Assert
		result.Should().BeEquivalentTo(userList.ToDtoList());
		_repositoryMock
			.Verify(r => r.GetAllAsync(), Times.Once);
	}
}
