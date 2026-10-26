using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Servicos;

namespace EventoParticipantes.App.Menus;

internal sealed class MenuOrganizadores {
  private readonly Gerenciador _sistema;

  public MenuOrganizadores(Gerenciador sistema) {
    _sistema = sistema;
  }

  public void Executar() {
    Console.WriteLine("\n--- Organizadores ---");
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
    var id = _sistema.CriarOrganizador(Entrada.Texto("Nome"),
      Entrada.Texto("E-mail"));
    Console.WriteLine($"Organizador cadastrado. Id: {id}");
  }

  private void Listar() {
    var organizadores = _sistema.ListarOrganizadores();
    if (organizadores.Count == 0)
      Console.WriteLine("Nenhum organizador cadastrado.");
    foreach (var x in organizadores)
      Exibir(x);
  }

  private void Buscar() =>
    Exibir(_sistema.ObterOrganizador(Entrada.Id("Id")));

  private void Alterar() {
    _sistema.AlterarOrganizador(Entrada.Id("Id"),
      Entrada.TextoOpcional("Novo nome"),
      Entrada.TextoOpcional("Novo e-mail"));
    Console.WriteLine("Organizador alterado.");
  }

  private void Remover() {
    _sistema.RemoverOrganizador(Entrada.Id("Id"));
    Console.WriteLine("Organizador removido.");
  }

  private static void Exibir(Organizador x) =>
    Console.WriteLine($"{x.OrganizadorId} | {x.Nome} | {x.Email}");
}
