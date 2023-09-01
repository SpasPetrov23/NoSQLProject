using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using No_SQL_Project.Models;

namespace No_SQL_Project.Utils;

public class TokenProvider
{
    private readonly IConfiguration configuration;

    public TokenProvider(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GenerateJwtToken(User user)
    {
        byte[] key = Encoding.ASCII.GetBytes(configuration["AppSettings:Secret"]);
        JwtSecurityToken jwToken = new(
            configuration["AppSettings:JWTIssuer"],
            configuration["AppSettings:JWTAudience"],
            GetUserClaims(user),
            new DateTimeOffset(DateTime.Now).DateTime,
            new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
            new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        );

        string token = new JwtSecurityTokenHandler().WriteToken(jwToken);

        return token;
    }
    
    private IEnumerable<Claim> GetUserClaims(User user)
    {
        IEnumerable<Claim> claims = new[]
        {
            new Claim(ClaimTypes.Email, user.UserEmail),
        };
        return claims;
    }
}