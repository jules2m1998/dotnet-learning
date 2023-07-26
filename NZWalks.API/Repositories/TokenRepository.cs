using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly IOptions<JwtOptions> options;
    private JwtOptions JwtOptions => options.Value;

    public TokenRepository(IOptions<JwtOptions> options)
    {
        this.options = options;
    }

    public string CreateJWTToken(IdentityUser user, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };
        var claimRoles = roles.Select(r => new Claim(ClaimTypes.Role, r));
        claims.AddRange(claimRoles);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(JwtOptions.Issuer, JwtOptions.Audience, claims, expires: DateTime.UtcNow.AddMinutes(15), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
