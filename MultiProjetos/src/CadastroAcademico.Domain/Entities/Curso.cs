namespace CadastroAcademico.Domain;

public class Curso
{
    private static int _ultimoId;
    private readonly List<Matricula> _matriculas = new();

    public int Id { get; }
    public string Nome { get; private set; }
    public string Sigla { get; private set; }
    public int CargaHoraria { get; private set; }
    public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();

    public Curso(string nome, string sigla, int cargaHoraria)
    {
        Id = ++_ultimoId;
        Nome = ValidarNome(nome);
        Sigla = ValidarSigla(sigla);
        CargaHoraria = ValidarCargaHoraria(cargaHoraria);
    }

    public void AtualizarDados(string nome, string sigla, int cargaHoraria)
    {
        Nome = ValidarNome(nome);
        Sigla = ValidarSigla(sigla);
        CargaHoraria = ValidarCargaHoraria(cargaHoraria);
    }

    public Matricula MatricularAluno(Aluno aluno)
    {
        if (aluno is null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        var matriculaAtiva = _matriculas.Any(m => m.Aluno.Id == aluno.Id && m.Status == StatusMatricula.Ativa);
        if (matriculaAtiva)
        {
            throw new InvalidOperationException("Aluno ja possui matricula ativa neste curso.");
        }

        var matricula = new Matricula(aluno, this, DateTime.UtcNow);
        _matriculas.Add(matricula);
        aluno.RegistrarMatricula(matricula);

        return matricula;
    }

    private static string ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("Nome do curso e obrigatorio.", nameof(nome));
        }

        return nome.Trim();
    }

    private static string ValidarSigla(string sigla)
    {
        if (string.IsNullOrWhiteSpace(sigla))
        {
            throw new ArgumentException("Sigla do curso e obrigatoria.", nameof(sigla));
        }

        return sigla.Trim().ToUpperInvariant();
    }

    private static int ValidarCargaHoraria(int cargaHoraria)
    {
        if (cargaHoraria <= 0)
        {
            throw new ArgumentException("Carga horaria deve ser maior que zero.", nameof(cargaHoraria));
        }

        return cargaHoraria;
    }
}