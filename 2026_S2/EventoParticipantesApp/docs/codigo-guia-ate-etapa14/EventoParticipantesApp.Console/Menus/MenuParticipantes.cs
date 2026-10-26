using EventoParticipantes.Negocio.Modelo;
using EventoParticipantes.Negocio.Servicos;

namespace EventoParticipantes.App.Menus;

internal sealed class MenuParticipantes : MenuCadastro {
  public MenuParticipantes(Gerenciador sistema) : base(sistema) { }

  protected override string Titulo => "Participantes";

  protected override void Cadastrar() {
    var id = Sistema.CriarParticipante(Entrada.Texto("Nome"),
      Entrada.Texto("E-mail"));
    Console.WriteLine($"Participante cadastrado. Id: {id}");
  }

  protected override void Listar() {
    var participantes = Sistema.ListarParticipantes();
    if (participantes.Count == 0)
      Console.WriteLine("Nenhum participante cadastrado.");
    foreach (var x in participantes)
      Exibir(x);
  }

  protected override void Buscar() =>
    Exibir(Sistema.ObterParticipante(Entrada.Id("Id")));

  protected override void Alterar() {
    Sistema.AlterarParticipante(Entrada.Id("Id"),
      Entrada.TextoOpcional("Novo nome"),
      Entrada.TextoOpcional("Novo e-mail"));
    Console.WriteLine("Participante alterado.");
  }

  protected override void Remover() {
    Sistema.RemoverParticipante(Entrada.Id("Id"));
    Console.WriteLine("Participante removido.");
  }

  private static void Exibir(Participante x) =>
    Console.WriteLine($"{x.ParticipanteId} | {x.Nome} | {x.Email}");
}
