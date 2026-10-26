namespace EventoParticipantes.Persistencia.Json.Dtos;

internal sealed class DadosDto {
  public List<PessoaDto> Organizadores { get; set; } = new();
  public List<PessoaDto> Participantes { get; set; } = new();
  public List<EventoDto> Eventos { get; set; } = new();
}
