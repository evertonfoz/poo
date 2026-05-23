namespace CadastroAcademico.Application.DTOs.Courses;

public sealed class CourseDto
{
    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string Sigla { get; init; } = string.Empty;
    public int CargaHoraria { get; init; }
}
