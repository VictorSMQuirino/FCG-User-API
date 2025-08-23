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
	private readonly IUserRepository _userRepositoryy;
	private readonly IApplicationUserService _applicationUserService;

	public UserService(IUserRepository userRepositoryy, IApplicationUserService applicationUserService)
	{
		_userRepositoryy = userRepositoryy;
		_applicationUserService = applicationUserService;
	}
	public async Task<UserDto?> GetByIdAsync(Guid id)
	{
		var user = await _userRepositoryy.GetByIdAsync(id);

		return user?.ToDto() ?? throw new NotFoundException(nameof(User), id);
	}

	public async Task<ICollection<UserDto>> GetAllAsync()
		=> (await _userRepositoryy.GetAllAsync()).ToDtoList();

	public async Task ChengeRoleAsync(Guid id)
	{
		if (_applicationUserService.GetUserId() == id)
			throw new DomainException("Cannot change own role.");

		var user = await _userRepositoryy.GetByIdAsync(id) ?? throw new NotFoundException(nameof(User), id);

		user.Role = user.Role == UserRole.Admin
			? UserRole.Common
			: UserRole.Admin;

		await _userRepositoryy.UpdateAsync(user);
	}
}
