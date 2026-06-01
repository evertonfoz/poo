using CadastroAcademico.Application.Common;
using CadastroAcademico.Application.DTOs.Courses;
using CadastroAcademico.Application.Results;
using CadastroAcademico.Domain;

namespace CadastroAcademico.Application.Services;

public sealed class CourseService
{
    private readonly InMemoryAcademicStore _store;

    public CourseService(InMemoryAcademicStore store)
    {
        _store = store;
    }

    public OperationResult<CourseDto> CreateCourse(CreateCourseInput input)
    {
        var sigla = input.Sigla.Trim();
        var siglaConflito = _store.Courses.Any(c =>
            c.Sigla.Equals(sigla, StringComparison.OrdinalIgnoreCase));
        if (siglaConflito)
        {
            return OperationResult<CourseDto>.Failure("Ja existe um curso com esta sigla.");
        }

        try
        {
            var course = new Curso(input.Nome, sigla, input.CargaHoraria);
            _store.AddCourse(course);

            return OperationResult<CourseDto>.Success(ToDto(course), "Curso criado com sucesso.");
        }
        catch (Exception ex)
        {
            return OperationResult<CourseDto>.Failure(ex.Message);
        }
    }

    public OperationResult<CourseDto> GetById(int id)
    {
        var course = _store.Courses.FirstOrDefault(c => c.Id == id);
        if (course is null)
        {
            return OperationResult<CourseDto>.Failure("Curso nao encontrado.");
        }

        return OperationResult<CourseDto>.Success(ToDto(course));
    }

    public IReadOnlyList<CourseDto> GetAll()
    {
        return _store.Courses.Select(ToDto).ToList();
    }

    public OperationResult UpdateCourse(int id, UpdateCourseInput input)
    {
        var course = _store.Courses.FirstOrDefault(c => c.Id == id);
        if (course is null)
            return OperationResult.Failure("Curso nao encontrado.");

        var siglaConflito = _store.Courses.Any(c => c.Id != id &&
            c.Sigla.Equals(input.Sigla.Trim(), StringComparison.OrdinalIgnoreCase));
        if (siglaConflito)
            return OperationResult.Failure("Ja existe outro curso com esta sigla.");

        try
        {
            course.AtualizarDados(input.Nome, input.Sigla, input.CargaHoraria);
            return OperationResult.Success("Curso atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            return OperationResult.Failure(ex.Message);
        }
    }

    public OperationResult DeleteCourse(int id)
    {
        var course = _store.Courses.FirstOrDefault(c => c.Id == id);
        if (course is null)
            return OperationResult.Failure("Curso nao encontrado.");

        var temMatriculaAtiva = _store.Enrollments.Any(e =>
            e.Curso.Id == id && e.Status == StatusMatricula.Ativa);
        if (temMatriculaAtiva)
            return OperationResult.Failure("Nao e possivel excluir um curso com matriculas ativas.");

        _store.RemoveCourse(course);
        return OperationResult.Success("Curso excluido com sucesso.");
    }

    private static CourseDto ToDto(Curso course)
    {
        return new CourseDto
        {
            Id = course.Id,
            Nome = course.Nome,
            Sigla = course.Sigla,
            CargaHoraria = course.CargaHoraria
        };
    }
}
