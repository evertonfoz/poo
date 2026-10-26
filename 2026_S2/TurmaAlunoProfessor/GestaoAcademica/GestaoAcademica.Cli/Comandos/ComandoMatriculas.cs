using GestaoAcademica.Apresentacao.Argumentos;
using GestaoAcademica.Negocio.Modelo;
using GestaoAcademica.Negocio.Servicos;

namespace GestaoAcademica.Apresentacao.Comandos;

public static class ComandoMatriculas
{
    public static void Executar(
        ServicoAcademico servico,
        string operacao,
        IReadOnlyList<string> argumentos)
    {
        switch (operacao)
        {
            case "adicionar":
                Adicionar(servico, argumentos);
                break;
            case "remover":
                Remover(servico, argumentos);
                break;
            case "listar":
                Listar(servico, argumentos);
                break;
            default:
                throw new UsoInvalidoException(
                    $"Operação desconhecida para matriculas: {operacao}."
                );
        }
    }

    private static void Adicionar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(
            argumentos,
            "--turma",
            "--aluno"
        );
        Guid turmaId = leitor.Identificador("--turma");
        Guid alunoId = leitor.Identificador("--aluno");

        servico.Matricular(turmaId, alunoId);
        Console.WriteLine("Aluno matriculado na turma.");
    }

    private static void Remover(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(
            argumentos,
            "--turma",
            "--aluno"
        );
        Guid turmaId = leitor.Identificador("--turma");
        Guid alunoId = leitor.Identificador("--aluno");

        servico.CancelarMatricula(turmaId, alunoId);
        Console.WriteLine(
            "Matrícula cancelada. O cadastro do aluno foi preservado."
        );
    }

    private static void Listar(
        ServicoAcademico servico,
        IReadOnlyList<string> argumentos)
    {
        var leitor = new LeitorArgumentos(argumentos, "--turma");
        Guid turmaId = leitor.Identificador("--turma");

        Turma turma = servico.ObterTurma(turmaId);

        if (turma.Alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno matriculado.");
            return;
        }

        foreach (Aluno aluno in turma.Alunos)
            Console.WriteLine($"{aluno.Id} {aluno.Nome} <{aluno.Email}>");
    }
}
