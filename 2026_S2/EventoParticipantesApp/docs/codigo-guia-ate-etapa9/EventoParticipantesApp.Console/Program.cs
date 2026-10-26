using EventoParticipantes.App.Menus;
using EventoParticipantes.Negocio.Contratos;
using EventoParticipantes.Negocio.Servicos;
using EventoParticipantes.Persistencia.Memoria;

IRepositorioSistema repositorio = new RepositorioMemoria();
var sistema = new Gerenciador(repositorio);
var organizadores = new MenuOrganizadores(sistema);

while (true) {
  Console.WriteLine("\n=== EVENTOPARTICIPANTES ===");
  Console.WriteLine("1-Organizadores | 0-Sair");
  Console.Write("Opção: ");

  // null: a entrada terminou (Ctrl+Z no Windows, Ctrl+D no macOS)
  string? opcao = Console.ReadLine()?.Trim();
  if (opcao == null || opcao == "0")
    break;

  try {
    switch (opcao) {
      case "1": organizadores.Executar(); break;
      default: Console.WriteLine("Opção inválida."); break;
    }
  }
  catch (Exception ex) when (ex is ArgumentException
      or InvalidOperationException
      or KeyNotFoundException) {
    Console.WriteLine($"Operação não concluída: {ex.Message}");
  }
}
