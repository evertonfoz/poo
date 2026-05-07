public class Curso
{
    private readonly List<Disciplina> _disciplinas = [];
    public string Nome { get; set; }
    public IReadOnlyList<Disciplina> Disciplinas => _disciplinas;

    public void AdicionarDisciplina(Disciplina disciplina)
    {
        _disciplinas.Add(disciplina);
    }
}