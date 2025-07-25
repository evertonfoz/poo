using System;

class Program
{
    static void Main()
    {
        // Declaração das variáveis
        string primeiroNome = "Maria";
        string sobrenome = "Silva";

        // Exibição com concatenação
        Console.WriteLine("Usando concatenação:");
        Console.WriteLine(primeiroNome + " " + sobrenome);

        // Exibição com interpolação
        Console.WriteLine("\nUsando interpolação:");
        Console.WriteLine($"{primeiroNome} {sobrenome}");
    }
}
