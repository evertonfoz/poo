namespace GestaoAcademica.Negocio.Validacao;

public static class Validador
{
    public static string TextoObrigatorio(string? valor, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new RegraDeNegocioException(
                $"O campo '{campo}' é obrigatório."
            );
        }

        return valor.Trim();
    }

    public static string Email(string? valor)
    {
        string email = TextoObrigatorio(valor, "E-mail");
        int arroba = email.IndexOf('@');

        bool valido =
            arroba > 0
            && arroba == email.LastIndexOf('@')
            && arroba < email.Length - 1
            && email.IndexOf('.', arroba) > arroba + 1
            && !email.EndsWith('.')
            && !email.Contains(' ');

        if (!valido)
            throw new RegraDeNegocioException("E-mail inválido.");

        return email;
    }

    public static DateOnly Data(DateOnly valor, string campo)
    {
        if (valor == default)
        {
            throw new RegraDeNegocioException(
                $"O campo '{campo}' é obrigatório."
            );
        }

        return valor;
    }

    public static Guid Id(Guid valor)
    {
        if (valor == Guid.Empty)
            throw new RegraDeNegocioException("Identificador inválido.");

        return valor;
    }
}
