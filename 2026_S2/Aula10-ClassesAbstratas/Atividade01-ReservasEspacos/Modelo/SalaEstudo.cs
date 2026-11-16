namespace Atividade01_ReservasEspacos.Modelo;

/// <summary>
/// Representa uma sala de estudos individual ou em grupo.
/// Cobra pelo tempo utilizado com base em uma taxa horária simples.
/// </summary>
public class SalaEstudo : Espaco
{
    public decimal ValorHora { get; private set; }

    public SalaEstudo(string identificacao, string localizacao, int capacidade, decimal valorHora)
        : base(identificacao, localizacao, capacidade)
    {
        DefinirValorHora(valorHora);
    }

    private void DefinirValorHora(decimal valorHora)
    {
        if (valorHora <= 0)
            throw new ArgumentOutOfRangeException(nameof(valorHora), "O valor da hora para sala de estudo deve ser maior que zero.");

        ValorHora = valorHora;
    }

    /// <summary>
    /// Implementação da cobrança para Sala de Estudo: horas * ValorHora.
    /// </summary>
    public override decimal CalcularCusto(int horas)
    {
        if (horas <= 0)
            throw new ArgumentOutOfRangeException(nameof(horas), "A quantidade de horas deve ser maior que zero.");

        return horas * ValorHora;
    }
}
