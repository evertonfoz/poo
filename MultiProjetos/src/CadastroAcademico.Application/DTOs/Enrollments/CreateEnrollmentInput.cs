namespace CadastroAcademico.Application.DTOs.Enrollments;

public sealed class CreateEnrollmentInput
{
    public int StudentId { get; init; }
    public int CourseId { get; init; }
}
