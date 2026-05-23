namespace CadastroAcademico.Application.DTOs.Courses;

public sealed class CreateCourseInput
{
    public string Nome { get; init; } = string.Empty;
    public string Sigla { get; init; } = string.Empty;
    public int CargaHoraria { get; init; }
}
