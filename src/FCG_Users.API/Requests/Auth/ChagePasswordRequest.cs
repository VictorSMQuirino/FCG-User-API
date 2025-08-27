namespace FCG_Users.API.Requests.Auth;

public record ChangePasswordRequest(string CurrentPassword, string NewPassword);
