namespace EventoParticipantes.Negocio.Modelo;

public sealed class Organizador : Pessoa {
  // O resto do sistema continua usando OrganizadorId
  public Guid OrganizadorId => Id;

  public Organizador(string nome, string email,
      Guid? organizadorId = null)
    : base(nome, email, organizadorId) { }
}
