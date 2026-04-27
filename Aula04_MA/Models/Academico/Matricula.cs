namespace Models.Academico;
public class Matricula
{
    public Aluno Aluno { get; init; }
    public Disciplina Disciplina { get; init; }
    public DateTime DataMatricula { get; init; }

    public Matricula(Aluno aluno, Disciplina disciplina, DateTime dataMatricula)
    {
        Aluno = aluno ?? throw new ArgumentNullException(nameof(aluno), "O aluno é obrigatório.");
        Disciplina = disciplina ?? throw new ArgumentNullException(nameof(disciplina), "A disciplina é obrigatória.");
        DataMatricula = (dataMatricula < DateTime.Now || dataMatricula > DateTime.Now) ? dataMatricula : throw new ArgumentException("A data de matrícula deve ser anterior à data atual.", nameof(dataMatricula));
    }

    override public string ToString()
    {
        return $"Aluno: {Aluno.Nome}, RA: {Aluno.RegistroAcademico}, Email: {Aluno.Email}\n" +
               $"Disciplina: {Disciplina.Nome}, Professor: {Disciplina.Professor}\n" +
               $"Data da Matrícula: {DataMatricula:d}";
    }
}