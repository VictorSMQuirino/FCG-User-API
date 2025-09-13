using FCG_Users.Domain.Enums;
using System.Net;

namespace FCG_Users.Domain.Exceptions;

public class NotFoundException(string entityName, object? key) 
	: AppException($"{entityName} not found.", ErrorCode.NotFoundError, HttpStatusCode.NotFound)
{
	public string EntityName { get; set; } = entityName;
	public object? Key { get; set; } = key;
}
