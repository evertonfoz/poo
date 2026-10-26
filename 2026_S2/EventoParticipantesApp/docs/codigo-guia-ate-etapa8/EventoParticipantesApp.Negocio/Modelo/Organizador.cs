using EventoParticipantes.Negocio.Validacao;

namespace EventoParticipantes.Negocio.Modelo;

public sealed class Organizador {
  public Guid OrganizadorId { get; }
  public string Nome { get; private set; }
  public string Email { get; private set; }

  public Organizador(string nome, string email, Guid? organizadorId = null) {
    OrganizadorId = organizadorId ?? Guid.NewGuid();
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
    obj is Organizador outro
    && OrganizadorId == outro.OrganizadorId;

  public override int GetHashCode() => OrganizadorId.GetHashCode();
}
