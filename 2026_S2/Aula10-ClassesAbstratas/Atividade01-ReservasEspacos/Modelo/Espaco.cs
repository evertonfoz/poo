namespace Atividade01_ReservasEspacos.Modelo;

/// <summary>
/// Representa um espaço genérico reservável dentro da instituição.
/// Declarada como classe abstrata pois não deve ser instanciada diretamente,
/// servindo de molde (contrato) para categorias específicas de espaços.
/// </summary>
public abstract class Espaco
{
    public string Identificacao { get; private set; } = string.Empty;
    public string Localizacao { get; private set; } = string.Empty;
    public int Capacidade { get; private set; }

    protected Espaco(string identificacao, string localizacao, int capacidade)
    {
        DefinirIdentificacao(identificacao);
        DefinirLocalizacao(localizacao);
        DefinirCapacidade(capacidade);
    }

    private void DefinirIdentificacao(string identificacao)
    {
        if (string.IsNullOrWhiteSpace(identificacao))
            throw new ArgumentException("A identificação do espaço é obrigatória e não pode ser vazia.", nameof(identificacao));

        Identificacao = identificacao.Trim();
    }

    private void DefinirLocalizacao(string localizacao)
    {
        if (string.IsNullOrWhiteSpace(localizacao))
            throw new ArgumentException("A localização do espaço é obrigatória e não pode ser vazia.", nameof(localizacao));

        Localizacao = localizacao.Trim();
    }

    private void DefinirCapacidade(int capacidade)
    {
        if (capacidade <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacidade), "A capacidade do espaço deve ser maior que zero.");

        Capacidade = capacidade;
    }

    /// <summary>
    /// Método concreto herdado por todas as subclasses para exibir os dados gerais comuns.
    /// </summary>
    public void ExibirInformacoes()
    {
        Console.WriteLine($"[Espaço] Tipo: {GetType().Name} | Identificação: {Identificacao} | Localização: {Localizacao} | Capacidade: {Capacidade} pessoas");
    }

    /// <summary>
    /// Método público abstrato que define o contrato de cálculo de custo.
    /// Cada classe concreta derivada é obrigada a fornecer sua própria implementação (override).
    /// </summary>
    /// <param name="horas">Quantidade de horas de utilização.</param>
    /// <returns>Valor total da reserva em formato decimal.</returns>
    public abstract decimal CalcularCusto(int horas);
}
