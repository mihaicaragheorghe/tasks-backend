using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tasks.Api.Models;

namespace Tasks.Api.Services;

public class RefreshTokenValidator(IOptions<JwtOptions> options)
{
    private readonly JwtOptions _options = options.Value;

    public bool Validate(string refreshToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.RefreshTokenSecret);
            tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }
}