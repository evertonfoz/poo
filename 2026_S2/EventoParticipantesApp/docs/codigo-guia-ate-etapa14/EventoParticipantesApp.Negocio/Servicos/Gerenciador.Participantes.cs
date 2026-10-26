using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Validacao;

namespace EventoParticipantes.Negocio.Servicos;

public sealed partial class Gerenciador {
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

  private Participante BuscarParticipante(Guid participanteId) =>
    _participantes.FirstOrDefault(
        x => x.ParticipanteId == participanteId)
      ?? throw new KeyNotFoundException("Participante não encontrado.");
}
