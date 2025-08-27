namespace FCG_Users.Domain.DTO.Auth;

public record ChangePasswordDto(string CurrentPassword, string NewPassword);
