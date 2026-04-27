namespace Models.Academico;
public class Aluno
{
    public string RegistroAcademico { get; init; }
    public string Nome { get; private set; }
    public DateTime DataNascimento { get; init; }
    public string? Email { get; private set; }

    public Aluno(string registroAcademico, string nome, DateTime dataNascimento, string? email)
    {
        RegistroAcademico = string.IsNullOrWhiteSpace(registroAcademico) ? throw new ArgumentException("O registro acadêmico é obrigatório.", nameof(registroAcademico)) : registroAcademico;
        Nome = string.IsNullOrWhiteSpace(nome) ? throw new ArgumentException("O nome do aluno é obrigatório.", nameof(nome)) : nome;
        DataNascimento = dataNascimento < DateTime.Now ? dataNascimento : throw new ArgumentException("A data de nascimento deve ser anterior à data atual.", nameof(dataNascimento));
        Email = email?.Contains('@') == true ? email : throw new ArgumentException("O email deve conter '@'.", nameof(email));
    }

    public void AtualizarEmail(string novoEmail)
    {
        if (string.IsNullOrWhiteSpace(novoEmail) || !novoEmail.Contains('@'))
        {
            throw new ArgumentException("O email deve conter '@' e não pode ser vazio.", nameof(novoEmail));
        }
        Email = novoEmail;
    }

    public void AtualizarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
        {
            throw new ArgumentException("O nome do aluno não pode ser vazio.", nameof(novoNome));
        }
        Nome = novoNome;
    }
}