using Microsoft.AspNetCore.Identity;

namespace Tasks.Domain;

public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public string? Bio { get; private set; }

    public ApplicationUser(
        string displayName,
        string? profilePictureUrl,
        string? bio)
    {
        DisplayName = displayName;
        ProfilePictureUrl = profilePictureUrl;
        Bio = bio;
    }

    public static ApplicationUser Create(
        string displayName,
        string? profilePictureUrl,
        string? bio)
    {
        return new ApplicationUser(
            displayName,
            profilePictureUrl,
            bio);
    }
}