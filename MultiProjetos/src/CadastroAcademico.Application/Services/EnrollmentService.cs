using CadastroAcademico.Application.Common;
using CadastroAcademico.Application.DTOs.Enrollments;
using CadastroAcademico.Application.Results;
using CadastroAcademico.Domain;

namespace CadastroAcademico.Application.Services;

public sealed class EnrollmentService
{
    private readonly InMemoryAcademicStore _store;

    public EnrollmentService(InMemoryAcademicStore store)
    {
        _store = store;
    }

    public OperationResult<EnrollmentDto> Enroll(CreateEnrollmentInput input)
    {
        var student = _store.Students.FirstOrDefault(s => s.Id == input.StudentId);
        if (student is null)
        {
            return OperationResult<EnrollmentDto>.Failure("Aluno nao encontrado.");
        }

        var course = _store.Courses.FirstOrDefault(c => c.Id == input.CourseId);
        if (course is null)
        {
            return OperationResult<EnrollmentDto>.Failure("Curso nao encontrado.");
        }

        try
        {
            var enrollment = course.MatricularAluno(student);
            _store.AddEnrollment(enrollment);

            return OperationResult<EnrollmentDto>.Success(ToDto(enrollment), "Matricula realizada com sucesso.");
        }
        catch (Exception ex)
        {
            return OperationResult<EnrollmentDto>.Failure(ex.Message);
        }
    }

    public OperationResult<EnrollmentDto> CancelEnrollment(int enrollmentId)
    {
        var enrollment = _store.Enrollments.FirstOrDefault(e => e.Id == enrollmentId);
        if (enrollment is null)
        {
            return OperationResult<EnrollmentDto>.Failure("Matricula nao encontrada.");
        }

        try
        {
            enrollment.Cancelar();
            return OperationResult<EnrollmentDto>.Success(ToDto(enrollment), "Matricula cancelada com sucesso.");
        }
        catch (Exception ex)
        {
            return OperationResult<EnrollmentDto>.Failure(ex.Message);
        }
    }

    public OperationResult<EnrollmentDto> CompleteEnrollment(int enrollmentId)
    {
        var enrollment = _store.Enrollments.FirstOrDefault(e => e.Id == enrollmentId);
        if (enrollment is null)
        {
            return OperationResult<EnrollmentDto>.Failure("Matricula nao encontrada.");
        }

        try
        {
            enrollment.Concluir();
            return OperationResult<EnrollmentDto>.Success(ToDto(enrollment), "Matricula concluida com sucesso.");
        }
        catch (Exception ex)
        {
            return OperationResult<EnrollmentDto>.Failure(ex.Message);
        }
    }

    public IReadOnlyList<EnrollmentDto> GetAll()
    {
        return _store.Enrollments.Select(ToDto).ToList();
    }

    public OperationResult DeleteEnrollment(int enrollmentId)
    {
        var enrollment = _store.Enrollments.FirstOrDefault(e => e.Id == enrollmentId);
        if (enrollment is null)
            return OperationResult.Failure("Matricula nao encontrada.");

        if (enrollment.Status == StatusMatricula.Ativa)
        {
            try { enrollment.Cancelar(); }
            catch (Exception ex) { return OperationResult.Failure(ex.Message); }
        }

        _store.RemoveEnrollment(enrollment);
        return OperationResult.Success("Matricula excluida com sucesso.");
    }

    private static EnrollmentDto ToDto(Matricula enrollment)
    {
        return new EnrollmentDto
        {
            Id = enrollment.Id,
            StudentId = enrollment.Aluno.Id,
            StudentName = enrollment.Aluno.Nome,
            CourseId = enrollment.Curso.Id,
            CourseName = enrollment.Curso.Nome,
            DataMatricula = enrollment.DataMatricula,
            Status = enrollment.Status.ToString()
        };
    }
}
