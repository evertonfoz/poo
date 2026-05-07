public class Aluno(Guid? alunoId, string nome)
{
    public Guid AlunoId { get; init; } = alunoId ?? Guid.NewGuid();
    public string Nome { get; init; } = string.IsNullOrWhiteSpace(nome) ? throw new ArgumentException("O nome do aluno é obrigatório.", nameof(nome)) : nome;
}