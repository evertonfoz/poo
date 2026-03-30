/*
    Mini-Cadastro de Produto

    Peça: nome do produto (string), preço (decimal), quantidade em estoque (int) e confirmação de venda ativa (bool via s/n).

    Ao final, mostre um resumo e calcule o valor total em estoque (preço * quantidade).
*/

namespace Aula02_OO.Modelo;

public class Produto
{
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public int QuantidadeEstoque { get; set; }
    public bool VendaAtiva { get; set; }

    public decimal ValorTotalEstoque()
    {
        return Preco * QuantidadeEstoque;
    }
}