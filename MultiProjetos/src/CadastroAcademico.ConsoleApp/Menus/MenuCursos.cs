using CadastroAcademico.Application.DTOs.Courses;
using CadastroAcademico.Application.Services;
using CadastroAcademico.ConsoleApp.IO;

namespace CadastroAcademico.ConsoleApp.Menus;

internal sealed class MenuCursos
{
    private readonly CourseService _courseService;

    public MenuCursos(CourseService courseService)
    {
        _courseService = courseService;
    }

    public void Executar()
    {
        while (true)
        {
            ConsoleIO.ExibirCabecalho("Cursos");
            Console.WriteLine("1. Cadastrar curso");
            Console.WriteLine("2. Listar cursos");
            Console.WriteLine("3. Consultar curso por Id");
            Console.WriteLine("4. Atualizar curso");
            Console.WriteLine("5. Excluir curso");
            Console.WriteLine("0. Voltar");
            Console.WriteLine();

            switch (ConsoleIO.LerInteiro("Opcao"))
            {
                case 1: CadastrarCurso(); break;
                case 2: ListarCursos(); break;
                case 3: ConsultarCurso(); break;
                case 4: AtualizarCurso(); break;
                case 5: ExcluirCurso(); break;
                case 0: return;
                default:
                    ConsoleIO.ExibirErro("Opcao invalida.");
                    ConsoleIO.PausarParaContinuar();
                    break;
            }
        }
    }

    private void CadastrarCurso()
    {
        ConsoleIO.ExibirCabecalho("Cadastrar Curso");

        var nome = ConsoleIO.LerTexto("Nome");
        var sigla = ConsoleIO.LerTexto("Sigla");
        var cargaHoraria = ConsoleIO.LerInteiro("Carga horaria (h)");

        var result = _courseService.CreateCourse(new CreateCourseInput
        {
            Nome = nome,
            Sigla = sigla,
            CargaHoraria = cargaHoraria
        });

        Console.WriteLine();
        if (result.IsSuccess) ConsoleIO.ExibirSucesso(result.Message);
        else ConsoleIO.ExibirErro(result.Message);

        ConsoleIO.PausarParaContinuar();
    }

    private void ListarCursos()
    {
        ConsoleIO.ExibirCabecalho("Cursos Cadastrados");

        var cursos = _courseService.GetAll();

        if (cursos.Count == 0)
        {
            Console.WriteLine("Nenhum curso cadastrado.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine($"{"Id",-5} {"Nome",-35} {"Sigla",-8} {"CH",5}h");
        ConsoleIO.ExibirLinha();
        foreach (var c in cursos)
            Console.WriteLine($"{c.Id,-5} {c.Nome,-35} {c.Sigla,-8} {c.CargaHoraria,5}");

        ConsoleIO.PausarParaContinuar();
    }

    private void ConsultarCurso()
    {
        ConsoleIO.ExibirCabecalho("Consultar Curso");

        var id = ConsoleIO.LerInteiro("Id do curso");
        var result = _courseService.GetById(id);

        Console.WriteLine();
        if (!result.IsSuccess || result.Value is null)
        {
            ConsoleIO.ExibirErro(result.Message);
            ConsoleIO.PausarParaContinuar();
            return;
        }

        var c = result.Value;
        ConsoleIO.ExibirLinha();
        Console.WriteLine($"Id           : {c.Id}");
        Console.WriteLine($"Nome         : {c.Nome}");
        Console.WriteLine($"Sigla        : {c.Sigla}");
        Console.WriteLine($"Carga Horaria: {c.CargaHoraria}h");
        ConsoleIO.ExibirLinha();

        ConsoleIO.PausarParaContinuar();
    }

    private void AtualizarCurso()
    {
        ConsoleIO.ExibirCabecalho("Atualizar Curso");

        var id = ConsoleIO.LerInteiro("Id do curso");
        var consulta = _courseService.GetById(id);

        if (!consulta.IsSuccess || consulta.Value is null)
        {
            ConsoleIO.ExibirErro(consulta.Message);
            ConsoleIO.PausarParaContinuar();
            return;
        }

        var atual = consulta.Value;
        Console.WriteLine();
        Console.WriteLine($"Editando: {atual.Nome} ({atual.Sigla}) — CH {atual.CargaHoraria}h");
        Console.WriteLine("Deixe em branco para manter o valor atual.");
        Console.WriteLine();

        var nome = ConsoleIO.LerTexto($"Nome [{atual.Nome}]");
        if (string.IsNullOrWhiteSpace(nome)) nome = atual.Nome;

        var sigla = ConsoleIO.LerTexto($"Sigla [{atual.Sigla}]");
        if (string.IsNullOrWhiteSpace(sigla)) sigla = atual.Sigla;

        Console.Write($"Carga horaria [{atual.CargaHoraria}] (0 para manter): ");
        var chStr = Console.ReadLine();
        var cargaHoraria = int.TryParse(chStr, out var ch) && ch > 0 ? ch : atual.CargaHoraria;

        Console.WriteLine();
        var result = _courseService.UpdateCourse(id, new UpdateCourseInput
        {
            Nome = nome,
            Sigla = sigla,
            CargaHoraria = cargaHoraria
        });

        if (result.IsSuccess) ConsoleIO.ExibirSucesso(result.Message);
        else ConsoleIO.ExibirErro(result.Message);

        ConsoleIO.PausarParaContinuar();
    }

    private void ExcluirCurso()
    {
        ConsoleIO.ExibirCabecalho("Excluir Curso");

        var id = ConsoleIO.LerInteiro("Id do curso");
        var consulta = _courseService.GetById(id);

        if (!consulta.IsSuccess || consulta.Value is null)
        {
            ConsoleIO.ExibirErro(consulta.Message);
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Curso: {consulta.Value.Nome} ({consulta.Value.Sigla})");

        if (!ConsoleIO.ConfirmarAcao("Confirmar exclusao?"))
        {
            Console.WriteLine("Operacao cancelada.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine();
        var result = _courseService.DeleteCourse(id);

        if (result.IsSuccess) ConsoleIO.ExibirSucesso(result.Message);
        else ConsoleIO.ExibirErro(result.Message);

        ConsoleIO.PausarParaContinuar();
    }
}
