using System.Text.RegularExpressions;

namespace TrabalhoPOO.Models.EntidadesBase;

public static class ValidadorDominio
{
    private static readonly Regex EmailRegex = new(
        @"^[^\s@]+@[^\s@]+\.[^\s@]+$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public static string ValidarCampoObrigatorio(string? valor, string nomeParametro)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new ArgumentException("O campo deve ser informado.", nomeParametro);
        }

        return valor.Trim();
    }

    public static string ValidarEmail(string? email, string nomeParametro)
    {
        var emailValidado = ValidarCampoObrigatorio(email, nomeParametro);

        if (!EmailRegex.IsMatch(emailValidado))
        {
            throw new ArgumentException("O email informado e invalido.", nomeParametro);
        }

        return emailValidado;
    }
}