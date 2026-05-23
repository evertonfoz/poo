using CadastroAcademico.Application.Services;
using CadastroAcademico.ConsoleApp.IO;

namespace CadastroAcademico.ConsoleApp.Menus;

internal sealed class MenuPrincipal
{
    private readonly MenuCursos _menuCursos;
    private readonly MenuAlunos _menuAlunos;
    private readonly MenuMatriculas _menuMatriculas;
    private readonly MenuRelatorios _menuRelatorios;

    public MenuPrincipal(
        CourseService courseService,
        StudentService studentService,
        EnrollmentService enrollmentService)
    {
        _menuCursos = new MenuCursos(courseService);
        _menuAlunos = new MenuAlunos(studentService);
        _menuMatriculas = new MenuMatriculas(courseService, studentService, enrollmentService);
        _menuRelatorios = new MenuRelatorios(courseService, studentService, enrollmentService);
    }

    public void Executar()
    {
        while (true)
        {
            ConsoleIO.ExibirCabecalho("Sistema de Cadastro Academico");
            Console.WriteLine("1. Gerenciar cursos");
            Console.WriteLine("2. Gerenciar alunos");
            Console.WriteLine("3. Gerenciar matriculas");
            Console.WriteLine("4. Relatorios e consultas");
            Console.WriteLine("0. Sair");
            Console.WriteLine();

            switch (ConsoleIO.LerInteiro("Opcao"))
            {
                case 1: _menuCursos.Executar(); break;
                case 2: _menuAlunos.Executar(); break;
                case 3: _menuMatriculas.Executar(); break;
                case 4: _menuRelatorios.Executar(); break;
                case 0:
                    ConsoleIO.ExibirCabecalho("Sistema de Cadastro Academico");
                    Console.WriteLine("Encerrando o sistema. Ate logo!");
                    Console.WriteLine();
                    return;
                default:
                    ConsoleIO.ExibirErro("Opcao invalida.");
                    ConsoleIO.PausarParaContinuar();
                    break;
            }
        }
    }
}
