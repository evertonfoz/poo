using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Servicos;

namespace EventoParticipantes.App.Menus;

internal sealed class MenuOrganizadores : MenuCadastro {
  public MenuOrganizadores(Gerenciador sistema) : base(sistema) { }

  protected override string Titulo => "Organizadores";

  protected override void Cadastrar() {
    var id = Sistema.CriarOrganizador(Entrada.Texto("Nome"),
      Entrada.Texto("E-mail"));
    Console.WriteLine($"Organizador cadastrado. Id: {id}");
  }

  protected override void Listar() {
    var organizadores = Sistema.ListarOrganizadores();
    if (organizadores.Count == 0)
      Console.WriteLine("Nenhum organizador cadastrado.");
    foreach (var x in organizadores)
      Exibir(x);
  }

  protected override void Buscar() =>
    Exibir(Sistema.ObterOrganizador(Entrada.Id("Id")));

  protected override void Alterar() {
    Sistema.AlterarOrganizador(Entrada.Id("Id"),
      Entrada.TextoOpcional("Novo nome"),
      Entrada.TextoOpcional("Novo e-mail"));
    Console.WriteLine("Organizador alterado.");
  }

  protected override void Remover() {
    Sistema.RemoverOrganizador(Entrada.Id("Id"));
    Console.WriteLine("Organizador removido.");
  }

  private static void Exibir(Organizador x) =>
    Console.WriteLine($"{x.OrganizadorId} | {x.Nome} | {x.Email}");
}
