int idade = 58;
string nome = "Everton";
bool ativo = false;

double altura = 1.72;
decimal saldo = 1250.50m;
char inicial = 'A';
DateTime hoje = DateTime.Now;

Console.WriteLine($"Nome: {nome} ({inicial})");
Console.WriteLine($"Idade: {idade}");
Console.WriteLine($"Ativo: {ativo}");
Console.WriteLine($"Altura: {altura}");
Console.WriteLine($"Saldo: {saldo}");
Console.WriteLine($"Hoje: {hoje}");

var pontos = 100;
var titulo = "Missão 2";
var ok = false;

Console.WriteLine($"'{titulo}' | pontos={pontos} | ok={ok}");

var ano = DateTime.Now.Year;
Console.WriteLine($"Ano: {ano}");