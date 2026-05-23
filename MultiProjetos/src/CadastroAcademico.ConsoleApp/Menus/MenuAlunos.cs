using CadastroAcademico.Application.DTOs.Students;
using CadastroAcademico.Application.Services;
using CadastroAcademico.ConsoleApp.IO;

namespace CadastroAcademico.ConsoleApp.Menus;

internal sealed class MenuAlunos
{
    private readonly StudentService _studentService;

    public MenuAlunos(StudentService studentService)
    {
        _studentService = studentService;
    }

    public void Executar()
    {
        while (true)
        {
            ConsoleIO.ExibirCabecalho("Alunos");
            Console.WriteLine("1. Cadastrar aluno");
            Console.WriteLine("2. Listar alunos");
            Console.WriteLine("3. Consultar aluno por Id");
            Console.WriteLine("4. Atualizar aluno");
            Console.WriteLine("5. Excluir aluno");
            Console.WriteLine("0. Voltar");
            Console.WriteLine();

            switch (ConsoleIO.LerInteiro("Opcao"))
            {
                case 1: CadastrarAluno(); break;
                case 2: ListarAlunos(); break;
                case 3: ConsultarAluno(); break;
                case 4: AtualizarAluno(); break;
                case 5: ExcluirAluno(); break;
                case 0: return;
                default:
                    ConsoleIO.ExibirErro("Opcao invalida.");
                    ConsoleIO.PausarParaContinuar();
                    break;
            }
        }
    }

    private void CadastrarAluno()
    {
        ConsoleIO.ExibirCabecalho("Cadastrar Aluno");

        Console.WriteLine("--- Dados do Aluno ---");
        var nome = ConsoleIO.LerTexto("Nome");
        var email = ConsoleIO.LerTexto("E-mail");

        Console.WriteLine();
        Console.WriteLine("--- Perfil Academico ---");
        var registro = ConsoleIO.LerTexto("Registro academico");
        var periodo = ConsoleIO.LerTexto("Periodo");

        Console.WriteLine();
        var result = _studentService.CreateStudent(new CreateStudentInput
        {
            Nome = nome,
            Email = email,
            RegistroAcademico = registro,
            Periodo = periodo
        });

        if (result.IsSuccess) ConsoleIO.ExibirSucesso(result.Message);
        else ConsoleIO.ExibirErro(result.Message);

        ConsoleIO.PausarParaContinuar();
    }

    private void ListarAlunos()
    {
        ConsoleIO.ExibirCabecalho("Alunos Cadastrados");

        var alunos = _studentService.GetAll();

        if (alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine($"{"Id",-5} {"Nome",-25} {"E-mail",-28} {"RA",-12} {"Periodo"}");
        ConsoleIO.ExibirLinha();
        foreach (var a in alunos)
            Console.WriteLine($"{a.Id,-5} {a.Nome,-25} {a.Email,-28} {a.RegistroAcademico,-12} {a.Periodo}");

        ConsoleIO.PausarParaContinuar();
    }

    private void ConsultarAluno()
    {
        ConsoleIO.ExibirCabecalho("Consultar Aluno");

        var id = ConsoleIO.LerInteiro("Id do aluno");
        var result = _studentService.GetById(id);

        Console.WriteLine();
        if (!result.IsSuccess || result.Value is null)
        {
            ConsoleIO.ExibirErro(result.Message);
            ConsoleIO.PausarParaContinuar();
            return;
        }

        var a = result.Value;
        ConsoleIO.ExibirLinha();
        Console.WriteLine($"Id                  : {a.Id}");
        Console.WriteLine($"Nome                : {a.Nome}");
        Console.WriteLine($"E-mail              : {a.Email}");
        Console.WriteLine($"Registro Academico  : {a.RegistroAcademico}");
        Console.WriteLine($"Periodo             : {a.Periodo}");
        ConsoleIO.ExibirLinha();

        ConsoleIO.PausarParaContinuar();
    }

    private void AtualizarAluno()
    {
        ConsoleIO.ExibirCabecalho("Atualizar Aluno");

        var id = ConsoleIO.LerInteiro("Id do aluno");
        var consulta = _studentService.GetById(id);

        if (!consulta.IsSuccess || consulta.Value is null)
        {
            ConsoleIO.ExibirErro(consulta.Message);
            ConsoleIO.PausarParaContinuar();
            return;
        }

        var atual = consulta.Value;
        Console.WriteLine();
        Console.WriteLine($"Editando: {atual.Nome} — {atual.Email}");
        Console.WriteLine("Deixe em branco para manter o valor atual.");
        Console.WriteLine();

        Console.WriteLine("--- Dados do Aluno ---");
        var nome = ConsoleIO.LerTexto($"Nome [{atual.Nome}]");
        if (string.IsNullOrWhiteSpace(nome)) nome = atual.Nome;

        var email = ConsoleIO.LerTexto($"E-mail [{atual.Email}]");
        if (string.IsNullOrWhiteSpace(email)) email = atual.Email;

        Console.WriteLine();
        Console.WriteLine("--- Perfil Academico ---");
        var registro = ConsoleIO.LerTexto($"Registro academico [{atual.RegistroAcademico}]");
        if (string.IsNullOrWhiteSpace(registro)) registro = atual.RegistroAcademico;

        var periodo = ConsoleIO.LerTexto($"Periodo [{atual.Periodo}]");
        if (string.IsNullOrWhiteSpace(periodo)) periodo = atual.Periodo;

        Console.WriteLine();
        var result = _studentService.UpdateStudent(id, new UpdateStudentInput
        {
            Nome = nome,
            Email = email,
            RegistroAcademico = registro,
            Periodo = periodo
        });

        if (result.IsSuccess) ConsoleIO.ExibirSucesso(result.Message);
        else ConsoleIO.ExibirErro(result.Message);

        ConsoleIO.PausarParaContinuar();
    }

    private void ExcluirAluno()
    {
        ConsoleIO.ExibirCabecalho("Excluir Aluno");

        var id = ConsoleIO.LerInteiro("Id do aluno");
        var consulta = _studentService.GetById(id);

        if (!consulta.IsSuccess || consulta.Value is null)
        {
            ConsoleIO.ExibirErro(consulta.Message);
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Aluno: {consulta.Value.Nome} — {consulta.Value.Email}");

        if (!ConsoleIO.ConfirmarAcao("Confirmar exclusao?"))
        {
            Console.WriteLine("Operacao cancelada.");
            ConsoleIO.PausarParaContinuar();
            return;
        }

        Console.WriteLine();
        var result = _studentService.DeleteStudent(id);

        if (result.IsSuccess) ConsoleIO.ExibirSucesso(result.Message);
        else ConsoleIO.ExibirErro(result.Message);

        ConsoleIO.PausarParaContinuar();
    }
}
