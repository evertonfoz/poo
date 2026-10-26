using EventoParticipantes.Negocio.Contratos;
using EventoParticipantes.Negocio.Modelo;

namespace EventoParticipantes.Negocio.Servicos;

// Núcleo: estado, construtor e apoio comum
public sealed partial class Gerenciador {
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
    ValidarIntegridade();
  }

  // ---------- Apoio ----------

  private static bool MesmoEmail(string a, string b) =>
    string.Equals(a, b, StringComparison.OrdinalIgnoreCase);

  private void Gravar() => _repositorio.Salvar(
    new EstadoSistema(_organizadores, _participantes, _eventos));

  // Regras do conjunto: dados vindos de fora (o arquivo)
  // também precisam respeitá-las
  private void ValidarIntegridade() {
    ExigirUnicos(
      _organizadores.Select(x => x.OrganizadorId.ToString()),
      "Guid repetido entre organizadores.");
    ExigirUnicos(
      _participantes.Select(x => x.ParticipanteId.ToString()),
      "Guid repetido entre participantes.");
    ExigirUnicos(
      _eventos.Select(x => x.EventoId.ToString()),
      "Guid repetido entre eventos.");
    ExigirUnicos(
      _organizadores.Select(x => x.Email),
      "e-mail repetido entre organizadores.");
    ExigirUnicos(
      _participantes.Select(x => x.Email),
      "e-mail repetido entre participantes.");
  }

  private static void ExigirUnicos(IEnumerable<string> valores,
      string problema) {
    var vistos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    foreach (var valor in valores)
      if (!vistos.Add(valor))
        throw new InvalidOperationException(
          $"Dados inconsistentes: {problema}");
  }
}
