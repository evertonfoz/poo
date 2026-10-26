using GestaoAcademica.Apresentacao.Argumentos;
using GestaoAcademica.Negocio.Modelo;
using GestaoAcademica.Negocio.Servicos;

namespace GestaoAcademica.Apresentacao.Comandos;

public static class ComandoAlunos
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
                    $"Operação desconhecida para alunos: {operacao}."
                );
        }
    }

    private static void Listar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        _ = new LeitorArgumentos(argumentos);

        IReadOnlyList<Aluno> alunos = servico.ListarAlunos();

        if (alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            return;
        }

        foreach (Aluno aluno in alunos)
            Console.WriteLine(Formatar(aluno));
    }

    private static void Consultar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(argumentos, "--id");
        Guid id = leitor.Identificador("--id");

        Aluno aluno = servico.ObterAluno(id);
        Console.WriteLine($"Id: {aluno.Id}");
        Console.WriteLine($"Nome: {aluno.Nome}");
        Console.WriteLine($"E-mail: {aluno.Email}");
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

        Aluno aluno = servico.CadastrarAluno(nome, email);
        Console.WriteLine($"Aluno cadastrado. Id: {aluno.Id}");
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

        servico.AlterarAluno(id, nome, email);
        Console.WriteLine($"Aluno alterado. Id: {id}");
    }

    private static void Remover(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(argumentos, "--id");
        Guid id = leitor.Identificador("--id");

        servico.RemoverAluno(id);
        Console.WriteLine($"Aluno removido. Id: {id}");
    }

    private static string Formatar(Aluno aluno) =>
        $"{aluno.Id} {aluno.Nome} <{aluno.Email}>";
}
