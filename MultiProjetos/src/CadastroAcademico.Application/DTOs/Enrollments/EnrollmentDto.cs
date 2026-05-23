namespace CadastroAcademico.Application.DTOs.Enrollments;

public sealed class EnrollmentDto
{
    public int Id { get; init; }
    public int StudentId { get; init; }
    public string StudentName { get; init; } = string.Empty;
    public int CourseId { get; init; }
    public string CourseName { get; init; } = string.Empty;
    public DateTime DataMatricula { get; init; }
    public string Status { get; init; } = string.Empty;
}
