
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectAcademy.Authorization_and_authentication_JWT_approach_;
using ProjectAcademy.Jwt;

namespace ProjectAcademy.Authorization_and_authentication_JWT_approach_
{
    public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly AuthenticationOptions _options;
        public ConfigureJwtBearerOptions(AuthenticationOptions options) => _options = options;

      
        public void Configure(string? name, JwtBearerOptions options)
        {
            Console.WriteLine("JWT CONFIGURED");
            Console.WriteLine(_options.GetKey());
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthenticationOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AuthenticationOptions.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = _options.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
            };
        }

        public void Configure(JwtBearerOptions options)
        {
            Configure(string.Empty, options);
        }
    }
}
