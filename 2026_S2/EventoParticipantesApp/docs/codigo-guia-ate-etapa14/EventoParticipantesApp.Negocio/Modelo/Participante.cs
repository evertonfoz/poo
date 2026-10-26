namespace EventoParticipantes.Negocio.Modelo;

public sealed class Participante : Pessoa {
  // O resto do sistema continua usando ParticipanteId
  public Guid ParticipanteId => Id;

  public Participante(string nome, string email,
      Guid? participanteId = null)
    : base(nome, email, participanteId) { }
}
