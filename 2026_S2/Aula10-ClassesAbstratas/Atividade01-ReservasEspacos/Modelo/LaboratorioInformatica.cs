namespace Atividade01_ReservasEspacos.Modelo;

/// <summary>
/// Representa um laboratório de informática.
/// Cobra com base na quantidade de computadores e no valor/hora de cada máquina em uso.
/// </summary>
public class LaboratorioInformatica : Espaco
{
    public int QuantidadeComputadores { get; private set; }
    public decimal ValorHoraPorComputador { get; private set; }

    public LaboratorioInformatica(
        string identificacao,
        string localizacao,
        int capacidade,
        int quantidadeComputadores,
        decimal valorHoraPorComputador)
        : base(identificacao, localizacao, capacidade)
    {
        DefinirQuantidadeComputadores(quantidadeComputadores);
        DefinirValorHoraPorComputador(valorHoraPorComputador);
    }

    private void DefinirQuantidadeComputadores(int quantidadeComputadores)
    {
        if (quantidadeComputadores <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantidadeComputadores), "A quantidade de computadores deve ser maior que zero.");

        if (quantidadeComputadores > Capacidade)
            throw new ArgumentOutOfRangeException(nameof(quantidadeComputadores), $"A quantidade de computadores ({quantidadeComputadores}) não pode exceder a capacidade total do espaço ({Capacidade}).");

        QuantidadeComputadores = quantidadeComputadores;
    }

    private void DefinirValorHoraPorComputador(decimal valorHoraPorComputador)
    {
        if (valorHoraPorComputador <= 0)
            throw new ArgumentOutOfRangeException(nameof(valorHoraPorComputador), "O valor da hora por computador deve ser maior que zero.");

        ValorHoraPorComputador = valorHoraPorComputador;
    }

    /// <summary>
    /// Implementação da cobrança para Laboratório de Informática: horas * QuantidadeComputadores * ValorHoraPorComputador.
    /// Considera todos os computadores disponíveis em uso.
    /// </summary>
    public override decimal CalcularCusto(int horas)
    {
        if (horas <= 0)
            throw new ArgumentOutOfRangeException(nameof(horas), "A quantidade de horas deve ser maior que zero.");

        return horas * QuantidadeComputadores * ValorHoraPorComputador;
    }
}
