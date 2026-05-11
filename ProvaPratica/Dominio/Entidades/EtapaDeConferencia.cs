namespace ProvaPratica.Dominio.Entidades;

public class EtapaDeConferencia
{
    public string Descricao { get; }
    public bool Obrigatoria { get; }

    internal EtapaDeConferencia(string descricao, bool obrigatoria)
    {
        Descricao = descricao;
        Obrigatoria = obrigatoria;
    }
}
