namespace Tasks.Api.Models;

public class JwtOptions
{
    public string AccessTokenSecret { get; set; } = null!;
    public string RefreshTokenSecret { get; set; } = null!;
    public int AccessTokenExpirationMinutes { get; set; }
    public int RefreshTokenExpirationHours { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}