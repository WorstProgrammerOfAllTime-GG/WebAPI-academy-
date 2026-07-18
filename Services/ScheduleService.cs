using Dapper;
using Npgsql;
using ProjectAcademy.DBContext;
using ProjectAcademy.Models.DTO;
using System.Security.Claims;

namespace ProjectAcademy.Services
{
    public class ScheduleService
    {
        private readonly PostgresConnectionProvider _postgres;
        public ScheduleService(PostgresConnectionProvider postgres)=> _postgres = postgres;
       
        public async Task<IEnumerable<StudentSchedule>> GetSchedule(HttpContext context)
        {
            using var db = _postgres.CreateConnection();
            var stringID = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(stringID, out int studentID))
            {
                var result = await db.QueryAsync<StudentSchedule>
               ("SELECT st.FullName, cs.Title, sc.DateTime " +
               "FROM Students st " +
               "JOIN GroupsStudents sg ON sg.IDStudent = st.ID " +
               "JOIN Groups gp ON sg.IDGroup = gp.ID " +
               "JOIN Courses cs ON gp.IDCourse = cs.ID " +
               "JOIN Schedule sc ON gp.ID = sc.IDGroup " +
               "WHERE st.ID = @StudentID", new { StudentID = studentID });
                return result;
            }
            throw new UnauthorizedAccessException("Invalid or missing Student ID in token.");

        }
    }
}
