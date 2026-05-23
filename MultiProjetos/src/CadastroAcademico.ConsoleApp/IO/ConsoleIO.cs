namespace CadastroAcademico.ConsoleApp.IO;

internal static class ConsoleIO
{
    private const int LarguraLinha = 52;

    public static void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine(new string('=', LarguraLinha));
        Console.WriteLine($"  {titulo}");
        Console.WriteLine(new string('=', LarguraLinha));
        Console.WriteLine();
    }

    public static void ExibirSucesso(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[OK] {mensagem}");
        Console.ResetColor();
    }

    public static void ExibirErro(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERRO] {mensagem}");
        Console.ResetColor();
    }

    public static void ExibirLinha() =>
        Console.WriteLine(new string('-', LarguraLinha));

    public static void PausarParaContinuar()
    {
        Console.WriteLine();
        Console.Write("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    public static string LerTexto(string prompt)
    {
        Console.Write($"{prompt}: ");
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public static int LerInteiro(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            if (int.TryParse(Console.ReadLine(), out var valor))
                return valor;
            ExibirErro("Valor invalido. Digite um numero inteiro.");
        }
    }

    public static bool ConfirmarAcao(string mensagem)
    {
        Console.Write($"{mensagem} (s/N): ");
        var resposta = Console.ReadLine()?.Trim().ToLowerInvariant();
        return resposta is "s" or "sim";
    }
}
