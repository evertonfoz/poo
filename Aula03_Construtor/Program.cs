var conta = new ContaBancaria("12345-6");

// conta.NumeroConta = "12345-6";
// conta.Saldo = 1000.00m;
conta.Depositar(-1000.00m);

Console.WriteLine($"Número da conta: {conta.NumeroConta}");
Console.WriteLine($"Saldo: {conta.Saldo}");

var bancoInternacional = new Banco("123", "Banco Internacional"); ;

Console.WriteLine($"Número do banco: {bancoInternacional.Numero}");
Console.WriteLine($"Nome do banco: {bancoInternacional.Nome}");


try
{
    var banco = new Banco("", "Banco do Brasil");
    // banco.AtualizarContato("(11 1234-5678");
    // Console.WriteLine($"Contato atualizado: {banco.Contato}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}

// banco.AtualizarContato("(11) 1234-5678"); 
// { 
//     Contato = "(11) 1234-5678" 
// };