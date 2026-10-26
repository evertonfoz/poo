using System.Text.Json;
using EventoParticipantes.Negocio.Contratos;
using EventoParticipantes.Persistencia.Json.Dtos;

namespace EventoParticipantes.Persistencia.Json;

public sealed class RepositorioJson : IRepositorioSistema
{
    // RespectNullableAnnotations: um "null" no arquivo em propriedade não anulável
    // (ex.: "Organizadores": null) vira JsonException em vez de NullReferenceException.
    private static readonly JsonSerializerOptions Opcoes = new()
    {
        WriteIndented = true,
        RespectNullableAnnotations = true
    };

    private readonly string _caminho;

    public RepositorioJson(string caminho)
    {
        if (string.IsNullOrWhiteSpace(caminho))
            throw new ArgumentException("Caminho obrigatório.", nameof(caminho));
        _caminho = caminho;
    }

    public EstadoSistema Carregar()
    {
        if (!File.Exists(_caminho))
            return new EstadoSistema([], [], []);

        try
        {
            var dados = JsonSerializer.Deserialize<DadosDto>(File.ReadAllText(_caminho), Opcoes)
                ?? throw new InvalidDataException("Arquivo JSON vazio.");
            return MapeadorDados.ParaEstado(dados);
        }
        catch (JsonException ex)
        {
            throw new InvalidDataException("Arquivo JSON inválido.", ex);
        }
        catch (ArgumentException ex)
        {
            throw new InvalidDataException($"Dados inválidos no arquivo: {ex.Message}", ex);
        }
    }

    public void Salvar(EstadoSistema estado)
    {
        var json = JsonSerializer.Serialize(MapeadorDados.ParaDto(estado), Opcoes);

        var pasta = Path.GetDirectoryName(Path.GetFullPath(_caminho))!;
        Directory.CreateDirectory(pasta);

        // Grava em arquivo temporário e substitui o original,
        // para não corromper os dados se a gravação for interrompida.
        var temporario = _caminho + ".tmp";
        try
        {
            File.WriteAllText(temporario, json);
            File.Move(temporario, _caminho, overwrite: true);
        }
        finally
        {
            if (File.Exists(temporario))
                File.Delete(temporario);
        }
    }
}
