namespace CadastroAcademico.Domain;

public class PerfilAcademico
{
    public string RegistroAcademico { get; private set; }
    public string Periodo { get; private set; }

    public PerfilAcademico(string registroAcademico, string periodo)
    {
        RegistroAcademico = ValidarRegistroAcademico(registroAcademico);
        Periodo = ValidarPeriodo(periodo);
    }

    public void AtualizarPeriodo(string periodo)
    {
        Periodo = ValidarPeriodo(periodo);
    }

    private static string ValidarRegistroAcademico(string registroAcademico)
    {
        if (string.IsNullOrWhiteSpace(registroAcademico))
        {
            throw new ArgumentException("Registro academico e obrigatorio.", nameof(registroAcademico));
        }

        return registroAcademico.Trim().ToUpperInvariant();
    }

    private static string ValidarPeriodo(string periodo)
    {
        if (string.IsNullOrWhiteSpace(periodo))
        {
            throw new ArgumentException("Periodo e obrigatorio.", nameof(periodo));
        }

        return periodo.Trim();
    }
}