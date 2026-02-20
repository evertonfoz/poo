internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Por favor, digite seu primeiro nome:");
        string primeiroNome = Console.ReadLine();

        Console.WriteLine("Agora, digite sua idade:");
        // Para este exercício, vamos assumir que o usuário digita um número válido.
        int idade = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Por fim, digite sua altura em metros (ex: 1.80):");
        // E que também digita um float válido.
        float altura = Convert.ToSingle(Console.ReadLine());

        Console.WriteLine($"Resumo do Cadastro: Nome: {primeiroNome}, Idade: {idade} anos, Altura: {altura}m.");
    }
}