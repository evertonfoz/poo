using GestaoAcademica.Apresentacao.Argumentos;
using GestaoAcademica.Negocio.Modelo;
using GestaoAcademica.Negocio.Servicos;

namespace GestaoAcademica.Apresentacao.Comandos;

public static class ComandoProfessores
{
    public static void Executar(
        ServicoAcademico servico,
        string operacao,
        IReadOnlyList<string> argumentos)
    {
        switch (operacao)
        {
            case "listar":
                Listar(servico, argumentos);
                break;
            case "consultar":
                Consultar(servico, argumentos);
                break;
            case "cadastrar":
                Cadastrar(servico, argumentos);
                break;
            case "alterar":
                Alterar(servico, argumentos);
                break;
            case "remover":
                Remover(servico, argumentos);
                break;
            default:
                throw new UsoInvalidoException(
                    $"Operação desconhecida para professores: {operacao}."
                );
        }
    }

    private static void Listar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        _ = new LeitorArgumentos(argumentos);

        IReadOnlyList<Professor> professores =
            servico.ListarProfessores();

        if (professores.Count == 0)
        {
            Console.WriteLine("Nenhum professor cadastrado.");
            return;
        }

        foreach (Professor professor in professores)
            Console.WriteLine(Formatar(professor));
    }

    private static void Consultar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(argumentos, "--id");
        Guid id = leitor.Identificador("--id");

        Professor professor = servico.ObterProfessor(id);
        Console.WriteLine($"Id: {professor.Id}");
        Console.WriteLine($"Nome: {professor.Nome}");
        Console.WriteLine($"E-mail: {professor.Email}");
    }

    private static void Cadastrar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(
            argumentos,
            "--nome",
            "--email"
        );
        string nome = leitor.Texto("--nome");
        string email = leitor.Texto("--email");

        Professor professor = servico.CadastrarProfessor(nome, email);
        Console.WriteLine($"Professor cadastrado. Id: {professor.Id}");
    }

    private static void Alterar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(
            argumentos,
            "--id",
            "--nome",
            "--email"
        );
        Guid id = leitor.Identificador("--id");
        string nome = leitor.Texto("--nome");
        string email = leitor.Texto("--email");

        servico.AlterarProfessor(id, nome, email);
        Console.WriteLine($"Professor alterado. Id: {id}");
    }

    private static void Remover(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(argumentos, "--id");
        Guid id = leitor.Identificador("--id");

        servico.RemoverProfessor(id);
        Console.WriteLine($"Professor removido. Id: {id}");
    }

    private static string Formatar(Professor professor) =>
        $"{professor.Id} {professor.Nome} <{professor.Email}>";
}
