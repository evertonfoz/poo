namespace Models.Academico;
public class Disciplina
{
    public string Nome { get; init; }
    public string Professor { get; private set; }  

    public Disciplina(string nome, string professor)
    {
        Nome = string.IsNullOrWhiteSpace(nome) ? throw new ArgumentException("O nome da disciplina é obrigatório.", nameof(nome)) : nome;
        Professor = string.IsNullOrWhiteSpace(professor) ? throw new ArgumentException("O nome do professor é obrigatório.", nameof(professor)) : professor;
    }

    public void AtualizarProfessor(string novoProfessor)
    {
        if (string.IsNullOrWhiteSpace(novoProfessor))
        {
            throw new ArgumentException("O nome do professor não pode ser vazio.", nameof(novoProfessor));
        }
        Professor = novoProfessor;
    }
}    