namespace ProvaPratica.Dominio.Entidades;

public class Reserva
{
    // Associações 1:1
    public Usuario Solicitante { get; }
    public Sala Sala { get; }
    public Equipamento Equipamento { get; }
    public Usuario? Supervisor { get; }
    public bool Finalizada { get; private set; }

    // Agregação: responsáveis existem independentemente da reserva
    private readonly List<Usuario> _responsaveis = new();
    public IReadOnlyCollection<Usuario> Responsaveis => _responsaveis.AsReadOnly();

    // Composição: etapas pertencem exclusivamente à reserva
    private readonly List<EtapaDeConferencia> _etapas = new();
    public IReadOnlyCollection<EtapaDeConferencia> Etapas => _etapas.AsReadOnly();

    // Composição: registros pertencem exclusivamente à reserva
    private readonly List<RegistroDeConferencia> _registros = new();
    public IReadOnlyCollection<RegistroDeConferencia> Registros => _registros.AsReadOnly();

    public Reserva(Usuario solicitante, Sala sala, Equipamento equipamento, Usuario? supervisor = null)
    {
        Solicitante = solicitante;
        Sala = sala;
        Equipamento = equipamento;
        Supervisor = supervisor;
    }

    public void AdicionarResponsavel(Usuario responsavel)
    {
        _responsaveis.Add(responsavel);
    }

    public EtapaDeConferencia AdicionarEtapa(string descricao, bool obrigatoria)
    {
        var etapa = new EtapaDeConferencia(descricao, obrigatoria);
        _etapas.Add(etapa);
        return etapa;
    }

    public RegistroDeConferencia AdicionarRegistro(Usuario responsavel, EtapaDeConferencia etapa, string observacao, bool aprovada)
    {
        var registro = new RegistroDeConferencia(responsavel, etapa, observacao, aprovada);
        _registros.Add(registro);
        return registro;
    }

    public void Finalizar()
    {
        Finalizada = true;
    }
}
