using Microsoft.AspNetCore.Identity;

namespace Tasks.Domain;

public class User : IdentityUser<Guid>
{
    public string? DisplayName { get; private set; } = null!;
    public string? ProfilePictureUrl { get; private set; }
    public string? Bio { get; private set; }
    public string? RefreshToken { get; private set; }

    public User(
        Guid id,
        string username,
        string email,
        string? displayName,
        string? profilePictureUrl,
        string? bio,
        string? refreshToken)
    {
        Id = id;
        UserName = username;
        Email = email;
        DisplayName = displayName;
        ProfilePictureUrl = profilePictureUrl;
        Bio = bio;
        RefreshToken = refreshToken;
    }

    public static User Create(
        string username,
        string email,
        string? displayName = null,
        string? profilePictureUrl = null,
        string? bio = null,
        string? refreshToken = null)
    {
        return new User(
            Guid.NewGuid(),
            username,
            email,
            displayName,
            profilePictureUrl,
            bio,
            refreshToken);
    }

    public User Update(
        string? username = null,
        string? email = null,
        string? displayName = null,
        string? profilePictureUrl = null,
        string? bio = null,
        string? refreshToken = null)
    {
        UserName = username ?? UserName;
        Email = email ?? Email;
        DisplayName = displayName ?? DisplayName;
        ProfilePictureUrl = profilePictureUrl ?? ProfilePictureUrl;
        Bio = bio ?? Bio;
        RefreshToken = refreshToken ?? RefreshToken;

        return this;
    }

    private User() { }
}