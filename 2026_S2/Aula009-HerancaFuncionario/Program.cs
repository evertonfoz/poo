using System.Globalization;
using Aula009_HerancaFuncionario.Modelo;

// Configura a cultura para formato monetário em Real (R$)
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

Console.WriteLine("==================================================");
Console.WriteLine("       ATIVIDADE 1 - SISTEMA DE FUNCIONÁRIOS      ");
Console.WriteLine("==================================================\n");

// 1. Criação dos objetos
Console.WriteLine("--- 1. Instanciando Funcionário Administrativo e Vendedor ---");
FuncionarioAdministrativo admin = new(
    nome: "Ana Paula Silva",
    matricula: "ADM-101",
    salarioBase: 3500.00m,
    setor: "Recursos Humanos",
    cargo: "Analista de RH"
);

Vendedor vendedor = new(
    nome: "Carlos Eduardo Souza",
    matricula: "VEN-202",
    salarioBase: 2200.00m,
    percentualComissao: 5.0m, // 5%
    totalVendasMes: 45000.00m
);

// 2. Chamada de ExibirDados() herdado da classe base Funcionario
Console.WriteLine("\n--- 2. Dados Gerais (método herdado ExibirDados) ---");
Console.Write("[Administrativo] ");
admin.ExibirDados();

Console.Write("[Vendedor]       ");
vendedor.ExibirDados();

// 3. Apresentação dos dados específicos de cada categoria
Console.WriteLine("\n--- 3. Dados Específicos de Cada Categoria ---");
Console.WriteLine($"[Administrativo] Setor: {admin.Setor} | Cargo: {admin.Cargo}");
Console.WriteLine($"[Vendedor]       Comissão: {vendedor.PercentualComissao}% | Total de Vendas no Mês: {vendedor.TotalVendasMes:C2}");

// 4. Cálculo e apresentação da comissão do vendedor
Console.WriteLine("\n--- 4. Cálculo da Comissão do Vendedor ---");
decimal comissao = vendedor.CalcularComissao();
decimal remuneracaoTotal = vendedor.SalarioBase + comissao;

Console.WriteLine($"Valor da Comissão Calculada: {comissao:C2}");
Console.WriteLine($"Remuneração Total (Salário-base + Comissão): {remuneracaoTotal:C2}");

// 5. Operações de negócio semânticas
Console.WriteLine("\n--- 5. Operações Semânticas de Domínio ---");
Console.WriteLine("Promovendo funcionária administrativa e transferindo de setor...");
admin.Promover(novoCargo: "Coordenadora de Operações", novoSalario: 5200.00m);
admin.TransferirSetor("Operações");
Console.WriteLine($"[Admin atualizado] Cargo: {admin.Cargo} | Setor: {admin.Setor} | Novo Salário: {admin.SalarioBase:C2}");

Console.WriteLine("\nRegistrando nova venda de R$ 15.000,00 para o vendedor...");
vendedor.RegistrarVenda(15000.00m);
Console.WriteLine($"[Vendedor atualizado] Total Vendas: {vendedor.TotalVendasMes:C2} | Nova Comissão: {vendedor.CalcularComissao():C2}");

// Demonstração de validação de dados inválidos
Console.WriteLine("\n--- 6. Validação e Tratamento de Exceções ---");
try
{
    Console.WriteLine("Tentando criar um funcionário com nome vazio...");
    _ = new FuncionarioAdministrativo("", "ADM-999", 3000m, "Financeiro", "Assistente");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"[Exceção capturada com sucesso]: {ex.Message}");
}

try
{
    Console.WriteLine("\nTentando reajustar salário para valor negativo (-100,00)...");
    admin.ReajustarSalario(-100.00m);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"[Exceção capturada com sucesso]: {ex.Message}");
}

try
{
    Console.WriteLine("\nTentando registrar venda com valor negativo (-500,00)...");
    vendedor.RegistrarVenda(-500.00m);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"[Exceção capturada com sucesso]: {ex.Message}");
}

Console.WriteLine("\n==================================================");
Console.WriteLine("             Fim da Demonstração                  ");
Console.WriteLine("==================================================");
