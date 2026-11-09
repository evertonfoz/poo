namespace Aula009_HerancaVeiculo.Modelo;

public class Motocicleta : Veiculo
{
    public int Cilindradas { get; private set; }
    public string TipoPartida { get; private set; } = string.Empty;

    public Motocicleta(
        string placa,
        string marca,
        string modelo,
        int anoFabricacao,
        double quilometragemInicial,
        int cilindradas,
        string tipoPartida)
        : base(placa, marca, modelo, anoFabricacao, quilometragemInicial)
    {
        DefinirCilindradas(cilindradas);
        DefinirTipoPartida(tipoPartida);
    }

    private void DefinirCilindradas(int cilindradas)
    {
        if (cilindradas <= 0)
            throw new ArgumentOutOfRangeException(nameof(cilindradas), "As cilindradas devem ser maiores que zero.");

        Cilindradas = cilindradas;
    }

    private void DefinirTipoPartida(string tipoPartida)
    {
        if (string.IsNullOrWhiteSpace(tipoPartida))
            throw new ArgumentException("O tipo de partida é obrigatório e não pode ser vazio.", nameof(tipoPartida));

        TipoPartida = tipoPartida.Trim();
    }

    public void ExibirCaracteristicas()
    {
        Console.WriteLine($"[Características Motocicleta] {Marca} {Modelo} - Cilindradas: {Cilindradas} cc | Partida: {TipoPartida}.");
    }
}

