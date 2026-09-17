using Encapsulamento.Modelo;

var notebook = new Produto();
Console.WriteLine($"Estoque: {notebook.Estoque}");

notebook.RegistrarEntrada(10);
Console.WriteLine($"Estoque: {notebook.Estoque}");

notebook.RegistrarSaida(5);
Console.WriteLine($"Estoque: {notebook.Estoque}");

notebook.RegistrarSaida(1);
Console.WriteLine($"Estoque: {notebook.Estoque}");



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
