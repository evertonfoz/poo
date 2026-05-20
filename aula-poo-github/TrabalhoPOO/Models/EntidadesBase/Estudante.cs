namespace TrabalhoPOO.Models.EntidadesBase;

public class Estudante
{
    public string Nome { get; private set; }
    public string Matricula { get; }
    public string Email { get; }
    public Curso Curso { get; }

    public Estudante(string nome, string matricula, string email, string curso)
        : this(nome, matricula, email, new Curso(curso))
    {
    }

    public Estudante(string nome, string matricula, string email, Curso curso)
    {
        Nome = ValidadorDominio.ValidarCampoObrigatorio(nome, nameof(nome));
        Matricula = ValidadorDominio.ValidarCampoObrigatorio(matricula, nameof(matricula));
        Email = ValidadorDominio.ValidarEmail(email, nameof(email));
        Curso = curso ?? throw new ArgumentNullException(nameof(curso));
    }

    public void AtualizarNome(string nome)
    {
        Nome = ValidadorDominio.ValidarCampoObrigatorio(nome, nameof(nome));
    }
}