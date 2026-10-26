namespace GestaoAcademica.Apresentacao.Argumentos;

public class UsoInvalidoException : Exception
{
    public UsoInvalidoException(string mensagem)
        : base(mensagem)
    {
    }
}
