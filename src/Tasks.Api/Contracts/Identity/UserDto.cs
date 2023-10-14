using Tasks.Domain;

namespace Tasks.Api.Contracts;

public record UserIdentityDto(
    UserProfileDto Profile,
    string AccessToken,
    string RefreshToken)
{
    public UserIdentityDto(User user, string accessToken, string refreshToken)
        : this(new UserProfileDto(user), accessToken, refreshToken) { }
}
