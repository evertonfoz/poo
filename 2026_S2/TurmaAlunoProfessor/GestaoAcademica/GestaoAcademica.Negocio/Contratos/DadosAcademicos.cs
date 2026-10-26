using GestaoAcademica.Negocio.Modelo;

namespace GestaoAcademica.Negocio.Contratos;

public sealed class DadosAcademicos
{
    public IReadOnlyList<Professor> Professores { get; }
    public IReadOnlyList<Aluno> Alunos { get; }
    public IReadOnlyList<Turma> Turmas { get; }

    public DadosAcademicos(
        IEnumerable<Professor> professores,
        IEnumerable<Aluno> alunos,
        IEnumerable<Turma> turmas)
    {
        Professores = professores.ToList().AsReadOnly();
        Alunos = alunos.ToList().AsReadOnly();
        Turmas = turmas.ToList().AsReadOnly();
    }
}
