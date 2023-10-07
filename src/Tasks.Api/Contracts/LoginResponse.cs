using Tasks.Domain;

namespace Tasks.Api.Contracts;

public record LoginResponse(
    Guid Id,
    string Email,
    string Username,
    string? DisplayName,
    string? Bio,
    string? ProfilePictureUrl,
    string AccessToken,
    string RefreshToken)
{
    public LoginResponse(User user, string accessToken, string refreshToken)
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
