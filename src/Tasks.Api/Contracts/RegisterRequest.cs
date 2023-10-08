using System.ComponentModel.DataAnnotations;

namespace Tasks.Api.Contracts;

public class RegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Username { get; set; } = null!;

    [Required]
    [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password must be complex")]
    public string Password { get; set; } = null!;

    [Required]
    public string DisplayName { get; set; } = null!;
}