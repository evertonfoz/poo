using System;

class Program
{
    static void Main()
    {
        // Declaração e inicialização das variáveis
        double precoProduto = 49.90;
        int quantidadeComprada = 3;

        // Cálculo do valor total
        double valorTotal = precoProduto * quantidadeComprada;

        // Exibição do resultado no console
        Console.WriteLine("Valor total da compra: R$" + valorTotal);
    }
}
