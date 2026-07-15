using Npgsql;

namespace ProjectAcademy.DBContext
{
    public class PostgresCreate
    {
        private readonly string connection;
        public PostgresCreate(IConfiguration configuration)
        {
            connection = configuration["ConnectionStrings:DefaultConnection"] ?? throw new InvalidOperationException(nameof(configuration)); 
        }
        public NpgsqlConnection CreateConnection () => new NpgsqlConnection(connection);

    }
}
