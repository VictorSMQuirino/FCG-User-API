namespace FCG_Users.API.Responses.User;

public record GetUserByIdResponse(Guid Id, string Username, string Email);
