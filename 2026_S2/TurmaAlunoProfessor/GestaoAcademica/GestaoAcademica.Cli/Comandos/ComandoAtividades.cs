using GestaoAcademica.Apresentacao.Argumentos;
using GestaoAcademica.Negocio.Modelo;
using GestaoAcademica.Negocio.Servicos;

namespace GestaoAcademica.Apresentacao.Comandos;

public static class ComandoAtividades
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
                    $"Operação desconhecida para atividades: {operacao}."
                );
        }
    }

    private static void Listar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(argumentos, "--turma");
        Guid turmaId = leitor.Identificador("--turma");

        IReadOnlyList<Atividade> atividades =
            servico.ListarAtividades(turmaId);

        if (atividades.Count == 0)
        {
            Console.WriteLine("Nenhuma atividade cadastrada.");
            return;
        }

        foreach (Atividade atividade in atividades)
            Console.WriteLine(Formatar(atividade));
    }

    private static void Consultar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(
            argumentos,
            "--turma",
            "--id"
        );
        Guid turmaId = leitor.Identificador("--turma");
        Guid id = leitor.Identificador("--id");

        Atividade atividade = servico.ObterAtividade(turmaId, id);
        Console.WriteLine($"Id: {atividade.Id}");
        Console.WriteLine($"Título: {atividade.Titulo}");
        Console.WriteLine($"Descrição: {atividade.Descricao}");
        Console.WriteLine($"Entrega: {atividade.DataEntrega:yyyy-MM-dd}");
    }

    private static void Cadastrar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(
            argumentos,
            "--turma",
            "--titulo",
            "--descricao",
            "--entrega"
        );
        Guid turmaId = leitor.Identificador("--turma");
        string titulo = leitor.Texto("--titulo");
        string descricao = leitor.Texto("--descricao");
        DateOnly entrega = leitor.Data("--entrega");

        Atividade atividade = servico.CadastrarAtividade(
            turmaId,
            titulo,
            descricao,
            entrega
        );
        Console.WriteLine($"Atividade cadastrada. Id: {atividade.Id}");
    }

    private static void Alterar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(
            argumentos,
            "--turma",
            "--id",
            "--titulo",
            "--descricao",
            "--entrega"
        );
        Guid turmaId = leitor.Identificador("--turma");
        Guid id = leitor.Identificador("--id");
        string titulo = leitor.Texto("--titulo");
        string descricao = leitor.Texto("--descricao");
        DateOnly entrega = leitor.Data("--entrega");

        servico.AlterarAtividade(turmaId, id, titulo, descricao, entrega);
        Console.WriteLine($"Atividade alterada. Id: {id}");
    }

    private static void Remover(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(
            argumentos,
            "--turma",
            "--id"
        );
        Guid turmaId = leitor.Identificador("--turma");
        Guid id = leitor.Identificador("--id");

        servico.RemoverAtividade(turmaId, id);
        Console.WriteLine($"Atividade removida. Id: {id}");
    }

    private static string Formatar(Atividade atividade) =>
        $"{atividade.Id} {atividade.Titulo} " +
        $"entrega={atividade.DataEntrega:yyyy-MM-dd}";
}
