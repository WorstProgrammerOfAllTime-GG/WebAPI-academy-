using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ProjectAcademy.Jwt;

namespace ProjectAcademy.Authorization_and_authentication_JWT_approach_
{
    public class CreateToken
    {
        public static JwtSecurityToken GetJwtToken(List<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                         issuer: AuthenticationOptions.ISSUER,
                         audience: AuthenticationOptions.AUDIENCE,
                         claims: claims,
                         notBefore: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),
                         signingCredentials: new SigningCredentials(AuthenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                       );
            return jwt;
        }
    }
}
