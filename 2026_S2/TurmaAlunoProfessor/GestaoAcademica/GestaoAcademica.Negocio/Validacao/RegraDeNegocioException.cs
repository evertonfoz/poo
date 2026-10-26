namespace GestaoAcademica.Negocio.Validacao;

public class RegraDeNegocioException : Exception
{
    public RegraDeNegocioException(string mensagem)
        : base(mensagem)
    {
    }
}
