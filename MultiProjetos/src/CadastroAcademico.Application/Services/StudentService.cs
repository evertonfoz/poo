using CadastroAcademico.Application.Common;
using CadastroAcademico.Application.DTOs.Students;
using CadastroAcademico.Application.Results;
using CadastroAcademico.Domain;

namespace CadastroAcademico.Application.Services;

public sealed class StudentService
{
    private readonly InMemoryAcademicStore _store;

    public StudentService(InMemoryAcademicStore store)
    {
        _store = store;
    }

    public OperationResult<StudentDto> CreateStudent(CreateStudentInput input)
    {
        var email = input.Email.Trim();
        var registroAcademico = input.RegistroAcademico.Trim();

        var emailConflito = _store.Students.Any(s =>
            s.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        if (emailConflito)
        {
            return OperationResult<StudentDto>.Failure("Ja existe um aluno com este e-mail.");
        }

        var raConflito = _store.Students.Any(s =>
            s.PerfilAcademico.RegistroAcademico.Equals(registroAcademico, StringComparison.OrdinalIgnoreCase));
        if (raConflito)
        {
            return OperationResult<StudentDto>.Failure("Ja existe um aluno com este registro academico.");
        }

        try
        {
            var profile = new PerfilAcademico(registroAcademico, input.Periodo);
            var student = new Aluno(input.Nome, email, profile);
            _store.AddStudent(student);

            return OperationResult<StudentDto>.Success(ToDto(student), "Aluno criado com sucesso.");
        }
        catch (Exception ex)
        {
            return OperationResult<StudentDto>.Failure(ex.Message);
        }
    }

    public OperationResult<StudentDto> GetById(int id)
    {
        var student = _store.Students.FirstOrDefault(s => s.Id == id);
        if (student is null)
        {
            return OperationResult<StudentDto>.Failure("Aluno nao encontrado.");
        }

        return OperationResult<StudentDto>.Success(ToDto(student));
    }

    public IReadOnlyList<StudentDto> GetAll()
    {
        return _store.Students.Select(ToDto).ToList();
    }

    public OperationResult UpdateStudent(int id, UpdateStudentInput input)
    {
        var student = _store.Students.FirstOrDefault(s => s.Id == id);
        if (student is null)
            return OperationResult.Failure("Aluno nao encontrado.");

        var emailConflito = _store.Students.Any(s => s.Id != id &&
            s.Email.Equals(input.Email.Trim(), StringComparison.OrdinalIgnoreCase));
        if (emailConflito)
            return OperationResult.Failure("Ja existe outro aluno com este e-mail.");

        var raConflito = _store.Students.Any(s => s.Id != id &&
            s.PerfilAcademico.RegistroAcademico.Equals(input.RegistroAcademico.Trim(), StringComparison.OrdinalIgnoreCase));
        if (raConflito)
            return OperationResult.Failure("Ja existe outro aluno com este registro academico.");

        try
        {
            student.AtualizarNome(input.Nome);
            student.AtualizarEmail(input.Email);
            student.AtualizarPerfilAcademico(new PerfilAcademico(input.RegistroAcademico, input.Periodo));
            return OperationResult.Success("Aluno atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            return OperationResult.Failure(ex.Message);
        }
    }

    public OperationResult DeleteStudent(int id)
    {
        var student = _store.Students.FirstOrDefault(s => s.Id == id);
        if (student is null)
            return OperationResult.Failure("Aluno nao encontrado.");

        var temMatriculaAtiva = _store.Enrollments.Any(e =>
            e.Aluno.Id == id && e.Status == StatusMatricula.Ativa);
        if (temMatriculaAtiva)
            return OperationResult.Failure("Nao e possivel excluir um aluno com matriculas ativas.");

        _store.RemoveStudent(student);
        return OperationResult.Success("Aluno excluido com sucesso.");
    }

    private static StudentDto ToDto(Aluno student)
    {
        return new StudentDto
        {
            Id = student.Id,
            Nome = student.Nome,
            Email = student.Email,
            RegistroAcademico = student.PerfilAcademico.RegistroAcademico,
            Periodo = student.PerfilAcademico.Periodo
        };
    }
}
