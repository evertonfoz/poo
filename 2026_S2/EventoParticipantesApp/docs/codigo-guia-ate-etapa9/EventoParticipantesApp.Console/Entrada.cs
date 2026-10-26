namespace EventoParticipantes.App;

internal static class Entrada {
  public static string Texto(string rotulo) {
    Console.Write($"{rotulo}: ");
    return Console.ReadLine() ?? "";
  }

  // Enter em branco devolve null: o Gerenciador entende "não alterar"
  public static string? TextoOpcional(string rotulo) {
    var valor = Texto($"{rotulo} (Enter mantém)");
    return string.IsNullOrWhiteSpace(valor) ? null : valor;
  }

  public static Guid Id(string rotulo) {
    if (!Guid.TryParse(Texto(rotulo), out var id))
      throw new ArgumentException("Identificador inválido.");
    return id;
  }
}
