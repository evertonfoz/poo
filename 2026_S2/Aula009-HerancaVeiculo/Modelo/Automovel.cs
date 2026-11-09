namespace Aula009_HerancaVeiculo.Modelo;

public class Automovel : Veiculo
{
    public int QuantidadePortas { get; private set; }
    public int CapacidadePassageiros { get; private set; }

    public Automovel(
        string placa,
        string marca,
        string modelo,
        int anoFabricacao,
        double quilometragemInicial,
        int quantidadePortas,
        int capacidadePassageiros)
        : base(placa, marca, modelo, anoFabricacao, quilometragemInicial)
    {
        DefinirQuantidadePortas(quantidadePortas);
        DefinirCapacidadePassageiros(capacidadePassageiros);
    }

    private void DefinirQuantidadePortas(int quantidadePortas)
    {
        if (quantidadePortas <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantidadePortas), "A quantidade de portas deve ser maior que zero.");

        QuantidadePortas = quantidadePortas;
    }

    private void DefinirCapacidadePassageiros(int capacidadePassageiros)
    {
        if (capacidadePassageiros <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacidadePassageiros), "A capacidade de passageiros deve ser maior que zero.");

        CapacidadePassageiros = capacidadePassageiros;
    }

    public void ExibirCapacidade()
    {
        Console.WriteLine($"[Capacidade Automóvel] {Marca} {Modelo} - Portas: {QuantidadePortas} | Lotação máxima: {CapacidadePassageiros} passageiros.");
    }
}

