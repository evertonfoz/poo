namespace EventoParticipantes.App;

// Leitura e conversão do que o usuário digita no console.
internal static class Entrada
{
    public static string Texto(string rotulo)
    {
        Console.Write($"{rotulo}: ");
        return Console.ReadLine() ?? "";
    }

    // Enter em branco devolve null, que o Gerenciador entende como "não alterar".
    public static string? TextoOpcional(string rotulo)
    {
        var valor = Texto($"{rotulo} (Enter mantém)");
        return string.IsNullOrWhiteSpace(valor) ? null : valor;
    }

    public static Guid Id(string rotulo)
    {
        if (!Guid.TryParse(Texto(rotulo), out var id) || id == Guid.Empty)
            throw new ArgumentException("Identificador inválido.");
        return id;
    }

    public static int Inteiro(string rotulo)
    {
        if (!int.TryParse(Texto(rotulo), out var valor))
            throw new ArgumentException("Informe um número inteiro.");
        return valor;
    }
}
