namespace Aula009_HerancaFuncionario.Modelo;

public class Vendedor : Funcionario
{
    public decimal PercentualComissao { get; private set; }
    public decimal TotalVendasMes { get; private set; }

    public Vendedor(
        string nome,
        string matricula,
        decimal salarioBase,
        decimal percentualComissao,
        decimal totalVendasMes)
        : base(nome, matricula, salarioBase)
    {
        AlterarPercentualComissao(percentualComissao);
        AtualizarTotalVendas(totalVendasMes);
    }

    // Métodos semânticos de negócio
    public void AlterarPercentualComissao(decimal novoPercentual)
    {
        if (novoPercentual < 0)
            throw new ArgumentOutOfRangeException(nameof(novoPercentual), "O percentual de comissão não pode ser negativo.");

        PercentualComissao = novoPercentual;
    }

    public void AtualizarTotalVendas(decimal novoTotal)
    {
        if (novoTotal < 0)
            throw new ArgumentOutOfRangeException(nameof(novoTotal), "O total de vendas não pode ser negativo.");

        TotalVendasMes = novoTotal;
    }

    public void RegistrarVenda(decimal valorVenda)
    {
        if (valorVenda <= 0)
            throw new ArgumentOutOfRangeException(nameof(valorVenda), "O valor da venda deve ser maior que zero.");

        TotalVendasMes += valorVenda;
    }

    public void ReiniciarCicloVendas()
    {
        TotalVendasMes = 0;
    }

    public decimal CalcularComissao()
    {
        return TotalVendasMes * (PercentualComissao / 100m);
    }
}
