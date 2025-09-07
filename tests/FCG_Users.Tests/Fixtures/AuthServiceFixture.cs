using AutoFixture;
using FCG_Users.Application.Services;
using FCG_Users.Domain.DTO.Auth;
using FCG_Users.Domain.DTO.User;
using FCG_Users.Domain.Entities;
using FCG_Users.Domain.Interfaces.Repositories;
using FCG_Users.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FCG_Users.Tests.Fixtures;

[CollectionDefinition(nameof(AuthServiceCollection))]
public class AuthServiceCollection : ICollectionFixture<AuthServiceFixture>;

public class AuthServiceFixture : ServiceFixture
{
	public IAuthService GetService(
		Mock<IUserRepository> userRepositoryMock,
		Mock<ITokenService> tokenServiceMock,
		Mock<IPasswordService> passwordServiceMock,
		Mock<IApplicationUserService> applicationUserServiceMock
		)
	{
		return new AuthService(
			userRepositoryMock.Object,
			tokenServiceMock.Object,
			passwordServiceMock.Object,
			applicationUserServiceMock.Object
			);
	}

	internal static Mock<IUserRepository> GetUserRepositoryMock() => new();

	internal static Mock<ITokenService> GetTokenServiceMock() => new();

	internal static Mock<IPasswordService> GetPasswordServiceMock() => new();

	internal static Mock<IApplicationUserService> GetApplicationUserServiceMock() => new();

	internal static IConfiguration GetConfiguration() => new Mock<IConfiguration>().Object;

	internal LoginDto GetValidLoginDto()
		=> _fixture.Build<LoginDto>().Create();

	internal User GetValidUser()
		=> _fixture.Build<User>().Create();

	internal string GetValidToken()
		=> _fixture.Build<string>().Create();

	internal CreateUserDto GetValidCreateUserDto(
		string userName = "Fulano",
		string email = "fulano@teste.com",
		string password = "Teste@321/"
		)
		=> _fixture.Build<CreateUserDto>()
		.With(dto => dto.UserName, userName)
		.With(dto => dto.Email, email)
		.With(dto => dto.Password, password)
		.Create();

	internal ChangePasswordDto GetValidChangePasswordDto(string? newPassword = null)
		=> _fixture
		.Build<ChangePasswordDto>()
		.With(dto => dto.NewPassword, newPassword ?? "Teste@321/")
		.Create();
}
