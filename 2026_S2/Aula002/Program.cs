Console.Write("Digite seu nome: ");
var nome = Console.ReadLine();

Console.Write("Digite sua idade: ");
var idade = Console.ReadLine();

Console.WriteLine($"Nome: {nome}, Idade: {idade}");
bool eHMaiorDeIdade = int.Parse(idade) >= 18;
Console.WriteLine($"É maior de idade? {eHMaiorDeIdade}");

// if (string.IsNullOrEmpty(nome))
// {
//     Console.WriteLine("Nome inválido. Por favor, digite um nome válido.");
//     return;
// }

char primeiraLetra =  string.IsNullOrEmpty(nome) ? '?' : nome[0];

DateTime dataEHoraAtual = DateTime.Now;

Console.WriteLine($"Olá {nome}, bem-vindo(a) {primeiraLetra}");
Console.WriteLine($"Data e hora atual: {dataEHoraAtual}");
Console.WriteLine($"Data e hora atual formatada: {dataEHoraAtual:dd/MM/yyyy HH:mm:ss}");
Console.WriteLine($"Data {dataEHoraAtual.Date:dd/MM/yyyy}");
Console.WriteLine($"Hora {dataEHoraAtual.TimeOfDay:hh\\:mm\\:ss}");

Console.WriteLine($"Amanhã será: {dataEHoraAtual.AddDays(1):dd/MM/yyyy}");