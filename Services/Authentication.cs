using Dapper;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using ProjectAcademy.DBContext;
using ProjectAcademy.EndPointsAndControllers;
using ProjectAcademy.Jwt;
using ProjectAcademy.Models;
using ProjectAcademy.Validation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using ProjectAcademy.Authorization_and_authentication_JWT_approach_;

namespace ProjectAcademy.Services
{
    public class Authentication
    {
        private readonly PostgresCreate _postgres;
        private readonly Validator _validator;
        private readonly CreatorToken _creatorToken;
        public Authentication(PostgresCreate postgres, Validator validator, CreatorToken createrToken)
        {
            _postgres = postgres;
            _validator = validator;
            _creatorToken = createrToken;
        }
        public async Task Registration(RequestReg request)
        {
            string fullName =  _validator.Validation(request.FullName,  fullname=> !string.IsNullOrEmpty(fullname), "FullName is empty or null");
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            string email =  _validator.Validation(request.Email, email => !string.IsNullOrEmpty(email) && Regex.IsMatch(email, emailPattern), "Email format is invalid");
            email = await _validator.ValidationUniqueEmail(email);
            string phonePattern = @"^\+?[1-9]\d{1,14}$";
            string phoneNumber =  _validator.Validation(request.PhoneNumber, phone => !string.IsNullOrEmpty(phone) && Regex.IsMatch(phone, phonePattern), "Phone Number format is invalid");
            phoneNumber = await _validator.ValidationUniquePhoneNumber(phoneNumber);
            string password =  _validator.Validation(request.Password, password => !string.IsNullOrEmpty(password) && password.Length >= 6, "Password is invalid");
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            using var db = _postgres.CreateConnection();

            await db.ExecuteAsync("INSERT INTO Students (FullName,Email,PhoneNumber,Password) VALUES (@FullName, @Email, @PhoneNumber, @Password)", new
            {
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                Password = passwordHash,
            });
        }

        public async Task<string> Login (RequestLogin request)
        {
            List<Claim> claims;
            using var db = _postgres.CreateConnection();

            var student = await db.QueryFirstOrDefaultAsync<Students>("SELECT * FROM Students " +
                "WHERE Email = @Email OR PhoneNumber = @PhoneNumber", new
                {
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                });
            if (student != null)
            {
                bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, student.Password);
                if (isValid)
                {
                    claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, student.ID.ToString()),
                        new Claim(ClaimTypes.Name, student.FullName),
                        new Claim(ClaimTypes.Email, student.Email),
                        new Claim(ClaimTypes.MobilePhone, student.PhoneNumber)
                    };

                    var jwt = _creatorToken.GetJwtToken(claims);
                    return new JwtSecurityTokenHandler().WriteToken(jwt);
                }
                else throw new UnauthorizedAccessException("Invalid data");
            } else throw new NullReferenceException("Student not found");                              
        }
    }
}
