namespace Aula009_HerancaProduto.Modelo;

public class Produto
{
    public string Codigo { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public decimal PrecoBase { get; private set; }

    public Produto(string codigo, string descricao, decimal precoBase)
    {
        DefinirCodigo(codigo);
        AtualizarDescricao(descricao);
        AtualizarPreco(precoBase);
    }

    // Código é identificador do produto: método privado usado apenas na criação
    private void DefinirCodigo(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("O código do produto não pode ser nulo ou vazio.", nameof(codigo));

        Codigo = codigo;
    }

    // Métodos semânticos de negócio
    public void AtualizarDescricao(string novaDescricao)
    {
        if (string.IsNullOrWhiteSpace(novaDescricao))
            throw new ArgumentException("A descrição do produto não pode ser nula ou vazia.", nameof(novaDescricao));

        Descricao = novaDescricao;
    }

    public void AtualizarPreco(decimal novoPreco)
    {
        if (novoPreco <= 0)
            throw new ArgumentOutOfRangeException(nameof(novoPreco), "O preço-base deve ser estritamente maior que zero.");

        PrecoBase = novoPreco;
    }

    public void ExibirInformacoes()
    {
        Console.WriteLine($"Código: {Codigo} | Descrição: {Descricao} | Preço-base: {PrecoBase:C2}");
    }
}
