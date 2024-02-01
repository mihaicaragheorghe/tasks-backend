using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser<Guid>
{
    public string? DisplayName { get; private set; } = null!;
    public string? ProfilePictureUrl { get; private set; }
    public string? Bio { get; private set; }

    public User(
        Guid id,
        string username,
        string email,
        string? displayName,
        string? profilePictureUrl,
        string? bio)
    {
        Id = id;
        UserName = username;
        Email = email;
        DisplayName = displayName;
        ProfilePictureUrl = profilePictureUrl;
        Bio = bio;
    }

    public static User Create(
        string username,
        string email,
        string? displayName = null,
        string? profilePictureUrl = null,
        string? bio = null)
    {
        return new User(
            Guid.NewGuid(),
            username,
            email,
            displayName,
            profilePictureUrl,
            bio);
    }

    public User Update(
        string? username = null,
        string? email = null,
        string? displayName = null,
        string? profilePictureUrl = null,
        string? bio = null)
    {
        UserName = username ?? UserName;
        Email = email ?? Email;
        DisplayName = displayName ?? DisplayName;
        ProfilePictureUrl = profilePictureUrl ?? ProfilePictureUrl;
        Bio = bio ?? Bio;

        return this;
    }

    private User() { }
}