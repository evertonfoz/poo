using EventoParticipantes.App;
using EventoParticipantes.App.Menus;
using EventoParticipantes.Negocio.Contratos;
using EventoParticipantes.Negocio.Servicos;
using EventoParticipantes.Persistencia.Json;

var arquivo = Path.Combine(AppContext.BaseDirectory,
  "dados", "eventos.json");
Console.WriteLine($"Arquivo de dados: {arquivo}");
IRepositorioSistema repositorio = new RepositorioJson(arquivo);

// O Carregar roda aqui: um arquivo ruim impede a abertura
Gerenciador sistema;
try {
  sistema = new Gerenciador(repositorio);
}
catch (Exception ex) when (ex is ArgumentException
    or InvalidOperationException
    or InvalidDataException
    or IOException
    or UnauthorizedAccessException) {
  Console.WriteLine($"Não foi possível abrir os dados: {ex.Message}");
  Console.WriteLine("Corrija ou apague o arquivo e execute de novo.");
  return;
}

var organizadores = new MenuOrganizadores(sistema);
var participantes = new MenuParticipantes(sistema);
var eventos = new MenuEventos(sistema);

while (true) {
  Console.WriteLine("\n=== EVENTOPARTICIPANTES ===");
  Console.WriteLine("1-Organizadores | 2-Participantes | 3-Eventos");
  Console.WriteLine("4-Inscrever | 5-Cancelar inscrição | 0-Sair");
  Console.Write("Opção: ");

  // null: a entrada terminou (Ctrl+Z no Windows, Ctrl+D no macOS)
  string? opcao = Console.ReadLine()?.Trim();
  if (opcao == null || opcao == "0")
    break;

  try {
    switch (opcao) {
      case "1": organizadores.Executar(); break;
      case "2": participantes.Executar(); break;
      case "3": eventos.Executar(); break;
      case "4":
        sistema.Inscrever(Entrada.Id("Id do evento"),
          Entrada.Id("Id do participante"));
        Console.WriteLine("Inscrição realizada.");
        break;
      case "5":
        sistema.CancelarInscricao(Entrada.Id("Id do evento"),
          Entrada.Id("Id do participante"));
        Console.WriteLine("Inscrição cancelada.");
        break;
      default: Console.WriteLine("Opção inválida."); break;
    }
  }
  catch (Exception ex) when (ex is ArgumentException
      or InvalidOperationException
      or KeyNotFoundException
      or IOException
      or UnauthorizedAccessException) {
    Console.WriteLine($"Operação não concluída: {ex.Message}");
  }
}
