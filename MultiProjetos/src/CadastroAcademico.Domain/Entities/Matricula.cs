namespace CadastroAcademico.Domain;

public class Matricula
{
    private static int _ultimoId;

    public int Id { get; }
    public Aluno Aluno { get; }
    public Curso Curso { get; }
    public DateTime DataMatricula { get; }
    public StatusMatricula Status { get; private set; }

    internal Matricula(Aluno aluno, Curso curso, DateTime dataMatricula)
    {
        Aluno = aluno ?? throw new ArgumentNullException(nameof(aluno), "Aluno e obrigatorio para matricula.");
        Curso = curso ?? throw new ArgumentNullException(nameof(curso), "Curso e obrigatorio para matricula.");
        DataMatricula = dataMatricula;
        Status = StatusMatricula.Ativa;
        Id = ++_ultimoId;
    }

    public void Cancelar()
    {
        if (Status != StatusMatricula.Ativa)
        {
            throw new InvalidOperationException("Somente matriculas ativas podem ser canceladas.");
        }

        Status = StatusMatricula.Cancelada;
    }

    public void Concluir()
    {
        if (Status != StatusMatricula.Ativa)
        {
            throw new InvalidOperationException("Somente matriculas ativas podem ser concluidas.");
        }

        Status = StatusMatricula.Concluida;
    }
}