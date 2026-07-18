using Npgsql;

namespace ProjectAcademy.DBContext
{
    public class PostgresConnectionProvider
    {
        private readonly string connection;
        public PostgresConnectionProvider(IConfiguration configuration)
        {
            connection = configuration["ConnectionStrings:DefaultConnection"] ?? throw new InvalidOperationException(nameof(configuration)); 
        }
        public NpgsqlConnection CreateConnection () => new NpgsqlConnection(connection);

    }
}
