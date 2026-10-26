using EventoParticipantes.Negocio.Modelo;

namespace EventoParticipantes.Negocio.Servicos;

public sealed partial class Gerenciador {
  // ---------- Eventos ----------

  public Guid CriarEvento(string nome, Guid organizadorId,
      int capacidade) {
    var organizador = BuscarOrganizador(organizadorId);
    var evento = new Evento(nome, organizador, capacidade);
    _eventos.Add(evento);
    Gravar();
    return evento.EventoId;
  }

  public IReadOnlyCollection<Evento> ListarEventos() =>
    _eventos.AsReadOnly();

  public Evento ObterEvento(Guid eventoId) =>
    BuscarEvento(eventoId);

  public void AlterarEvento(Guid eventoId,
      string? nome = null, int? capacidade = null) {
    var evento = BuscarEvento(eventoId);
    evento.Alterar(nome, capacidade);
    Gravar();
  }

  public void RemoverEvento(Guid eventoId) {
    var evento = BuscarEvento(eventoId);
    if (evento.Participantes.Count > 0)
      throw new InvalidOperationException(
        "Evento possui inscrições.");
    _eventos.Remove(evento);
    Gravar();
  }

  public void Inscrever(Guid eventoId, Guid participanteId) {
    var evento = BuscarEvento(eventoId);
    var participante = BuscarParticipante(participanteId);
    evento.Inscrever(participante);
    Gravar();
  }

  public void CancelarInscricao(Guid eventoId,
      Guid participanteId) {
    var evento = BuscarEvento(eventoId);
    var participante = BuscarParticipante(participanteId);
    evento.CancelarInscricao(participante);
    Gravar();
  }

  private Evento BuscarEvento(Guid eventoId) =>
    _eventos.FirstOrDefault(x => x.EventoId == eventoId)
      ?? throw new KeyNotFoundException("Evento não encontrado.");
}
