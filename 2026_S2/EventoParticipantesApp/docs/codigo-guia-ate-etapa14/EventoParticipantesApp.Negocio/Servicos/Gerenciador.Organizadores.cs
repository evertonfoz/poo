using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Validacao;

namespace EventoParticipantes.Negocio.Servicos;

public sealed partial class Gerenciador {
  // ---------- Organizadores ----------

  public Guid CriarOrganizador(string nome, string email) {
    var organizador = new Organizador(nome, email);
    if (_organizadores.Any(x => MesmoEmail(x.Email, organizador.Email)))
      throw new InvalidOperationException(
        "E-mail de organizador já cadastrado.");
    _organizadores.Add(organizador);
    Gravar();
    return organizador.OrganizadorId;
  }

  public IReadOnlyCollection<Organizador> ListarOrganizadores() =>
    _organizadores.AsReadOnly();

  public Organizador ObterOrganizador(Guid organizadorId) =>
    BuscarOrganizador(organizadorId);

  public void AlterarOrganizador(Guid organizadorId,
      string? nome = null, string? email = null) {
    var organizador = BuscarOrganizador(organizadorId);
    if (email is not null) {
      var novoEmail = Validador.Email(email);
      if (_organizadores.Any(x => x.OrganizadorId != organizadorId
          && MesmoEmail(x.Email, novoEmail)))
        throw new InvalidOperationException(
          "E-mail de organizador já cadastrado.");
    }
    organizador.Alterar(nome, email);
    Gravar();
  }

  public void RemoverOrganizador(Guid organizadorId) {
    var organizador = BuscarOrganizador(organizadorId);
    if (_eventos.Any(x => x.Organizador.Equals(organizador)))
      throw new InvalidOperationException(
        "Organizador possui eventos.");
    _organizadores.Remove(organizador);
    Gravar();
  }

  private Organizador BuscarOrganizador(Guid organizadorId) =>
    _organizadores.FirstOrDefault(
        x => x.OrganizadorId == organizadorId)
      ?? throw new KeyNotFoundException("Organizador não encontrado.");
}
