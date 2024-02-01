namespace Application.Common.Models;

public class RefreshToken
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;

    public RefreshToken(Guid userId, string token)
    {
        UserId = userId;
        Token = token;
    }
}