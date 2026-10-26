using System.Text.Json;
using EventoParticipantes.Negocio.Contratos;
using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Persistencia.Json.Dtos;

namespace EventoParticipantes.Persistencia.Json;

public sealed class RepositorioJson : IRepositorioSistema {
  private static readonly JsonSerializerOptions Opcoes =
    new() { WriteIndented = true };

  private readonly string _caminho;

  public RepositorioJson(string caminho) {
    if (string.IsNullOrWhiteSpace(caminho))
      throw new ArgumentException("Caminho obrigatório.");
    _caminho = caminho;
  }

  public EstadoSistema Carregar() {
    // Primeira execução: ainda não há arquivo
    if (!File.Exists(_caminho))
      return new([], [], []);
    var json = File.ReadAllText(_caminho);
    var dados = LerDto(json);
    return ParaEstado(dados);
  }

  public void Salvar(EstadoSistema estado) {
    var dados = ParaDto(estado);
    var json = JsonSerializer.Serialize(dados, Opcoes);
    GravarArquivo(json);
  }

  // Texto -> DTOs: JSON quebrado vira InvalidDataException
  private static DadosDto LerDto(string json) {
    try {
      return JsonSerializer.Deserialize<DadosDto>(json)
        ?? throw new InvalidDataException("Arquivo de dados vazio.");
    }
    catch (JsonException ex) {
      throw new InvalidDataException(
        $"Arquivo de dados inválido. {ex.Message}", ex);
    }
  }

  // DTOs -> entidades: os construtores validam de novo
  // e os Ids do arquivo voltam a ser objetos
  private static EstadoSistema ParaEstado(DadosDto dados) {
    var organizadores = dados.Organizadores
      .Select(x => new Organizador(x.Nome, x.Email, x.Id))
      .ToList();
    var participantes = dados.Participantes
      .Select(x => new Participante(x.Nome, x.Email, x.Id))
      .ToList();

    var eventos = new List<Evento>();
    foreach (var dto in dados.Eventos) {
      var organizador = organizadores.FirstOrDefault(
          x => x.OrganizadorId == dto.OrganizadorId)
        ?? throw new InvalidDataException(
          $"Evento '{dto.Nome}': organizador não encontrado.");
      var evento = new Evento(dto.Nome, organizador,
        dto.Capacidade, dto.Id);
      foreach (var id in dto.ParticipanteIds) {
        var participante = participantes.FirstOrDefault(
            x => x.ParticipanteId == id)
          ?? throw new InvalidDataException(
            $"Evento '{dto.Nome}': participante não encontrado.");
        evento.Inscrever(participante);
      }
      eventos.Add(evento);
    }
    return new EstadoSistema(organizadores, participantes, eventos);
  }

  // Entidades -> DTOs: objetos na memória viram Ids no arquivo
  private static DadosDto ParaDto(EstadoSistema estado) => new() {
    Organizadores = estado.Organizadores
      .Select(x => new PessoaDto {
        Id = x.OrganizadorId, Nome = x.Nome, Email = x.Email })
      .ToList(),
    Participantes = estado.Participantes
      .Select(x => new PessoaDto {
        Id = x.ParticipanteId, Nome = x.Nome, Email = x.Email })
      .ToList(),
    Eventos = estado.Eventos
      .Select(x => new EventoDto {
        Id = x.EventoId,
        Nome = x.Nome,
        OrganizadorId = x.Organizador.OrganizadorId,
        Capacidade = x.Capacidade,
        ParticipanteIds = x.Participantes
          .Select(p => p.ParticipanteId).ToList() })
      .ToList()
  };

  // Grava num .tmp e só então substitui o arquivo verdadeiro
  private void GravarArquivo(string json) {
    var pasta = Path.GetDirectoryName(Path.GetFullPath(_caminho))!;
    Directory.CreateDirectory(pasta);
    var temporario = _caminho + ".tmp";
    try {
      File.WriteAllText(temporario, json);
      File.Move(temporario, _caminho, overwrite: true);
    }
    finally {
      if (File.Exists(temporario))
        File.Delete(temporario);
    }
  }
}
