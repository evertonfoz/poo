namespace Aula009_HerancaProduto.Modelo;

public class ProdutoEletronico : Produto
{
    public string Marca { get; private set; } = string.Empty;
    public int GarantiaMeses { get; private set; }

    public ProdutoEletronico(
        string codigo,
        string descricao,
        decimal precoBase,
        string marca,
        int garantiaMeses)
        : base(codigo, descricao, precoBase) // A classe base cuida de suas próprias validações
    {
        DefinirMarca(marca);
        AtualizarGarantia(garantiaMeses);
    }

    // A marca é uma característica de fabricação imutável externamente
    private void DefinirMarca(string marca)
    {
        if (string.IsNullOrWhiteSpace(marca))
            throw new ArgumentException("A marca é obrigatória e não pode ser vazia.", nameof(marca));

        Marca = marca;
    }

    // Métodos semânticos de negócio
    public void AtualizarGarantia(int novaGarantiaMeses)
    {
        if (novaGarantiaMeses < 0)
            throw new ArgumentOutOfRangeException(nameof(novaGarantiaMeses), "O período de garantia não pode ser negativo.");

        GarantiaMeses = novaGarantiaMeses;
    }

    public void EstenderGarantia(int mesesAdicionais)
    {
        if (mesesAdicionais <= 0)
            throw new ArgumentOutOfRangeException(nameof(mesesAdicionais), "Os meses adicionais de garantia devem ser maiores que zero.");

        GarantiaMeses += mesesAdicionais;
    }

    public void ExibirGarantia()
    {
        Console.WriteLine($"[Garantia Eletrônico] {Descricao} ({Marca}) - Garantia: {GarantiaMeses} meses.");
    }
}
