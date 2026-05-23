namespace CadastroAcademico.Domain;

public class Aluno
{
    private static int _ultimoId;
    private readonly List<Matricula> _matriculas = new();

    public int Id { get; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public PerfilAcademico PerfilAcademico { get; private set; }
    public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();

    public Aluno(string nome, string email, PerfilAcademico perfilAcademico)
    {
        Id = ++_ultimoId;
        Nome = ValidarNome(nome);
        Email = ValidarEmail(email);
        PerfilAcademico = perfilAcademico ?? throw new ArgumentNullException(nameof(perfilAcademico), "Perfil academico e obrigatorio.");
    }

    public void AtualizarNome(string nome)
    {
        Nome = ValidarNome(nome);
    }

    public void AtualizarEmail(string email)
    {
        Email = ValidarEmail(email);
    }

    public void AtualizarPerfilAcademico(PerfilAcademico perfilAcademico)
    {
        PerfilAcademico = perfilAcademico ?? throw new ArgumentNullException(nameof(perfilAcademico), "Perfil academico e obrigatorio.");
    }

    internal void RegistrarMatricula(Matricula matricula)
    {
        if (matricula is null)
        {
            throw new ArgumentNullException(nameof(matricula));
        }

        if (_matriculas.Any(m => m.Id == matricula.Id))
        {
            return;
        }

        _matriculas.Add(matricula);
    }

    private static string ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("Nome do aluno e obrigatorio.", nameof(nome));
        }

        return nome.Trim();
    }

    private static string ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email do aluno e obrigatorio.", nameof(email));
        }

        return email.Trim().ToLowerInvariant();
    }
}