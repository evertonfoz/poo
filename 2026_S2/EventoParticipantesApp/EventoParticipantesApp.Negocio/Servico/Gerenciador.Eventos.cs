using EventoParticipantes.Negocio.Modelo;

namespace EventoParticipantes.Negocio.Servico;

public sealed partial class Gerenciador
{
    public Guid CriarEvento(string nome, Guid organizadorId, int capacidade)
    {
        BuscarOrganizador(organizadorId);
        var evento = new Evento(nome, organizadorId, capacidade);
        _eventos.Add(evento);
        Gravar();
        return evento.EventoId;
    }

    public IReadOnlyCollection<Evento> ListarEventos() => _eventos.AsReadOnly();

    public Evento ObterEvento(Guid id) => BuscarEvento(id);

    public void AlterarCapacidadeEvento(Guid id, int capacidade)
    {
        BuscarEvento(id).AlterarCapacidade(capacidade);
        Gravar();
    }

    public void RemoverEvento(Guid id)
    {
        var item = BuscarEvento(id);
        if (item.ParticipanteIds.Count > 0)
            throw new InvalidOperationException("Evento possui inscrições.");
        _eventos.Remove(item);
        Gravar();
    }

    public void Inscrever(Guid eventoId, Guid participanteId)
    {
        BuscarParticipante(participanteId);
        BuscarEvento(eventoId).Inscrever(participanteId);
        Gravar();
    }

    public void CancelarInscricao(Guid eventoId, Guid participanteId)
    {
        BuscarEvento(eventoId).CancelarInscricao(participanteId);
        Gravar();
    }

    private Evento BuscarEvento(Guid id) =>
        _eventos.FirstOrDefault(x => x.EventoId == id)
            ?? throw new KeyNotFoundException("Evento não encontrado.");
}
