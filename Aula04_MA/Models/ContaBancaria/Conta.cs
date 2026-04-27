public class Conta
{
    public decimal Saldo { get; private set; }
    public string Numero { get; private set; }

    public Conta(string numero)
    {
        if (string.IsNullOrEmpty(numero))
        {
            throw new ArgumentException("O número da conta não pode ser vazio.");
        }
        Numero = numero;
        Saldo = 0M;
    }

    public void Creditar(decimal valor)
    {
        if (valor <= 0)
        {
            throw new ArgumentException("O valor do crédito deve ser maior que zero.");
        }
        Saldo += valor;
    }

    public void Debitar(decimal valor)
    {
        if (valor <= 0)
        {
            throw new ArgumentException("O valor do débito deve ser maior que zero.");
        }
        if (valor > Saldo)
        {
            throw new InvalidOperationException("Saldo insuficiente para débito.");
        }
        Saldo -= valor;
    }

    // public void Transacao(string tipo, decimal valor)
    // {
    //     if (tipo == "credito")
    //     {
    //         Saldo += valor;
    //     }
    //     else if (tipo == "debito")
    //     {
    //         Saldo -= valor;
    //     }
    // }
}

