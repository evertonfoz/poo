namespace CadastroAcademico.Application.DTOs.Students;

public sealed class UpdateStudentInput
{
    public string Nome { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string RegistroAcademico { get; init; } = string.Empty;
    public string Periodo { get; init; } = string.Empty;
}
