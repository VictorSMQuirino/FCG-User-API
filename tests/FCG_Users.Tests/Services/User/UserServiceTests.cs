using FCG_Users.Application.Services;
using FCG_Users.Domain.Interfaces.Repositories;
using FCG_Users.Domain.Interfaces.Services;
using FCG_Users.Tests.Fixtures;
using Moq;

namespace FCG_Users.Tests.Services;

public class UserServiceTests
{
	protected readonly UserServiceFixture _serviceFixture;
	protected readonly Mock<IUserRepository> _repositoryMock;
	protected readonly Mock<IApplicationUserService> _applicationUserServiceMock;
	protected readonly IUserService _service;

	public UserServiceTests()
	{
		_serviceFixture = new();
		_repositoryMock = UserServiceFixture.GetRepositoryMock();
		_applicationUserServiceMock = UserServiceFixture.GetApplicationUserServiceMock();
		_service = new UserService(_repositoryMock.Object, _applicationUserServiceMock.Object);
	}
}
