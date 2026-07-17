using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ProjectAcademy.Jwt;

namespace ProjectAcademy.Authorization_and_authentication_JWT_approach_
{
    public class CreatorToken
    {
        private readonly AuthenticationOptions _options;
        public CreatorToken(AuthenticationOptions options) => _options = options;
        
        public JwtSecurityToken GetJwtToken(List<Claim> claims)
        {      
            var jwt = new JwtSecurityToken(
                         issuer: AuthenticationOptions.ISSUER,
                         audience: AuthenticationOptions.AUDIENCE,
                         claims: claims,
                         expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),
                         signingCredentials: new SigningCredentials(_options.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                       );
            return jwt;
        }
    }
}
