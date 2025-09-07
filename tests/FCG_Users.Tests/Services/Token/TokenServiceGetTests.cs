using FCG_Users.Tests.Fixtures;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FCG_Users.Tests.Services.Token;

[Collection(nameof(TokenServiceCollection))]
public class TokenServiceGetTests : TokenServiceTests
{
	[Fact]
	public async Task ValidLogin_GenerateToken_MustGenerateAValidToken()
	{
		//Arrange
		var user = _serviceFixture.GetValidUser();
		var handler = new JwtSecurityTokenHandler();

		//Act
		var token = _service.GenerateToken(user);
		var jwt = handler.ReadJwtToken(token);

		//Assert
		token.Should().NotBeNullOrWhiteSpace();
		jwt.Claims.Should().Contain(
			c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == user.Id.ToString()
			);
		jwt.Claims.Should().Contain(
			c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id.ToString()
			);
		jwt.Claims.Should().Contain(
			c => c.Type == ClaimTypes.Name && c.Value == user.UserName
			);
		jwt.Claims.Should().Contain(
			c => c.Type == ClaimTypes.Role && c.Value == user.Role.ToString()
			);
		jwt.ValidTo.Should()
			.BeCloseTo(DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes), precision: TimeSpan.FromSeconds(5));
		jwt.Header.Alg.Should().Be(SecurityAlgorithms.HmacSha256);
	}
}
