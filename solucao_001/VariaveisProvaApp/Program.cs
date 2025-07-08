using System;

class Program
{
    static void Main()
    {
        Console.Write("Digite seu nome: ");
        string nome = Console.ReadLine();

        // Exibe mensagem de boas-vindas usando interpolação de strings
        Console.WriteLine($"Bem-vindo(a), {nome}!");
    }
}
