using AutoFixture;
using FCG_Users.Application.Services;
using FCG_Users.Domain.Entities;
using FCG_Users.Domain.Interfaces.Repositories;
using FCG_Users.Domain.Interfaces.Services;
using Moq;

namespace FCG_Users.Tests.Fixtures;

[CollectionDefinition(nameof(UserServiceCollection))]
public class UserServiceCollection : ICollectionFixture<UserServiceFixture>;

public class UserServiceFixture : ServiceFixture
{
	public IUserService GetService(
		Mock<IUserRepository> repositoryMock,
		Mock<IApplicationUserService> applicationUserServiceMock
		) => new UserService(repositoryMock.Object, applicationUserServiceMock.Object);

	internal static Mock<IUserRepository> GetRepositoryMock() => new();

	internal static Mock<IApplicationUserService> GetApplicationUserServiceMock() => new();

	internal User GetValidUser()
		=> _fixture.Build<User>().Create();

	internal ICollection<User> GetValidUserList()
		=> [.. _fixture.Build<User>().CreateMany()];
}
