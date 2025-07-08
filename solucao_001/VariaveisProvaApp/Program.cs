using System;

class Program
{
    static void Main()
    {
        // Declaração das variáveis
        string nomeProduto = "Notebook";
        int quantidade = 15;
        double precoUnitario = 2850.90;
        bool ativoParaVenda = true;

        // Exibição das informações no console usando concatenação
        Console.WriteLine("Produto: " + nomeProduto);
        Console.WriteLine("Quantidade em estoque: " + quantidade);
        Console.WriteLine("Preço unitário: R$" + precoUnitario);
        Console.WriteLine("Ativo para venda: " + ativoParaVenda);
    }
}
