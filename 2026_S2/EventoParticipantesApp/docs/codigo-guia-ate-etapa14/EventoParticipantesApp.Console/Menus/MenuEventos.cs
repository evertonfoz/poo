using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Servicos;

namespace EventoParticipantes.App.Menus;

internal sealed class MenuEventos : MenuCadastro {
  public MenuEventos(Gerenciador sistema) : base(sistema) { }

  protected override string Titulo => "Eventos";

  protected override void Cadastrar() {
    var id = Sistema.CriarEvento(Entrada.Texto("Nome"),
      Entrada.Id("Id do organizador"),
      Entrada.Inteiro("Capacidade"));
    Console.WriteLine($"Evento cadastrado. Id: {id}");
  }

  protected override void Listar() {
    var eventos = Sistema.ListarEventos();
    if (eventos.Count == 0)
      Console.WriteLine("Nenhum evento cadastrado.");
    foreach (var x in eventos)
      Exibir(x);
  }

  // Mostra o evento e, abaixo, quem está inscrito
  protected override void Buscar() {
    var evento = Sistema.ObterEvento(Entrada.Id("Id"));
    Exibir(evento);
    foreach (var p in evento.Participantes)
      Console.WriteLine($"  - {p.Nome} <{p.Email}>");
  }

  protected override void Alterar() {
    Sistema.AlterarEvento(Entrada.Id("Id"),
      Entrada.TextoOpcional("Novo nome"),
      Entrada.InteiroOpcional("Nova capacidade"));
    Console.WriteLine("Evento alterado.");
  }

  protected override void Remover() {
    Sistema.RemoverEvento(Entrada.Id("Id"));
    Console.WriteLine("Evento removido.");
  }

  private static void Exibir(Evento x) =>
    Console.WriteLine($"{x.EventoId} | {x.Nome} | "
      + $"{x.Organizador.Nome} | "
      + $"{x.Participantes.Count}/{x.Capacidade} inscritos");
}
