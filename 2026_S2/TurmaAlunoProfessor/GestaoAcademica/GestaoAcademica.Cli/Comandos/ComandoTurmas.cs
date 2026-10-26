using GestaoAcademica.Apresentacao.Argumentos;
using GestaoAcademica.Negocio.Modelo;
using GestaoAcademica.Negocio.Servicos;

namespace GestaoAcademica.Apresentacao.Comandos;

public static class ComandoTurmas
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
                    $"Operação desconhecida para turmas: {operacao}."
                );
        }
    }

    private static void Listar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        _ = new LeitorArgumentos(argumentos);

        IReadOnlyList<Turma> turmas = servico.ListarTurmas();

        if (turmas.Count == 0)
        {
            Console.WriteLine("Nenhuma turma cadastrada.");
            return;
        }

        foreach (Turma turma in turmas)
            Console.WriteLine(Formatar(turma));
    }

    private static void Consultar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(argumentos, "--id");
        Guid id = leitor.Identificador("--id");

        Turma turma = servico.ObterTurma(id);
        Console.WriteLine($"Id: {turma.Id}");
        Console.WriteLine($"Nome: {turma.Nome}");
        Console.WriteLine(
            $"Professor: {turma.Professor.Nome} ({turma.Professor.Id})"
        );

        Console.WriteLine($"Alunos matriculados: {turma.Alunos.Count}");
        foreach (Aluno aluno in turma.Alunos)
            Console.WriteLine($"  - {aluno}");

        Console.WriteLine($"Atividades: {turma.Atividades.Count}");
        foreach (Atividade atividade in turma.Atividades)
            Console.WriteLine($"  - {atividade.Id} {atividade}");
    }

    private static void Cadastrar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(
            argumentos,
            "--nome",
            "--professor"
        );
        string nome = leitor.Texto("--nome");
        Guid professorId = leitor.Identificador("--professor");

        Turma turma = servico.CadastrarTurma(nome, professorId);
        Console.WriteLine($"Turma cadastrada. Id: {turma.Id}");
    }

    private static void Alterar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(
            argumentos,
            "--id",
            "--nome",
            "--professor"
        );
        Guid id = leitor.Identificador("--id");
        string nome = leitor.Texto("--nome");
        Guid professorId = leitor.Identificador("--professor");

        servico.AlterarTurma(id, nome, professorId);
        Console.WriteLine($"Turma alterada. Id: {id}");
    }

    private static void Remover(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(argumentos, "--id");
        Guid id = leitor.Identificador("--id");

        servico.RemoverTurma(id);
        Console.WriteLine(
            $"Turma removida. Id: {id}. Atividades excluídas; " +
            "professor e alunos preservados."
        );
    }

    private static string Formatar(Turma turma) =>
        $"{turma.Id} {turma.Nome} Prof. {turma.Professor.Nome} " +
        $"alunos={turma.Alunos.Count} atividades={turma.Atividades.Count}";
}
