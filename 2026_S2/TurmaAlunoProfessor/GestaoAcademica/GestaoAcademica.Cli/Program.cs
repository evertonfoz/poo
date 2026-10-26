using GestaoAcademica.Apresentacao;
using GestaoAcademica.Apresentacao.Argumentos;
using GestaoAcademica.Apresentacao.Comandos;
using GestaoAcademica.Negocio.Contratos;
using GestaoAcademica.Negocio.Servicos;
using GestaoAcademica.Negocio.Validacao;
using GestaoAcademica.Persistencia.Json;

try
{
    if (Ajuda.TentarExecutar(args))
        return CodigosSaida.Sucesso;

    if (args.Length < 2)
    {
        throw new UsoInvalidoException(
            "Informe o recurso e a operação."
        );
    }

    string recurso = args[0];
    string operacao = args[1];
    string[] opcoes = args[2..];

    string[] recursos =
    {
        "professores", "alunos", "turmas", "matriculas", "atividades"
    };
    if (!recursos.Contains(recurso))
        throw new UsoInvalidoException($"Recurso desconhecido: {recurso}.");

    var repositorio = new RepositorioJson(ResolverCaminhoArquivo());
    var servico = new ServicoAcademico(repositorio);

    switch (recurso)
    {
        case "professores":
            ComandoProfessores.Executar(servico, operacao, opcoes);
            break;
        case "alunos":
            ComandoAlunos.Executar(servico, operacao, opcoes);
            break;
        case "turmas":
            ComandoTurmas.Executar(servico, operacao, opcoes);
            break;
        case "matriculas":
            ComandoMatriculas.Executar(servico, operacao, opcoes);
            break;
        case "atividades":
            ComandoAtividades.Executar(servico, operacao, opcoes);
            break;
        default:
            throw new UsoInvalidoException(
                $"Recurso ainda não implementado: {recurso}."
            );
    }

    return CodigosSaida.Sucesso;
}
catch (UsoInvalidoException ex)
{
    Console.Error.WriteLine($"Erro de uso: {ex.Message}");
    Console.Error.WriteLine("Consulte a ajuda: gestao --help");
    return CodigosSaida.UsoInvalido;
}
catch (RegraDeNegocioException ex)
{
    Console.Error.WriteLine($"Operação recusada: {ex.Message}");
    return CodigosSaida.RegraDeNegocio;
}
catch (PersistenciaException ex)
{
    Console.Error.WriteLine($"Falha de persistência: {ex.Message}");
    return CodigosSaida.Persistencia;
}

static string ResolverCaminhoArquivo()
{
    string? definido =
        Environment.GetEnvironmentVariable("GESTAO_ARQUIVO");

    if (!string.IsNullOrWhiteSpace(definido))
        return definido;

    string pasta = Path.Combine(
        Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData
        ),
        "GestaoAcademica"
    );

    return Path.Combine(pasta, "gestao-academica.json");
}
