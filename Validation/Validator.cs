using Dapper;
using ProjectAcademy.Models;
using ProjectAcademy.DBContext;

namespace ProjectAcademy.Validation
{
    public class Validator
    {
        private readonly PostgresCreate _postgres;
        public Validator(PostgresCreate postgres)=> _postgres = postgres;
       
        public string Validation(string data, Func<string, bool> func, string error)
        {
            if (func(data)) return data;
            else throw new ValidationException(error);
        }

        public async Task<string> ValidationUniqueEmail(string data)
        {
            using var db = _postgres.CreateConnection();

            bool exists = await db.QueryFirstOrDefaultAsync<bool>(
            "SELECT EXISTS(SELECT 1 FROM Students WHERE Email = @Value)",
            new { Value = data }
            );

            if (exists) throw new ValidationException("Email already is exists");

            return data;
  
        }

        public async Task<string> ValidationUniquePhoneNumber(string data)
        {
            using var db = _postgres.CreateConnection();

            bool exists = await db.QueryFirstOrDefaultAsync<bool>(
            "SELECT EXISTS(SELECT 1 FROM Students WHERE PhoneNumber = @Value)",
            new { Value = data }
            );

            if (exists) throw new ValidationException("Phone number already is exists");

            return data;

        }
    }
}
