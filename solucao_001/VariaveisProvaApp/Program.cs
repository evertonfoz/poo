// Programa: Informação de Produto (concatenação)
using System;

class Program
{
    static void Main()
    {
        // Declaração das variáveis
        string productName = "Teclado Mecânico";
        int quantity = 25;
        decimal unitPrice = 199.90m; // sufixo 'm' para literal decimal
        bool isActive = true;

        // Exibição no console usando concatenação
        Console.WriteLine("Produto: " + productName);
        Console.WriteLine("Quantidade: " + quantity);
        Console.WriteLine("Preço unitário: R$ " + unitPrice);
        Console.WriteLine("Ativo para venda: " + isActive);
    }
}
