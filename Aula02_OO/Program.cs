/*
    Ficha do Usuário (combinação de tipos)

    Crie um programa que leia e valide:
    . nome (string)
    . idade (int)
    . peso (double)
    . ativo (bool, no formato s/n)
    . inicial do nome (char)
*/

using Aula02_OO.Modelo;

// Usuario usuario = new Usuario();

Usuario professor = new();

Console.WriteLine("Digite o nome do professor:");
professor.Nome = Console.ReadLine();

Console.WriteLine("Digite a idade do professor:");
professor.Idade = int.Parse(Console.ReadLine()!);

Console.WriteLine("Digite o peso do professor:");
professor.Peso = double.Parse(Console.ReadLine()!);

Console.WriteLine("Digite a altura do professor:");
professor.Altura = double.Parse(Console.ReadLine()!);

Console.WriteLine("O professor está ativo? (s/n)");
string resposta = Console.ReadLine()!.ToLower();

professor.Ativo = resposta == "s";

Console.WriteLine($"Inicial do nome: {professor.InicialNome}");

var imc = professor.CalcularIMC();

Console.WriteLine($"IMC: {imc:F2}");

// var aluno = new Usuario();

// usuario.Nome = "João";
// usuario.Idade = 30;
// usuario.Peso = 70.5;
// usuario.Ativo = true;

// professor.Nome = "Maria";
// professor.Idade = 40;
// professor.Peso = 65.0;
// professor.Ativo = false;    

// aluno.Nome = "Shi";
// aluno.Idade = 20;
// aluno.Peso = 55.0;
// aluno.Ativo = true;