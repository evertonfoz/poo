using SuporteTecnico.Domain.Exceptions;

namespace SuporteTecnico.Domain.Validations;

public static class ValidadorDominio
{
    public static string ValidarTextoObrigatorio(string valor, string mensagemErro)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new DomainException(mensagemErro);

        return valor.Trim();
    }
}