class ReservaCinema
{
    public string Filme;
    public string Sala;
    public decimal ValorIngresso;
    public int QuantidadeIngressos;
    public string NomesPessoas;


    public void ExibirDetalhesReserva()
    {
        Console.WriteLine("Filme: " + Filme);
        Console.WriteLine("Sala: " + Sala);
        Console.WriteLine("Valor do Ingresso: " + ValorIngresso);
        Console.WriteLine("Quantidade de Ingressos: " + QuantidadeIngressos);
        Console.WriteLine("Nomes das Pessoas: " + NomesPessoas);
        Console.WriteLine($"Valor Total: {CalcularValorTotal()}");
    }
    
    public decimal CalcularValorTotal()
    {
        return ValorIngresso * QuantidadeIngressos;
    }
}