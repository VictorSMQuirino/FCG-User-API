using AutoFixture;
using FCG_Users.Application.Auth;
using FCG_Users.Application.Services;
using FCG_Users.Domain.Entities;
using FCG_Users.Domain.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace FCG_Users.Tests.Fixtures;

[CollectionDefinition(nameof(TokenServiceCollection))]
public class TokenServiceCollection : ICollectionFixture<TokenServiceFixture>;

public class TokenServiceFixture : ServiceFixture
{
	public ITokenService GetService(JwtSettings jwtSettings)
	{
		var options = Options.Create(jwtSettings);

		return new TokenService(options);
	}

	internal static JwtSettings GetJwtSettings() => new()
	{
		Key = "eedf796e-5c49-4888-8844-80160dcce097",
		Issuer = "Issuer_test",
		Audience = "Audience_test",
		ExpireMinutes = 60
	};

	internal User GetValidUser()
		=> _fixture.Build<User>().Create();
}
