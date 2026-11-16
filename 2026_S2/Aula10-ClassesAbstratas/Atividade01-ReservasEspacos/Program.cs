using System.Globalization;
using Atividade01_ReservasEspacos.Modelo;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

Console.WriteLine("=============================================================================");
Console.WriteLine("  ATIVIDADE 1 - SISTEMA DE RESERVAS DE ESPAÇOS: COLEÇÕES E POLIMORFISMO     ");
Console.WriteLine("=============================================================================\n");

// ============================================================================
// 1. PREPARAÇÃO: Criando a List<Espaco> e cadastrando pelo menos 6 objetos
// (mínimo: duas salas de estudo, dois auditórios e dois laboratórios)
// ============================================================================
Console.WriteLine("--- 1. Preparação da Coleção List<Espaco> ---");

List<Espaco> espacos = new()
{
    new SalaEstudo(identificacao: "SALA-01", localizacao: "Bloco A - Térreo", capacidade: 4, valorHora: 20.00m),
    new SalaEstudo(identificacao: "SALA-02", localizacao: "Bloco A - 2º Andar", capacidade: 8, valorHora: 35.00m),
    new Auditorio(identificacao: "AUD-01", localizacao: "Prédio Principal", capacidade: 100, valorHora: 120.00m, taxaPreparacao: 50.00m),
    new Auditorio(identificacao: "AUD-NOBRE", localizacao: "Centro de Convenções", capacidade: 250, valorHora: 250.00m, taxaPreparacao: 150.00m),
    new LaboratorioInformatica(identificacao: "LAB-BASICO", localizacao: "Bloco B - Sala 10", capacidade: 20, quantidadeComputadores: 15, valorHoraPorComputador: 3.50m),
    new LaboratorioInformatica(identificacao: "LAB-AVANCADO", localizacao: "Bloco C - Sala 05", capacidade: 35, quantidadeComputadores: 30, valorHoraPorComputador: 5.00m)
};

Console.WriteLine($"[Contagem Inicial] Espaços cadastrados: {espacos.Count}\n");

// ============================================================================
// 2. OPERAÇÕES OBRIGATÓRIAS
// ============================================================================

// 2.1 e 2.2: Contagem, Listagem e Cálculo Individual de 4 horas
// 2.3: Totalização usando acumulador do tipo decimal
Console.WriteLine("--- 2. Listagem, Custos Individuais (4h) e Totalização ---");
decimal custoTotalGeral4h = 0.0m;

Console.WriteLine($"{"Identificação",-15} | {"Tipo",-23} | {"Capacidade",-10} | {"Custo 4 Horas",-14} | Regra Aplicada");
Console.WriteLine(new string('-', 95));

foreach (Espaco espaco in espacos)
{
    decimal custo4h = espaco.CalcularCusto(4);
    custoTotalGeral4h += custo4h;

    string regra = espaco switch
    {
        SalaEstudo se => $"4h × {se.ValorHora:C2}",
        Auditorio au => $"(4h × {au.ValorHora:C2}) + {au.TaxaPreparacao:C2}",
        LaboratorioInformatica lab => $"4h × {lab.QuantidadeComputadores} pcs × {lab.ValorHoraPorComputador:C2}",
        _ => "Regra não mapeada"
    };

    Console.WriteLine($"{espaco.Identificacao,-15} | {espaco.GetType().Name,-23} | {espaco.Capacidade,10} | {custo4h,14:C2} | {regra}");
}

Console.WriteLine(new string('-', 95));
Console.WriteLine($"TOTAL GERAL PARA 4 HORAS DE TODOS OS ESPAÇOS: {custoTotalGeral4h:C2}\n");

// 2.4: Busca utilizando Find() com caso existente e inexistente
Console.WriteLine("--- 3. Busca de Espaços por Identificação com Find() ---");

void BuscarEspaco(string identificacaoBuscada)
{
    Console.WriteLine($"-> Buscando espaço com identificação: \"{identificacaoBuscada}\"...");
    Espaco? encontrado = espacos.Find(espaco => espaco.Identificacao == identificacaoBuscada);

    if (encontrado is not null)
    {
        Console.Write("   [Sucesso]: ");
        encontrado.ExibirInformacoes();
        Console.WriteLine($"   Custo calculado para 4h: {encontrado.CalcularCusto(4):C2}");
    }
    else
    {
        Console.WriteLine($"   [Não Encontrado]: Espaço com identificação \"{identificacaoBuscada}\" não existe na coleção.");
    }
}

// Caso de busca bem-sucedida
BuscarEspaco("AUD-NOBRE");

// Caso de busca sem resultado
BuscarEspaco("SALA-999_INEXISTENTE");

// 2.5: Maior Custo de 4 horas
Console.WriteLine("\n--- 4. Determinação do Espaço com Maior Custo para 4 Horas ---");
Espaco espacoMaisCaro = espacos[0];
decimal maiorCusto = espacoMaisCaro.CalcularCusto(4);

for (int i = 1; i < espacos.Count; i++)
{
    decimal custoAtual = espacos[i].CalcularCusto(4);
    if (custoAtual > maiorCusto)
    {
        maiorCusto = custoAtual;
        espacoMaisCaro = espacos[i];
    }
}

Console.WriteLine($"Espaço mais caro para reserva de 4 horas: {espacoMaisCaro.Identificacao} ({espacoMaisCaro.GetType().Name})");
Console.WriteLine($"Valor correspondente: {maiorCusto:C2} | Localização: {espacoMaisCaro.Localizacao}");

// ============================================================================
// 3. CONSOLIDAÇÃO DA PARTE 2: Validações e Restrições de Classe Abstrata
// ============================================================================
Console.WriteLine("\n--- 5. Consolidação: Restrições de Abstração e Validações de Negócio ---");

Console.WriteLine("Tentativa de instanciar classe abstrata Espaco:");
Console.WriteLine("// Espaco espacoGenerico = new Espaco(\"X-01\", \"Bloco 1\", 10);");
Console.WriteLine("-> Erro de compilação CS0144 mantido documentado: tipos abstratos não podem ser instanciados.\n");

Console.WriteLine("Testes de exceção com entradas inválidas:");
void TestarExcecao(string descricao, Action acao)
{
    try
    {
        acao();
        Console.WriteLine($"  [FALHA]: Nenhuma exceção para {descricao}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"  [OK - Exceção Capturada] {descricao} -> {ex.GetType().Name}: {ex.Message}");
    }
}

TestarExcecao("Horas menores ou iguais a zero no cálculo", () => espacos[0].CalcularCusto(0));
TestarExcecao("Capacidade menor ou igual a zero", () => _ = new SalaEstudo("SALA-X", "Bloco X", 0, 10m));
TestarExcecao("Identificação vazia", () => _ = new Auditorio("   ", "Bloco A", 50, 100m, 50m));
TestarExcecao("Mais computadores que a capacidade", () => _ = new LaboratorioInformatica("LAB-ERR", "Bloco C", 10, 15, 5m));

// ============================================================================
// 4. QUESTÕES PARA REFLEXÃO (PARTE 3)
// ============================================================================
Console.WriteLine("\n=============================================================================");
Console.WriteLine("  RESPOSTAS ÀS QUESTÕES PARA REFLEXÃO (ATIVIDADE 1 - PARTE 3)                ");
Console.WriteLine("=============================================================================");
Console.WriteLine("1. Por que uma única List<Espaco> pode armazenar objetos de três subclasses?");
Console.WriteLine("   R: Porque SalaEstudo, Auditorio e LaboratorioInformatica herdam de Espaco (relação 'é-um'). Em C#, o princípio de substituição e a covariância em tempo de execução permitem armazenar qualquer instância derivada em uma coleção tipada para sua classe base.");
Console.WriteLine("\n2. Por que CalcularCusto(4) executa a regra apropriada sem uma sequência de if para identificar o tipo do espaço?");
Console.WriteLine("   R: Devido ao polimorfismo dinâmico. O método CalcularCusto é abstrato na base e sobrescrito com 'override' em cada subclasse. Em tempo de execução, o CLR consulta a vtable da instância concreta correspondente.");
Console.WriteLine("\n3. Qual é a diferença entre percorrer toda a coleção e localizar apenas um elemento com Find()?");
Console.WriteLine("   R: O percurso com foreach itera incondicionalmente sobre todos os elementos da lista (complexidade O(n) completa). O Find() interrompe a busca assim que encontra o primeiro elemento que satisfaz a condição lambda e o retorna imediatamente, retornando null se não encontrar.");
Console.WriteLine("\n4. O que pode acontecer se o resultado de Find() for utilizado sem verificar se é null?");
Console.WriteLine("   R: Se a busca não encontrar correspondência, o retorno será 'null'. Ao tentar acessar qualquer propriedade ou método desse retorno sem verificação prévia, ocorrerá uma exceção NullReferenceException em tempo de execução.");
