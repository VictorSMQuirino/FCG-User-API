using FCG_Users.Application.Services;
using FCG_Users.Domain.Interfaces.Services;

namespace FCG_Users.Tests.Fixtures;

[CollectionDefinition(nameof(BcryptServiceCollection))]
public class BcryptServiceCollection : ICollectionFixture<BcryptServiceFixture>;

public class BcryptServiceFixture : ServiceFixture
{
	public IPasswordService GetService() => new BcryptService();

	public string GetValidPassword() => "Teste@321/";
}
