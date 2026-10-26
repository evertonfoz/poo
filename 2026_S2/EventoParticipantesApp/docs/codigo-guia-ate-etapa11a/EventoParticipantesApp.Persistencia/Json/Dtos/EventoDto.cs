namespace EventoParticipantes.Persistencia.Json.Dtos;

internal sealed class EventoDto {
  public Guid Id { get; set; }
  public string Nome { get; set; } = "";
  public Guid OrganizadorId { get; set; }
  public int Capacidade { get; set; }
  public List<Guid> ParticipanteIds { get; set; } = new();
}
