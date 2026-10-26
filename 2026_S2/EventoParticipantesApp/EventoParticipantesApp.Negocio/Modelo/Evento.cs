using EventoParticipantes.Negocio.Validacao;

namespace EventoParticipantes.Negocio.Modelo;

public sealed class Evento
{
    private readonly List<Guid> _participanteIds = new();
    public Guid EventoId { get; }
    public string Nome { get; private set; }
    public Guid OrganizadorId { get; }
    public int Capacidade { get; private set; }
    public IReadOnlyCollection<Guid> ParticipanteIds => _participanteIds.AsReadOnly();

    // participanteIds é usado ao reconstituir o evento a partir da persistência;
    // duplicidade e capacidade são conferidas pelo Gerenciador ao carregar.
    public Evento(string nome, Guid organizadorId, int capacidade,
                  Guid? eventoId = null, IEnumerable<Guid>? participanteIds = null)
    {
        EventoId = eventoId ?? Guid.NewGuid();
        Nome = Validador.TextoObrigatorio(nome, "Nome");
        OrganizadorId = organizadorId;
        Capacidade = Validador.InteiroPositivo(capacidade, "Capacidade");
        if (participanteIds is not null)
            _participanteIds.AddRange(participanteIds);
    }

    public void AlterarCapacidade(int capacidade)
    {
        Validador.InteiroPositivo(capacidade, "Capacidade");
        if (capacidade < _participanteIds.Count)
            throw new InvalidOperationException("Capacidade inválida para as inscrições atuais.");
        Capacidade = capacidade;
    }

    public void Inscrever(Guid participanteId)
    {
        if (_participanteIds.Contains(participanteId))
            throw new InvalidOperationException("Participante já inscrito.");
        if (_participanteIds.Count >= Capacidade)
            throw new InvalidOperationException("Evento lotado.");
        _participanteIds.Add(participanteId);
    }

    public void CancelarInscricao(Guid participanteId)
    {
        if (!_participanteIds.Remove(participanteId))
            throw new InvalidOperationException("Inscrição não encontrada.");
    }
}
