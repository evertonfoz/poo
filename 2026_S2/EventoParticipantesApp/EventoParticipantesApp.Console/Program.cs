using EventoParticipantes.App;
using EventoParticipantes.App.Menus;
using EventoParticipantes.Negocio.Servico;
using EventoParticipantes.Persistencia.Json;

var arquivo = Path.Combine(AppContext.BaseDirectory, "dados", "eventos.json");

Gerenciador sistema;
try
{
    sistema = new Gerenciador(new RepositorioJson(arquivo));
}
catch (Exception ex)
    when (ex is InvalidDataException or InvalidOperationException or IOException)
{
    Console.WriteLine($"Não foi possível carregar '{arquivo}': {ex.Message}");
    return 1;
}

var organizadores = new MenuOrganizadores(sistema);
var participantes = new MenuParticipantes(sistema);
var eventos = new MenuEventos(sistema);

while (true)
{
    Console.WriteLine("\n=== EVENTOPARTICIPANTES ===");
    Console.WriteLine("1-Organizadores | 2-Participantes");
    Console.WriteLine("3-Eventos | 4-Inscrever");
    Console.WriteLine("5-Cancelar inscrição | 0-Sair");
    Console.Write("Opção: ");

    // null = fim da entrada (Ctrl+D / Ctrl+Z): encerra em vez de repetir o menu.
    var opcao = Console.ReadLine()?.Trim();
    if (opcao is null or "0")
        return 0;

    try
    {
        switch (opcao)
        {
            case "1": organizadores.Executar(); break;
            case "2": participantes.Executar(); break;
            case "3": eventos.Executar(); break;
            case "4":
                sistema.Inscrever(Entrada.Id("Id do evento"), Entrada.Id("Id do participante"));
                Console.WriteLine("Inscrição realizada.");
                break;
            case "5":
                sistema.CancelarInscricao(Entrada.Id("Id do evento"), Entrada.Id("Id do participante"));
                Console.WriteLine("Inscrição cancelada.");
                break;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }
    catch (Exception ex)
        when (ex is ArgumentException
                 or InvalidOperationException
                 or KeyNotFoundException
                 or IOException)
    {
        Console.WriteLine($"Operação não concluída: {ex.Message}");
    }
}
