namespace Aula009_HerancaFuncionario.Modelo;

public class Funcionario
{
    // Propriedades com consulta pública e modificação restrita
    public string Nome { get; private set; } = string.Empty;
    public string Matricula { get; private set; } = string.Empty;
    public decimal SalarioBase { get; private set; }

    // O construtor delega a inicialização e validação aos métodos semânticos
    public Funcionario(string nome, string matricula, decimal salarioBase)
    {
        AtualizarNome(nome);
        DefinirMatricula(matricula);
        ReajustarSalario(salarioBase);
    }

    // Métodos semânticos de negócio (responsáveis por validar e alterar o estado)
    public void AtualizarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            throw new ArgumentException("O nome do funcionário não pode ser nulo ou vazio.", nameof(novoNome));

        Nome = novoNome;
    }

    // A matrícula é um identificador imutável externamente: método privado usado apenas na criação
    private void DefinirMatricula(string matricula)
    {
        if (string.IsNullOrWhiteSpace(matricula))
            throw new ArgumentException("A matrícula não pode ser nula ou vazia.", nameof(matricula));

        Matricula = matricula;
    }

    public void ReajustarSalario(decimal novoSalario)
    {
        if (novoSalario < 0)
            throw new ArgumentOutOfRangeException(nameof(novoSalario), "O salário-base não pode ser negativo.");

        SalarioBase = novoSalario;
    }

    public void ExibirDados()
    {
        Console.WriteLine($"Nome: {Nome} | Matrícula: {Matricula} | Salário-base: {SalarioBase:C2}");
    }
}
