using Microsoft.Data.Sqlite;

string connectionString = "Data Source=escola.db";

using (var connection = new SqliteConnection(connectionString))
{
    connection.Open();

    var command = connection.CreateCommand();

    // command.CommandText = @"
    //     CREATE TABLE IF NOT EXISTS alunos (
    //         aluno_id INTEGER PRIMARY KEY AUTOINCREMENT,
    //         nome TEXT NOT NULL,
    //         matricula TEXT NOT NULL UNIQUE
    //     );
    // ";

command.CommandText = @"
    SELECT aluno_id, nome, matricula
    FROM alunos;
";

using var reader = command.ExecuteReader();

while (reader.Read())
{
    int alunoId = reader.GetInt32(0);
    string nome = reader.GetString(1);
    string matricula = reader.GetString(2);

    Console.WriteLine($"{alunoId} - {nome} - {matricula}");
}
}