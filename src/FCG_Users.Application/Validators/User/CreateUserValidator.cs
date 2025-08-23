using FCG_Users.Domain.DTO.User;
using FluentValidation;

namespace FCG_Users.Application.Validators.User;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
	public CreateUserValidator()
	{
		RuleFor(dto => dto.UserName)
			.NotEmpty().WithMessage("Username is required.")
			.Length(2, 50).WithMessage("Username must be between 2 and 50 characteres.");

		RuleFor(dto => dto.Email)
			.NotEmpty().WithMessage("Email is required.")
			.EmailAddress().WithMessage("Invalid email address.");

		RuleFor(dto => dto.Password)
			.NotEmpty().WithMessage("Password is required.")
			.Must(PasswordValidator.Validate)
			.WithMessage("The password must contain at least one uppercase letter, one lowercase letter, one special character and one number.");
	}
}
