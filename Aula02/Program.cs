/* Enunciado

    Crie um programa que peça o nome do usuário e mostre uma mensagem de boas-vindas contendo:
    . nome completo
    . a inicial do nome (primeira letra)
    . a data e hora atuais

    Proposta descritiva para resolução

    Ler nome como string.
    Se o nome estiver vazio, usar ? como inicial.
    Caso contrário, usar nome[0].
    Guardar DateTime.Now.
    Imprimir tudo com interpolação $"...". 
*/

// string? nome=null;

// while (string.IsNullOrEmpty(nome))
// {
//     Console.Write("Digite seu nome completo: ");
//     nome = Console.ReadLine() ?? "";

//     if (string.IsNullOrEmpty(nome))
//     {
//         Console.WriteLine("O nome não pode ser vazio. Por favor, tente novamente.");
//     }
// }

string? nome;

do
{
    Console.Write("Digite seu nome completo: ");
    nome = Console.ReadLine();

    if (string.IsNullOrEmpty(nome))
    {
        Console.WriteLine("O nome não pode ser vazio. Por favor, tente novamente.");
    }
} while (string.IsNullOrEmpty(nome));

char inicial = string.IsNullOrEmpty(nome) ? '?' : nome[0];

DateTime agora = DateTime.Now;

Console.WriteLine($"Bem-vindo, {nome}! Sua inicial é {inicial} e a hora atual é {agora}.");
