public class Produto
{
    public string Nome { get; private set; }
    public int QuantidadeEmEstoque { get; private set; }
    public string UnidadeDeMedida { get; private set; }
    public int QuantidadePorUnidadeDeMedida { get; private set; }
    public decimal PrecoUnitario { get; private set; }

    public void AlteraPreco(decimal novoPreco)
    {
        if (novoPreco <= 0)
        {
            Console.WriteLine("O preço deve ser maior que zero.");
            return;
        }
        PrecoUnitario = novoPreco;
    }

    public void RegistrarEntradaEmEstoque(int quantidade)
    {
        if (quantidade <= 0)
        {
            Console.WriteLine("A quantidade deve ser maior que zero.");
            return;
        }
        QuantidadeEmEstoque += quantidade;
    }

    public void RegistrarSaidaDeEstoque(int quantidade)
    {
        if (quantidade <= 0)
        {
            Console.WriteLine("A quantidade deve ser maior que zero.");
            return;
        }
        if (quantidade > QuantidadeEmEstoque)
        {
            Console.WriteLine("Quantidade insuficiente em estoque.");
            return;
        }
        QuantidadeEmEstoque -= quantidade;
    }
}