using FCG_Users.Domain.Enums;

namespace FCG_Users.Domain.Exceptions;

public class ValidationErrorException(IDictionary<string, string[]> errors) 
	: AppException("One or more errors occurred.", ErrorCode.ValidationError)
{
	public IDictionary<string, string[]> Errors { get; set; } = errors;
}
