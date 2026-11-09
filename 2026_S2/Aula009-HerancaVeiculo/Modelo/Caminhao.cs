namespace Aula009_HerancaVeiculo.Modelo;

public class Caminhao : Veiculo
{
    public double CapacidadeMaximaCargaKg { get; private set; }
    public int QuantidadeEixos { get; private set; }

    public Caminhao(
        string placa,
        string marca,
        string modelo,
        int anoFabricacao,
        double quilometragemInicial,
        double capacidadeMaximaCargaKg,
        int quantidadeEixos)
        : base(placa, marca, modelo, anoFabricacao, quilometragemInicial)
    {
        DefinirCapacidadeMaximaCarga(capacidadeMaximaCargaKg);
        DefinirQuantidadeEixos(quantidadeEixos);
    }

    private void DefinirCapacidadeMaximaCarga(double capacidadeKg)
    {
        if (capacidadeKg <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacidadeKg), "A capacidade máxima de carga deve ser estritamente maior que zero.");

        CapacidadeMaximaCargaKg = capacidadeKg;
    }

    private void DefinirQuantidadeEixos(int quantidadeEixos)
    {
        if (quantidadeEixos < 2)
            throw new ArgumentOutOfRangeException(nameof(quantidadeEixos), "Um caminhão deve possuir no mínimo 2 eixos.");

        QuantidadeEixos = quantidadeEixos;
    }

    // Método de negócio que valida e checa se uma carga pode ser transportada
    public bool VerificarCapacidade(double pesoCarga)
    {
        if (pesoCarga <= 0)
            return false;

        return pesoCarga <= CapacidadeMaximaCargaKg;
    }

    public void ExibirEspecificacoesCarga()
    {
        Console.WriteLine($"[Especificações Caminhão] {Marca} {Modelo} - Eixos: {QuantidadeEixos} | Carga máxima suportada: {CapacidadeMaximaCargaKg:N0} kg.");
    }
}

