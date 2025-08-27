using FCG_Users.API.Requests.User;
using FCG_Users.API.Responses.User;
using FCG_Users.Domain.DTO.User;

namespace FCG_Users.API.Extensions.Converters;

public static class UserConverter
{
    public static CreateUserDto ToDto(this CreateUserRequest request)
        => new(request.Username, request.Email, request.Password);

    public static GetUserByIdResponse ToResponse(this UserDto dto)
        => new(dto.Id, dto.UserName, dto.Email);

    public static ICollection<GetUserByIdResponse> ToResponse(this ICollection<UserDto> dtoList)
        => [.. dtoList.Select(ToResponse)];
}
