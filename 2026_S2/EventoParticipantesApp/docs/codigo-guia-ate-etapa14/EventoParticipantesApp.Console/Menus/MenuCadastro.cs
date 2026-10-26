using EventoParticipantes.Negocio.Servicos;

namespace EventoParticipantes.App.Menus;

// O roteiro comum aos três menus; cada filho preenche as ações
internal abstract class MenuCadastro {
  protected Gerenciador Sistema { get; }

  protected MenuCadastro(Gerenciador sistema) {
    Sistema = sistema;
  }

  protected abstract string Titulo { get; }

  public void Executar() {
    Console.WriteLine($"\n--- {Titulo} ---");
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

  protected abstract void Cadastrar();
  protected abstract void Listar();
  protected abstract void Buscar();
  protected abstract void Alterar();
  protected abstract void Remover();
}
