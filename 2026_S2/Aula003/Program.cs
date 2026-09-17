/*
  Enunciado: Peça a idade do usuário e só aceite inteiro válido. Ao final, mostre:
  . a idade
  . a idade daqui a 5 anos
*/

// Console.Write("Digite sua idade: ");
// string input = Console.ReadLine();
// int idade;
// bool isInt = int.TryParse(input, out idade);

// Console.WriteLine($"Idade atual: {idade}");
// Console.WriteLine($"isInt: {isInt}");


int idade;
while (true)
{
    Console.Write("Digite sua idade: ");
    string input = Console.ReadLine();
    if (int.TryParse(input, out idade))
    {
        if (idade <= 0)
        {
            Console.WriteLine("Por favor, digite um número inteiro válido (idade não pode ser negativa ou igual a ZERO).");
            continue;
        }
        break;
    }
    else
    {
        Console.WriteLine("Por favor, digite um número inteiro válido.");
    }
}   

Console.WriteLine($"Idade atual: {idade}");
int idadeDaqui5Anos = idade + 5;
Console.WriteLine($"Idade daqui a 5 anos: {idadeDaqui5Anos}");

// double altura;
// Console.Write("Digite sua altura (em metros): ");
// while ((!double.TryParse(Console.ReadLine(), out altura)) || altura <= 0 || altura > 3)
// {
//     Console.WriteLine("Por favor, digite uma altura válida (em metros).");
//     Console.Write("Digite sua altura (em metros): ");
// }
