using System.Data;
using Microsoft.Data.Sqlite;

namespace School.Persistence.AdoNet.Connections;

public sealed class SqliteConnectionFactory(string connectionString)
{
    private readonly string _connectionString = connectionString ??
               throw new ArgumentNullException(nameof(connectionString));

    public IDbConnection CreateConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();

            // Ensure the Courses table exists
            using var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Courses (
                    CourseId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    WorkloadHours INTEGER NOT NULL,
                    IsActive INTEGER NOT NULL DEFAULT 1
                );
            ";
            command.ExecuteNonQuery();

            return connection;
        }
}
