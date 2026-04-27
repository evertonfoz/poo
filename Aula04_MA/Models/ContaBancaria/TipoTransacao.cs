public record TipoTransacao
{
    public string Nome { get; init; }
    public char Tipo { get; init; }

    public TipoTransacao(string nome, char tipo)
    {
        if (string.IsNullOrEmpty(nome))
        {
            throw new ArgumentException("O nome da transação não pode ser vazio.");
        }
        if (tipo != 'C' && tipo != 'D' && tipo != 'c' && tipo != 'd')        {
            throw new ArgumentException("O tipo da transação deve ser 'C' para crédito ou 'D' para débito.");
        }
        Nome = nome.ToLower();
        Tipo = tipo.ToString().ToLower()[0]; // Converte para maiúsculo e pega o primeiro caractere
    }
}