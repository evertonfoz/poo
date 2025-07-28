using System;

class Program
{
    static void Main()
    {
        // Solicita o nome do usuário
        Console.Write("Digite seu nome: ");
        string nome = Console.ReadLine();

        // Exibe mensagem de boas-vindas com interpolação de strings
        Console.WriteLine($"Seja bem-vindo(a), {nome}!");
    }
}
