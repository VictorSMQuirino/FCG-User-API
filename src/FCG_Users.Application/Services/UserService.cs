using FCG_Common.Domain.Exceptions;
using FCG_Users.Application.Converters;
using FCG_Users.Domain.DTO.User;
using FCG_Users.Domain.Entities;
using FCG_Users.Domain.Enums;
using FCG_Users.Domain.Interfaces.Repositories;
using FCG_Users.Domain.Interfaces.Services;

namespace FCG_Users.Application.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly IApplicationUserService _applicationUserService;

	public UserService(IUserRepository userRepository, IApplicationUserService applicationUserService)
	{
		_userRepository = userRepository;
		_applicationUserService = applicationUserService;
	}
	public async Task<UserDto?> GetByIdAsync(Guid id)
	{
		var user = await _userRepository.GetByIdAsync(id);

		return user?.ToDto() ?? throw new NotFoundException(nameof(User), id);
	}

	public async Task<ICollection<UserDto>> GetAllAsync()
		=> (await _userRepository.GetAllAsync()).ToDtoList();

	public async Task ChangeRoleAsync(Guid id)
	{
		if (_applicationUserService.GetUserId() == id)
			throw new DomainException("Cannot change own role.");

		var user = await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(User), id);

		user.Role = user.Role == UserRole.Admin
			? UserRole.Common
			: UserRole.Admin;

		await _userRepository.UpdateAsync(user);
	}
}
