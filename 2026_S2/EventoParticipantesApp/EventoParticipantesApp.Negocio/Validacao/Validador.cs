using System.Text.RegularExpressions;

namespace EventoParticipantes.Negocio.Validacao;

public static class Validador
{
    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public static string TextoObrigatorio(string valor, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException($"{campo} obrigatório.");
        return valor.Trim();
    }

    public static string Email(string email)
    {
        if (string.IsNullOrWhiteSpace(email)
            || !EmailRegex.IsMatch(email.Trim()))
            throw new ArgumentException("E-mail inválido.");
        return email.Trim();
    }

    public static int InteiroPositivo(int valor, string campo)
    {
        if (valor < 1)
            throw new ArgumentException($"{campo} deve ser maior que zero.");
        return valor;
    }
}
