using ProjectAcademy.Models;
using ProjectAcademy.EndPointsAndControllers;
using ProjectAcademy.DBContext;
using ProjectAcademy.Validation;
using Npgsql;
using Dapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.RegularExpressions;


namespace ProjectAcademy.Services
{
    public class Authentication
    {
        private readonly PostgresCreate _postgres;
        private readonly Validator _validator;
        public Authentication(PostgresCreate postgres, Validator validator)
        {
            _postgres = postgres;
            _validator = validator;
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

        public async Task Login (RequestLogin request)
        {

        }
    }
}
