
using Microsoft.IdentityModel.Tokens;
using ProjectAcademy.Authorization_and_authentication_JWT_approach_;
using System.Text;
namespace ProjectAcademy.Jwt
{
    public class AuthenticationOptions
    {
        private readonly IConfiguration _configuration;
        public const string ISSUER = "Academy";
        public const string AUDIENCE = "Students";   
        public AuthenticationOptions(IConfiguration configuration) => _configuration = configuration;
      
       
        public string GetKey()
        {
            return  _configuration["Jwt:key"] ?? throw new NullReferenceException("key is null");
        }
          

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
          
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetKey()));
        }
    }
}
