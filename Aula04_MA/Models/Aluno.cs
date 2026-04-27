public class Aluno
{
    public string? Nome { get; set; }
    public string? Matricula { get; set; }
    public bool? Ativo { get; private set; }

    public void Ativar()
    {
        Ativo = true;
    }

    public void Desativar()
    {
        Ativo = false;
    }

    public void ExibirDados()
    {
        Console.WriteLine($"Nome: {Nome??"Não informado"}");
        Console.WriteLine($"Matrícula: {Matricula??"Não informado"}");
        Console.WriteLine($"Ativo: {(Ativo == null ? "Não informado" : Ativo.ToString())}");
    }
}