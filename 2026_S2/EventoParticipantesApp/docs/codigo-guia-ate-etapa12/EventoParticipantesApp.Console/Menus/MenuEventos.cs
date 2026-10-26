using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Servicos;

namespace EventoParticipantes.App.Menus;

internal sealed class MenuEventos {
  private readonly Gerenciador _sistema;

  public MenuEventos(Gerenciador sistema) {
    _sistema = sistema;
  }

  public void Executar() {
    Console.WriteLine("\n--- Eventos ---");
    var acao = Entrada.Texto(
        "C-Cadastrar | L-Listar | B-Buscar | A-Alterar | R-Remover")
      .Trim().ToUpperInvariant();
    switch (acao) {
      case "C": Cadastrar(); break;
      case "L": Listar(); break;
      case "B": Buscar(); break;
      case "A": Alterar(); break;
      case "R": Remover(); break;
      default: Console.WriteLine("Ação inválida."); break;
    }
  }

  private void Cadastrar() {
    var id = _sistema.CriarEvento(Entrada.Texto("Nome"),
      Entrada.Id("Id do organizador"),
      Entrada.Inteiro("Capacidade"));
    Console.WriteLine($"Evento cadastrado. Id: {id}");
  }

  private void Listar() {
    var eventos = _sistema.ListarEventos();
    if (eventos.Count == 0)
      Console.WriteLine("Nenhum evento cadastrado.");
    foreach (var x in eventos)
      Exibir(x);
  }

  // Mostra o evento e, abaixo, quem está inscrito
  private void Buscar() {
    var evento = _sistema.ObterEvento(Entrada.Id("Id"));
    Exibir(evento);
    foreach (var p in evento.Participantes)
      Console.WriteLine($"  - {p.Nome} <{p.Email}>");
  }

  private void Alterar() {
    _sistema.AlterarEvento(Entrada.Id("Id"),
      Entrada.TextoOpcional("Novo nome"),
      Entrada.InteiroOpcional("Nova capacidade"));
    Console.WriteLine("Evento alterado.");
  }

  private void Remover() {
    _sistema.RemoverEvento(Entrada.Id("Id"));
    Console.WriteLine("Evento removido.");
  }

  private static void Exibir(Evento x) =>
    Console.WriteLine($"{x.EventoId} | {x.Nome} | "
      + $"{x.Organizador.Nome} | "
      + $"{x.Participantes.Count}/{x.Capacidade} inscritos");
}
