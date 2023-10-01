using Tasks.Domain;

namespace Tasks.Api.Contracts;

public record LoginResponse(
    Guid Id,
    string Email,
    string Username,
    string? DisplayName,
    string? Bio,
    string? ProfilePictureUrl,
    string Token)
{
    public LoginResponse(User user, string token)
        : this(
            user.Id,
            user.Email!,
            user.UserName!,
            user.DisplayName,
            user.Bio,
            user.ProfilePictureUrl,
            token) { }
}