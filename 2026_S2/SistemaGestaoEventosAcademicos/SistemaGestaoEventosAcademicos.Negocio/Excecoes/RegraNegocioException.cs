namespace SistemaGestaoEventosAcademicos.Negocio.Excecoes;

/// <summary>
/// Exceção de domínio que encapsula e comunica qualquer violação das regras de negócio invariantes do sistema.
/// </summary>
public class RegraNegocioException : Exception
{
    public RegraNegocioException(string mensagem) : base(mensagem)
    {
    }

    public RegraNegocioException(string mensagem, Exception innerException) : base(mensagem, innerException)
    {
    }
}
