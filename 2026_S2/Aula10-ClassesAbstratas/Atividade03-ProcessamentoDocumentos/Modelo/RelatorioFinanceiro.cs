namespace Atividade03_ProcessamentoDocumentos.Modelo;

/// <summary>
/// Especialização de Documento para prestação de contas financeiras.
/// Apresenta receitas, despesas e saldo apurado.
/// </summary>
public class RelatorioFinanceiro : Documento
{
    public decimal ReceitaTotal { get; private set; }
    public decimal DespesaTotal { get; private set; }

    public RelatorioFinanceiro(
        string titulo,
        string autor,
        DateTime dataCriacao,
        decimal receitaTotal,
        decimal despesaTotal)
        : base(titulo, autor, dataCriacao)
    {
        DefinirReceitaTotal(receitaTotal);
        DefinirDespesaTotal(despesaTotal);
    }

    private void DefinirReceitaTotal(decimal receitaTotal)
    {
        if (receitaTotal < 0)
            throw new ArgumentOutOfRangeException(nameof(receitaTotal), "A receita total não pode ser negativa.");

        ReceitaTotal = receitaTotal;
    }

    private void DefinirDespesaTotal(decimal despesaTotal)
    {
        if (despesaTotal < 0)
            throw new ArgumentOutOfRangeException(nameof(despesaTotal), "A despesa total não pode ser negativa.");

        DespesaTotal = despesaTotal;
    }

    public decimal ObterSaldo() => ReceitaTotal - DespesaTotal;

    /// <summary>
    /// Implementa o método abstrato Processar() calculando e exibindo o saldo financeiro.
    /// </summary>
    public override void Processar()
    {
        decimal saldo = ObterSaldo();
        Console.WriteLine($"[Processando Relatório Financeiro: \"{Titulo}\"]");
        Console.WriteLine($"  Receita: {ReceitaTotal:C2} | Despesa: {DespesaTotal:C2} | Saldo Apurado: {saldo:C2}");
    }

    /// <summary>
    /// Sobrescreve (override) o método ExibirResumo da base, reaproveitando os metadados com base.ExibirResumo()
    /// e adicionando a informação do saldo apurado.
    /// </summary>
    public override void ExibirResumo()
    {
        base.ExibirResumo();
        Console.WriteLine($"  -> Detalhe Financeiro: Saldo de {ObterSaldo():C2} (Receita: {ReceitaTotal:C2} - Despesa: {DespesaTotal:C2})");
    }
}
