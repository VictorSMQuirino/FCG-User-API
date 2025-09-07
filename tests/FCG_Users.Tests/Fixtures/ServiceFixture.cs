using AutoFixture;

namespace FCG_Users.Tests.Fixtures;

public abstract class ServiceFixture
{
	protected readonly Fixture _fixture;

	protected ServiceFixture()
	{
		_fixture = new();
	}
}
