namespace Aula009_HerancaFuncionario.Modelo;

public class FuncionarioAdministrativo : Funcionario
{
    public string Setor { get; private set; } = string.Empty;
    public string Cargo { get; private set; } = string.Empty;

    public FuncionarioAdministrativo(
        string nome,
        string matricula,
        decimal salarioBase,
        string setor,
        string cargo)
        : base(nome, matricula, salarioBase) // A classe base cuida de seus próprios dados
    {
        TransferirSetor(setor);
        AlterarCargo(cargo);
    }

    // Métodos semânticos de negócio
    public void TransferirSetor(string novoSetor)
    {
        if (string.IsNullOrWhiteSpace(novoSetor))
            throw new ArgumentException("O setor de atuação não pode ser nulo ou vazio.", nameof(novoSetor));

        Setor = novoSetor;
    }

    public void AlterarCargo(string novoCargo)
    {
        if (string.IsNullOrWhiteSpace(novoCargo))
            throw new ArgumentException("O cargo administrativo não pode ser nulo ou vazio.", nameof(novoCargo));

        Cargo = novoCargo;
    }

    public void Promover(string novoCargo, decimal novoSalario)
    {
        AlterarCargo(novoCargo);
        ReajustarSalario(novoSalario); // Reutiliza o método semântico herdado de Funcionario
    }
}
