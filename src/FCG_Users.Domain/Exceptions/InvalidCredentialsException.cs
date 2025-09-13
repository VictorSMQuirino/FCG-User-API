using FCG_Users.Domain.Enums;
using System.Net;

namespace FCG_Users.Domain.Exceptions;

public class InvalidCredentialsException() 
	: AppException("Invalid email or password.", ErrorCode.InvalidCredentialsError, HttpStatusCode.Unauthorized);
