using System;

class Program
{
    static void Main()
    {
        // Declaração e inicialização das variáveis
        string nomeCompleto = "Ana Carolina Silva";
        int idade = 28;
        char inicialDoNome = 'A';
        double salario = 4500.75;

        // Exibição das informações no console
        Console.WriteLine("Informações da Pessoa:");
        Console.WriteLine($"Nome completo: {nomeCompleto}");
        Console.WriteLine($"Idade: {idade}");
        Console.WriteLine($"Letra inicial do nome: {inicialDoNome}");
        Console.WriteLine($"Salário: R$ {salario:F2}");
    }
}
