using EventoParticipantes.Negocio.Validacao;

namespace EventoParticipantes.Negocio.Modelo;

public sealed class Evento {
  private readonly List<Participante> _participantes = new();

  public Guid EventoId { get; }
  public string Nome { get; private set; }
  public Organizador Organizador { get; }
  public int Capacidade { get; private set; }
  public IReadOnlyCollection<Participante> Participantes =>
    _participantes.AsReadOnly();

  public Evento(string nome, Organizador organizador,
      int capacidade, Guid? eventoId = null) {
    EventoId = eventoId ?? Guid.NewGuid();
    Nome = Validador.TextoObrigatorio(nome, "Nome");
    Organizador = organizador
      ?? throw new ArgumentNullException(nameof(organizador));
    Capacidade = Validador.InteiroPositivo(capacidade, "Capacidade");
  }

  public void Alterar(string? nome = null, int? capacidade = null) {
    if (nome is null && capacidade is null)
      throw new ArgumentException(
        "Informe ao menos o nome ou a capacidade.");

    // 1) validar tudo, sem tocar no objeto
    var novoNome = nome is null
      ? Nome
      : Validador.TextoObrigatorio(nome, "Nome");
    var novaCapacidade = capacidade is null
      ? Capacidade
      : Validador.InteiroPositivo(capacidade.Value, "Capacidade");
    if (novaCapacidade < _participantes.Count)
      throw new InvalidOperationException(
        "Capacidade menor que o número de inscrições.");

    // 2) aplicar as mudanças somente depois que tudo passou
    Nome = novoNome;
    Capacidade = novaCapacidade;
  }

  public void Inscrever(Participante participante) {
    if (_participantes.Contains(participante))
      throw new InvalidOperationException("Participante já inscrito.");
    if (_participantes.Count >= Capacidade)
      throw new InvalidOperationException("Evento lotado.");
    _participantes.Add(participante);
  }

  public void CancelarInscricao(Participante participante) {
    if (!_participantes.Remove(participante))
      throw new InvalidOperationException("Inscrição não encontrada.");
  }
}
