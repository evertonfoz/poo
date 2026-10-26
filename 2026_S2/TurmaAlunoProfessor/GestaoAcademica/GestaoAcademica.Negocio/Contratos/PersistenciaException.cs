namespace GestaoAcademica.Negocio.Contratos;

public class PersistenciaException : Exception
{
    public PersistenciaException(
        string mensagem,
        Exception? causa = null)
        : base(mensagem, causa)
    {
    }
}
