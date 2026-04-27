public record Transacao
{
    public Conta Conta { get; init; }
    public TipoTransacao Tipo { get; init; }
    public decimal Valor { get; init; }

    public Transacao(Conta conta, TipoTransacao tipo, decimal valor)
    {
        if (conta == null)
        {
            throw new ArgumentNullException(nameof(conta), "A conta não pode ser nula.");
        }
        if (tipo == null)
        {
            throw new ArgumentNullException(nameof(tipo), "O tipo de transação não pode ser nulo.");
        }
        if (valor <= 0)
        {
            throw new ArgumentException("O valor da transação deve ser maior que zero.");
        }
        Conta = conta;
        Tipo = tipo;
        Valor = valor;

        if (Tipo.Tipo == 'c')
        {
            Conta.Creditar(Valor);
        }
        else if (Tipo.Tipo == 'd')
        {
            Conta.Debitar(Valor);
        }
    }
}