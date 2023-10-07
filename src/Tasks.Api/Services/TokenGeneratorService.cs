using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tasks.Api.Models;
using Tasks.Domain;

namespace Tasks.Api.Services;

public class TokenGeneratorService
{
    private readonly JwtOptions _options;

    public TokenGeneratorService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string Generate(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
        };

        if (string.IsNullOrWhiteSpace(_options.AccessTokenSecret))
            throw new Exception("Jwt key is missing from appsettings.json");

        var creds = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.AccessTokenSecret)), 
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(_options.AccessTokenExpirationHours),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        if (string.IsNullOrWhiteSpace(_options.RefreshTokenSecret))
            throw new Exception("Jwt refresh token key is missing from appsettings.json");

        var creds = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.RefreshTokenSecret)), 
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: new List<Claim>() 
            { 
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
            },
            expires: DateTime.Now.AddHours(_options.RefreshTokenExpirationHours),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}