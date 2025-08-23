using System.Text.RegularExpressions;

namespace FCG_Users.Application.Validators;

public static partial class PasswordValidator
{
	[GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$")]
	private static partial Regex PasswordValidatorRegex();

	public static bool Validate(string password)
		=> password.Length > 8 && PasswordValidatorRegex().IsMatch(password);
}
