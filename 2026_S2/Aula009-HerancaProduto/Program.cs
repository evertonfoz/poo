using System.Globalization;
using Aula009_HerancaProduto.Modelo;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

Console.WriteLine("==================================================");
Console.WriteLine("    ATIVIDADE 2 - SISTEMA DE PRODUTOS COMERCIAIS  ");
Console.WriteLine("==================================================\n");

// 1. Criação de dois produtos eletrônicos e dois alimentícios
Console.WriteLine("--- 1. Instanciando 2 Eletrônicos e 2 Alimentícios ---");
ProdutoEletronico tv = new(
    codigo: "EL-001",
    descricao: "Smart TV 55 Polegadas 4K",
    precoBase: 2899.90m,
    marca: "Samsung",
    garantiaMeses: 12
);

ProdutoEletronico fone = new(
    codigo: "EL-002",
    descricao: "Headphone Bluetooth Noise Cancelling",
    precoBase: 450.00m,
    marca: "Sony",
    garantiaMeses: 6
);

ProdutoAlimenticio leite = new(
    codigo: "AL-101",
    descricao: "Leite Integral Pasteurizado 1L",
    precoBase: 5.49m,
    dataValidade: DateTime.Today.AddDays(7), // Válido por mais 7 dias
    precisaRefrigeracao: true
);

ProdutoAlimenticio iogurte = new(
    codigo: "AL-102",
    descricao: "Iogurte Natural 500g",
    precoBase: 8.90m,
    dataValidade: DateTime.Today.AddDays(-2), // Vencido há 2 dias
    precisaRefrigeracao: true
);

// 2. Exibição das informações comuns por meio do método herdado ExibirInformacoes()
Console.WriteLine("\n--- 2. Informações Gerais (método herdado ExibirInformacoes) ---");
tv.ExibirInformacoes();
fone.ExibirInformacoes();
leite.ExibirInformacoes();
iogurte.ExibirInformacoes();

// 3. Execução dos métodos próprios de cada categoria
Console.WriteLine("\n--- 3. Métodos Próprios de Cada Categoria ---");
Console.WriteLine("[Método ExibirGarantia em Eletrônicos]");
tv.ExibirGarantia();
fone.ExibirGarantia();

Console.WriteLine("\n[Método EstaVencido em Alimentícios]");
Console.WriteLine($"{leite.Descricao}: Validade {leite.DataValidade:dd/MM/yyyy} | Refrigerado: {(leite.PrecisaRefrigeracao ? "Sim" : "Não")} | Está vencido? {(leite.EstaVencido() ? "SIM" : "NÃO")}");
Console.WriteLine($"{iogurte.Descricao}: Validade {iogurte.DataValidade:dd/MM/yyyy} | Refrigerado: {(iogurte.PrecisaRefrigeracao ? "Sim" : "Não")} | Está vencido? {(iogurte.EstaVencido() ? "SIM" : "NÃO")}");

// 4. Demonstração dos métodos semânticos de negócio pós-construção
Console.WriteLine("\n--- 4. Operações de Domínio com Métodos Semânticos ---");
Console.WriteLine("Aplicando promoção no preço da TV e estendendo sua garantia...");
tv.AtualizarPreco(2599.00m);
tv.EstenderGarantia(12); // Ganha mais 12 meses de garantia estendida
Console.WriteLine($"[TV Atualizada] Novo Preço: {tv.PrecoBase:C2} | Nova Garantia Total: {tv.GarantiaMeses} meses");

Console.WriteLine("\nAtualizando preço do leite por conta de reajuste de safra...");
leite.AtualizarPreco(5.99m);
Console.WriteLine($"[Leite Atualizado] Novo Preço: {leite.PrecoBase:C2}");

// 5. Teste de dados inválidos com tratamento de exceções
Console.WriteLine("\n--- 5. Teste de Dados Inválidos e Tratamento de Exceções ---");

try
{
    Console.WriteLine("Tentando criar Produto com preço-base zero ou negativo (-10.00)...");
    _ = new ProdutoEletronico("EL-ERR", "Mouse Sem Fio", -10.00m, "Logitech", 12);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"[Exceção capturada com sucesso]: {ex.Message}");
}

try
{
    Console.WriteLine("\nTentando atualizar o preço da TV para zero via AtualizarPreco(0)...");
    tv.AtualizarPreco(0m);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"[Exceção capturada com sucesso]: {ex.Message}");
}

try
{
    Console.WriteLine("\nTentando criar ProdutoEletronico com marca vazia...");
    _ = new ProdutoEletronico("EL-ERR2", "Teclado Mecânico", 250.00m, "", 12);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"[Exceção capturada com sucesso]: {ex.Message}");
}

try
{
    Console.WriteLine("\nTentando criar ProdutoAlimenticio sem data de validade informada...");
    _ = new ProdutoAlimenticio("AL-ERR", "Pão de Forma", 7.50m, default, false);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"[Exceção capturada com sucesso]: {ex.Message}");
}

Console.WriteLine("\n==================================================");
Console.WriteLine("             Fim da Demonstração                  ");
Console.WriteLine("==================================================");
