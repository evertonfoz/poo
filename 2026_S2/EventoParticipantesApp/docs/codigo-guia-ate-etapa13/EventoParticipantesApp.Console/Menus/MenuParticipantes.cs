using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Servicos;

namespace EventoParticipantes.App.Menus;

internal sealed class MenuParticipantes {
  private readonly Gerenciador _sistema;

  public MenuParticipantes(Gerenciador sistema) {
    _sistema = sistema;
  }

  public void Executar() {
    Console.WriteLine("\n--- Participantes ---");
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
    var id = _sistema.CriarParticipante(Entrada.Texto("Nome"),
      Entrada.Texto("E-mail"));
    Console.WriteLine($"Participante cadastrado. Id: {id}");
  }

  private void Listar() {
    var participantes = _sistema.ListarParticipantes();
    if (participantes.Count == 0)
      Console.WriteLine("Nenhum participante cadastrado.");
    foreach (var x in participantes)
      Exibir(x);
  }

  private void Buscar() =>
    Exibir(_sistema.ObterParticipante(Entrada.Id("Id")));

  private void Alterar() {
    _sistema.AlterarParticipante(Entrada.Id("Id"),
      Entrada.TextoOpcional("Novo nome"),
      Entrada.TextoOpcional("Novo e-mail"));
    Console.WriteLine("Participante alterado.");
  }

  private void Remover() {
    _sistema.RemoverParticipante(Entrada.Id("Id"));
    Console.WriteLine("Participante removido.");
  }

  private static void Exibir(Participante x) =>
    Console.WriteLine($"{x.ParticipanteId} | {x.Nome} | {x.Email}");
}
