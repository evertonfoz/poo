using CadastroAcademico.Application.Services;
using CadastroAcademico.ConsoleApp.IO;

namespace CadastroAcademico.ConsoleApp.Menus;

internal sealed class MenuRelatorios
{
    private readonly CourseService _courseService;
    private readonly StudentService _studentService;
    private readonly EnrollmentService _enrollmentService;

    public MenuRelatorios(
        CourseService courseService,
        StudentService studentService,
        EnrollmentService enrollmentService)
    {
        _courseService = courseService;
        _studentService = studentService;
        _enrollmentService = enrollmentService;
    }

    public void Executar()
    {
        while (true)
        {
            ConsoleIO.ExibirCabecalho("Relatorios e Consultas");
            Console.WriteLine("1. Alunos por curso");
            Console.WriteLine("2. Cursos de um aluno");
            Console.WriteLine("3. Total de alunos por curso");
            Console.WriteLine("4. Matriculas ativas");
            Console.WriteLine("5. Matriculas canceladas");
            Console.WriteLine("6. Matriculas concluidas");
            Console.WriteLine("0. Voltar");
            Console.WriteLine();

            switch (ConsoleIO.LerInteiro("Opcao"))
            {
                case 1: AlunosPorCurso(); break;
                case 2: CursosDeUmAluno(); break;
                case 3: TotalDeAlunosPorCurso(); break;
                case 4: MatriculasPorStatus("Ativa", "Matriculas Ativas"); break;
                case 5: MatriculasPorStatus("Cancelada", "Matriculas Canceladas"); break;
                case 6: MatriculasPorStatus("Concluida", "Matriculas Concluidas"); break;
                case 0: return;
                default:
                    ConsoleIO.ExibirErro("Opcao invalida.");
                    ConsoleIO.PausarParaContinuar();
                    break;
            }
        }
    }

    private void AlunosPorCurso()
    {
        ConsoleIO.ExibirCabecalho("Alunos por Curso");

        var cursos = _courseService.GetAll();
        if (cursos.Count == 0)
        {
            Console.WriteLine("Nenhum curso cadastrado.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine("Cursos disponiveis:");
        foreach (var c in cursos)
            Console.WriteLine($"  {c.Id}. {c.Nome} ({c.Sigla})");

        Console.WriteLine();
        var id = ConsoleIO.LerInteiro("Id do curso");
        var curso = cursos.FirstOrDefault(c => c.Id == id);

        Console.WriteLine();
        if (curso is null)
        {
            ConsoleIO.ExibirErro("Curso nao encontrado.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        var matriculas = _enrollmentService.GetAll()
            .Where(m => m.CourseId == id)
            .ToList();

        Console.WriteLine($"Curso: {curso.Nome} ({curso.Sigla})");
        ConsoleIO.ExibirLinha();

        if (matriculas.Count == 0)
        {
            Console.WriteLine("Nenhum aluno matriculado neste curso.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine($"{"Id",-5} {"Aluno",-30} {"Status",-12} {"Data":dd/MM/yyyy}");
        ConsoleIO.ExibirLinha();
        foreach (var m in matriculas)
            Console.WriteLine($"{m.Id,-5} {m.StudentName,-30} {m.Status,-12} {m.DataMatricula:dd/MM/yyyy}");

        Console.WriteLine();
        Console.WriteLine($"Total: {matriculas.Count} matricula(s)");

        ConsoleIO.PausarParaContinuar();
    }

    private void CursosDeUmAluno()
    {
        ConsoleIO.ExibirCabecalho("Cursos de um Aluno");

        var alunos = _studentService.GetAll();
        if (alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine("Alunos disponiveis:");
        foreach (var a in alunos)
            Console.WriteLine($"  {a.Id}. {a.Nome} — {a.RegistroAcademico}");

        Console.WriteLine();
        var id = ConsoleIO.LerInteiro("Id do aluno");
        var aluno = alunos.FirstOrDefault(a => a.Id == id);

        Console.WriteLine();
        if (aluno is null)
        {
            ConsoleIO.ExibirErro("Aluno nao encontrado.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        var matriculas = _enrollmentService.GetAll()
            .Where(m => m.StudentId == id)
            .ToList();

        Console.WriteLine($"Aluno: {aluno.Nome} — {aluno.RegistroAcademico}");
        ConsoleIO.ExibirLinha();

        if (matriculas.Count == 0)
        {
            Console.WriteLine("Nenhuma matricula encontrada para este aluno.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine($"{"Id",-5} {"Curso",-35} {"Status",-12} {"Data":dd/MM/yyyy}");
        ConsoleIO.ExibirLinha();
        foreach (var m in matriculas)
            Console.WriteLine($"{m.Id,-5} {m.CourseName,-35} {m.Status,-12} {m.DataMatricula:dd/MM/yyyy}");

        Console.WriteLine();
        Console.WriteLine($"Total: {matriculas.Count} matricula(s)");

        ConsoleIO.PausarParaContinuar();
    }

    private void TotalDeAlunosPorCurso()
    {
        ConsoleIO.ExibirCabecalho("Total de Alunos por Curso");

        var cursos = _courseService.GetAll();
        if (cursos.Count == 0)
        {
            Console.WriteLine("Nenhum curso cadastrado.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        var matriculas = _enrollmentService.GetAll();

        Console.WriteLine($"{"Curso",-35} {"Sigla",-8} {"Ativas",8} {"Total",8}");
        ConsoleIO.ExibirLinha();

        foreach (var c in cursos)
        {
            var dosCurso = matriculas.Where(m => m.CourseId == c.Id).ToList();
            var ativas = dosCurso.Count(m => m.Status == "Ativa");
            Console.WriteLine($"{c.Nome,-35} {c.Sigla,-8} {ativas,8} {dosCurso.Count,8}");
        }

        ConsoleIO.PausarParaContinuar();
    }

    private void MatriculasPorStatus(string status, string titulo)
    {
        ConsoleIO.ExibirCabecalho(titulo);

        var filtradas = _enrollmentService.GetAll()
            .Where(m => m.Status == status)
            .ToList();

        if (filtradas.Count == 0)
        {
            Console.WriteLine($"Nenhuma matricula com status '{status}'.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine($"{"Id",-5} {"Aluno",-25} {"Curso",-30} {"Data":dd/MM/yyyy}");
        ConsoleIO.ExibirLinha();
        foreach (var m in filtradas)
            Console.WriteLine($"{m.Id,-5} {m.StudentName,-25} {m.CourseName,-30} {m.DataMatricula:dd/MM/yyyy}");

        Console.WriteLine();
        Console.WriteLine($"Total: {filtradas.Count} matricula(s)");

        ConsoleIO.PausarParaContinuar();
    }
}
