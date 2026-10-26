using EventoParticipantes.Negocio.Contratos;
using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Validacao;

namespace EventoParticipantes.Negocio.Servicos;

public sealed class Gerenciador {
  private readonly IRepositorioSistema _repositorio;
  private readonly List<Organizador> _organizadores;
  private readonly List<Participante> _participantes;
  private readonly List<Evento> _eventos;

  public Gerenciador(IRepositorioSistema repositorio) {
    _repositorio = repositorio
      ?? throw new ArgumentNullException(nameof(repositorio));
    var estado = _repositorio.Carregar();
    _organizadores = estado.Organizadores.ToList();
    _participantes = estado.Participantes.ToList();
    _eventos = estado.Eventos.ToList();
  }

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

  // ---------- Participantes ----------

  public Guid CriarParticipante(string nome, string email) {
    var participante = new Participante(nome, email);
    if (_participantes.Any(
        x => MesmoEmail(x.Email, participante.Email)))
      throw new InvalidOperationException(
        "E-mail de participante já cadastrado.");
    _participantes.Add(participante);
    Gravar();
    return participante.ParticipanteId;
  }

  public IReadOnlyCollection<Participante> ListarParticipantes() =>
    _participantes.AsReadOnly();

  public Participante ObterParticipante(Guid participanteId) =>
    BuscarParticipante(participanteId);

  public void AlterarParticipante(Guid participanteId,
      string? nome = null, string? email = null) {
    var participante = BuscarParticipante(participanteId);
    if (email is not null) {
      var novoEmail = Validador.Email(email);
      if (_participantes.Any(x => x.ParticipanteId != participanteId
          && MesmoEmail(x.Email, novoEmail)))
        throw new InvalidOperationException(
          "E-mail de participante já cadastrado.");
    }
    participante.Alterar(nome, email);
    Gravar();
  }

  public void RemoverParticipante(Guid participanteId) {
    var participante = BuscarParticipante(participanteId);
    if (_eventos.Any(x => x.Participantes.Contains(participante)))
      throw new InvalidOperationException(
        "Participante inscrito em evento.");
    _participantes.Remove(participante);
    Gravar();
  }

  // ---------- Apoio ----------

  private Organizador BuscarOrganizador(Guid organizadorId) =>
    _organizadores.FirstOrDefault(
        x => x.OrganizadorId == organizadorId)
      ?? throw new KeyNotFoundException("Organizador não encontrado.");

  private Participante BuscarParticipante(Guid participanteId) =>
    _participantes.FirstOrDefault(
        x => x.ParticipanteId == participanteId)
      ?? throw new KeyNotFoundException("Participante não encontrado.");

  private static bool MesmoEmail(string a, string b) =>
    string.Equals(a, b, StringComparison.OrdinalIgnoreCase);

  private void Gravar() => _repositorio.Salvar(
    new EstadoSistema(_organizadores, _participantes, _eventos));
}
