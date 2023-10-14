using Tasks.Domain;

namespace Tasks.Api.Contracts;

public record UserProfileDto(
    Guid Id,
    string Email,
    string Username,
    string? DisplayName,
    string? Bio,
    string? ProfilePictureUrl)
{
    public UserProfileDto(User user)
        : this(
            user.Id,
            user.Email!,
            user.UserName!,
            user.DisplayName,
            user.Bio,
            user.ProfilePictureUrl) { }
}