namespace ProvaPratica.Dominio.Entidades;

public class RegistroDeConferencia
{
    public Usuario Responsavel { get; }
    public EtapaDeConferencia Etapa { get; }
    public string Observacao { get; }
    public bool Aprovada { get; }

    internal RegistroDeConferencia(Usuario responsavel, EtapaDeConferencia etapa, string observacao, bool aprovada)
    {
        Responsavel = responsavel;
        Etapa = etapa;
        Observacao = observacao;
        Aprovada = aprovada;
    }
}
