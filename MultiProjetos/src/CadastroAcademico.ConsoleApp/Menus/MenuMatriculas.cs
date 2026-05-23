using CadastroAcademico.Application.DTOs.Enrollments;
using CadastroAcademico.Application.Services;
using CadastroAcademico.ConsoleApp.IO;

namespace CadastroAcademico.ConsoleApp.Menus;

internal sealed class MenuMatriculas
{
    private readonly CourseService _courseService;
    private readonly StudentService _studentService;
    private readonly EnrollmentService _enrollmentService;

    public MenuMatriculas(
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
            ConsoleIO.ExibirCabecalho("Matriculas");
            Console.WriteLine("1. Realizar matricula");
            Console.WriteLine("2. Listar matriculas");
            Console.WriteLine("3. Consultar matricula por Id");
            Console.WriteLine("4. Cancelar matricula");
            Console.WriteLine("5. Concluir matricula");
            Console.WriteLine("6. Excluir matricula");
            Console.WriteLine("0. Voltar");
            Console.WriteLine();

            switch (ConsoleIO.LerInteiro("Opcao"))
            {
                case 1: RealizarMatricula(); break;
                case 2: ListarMatriculas(); break;
                case 3: ConsultarMatricula(); break;
                case 4: CancelarMatricula(); break;
                case 5: ConcluirMatricula(); break;
                case 6: ExcluirMatricula(); break;
                case 0: return;
                default:
                    ConsoleIO.ExibirErro("Opcao invalida.");
                    ConsoleIO.PausarParaContinuar();
                    break;
            }
        }
    }

    private void RealizarMatricula()
    {
        ConsoleIO.ExibirCabecalho("Realizar Matricula");

        var cursos = _courseService.GetAll();
        if (cursos.Count == 0)
        {
            ConsoleIO.ExibirErro("Nenhum curso cadastrado. Cadastre um curso primeiro.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        var alunos = _studentService.GetAll();
        if (alunos.Count == 0)
        {
            ConsoleIO.ExibirErro("Nenhum aluno cadastrado. Cadastre um aluno primeiro.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine("--- Cursos disponiveis ---");
        foreach (var c in cursos)
            Console.WriteLine($"  {c.Id}. {c.Nome} ({c.Sigla})");

        Console.WriteLine();
        var cursoId = ConsoleIO.LerInteiro("Id do curso");

        Console.WriteLine();
        Console.WriteLine("--- Alunos disponiveis ---");
        foreach (var a in alunos)
            Console.WriteLine($"  {a.Id}. {a.Nome} — {a.RegistroAcademico}");

        Console.WriteLine();
        var alunoId = ConsoleIO.LerInteiro("Id do aluno");

        Console.WriteLine();
        var result = _enrollmentService.Enroll(new CreateEnrollmentInput
        {
            CourseId = cursoId,
            StudentId = alunoId
        });

        if (result.IsSuccess) ConsoleIO.ExibirSucesso(result.Message);
        else ConsoleIO.ExibirErro(result.Message);

        ConsoleIO.PausarParaContinuar();
    }

    private void ListarMatriculas()
    {
        ConsoleIO.ExibirCabecalho("Matriculas Registradas");

        var matriculas = _enrollmentService.GetAll();

        if (matriculas.Count == 0)
        {
            Console.WriteLine("Nenhuma matricula registrada.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine($"{"Id",-5} {"Aluno",-25} {"Curso",-30} {"Data",-12} Status");
        ConsoleIO.ExibirLinha();
        foreach (var m in matriculas)
            Console.WriteLine($"{m.Id,-5} {m.StudentName,-25} {m.CourseName,-30} {m.DataMatricula:dd/MM/yyyy,-12} {m.Status}");

        ConsoleIO.PausarParaContinuar();
    }

    private void ConsultarMatricula()
    {
        ConsoleIO.ExibirCabecalho("Consultar Matricula");

        var id = ConsoleIO.LerInteiro("Id da matricula");
        var todas = _enrollmentService.GetAll();
        var matricula = todas.FirstOrDefault(m => m.Id == id);

        Console.WriteLine();
        if (matricula is null)
        {
            ConsoleIO.ExibirErro("Matricula nao encontrada.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        ConsoleIO.ExibirLinha();
        Console.WriteLine($"Id           : {matricula.Id}");
        Console.WriteLine($"Aluno        : {matricula.StudentName}");
        Console.WriteLine($"Curso        : {matricula.CourseName}");
        Console.WriteLine($"Data         : {matricula.DataMatricula:dd/MM/yyyy}");
        Console.WriteLine($"Status       : {matricula.Status}");
        ConsoleIO.ExibirLinha();

        ConsoleIO.PausarParaContinuar();
    }

    private void CancelarMatricula()
    {
        ConsoleIO.ExibirCabecalho("Cancelar Matricula");

        ExibirMatriculasAtivas();

        var id = ConsoleIO.LerInteiro("Id da matricula a cancelar");
        Console.WriteLine();

        var result = _enrollmentService.CancelEnrollment(id);

        if (result.IsSuccess) ConsoleIO.ExibirSucesso(result.Message);
        else ConsoleIO.ExibirErro(result.Message);

        ConsoleIO.PausarParaContinuar();
    }

    private void ConcluirMatricula()
    {
        ConsoleIO.ExibirCabecalho("Concluir Matricula");

        ExibirMatriculasAtivas();

        var id = ConsoleIO.LerInteiro("Id da matricula a concluir");
        Console.WriteLine();

        var result = _enrollmentService.CompleteEnrollment(id);

        if (result.IsSuccess) ConsoleIO.ExibirSucesso(result.Message);
        else ConsoleIO.ExibirErro(result.Message);

        ConsoleIO.PausarParaContinuar();
    }

    private void ExcluirMatricula()
    {
        ConsoleIO.ExibirCabecalho("Excluir Matricula");

        ListarMatriculasResumido();

        var id = ConsoleIO.LerInteiro("Id da matricula a excluir");
        var todas = _enrollmentService.GetAll();
        var matricula = todas.FirstOrDefault(m => m.Id == id);

        if (matricula is null)
        {
            ConsoleIO.ExibirErro("Matricula nao encontrada.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Matricula: {matricula.StudentName} -> {matricula.CourseName} ({matricula.Status})");

        if (!ConsoleIO.ConfirmarAcao("Confirmar exclusao?"))
        {
            Console.WriteLine("Operacao cancelada.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine();
        var result = _enrollmentService.DeleteEnrollment(id);

        if (result.IsSuccess) ConsoleIO.ExibirSucesso(result.Message);
        else ConsoleIO.ExibirErro(result.Message);

        ConsoleIO.PausarParaContinuar();
    }

    private void ExibirMatriculasAtivas()
    {
        var ativas = _enrollmentService.GetAll().Where(m => m.Status == "Ativa").ToList();
        if (ativas.Count == 0)
        {
            Console.WriteLine("Nenhuma matricula ativa no momento.");
            Console.WriteLine();
            return;
        }

        Console.WriteLine("Matriculas ativas:");
        foreach (var m in ativas)
            Console.WriteLine($"  {m.Id}. {m.StudentName} -> {m.CourseName}");
        Console.WriteLine();
    }

    private void ListarMatriculasResumido()
    {
        var todas = _enrollmentService.GetAll();
        if (todas.Count == 0) return;

        Console.WriteLine("Matriculas registradas:");
        foreach (var m in todas)
            Console.WriteLine($"  {m.Id}. {m.StudentName} -> {m.CourseName} ({m.Status})");
        Console.WriteLine();
    }
}
