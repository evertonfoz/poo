using GestaoAcademica.Apresentacao.Argumentos;

namespace GestaoAcademica.Apresentacao;

public static class Ajuda
{
    public const string Versao = "1.0.0";

    // Retorna true quando os argumentos eram de ajuda ou de versão.
    public static bool TentarExecutar(string[] args)
    {
        if (args.Length == 0)
            return false;

        switch (args[0])
        {
            case "--help":
            case "help":
                if (args.Length > 2)
                    throw new UsoInvalidoException("Uso: help [recurso].");

                Exibir(args.Length == 2 ? args[1] : null);
                return true;

            case "--version":
                if (args.Length > 1)
                {
                    throw new UsoInvalidoException(
                        "A opção --version não aceita argumentos."
                    );
                }

                Console.WriteLine($"gestao {Versao}");
                return true;

            default:
                return false;
        }
    }

    private static void Exibir(string? recurso)
    {
        string[] linhas = recurso switch
        {
            null => Geral,
            "professores" => Professores,
            "alunos" => Alunos,
            "turmas" => Turmas,
            "matriculas" => Matriculas,
            "atividades" => Atividades,
            _ => throw new UsoInvalidoException(
                $"Recurso desconhecido: {recurso}."
            )
        };

        foreach (string linha in linhas)
            Console.WriteLine(linha);
    }

    private static readonly string[] Geral =
    {
        $"GestaoAcademica {Versao} - professores, alunos, turmas,",
        "matrículas e atividades.",
        "",
        "Sintaxe:",
        "  gestao <recurso> <operacao> [--opcao valor ...]",
        "  gestao help [recurso]",
        "  gestao --help | --version",
        "",
        "Recursos: professores, alunos, turmas, matriculas, atividades",
        "Operações: listar, consultar, cadastrar, alterar, remover",
        "           (matriculas: adicionar, remover, listar)",
        "",
        "Datas: yyyy-MM-dd (exemplo: 2026-11-10).",
        "Identificadores: Guid exibido no cadastro e nas listagens.",
        "Valores com espaços devem estar entre aspas.",
        "Arquivo de dados: variável GESTAO_ARQUIVO ou pasta padrão.",
        "",
        "Exemplos:",
        "  gestao professores cadastrar --nome \"Ana Souza\"",
        "    --email ana@uni.edu",
        "  gestao turmas listar",
        "",
        "Detalhes de um recurso: gestao help <recurso>",
        "Em desenvolvimento: dotnet run --project GestaoAcademica.Cli",
        "  -- <recurso> <operacao> [opcoes]"
    };

    private static readonly string[] Professores =
    {
        "professores",
        "  listar",
        "  consultar --id <guid>",
        "  cadastrar --nome <texto> --email <texto>",
        "  alterar   --id <guid> --nome <texto> --email <texto>",
        "  remover   --id <guid>",
        "Exemplo: gestao professores consultar --id <guid>"
    };

    private static readonly string[] Alunos =
    {
        "alunos",
        "  listar",
        "  consultar --id <guid>",
        "  cadastrar --nome <texto> --email <texto>",
        "  alterar   --id <guid> --nome <texto> --email <texto>",
        "  remover   --id <guid>",
        "Exemplo: gestao alunos listar"
    };

    private static readonly string[] Turmas =
    {
        "turmas",
        "  listar",
        "  consultar --id <guid>",
        "  cadastrar --nome <texto> --professor <guid>",
        "  alterar   --id <guid> --nome <texto> --professor <guid>",
        "  remover   --id <guid>  (exclui também as atividades)",
        "Exemplo: gestao turmas cadastrar --nome POO-A --professor <guid>"
    };

    private static readonly string[] Matriculas =
    {
        "matriculas",
        "  adicionar --turma <guid> --aluno <guid>",
        "  remover   --turma <guid> --aluno <guid>",
        "  listar    --turma <guid>",
        "Exemplo: gestao matriculas listar --turma <guid>"
    };

    private static readonly string[] Atividades =
    {
        "atividades (datas em yyyy-MM-dd)",
        "  listar    --turma <guid>",
        "  consultar --turma <guid> --id <guid>",
        "  cadastrar --turma <guid> --titulo <texto>",
        "            --descricao <texto> --entrega <data>",
        "  alterar   --turma <guid> --id <guid> --titulo <texto>",
        "            --descricao <texto> --entrega <data>",
        "  remover   --turma <guid> --id <guid>",
        "Exemplo: gestao atividades listar --turma <guid>"
    };
}
