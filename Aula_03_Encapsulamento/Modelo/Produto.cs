public class Produto
{
    // Campos privados para armazenar os valores
    public int Id { get; set; }
    public string Nome { get; private set; }
    private decimal? preco;
    private int? estoque;

    // Propriedade para acessar o preço com validação

    public decimal? Preco { get; }
    public int? Estoque
    {
        get
        {
            return estoque;
        }
    }

    // public decimal? Preco
    // {
    //     get { return preco; }
    //     set
    //     {
    //         if (value <= 0)
    //         {
    //             Console.WriteLine("O preço não pode ser zero ou negativo.");
    //             // preco = 0;
    //         }
    //         else
    //         {
    //             preco = value;
    //         }
    //     }
    // }

    // Propriedade para acessar a quantidade com validação
    // public int? Quantidade
    // {
    //     get { return quantidade; }
    //     set
    //     {
    //         if (value <= 0)
    //         {
    //             Console.WriteLine("A quantidade não pode ser zero ou negativa.");
    //             // quantidade = 0;
    //         }
    //         else
    //         {
    //             quantidade = value;
    //         }
    //     }
    // }

    // Métodos para manipulação de campos privados
    public void RegistrarAtualizacaoDePreco(decimal value)
    {
        if (value <= 0)
        {
            Console.WriteLine("O preço não pode ser zero ou negativo.");
            // preco = 0;
        }
        else
        {
            preco = value;
        }
    }

    // public decimal? GetPreco()
    // {
    //     return preco;
    // }

    public void RegistrarEntradaDeEstoque(int value)
    {
        if (value <= 0)
        {
            Console.WriteLine("A quantidade não pode ser zero ou negativa.");
            // quantidade = 0;
        }
        else
        {
            if (estoque == null)
            {
                estoque = 0;
            }
            estoque += value;
        }
    }

    public void RegistrarSaidaDeEstoque(int value)
    {
        if (value <= 0)
        {
            Console.WriteLine("A quantidade não pode ser zero ou negativa.");
            // quantidade = 0;
        }
        else
        {
            if (estoque == null)
            {
                estoque = 0;
            }

            if (estoque < value)
            {
                Console.WriteLine("Não há estoque suficiente para realizar a saída.");
                return;
            }
            estoque -= value;
        }
    }

    public void RegistrarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("O nome do produto não pode ser vazio.");
            // Nome = "Produto sem nome";
        }
        else
        {
            Nome = nome;
        }
    }
}