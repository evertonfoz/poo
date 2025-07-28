using System;

class Program
{
    static void Main()
    {
        double a = 0.1;
        double b = 0.2;
        double c = 0.3;

        double soma = a + b;

        Console.WriteLine($"0.1 + 0.2 = {soma}");
        Console.WriteLine($"0.3       = {c}");
        Console.WriteLine($"Soma é igual a 0.3? {soma == c}");
    }
}
