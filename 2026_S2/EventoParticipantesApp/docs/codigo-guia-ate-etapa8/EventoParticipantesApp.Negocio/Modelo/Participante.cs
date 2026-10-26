using EventoParticipantes.Negocio.Validacao;

namespace EventoParticipantes.Negocio.Modelo;

public sealed class Participante {
  public Guid ParticipanteId { get; }
  public string Nome { get; private set; }
  public string Email { get; private set; }

  public Participante(string nome, string email,
      Guid? participanteId = null) {
    ParticipanteId = participanteId ?? Guid.NewGuid();
    Nome = Validador.TextoObrigatorio(nome, "Nome");
    Email = Validador.Email(email);
  }

  public void Alterar(string? nome = null, string? email = null) {
    if (nome is null && email is null)
      throw new ArgumentException("Informe ao menos o nome ou o e-mail.");
    if (nome is not null)
      Nome = Validador.TextoObrigatorio(nome, "Nome");
    if (email is not null)
      Email = Validador.Email(email);
  }

  public override bool Equals(object? obj) =>
    obj is Participante outro
    && ParticipanteId == outro.ParticipanteId;

  public override int GetHashCode() => ParticipanteId.GetHashCode();
}
