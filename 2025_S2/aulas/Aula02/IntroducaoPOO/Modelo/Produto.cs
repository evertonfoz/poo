namespace IntroducaoPOO.Modelo;

public class Produto
{
    public string Nome { get; private set; }
    public double PrecoUnitario { get; private set; }
    public int QuantidadeEmEstoque { get; private set; }
    // private double _valorDeCompra;
    // public double ValorDeCompra
    // {
    //     get { return _valorDeCompra; }
    //     set { _valorDeCompra = value; }
    // }


    public Produto(string nome, double precoUnitario, int quantidade)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome inválido");

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(precoUnitario);
        ArgumentOutOfRangeException.ThrowIfNegative(quantidade);

        Nome = nome;
        PrecoUnitario = precoUnitario;
        QuantidadeEmEstoque = quantidade;
    }

    public Produto(string nome, double precoUnitario) : this(nome, precoUnitario, 0)
    {

    }

    public double ValorTotal() => PrecoUnitario * QuantidadeEmEstoque;

    public void ReajustarPrecoUnitario(double percentualParaReajuste)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(percentualParaReajuste);
        PrecoUnitario += (PrecoUnitario * percentualParaReajuste / 100);
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Preço Unitário: {PrecoUnitario}, QuantidadeEmEstoque: {QuantidadeEmEstoque}";
    }

    public override bool Equals(object? obj)
    {
        return this.Nome.Equals((obj as Produto).Nome);
    }

    public override int GetHashCode()
    {
        return Nome.GetHashCode();
    }
}