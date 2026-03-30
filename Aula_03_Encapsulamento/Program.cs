Produto notebook = new();

notebook.Id = 1;
notebook.RegistrarNome("Notebook Gamer");
notebook.RegistrarAtualizacaoDePreco(5000.00m);
notebook.RegistrarEntradaDeEstoque(10);
notebook.RegistrarSaidaDeEstoque(2);

// notebook.Preco = -5000.00m;
// notebook.Quantidade = -10;

Console.WriteLine($"Id: {notebook.Id}");
Console.WriteLine($"Nome: {notebook.Nome}");
Console.WriteLine($"Preço: {notebook.Preco}");
Console.WriteLine($"Quantidade: {notebook.Estoque}");