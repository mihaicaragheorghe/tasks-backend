using Tasks.Domain;

namespace Tasks.Api.Contracts;

public record UserDto(
    Guid Id,
    string Email,
    string Username,
    string? DisplayName,
    string? Bio,
    string? ProfilePictureUrl,
    string AccessToken,
    string RefreshToken)
{
    public UserDto(User user, string accessToken, string refreshToken)
        : this(
            user.Id,
            user.Email!,
            user.UserName!,
            user.DisplayName,
            user.Bio,
            user.ProfilePictureUrl,
            accessToken,
            refreshToken) { }
}
