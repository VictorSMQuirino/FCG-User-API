using FCG_Common.Domain.Exceptions;
using FCG_Common.Domain.Extensions;
using FCG_Users.Application.Converters;
using FCG_Users.Application.Validators.User;
using FCG_Users.Domain.DTO.Auth;
using FCG_Users.Domain.DTO.User;
using FCG_Users.Domain.Entities;
using FCG_Users.Domain.Interfaces.Repositories;
using FCG_Users.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace FCG_Users.Application.Services;

public class AuthService : IAuthService
{
	private readonly IUserRepository _userRepository;
	private readonly ITokenService _tokenService;
	private readonly IPasswordService _passwordService;

	public AuthService(IUserRepository userRepository, ITokenService tokenService, IPasswordService passwordService)
	{
		_userRepository = userRepository;
		_tokenService = tokenService;
		_passwordService = passwordService;
	}

	public async Task<string> Login(LoginDto dto)
	{
		var user = await _userRepository.GetBy(u => u.Email == dto.Email);

		if (user is null || !_passwordService.VerifyHashedPassword(user.Password, dto.Password))
			throw new InvalidCredentialException();

		return _tokenService.GenerateToken(user);
	}

	public async Task SignUp(CreateUserDto dto, IConfiguration configuration)
	{
		var validationResult = await new CreateUserValidator().ValidateAsync(dto);

		if (!validationResult.IsValid) throw new ValidationErrorsException(validationResult.Errors.ToErrorsPropertyDictionary());

		var alreadyExistsUserWithEmail = await _userRepository.ExistsBy(user => user.Email == dto.Email);

		if (alreadyExistsUserWithEmail) throw new DuplicatedEntityException(nameof(User), nameof(dto.Email), dto.Email);

		var hashedPassword = _passwordService.HashPassword(dto.Password);

		var newUser = dto.ToUser(hashedPassword);

		await _userRepository.CreateAsync(newUser);
	}
}
