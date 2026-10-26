namespace GestaoAcademica.Persistencia.Json.Dtos;

public sealed class PessoaDto
{
    public required Guid Id { get; init; }
    public required string Nome { get; init; }
    public required string Email { get; init; }
}

public sealed class AtividadeDto
{
    public required Guid Id { get; init; }
    public required string Titulo { get; init; }
    public required string Descricao { get; init; }
    public required DateOnly DataEntrega { get; init; }
}

public sealed class TurmaDto
{
    public required Guid Id { get; init; }
    public required string Nome { get; init; }
    public required Guid ProfessorId { get; init; }
    public required List<Guid> AlunosIds { get; init; }
    public required List<AtividadeDto> Atividades { get; init; }
}

public sealed class BancoDadosDto
{
    public required int Versao { get; init; }
    public required List<PessoaDto> Professores { get; init; }
    public required List<PessoaDto> Alunos { get; init; }
    public required List<TurmaDto> Turmas { get; init; }
}
