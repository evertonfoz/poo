using System.Text.Json;
using EventoParticipantes.Negocio.Contratos;
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

  // Etapa 11b: ler o arquivo. Por enquanto, começa sempre vazio.
  public EstadoSistema Carregar() => new([], [], []);

  public void Salvar(EstadoSistema estado) {
    var dados = ParaDto(estado);
    var json = JsonSerializer.Serialize(dados, Opcoes);
    GravarArquivo(json);
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
