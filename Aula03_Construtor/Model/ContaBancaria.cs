public class ContaBancaria
{
    public string NumeroConta { get; init; }
    // public string NumeroConta { get; private set; }
    public decimal Saldo { get; private set; }

    public ContaBancaria(String numeroConta)
    {
        NumeroConta = numeroConta;
        Saldo = 0.00m;
    }

    public void Depositar(decimal valor)
    {
        if (valor <= 0)
        {
           throw new ArgumentException("Valor de depósito deve ser positivo.");
        }

        Saldo += valor;
    }
}