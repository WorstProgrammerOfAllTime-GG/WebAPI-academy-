
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace ProjectAcademy.Jwt
{
    public class AuthenticationOptions
    {
        public const string ISSUER = "Academy";
        public const string AUDIENCE = "Students";

        private const string KEY = "0123456789abcdef0123456789abcdef";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
