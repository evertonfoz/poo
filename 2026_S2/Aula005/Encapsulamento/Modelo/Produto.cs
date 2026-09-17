namespace Encapsulamento.Modelo;

public class Produto
{
    public string? Nome { get; set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("O nome não pode ser nulo ou vazio.");
            }
            field = value;
        } }
    public decimal Preco { get; set
        {
            if (value <= 0)
            {
                throw new ArgumentException("O preço não pode ser zero ou negativo.");
            }
            field = value;
         }
    }

    private double? _estoque;
    public double Estoque { get => _estoque ?? 0;}

    public Produto(string nome, decimal preco)
    {
        if (string.IsNullOrEmpty(nome))
        {
            throw new ArgumentException("O nome não pode ser nulo ou vazio.");
        }
        
        Nome = nome;
        AtualizarPreco(preco);
    }

    public void AtualizarPreco(decimal novoPreco)
    {
        if (novoPreco <= 0)
        {
            throw new ArgumentException("O preço não pode ser zero ou negativo.");
        }
        Preco = novoPreco;
    }
    public void RegistrarEntrada(double quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentException("A quantidade de entrada deve ser maior que zero.");
        }
        _estoque += quantidade;
    }

    public void RegistrarSaida(double quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentException("A quantidade de saída deve ser maior que zero.");
        }
        if (quantidade > _estoque)
        {
            throw new InvalidOperationException("Não há estoque suficiente para a saída.");
        }
        _estoque -= quantidade;
    }

    // private decimal? _preco;

    // public decimal? Preco
    // {
    //     get => _preco;
    //     set
    //     {
    //         if (value <= 0)
    //         {
    //             throw new ArgumentException("O preço não pode ser zero ou negativo.");
    //         }
    //         _preco = value;
    //     }
    // }
}