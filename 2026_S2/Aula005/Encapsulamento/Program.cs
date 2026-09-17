using Encapsulamento.Modelo;

var notebook = new Produto("Notebook Gamer", 5000m);

Console.WriteLine($"Nome: {notebook.Nome}");
Console.WriteLine($"Preço: {notebook.Preco}");
Console.WriteLine($"Estoque: {notebook.Estoque}");

notebook.AtualizarPreco(-4500m);
Console.WriteLine($"Preço atualizado: {notebook.Preco}");

// notebook.RegistrarEntrada(10);
// Console.WriteLine($"Estoque: {notebook.Estoque}");

// notebook.RegistrarSaida(5);
// Console.WriteLine($"Estoque: {notebook.Estoque}");

// notebook.RegistrarSaida(1);
// Console.WriteLine($"Estoque: {notebook.Estoque}");



// notebook.Preco = 100m;
// notebook.Nome = "Notebook Gamer";

// if (notebook.Preco == null)
// {
//     Console.WriteLine("Preço não definido.");
// }
// else
// {
    // Console.WriteLine($"Nome: {notebook.Nome}");
    // Console.WriteLine($"Preço: {notebook.Preco}");
// }   
