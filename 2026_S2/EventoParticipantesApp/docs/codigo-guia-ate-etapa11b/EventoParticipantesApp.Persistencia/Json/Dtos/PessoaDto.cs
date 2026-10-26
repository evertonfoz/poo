namespace EventoParticipantes.Persistencia.Json.Dtos;

internal sealed class PessoaDto {
  public Guid Id { get; set; }
  public string Nome { get; set; } = "";
  public string Email { get; set; } = "";
}
