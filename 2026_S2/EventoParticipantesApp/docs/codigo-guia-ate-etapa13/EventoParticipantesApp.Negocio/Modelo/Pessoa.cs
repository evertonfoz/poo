using EventoParticipantes.Negocio.Validacao;

namespace EventoParticipantes.Negocio.Modelo;

// O que Organizador e Participante têm em comum, escrito uma vez
public abstract class Pessoa {
  public Guid Id { get; }
  public string Nome { get; private set; }
  public string Email { get; private set; }

  protected Pessoa(string nome, string email, Guid? id) {
    Id = id ?? Guid.NewGuid();
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

  // Mesmo tipo e mesmo Guid: o mesmo cadastro (Etapa 4)
  public override bool Equals(object? obj) =>
    obj is Pessoa outra
    && GetType() == outra.GetType()
    && Id == outra.Id;

  public override int GetHashCode() => Id.GetHashCode();
}
