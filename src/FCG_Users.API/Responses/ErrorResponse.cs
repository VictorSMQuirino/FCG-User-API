namespace FCG_Users.API.Responses;

public record ErrorResponse(string Code, string Message, object? Details, DateTime Timestamp);
