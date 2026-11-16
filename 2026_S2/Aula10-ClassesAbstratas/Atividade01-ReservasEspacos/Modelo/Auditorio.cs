namespace Atividade01_ReservasEspacos.Modelo;

/// <summary>
/// Representa um auditório para palestras, eventos ou conferências.
/// Cobra uma taxa horária acrescida de uma taxa fixa de preparação do ambiente.
/// </summary>
public class Auditorio : Espaco
{
    public decimal ValorHora { get; private set; }
    public decimal TaxaPreparacao { get; private set; }

    public Auditorio(
        string identificacao,
        string localizacao,
        int capacidade,
        decimal valorHora,
        decimal taxaPreparacao)
        : base(identificacao, localizacao, capacidade)
    {
        DefinirValorHora(valorHora);
        DefinirTaxaPreparacao(taxaPreparacao);
    }

    private void DefinirValorHora(decimal valorHora)
    {
        if (valorHora <= 0)
            throw new ArgumentOutOfRangeException(nameof(valorHora), "O valor da hora para o auditório deve ser maior que zero.");

        ValorHora = valorHora;
    }

    private void DefinirTaxaPreparacao(decimal taxaPreparacao)
    {
        if (taxaPreparacao < 0)
            throw new ArgumentOutOfRangeException(nameof(taxaPreparacao), "A taxa de preparação do auditório não pode ser negativa.");

        TaxaPreparacao = taxaPreparacao;
    }

    /// <summary>
    /// Implementação da cobrança para Auditório: (horas * ValorHora) + TaxaPreparacao.
    /// </summary>
    public override decimal CalcularCusto(int horas)
    {
        if (horas <= 0)
            throw new ArgumentOutOfRangeException(nameof(horas), "A quantidade de horas deve ser maior que zero.");

        return (horas * ValorHora) + TaxaPreparacao;
    }
}
