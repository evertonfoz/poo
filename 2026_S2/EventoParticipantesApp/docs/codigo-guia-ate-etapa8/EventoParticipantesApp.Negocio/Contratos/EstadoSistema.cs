using EventoParticipantes.Negocio.Modelo;

namespace EventoParticipantes.Negocio.Contratos;

public sealed class EstadoSistema {
  public IReadOnlyCollection<Organizador> Organizadores { get; }
  public IReadOnlyCollection<Participante> Participantes { get; }
  public IReadOnlyCollection<Evento> Eventos { get; }

  public EstadoSistema(
      IEnumerable<Organizador> organizadores,
      IEnumerable<Participante> participantes,
      IEnumerable<Evento> eventos) {
    Organizadores = Array.AsReadOnly(organizadores.ToArray());
    Participantes = Array.AsReadOnly(participantes.ToArray());
    Eventos = Array.AsReadOnly(eventos.ToArray());
  }
}
