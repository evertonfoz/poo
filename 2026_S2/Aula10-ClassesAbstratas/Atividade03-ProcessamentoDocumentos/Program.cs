using System.Globalization;
using Atividade03_ProcessamentoDocumentos.Modelo;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

Console.WriteLine("=============================================================================");
Console.WriteLine("  ATIVIDADE 3 - PROCESSAMENTO DE DOCUMENTOS EM LOTE: COLEÇÕES E POLIMORFISMO ");
Console.WriteLine("=============================================================================\n");

// ============================================================================
// 1. PREPARAÇÃO: Criando a List<Documento> com pelo menos 9 objetos
// (3 relatórios financeiros, 3 documentos de texto e 3 planilhas)
// Com repetição de autor e datas variadas para permitir filtros e ordenação
// ============================================================================
Console.WriteLine("--- 1. Preparação da Coleção List<Documento> (9 Documentos) ---");

List<Documento> documentos = new()
{
    // Relatórios Financeiros
    new RelatorioFinanceiro(
        titulo: "Relatório de Faturamento 2025",
        autor: "Carlos Silveira",
        dataCriacao: new DateTime(2026, 1, 10),
        receitaTotal: 180000m,
        despesaTotal: 110000m),

    new RelatorioFinanceiro(
        titulo: "Prestação de Contas Auditada",
        autor: "Maria Clara",
        dataCriacao: new DateTime(2026, 2, 1),
        receitaTotal: 95000m,
        despesaTotal: 72000m),

    new RelatorioFinanceiro(
        titulo: "Balanço Patrimonial Q1",
        autor: "Carlos Silveira", // Mesmo autor
        dataCriacao: new DateTime(2026, 3, 5),
        receitaTotal: 250000m,
        despesaTotal: 140000m),

    // Documentos de Texto
    new DocumentoTexto(
        titulo: "Contrato de Prestação de Serviços",
        autor: "Fernanda Jurídico",
        dataCriacao: new DateTime(2026, 1, 15),
        conteudo: "O presente contrato rege os termos e condições gerais acordados entre as partes contratantes."),

    new DocumentoTexto(
        titulo: "Ata da Reunião de Diretoria",
        autor: "Carlos Silveira", // Mesmo autor
        dataCriacao: new DateTime(2026, 2, 10),
        conteudo: "A diretoria reuniu-se para discutir as metas de inovação e novos investimentos estruturais."),

    new DocumentoTexto(
        titulo: "Manual de Boas Práticas",
        autor: "Fernanda Jurídico",
        dataCriacao: new DateTime(2026, 2, 25),
        conteudo: "Diretrizes de conformidade regulatória e segurança das informações corporativas."),

    // Planilhas
    new Planilha(
        titulo: "Controle de Estoque Geral",
        autor: "Roberto Almoxarifado",
        dataCriacao: new DateTime(2026, 1, 5),
        quantidadeLinhas: 120,
        quantidadeColunas: 8),

    new Planilha(
        titulo: "Planejamento Tributário",
        autor: "Maria Clara", // Mesmo autor
        dataCriacao: new DateTime(2026, 2, 18),
        quantidadeLinhas: 60,
        quantidadeColunas: 15),

    new Planilha(
        titulo: "Fluxo de Caixa Operacional",
        autor: "Roberto Almoxarifado",
        dataCriacao: new DateTime(2026, 3, 1),
        quantidadeLinhas: 200,
        quantidadeColunas: 10)
};

Console.WriteLine($"[Contagem Inicial] Documentos carregados: {documentos.Count}\n");

// ============================================================================
// 2. OPERAÇÕES OBRIGATÓRIAS
// ============================================================================

// 2.1: Processar Lote (executa Processar() e ExibirResumo() com foreach)
Console.WriteLine("--- 2. Processamento em Lote (Processar() e ExibirResumo()) ---");
int contador = 1;
foreach (Documento doc in documentos)
{
    Console.WriteLine($"\n[{contador++}] Tipo Real: {doc.GetType().Name} | Tipo da Referência: Documento");
    doc.Processar();    // Despacho virtual polimórfico (abstrato na base)
    doc.ExibirResumo(); // Virtual na base; note o comportamento na Planilha devido ao 'new'!
}

// 2.2: Busca por título utilizando Find() (teste positivo e negativo)
Console.WriteLine("\n--- 3. Busca de Documento por Título com Find() ---");

void BuscarDocumento(string tituloBuscado)
{
    Console.WriteLine($"-> Buscando documento: \"{tituloBuscado}\"...");
    Documento? encontrado = documentos.Find(d => d.Titulo.Equals(tituloBuscado, StringComparison.OrdinalIgnoreCase));

    if (encontrado is not null)
    {
        Console.Write("   [Sucesso]: ");
        encontrado.ExibirResumo();
    }
    else
    {
        Console.WriteLine($"   [Não Encontrado]: Nenhum documento com o título \"{tituloBuscado}\" foi localizado.");
    }
}

BuscarDocumento("Ata da Reunião de Diretoria");
BuscarDocumento("Documento Fantasma Inexistente");

// 2.3: Filtro com FindAll() por autor determinado
Console.WriteLine("\n--- 4. Filtro por Autor com FindAll() ---");
string autorFiltro = "Carlos Silveira";
List<Documento> documentosDoAutor = documentos.FindAll(d => d.Autor == autorFiltro);

Console.WriteLine($"Total de documentos produzidos por \"{autorFiltro}\": {documentosDoAutor.Count}");
foreach (Documento doc in documentosDoAutor)
{
    Console.WriteLine($"  - [{doc.GetType().Name}] {doc.Titulo} ({doc.DataCriacao:dd/MM/yyyy})");
}

// 2.4: Contagens utilizando o operador 'is'
Console.WriteLine("\n--- 5. Contagens de Tipos Concretos com Operador 'is' ---");
int totalRelatorios = 0;
int totalTextos = 0;
int totalPlanilhas = 0;

foreach (Documento d in documentos)
{
    if (d is RelatorioFinanceiro) totalRelatorios++;
    else if (d is DocumentoTexto) totalTextos++;
    else if (d is Planilha) totalPlanilhas++;
}

Console.WriteLine($"Total geral de documentos: {documentos.Count}");
Console.WriteLine($"  * Relatórios Financeiros : {totalRelatorios}");
Console.WriteLine($"  * Documentos de Texto    : {totalTextos}");
Console.WriteLine($"  * Planilhas              : {totalPlanilhas}");

// 2.5: Remoção com Remove() e exibição da contagem atualizada
Console.WriteLine("\n--- 6. Remoção de Documento com Remove() ---");
string tituloRemocao = "Contrato de Prestação de Serviços";
Console.WriteLine($"Contagem antes da remoção: {documentos.Count}");

Documento? docRemover = documentos.Find(d => d.Titulo == tituloRemocao);
if (docRemover is not null)
{
    bool foiRemovido = documentos.Remove(docRemover);
    Console.WriteLine($"Documento \"{tituloRemocao}\" foi removido? {foiRemovido}");
}
Console.WriteLine($"Contagem após a remoção: {documentos.Count}");

// 2.6: Ordenação da lista original por DataCriacao com Sort()
Console.WriteLine("\n--- 7. Ordenação por Data de Criação com Sort() (Mais Antigo ao Mais Recente) ---");
documentos.Sort((a, b) => a.DataCriacao.CompareTo(b.DataCriacao));

Console.WriteLine("Documentos após ordenação cronológica:");
foreach (Documento d in documentos)
{
    Console.WriteLine($"  - {d.DataCriacao:dd/MM/yyyy} | [{d.GetType().Name,-20}] {d.Titulo}");
}

// ============================================================================
// 3. INVESTIGAÇÃO FINAL: O EXPERIMENTO OVERRIDE E NEW EM UMA COLEÇÃO
// ============================================================================
Console.WriteLine("\n=============================================================================");
Console.WriteLine("  INVESTIGAÇÃO FINAL: EXPERIMENTO OVERRIDE E NEW EM UMA COLEÇÃO               ");
Console.WriteLine("=============================================================================");

Console.WriteLine("\n--- Etapa A: Chamadas por Referência da Classe Base (Documento) ---");
Console.WriteLine("Previsão antes da execução:");
Console.WriteLine("  * RelatorioFinanceiro: executa método da subclasse (pois usou override).");
Console.WriteLine("  * DocumentoTexto: executa método da base Documento (pois herda diretamente).");
Console.WriteLine("  * Planilha: executa método da base Documento (pois usou new e a referência é Documento!).\n");

foreach (Documento documento in documentos)
{
    Console.WriteLine($"Objeto real: {documento.GetType().Name}");
    Console.Write("  -> Processar(): ");
    documento.Processar();
    Console.Write("  -> ExibirResumo() via ref Documento: ");
    documento.ExibirResumo();
    Console.WriteLine();
}

Console.WriteLine("--- Etapa B: Chamadas por Referência Concreta (Planilha) ---");
Console.WriteLine("Filtrando e invocando ExibirResumo() através da variável de tipo Planilha:");
foreach (Documento documento in documentos)
{
    if (documento is Planilha planilha)
    {
        Console.WriteLine($"\nDocumento: \"{planilha.Titulo}\"");
        Console.Write("  Chamada via referência Planilha: ");
        planilha.ExibirResumo();
    }
}

Console.WriteLine("\n--- Etapa C: Análise Teórica: Transformando Ocultação (new) em Sobrescrita (override) ---");
Console.WriteLine("""
====================================================================================================
QUADRO DE PREVISÃO E OBSERVAÇÃO:
----------------------------------------------------------------------------------------------------
Objeto Real            | ExibirResumo via Documento (Etapa A)  | ExibirResumo via Tipo Concreto (Etapa B)
----------------------------------------------------------------------------------------------------
RelatorioFinanceiro    | Resumo estendido (Subclasse/override) | Resumo estendido (Subclasse/override)
DocumentoTexto         | Resumo padrão da base [Documento]     | Resumo padrão da base [Documento]
Planilha com 'new'     | Resumo padrão da base [Documento] (!) | «Resumo da planilha: ...» (Planilha)
Planilha com 'override'| «Resumo da planilha: ...» (Planilha)  | «Resumo da planilha: ...» (Planilha)
====================================================================================================

JUSTIFICATIVA DAS DIFERENÇAS:
- Com 'new' (ocultação): a escolha do método é feita em tempo de compilação (early binding) baseando-se
  no TIPO DA VARIÁVEL. Por isso, quando a coleção é List<Documento>, o C# chama Documento.ExibirResumo().
- Com 'override' (sobrescrita): a escolha é dinâmica em tempo de execução (late binding / vtable). O C#
  sempre executa a implementação da instância real em memória, mesmo dentro de List<Documento>.
""");

// ============================================================================
// 4. QUESTÕES PARA REFLEXÃO (PARTE 3)
// ============================================================================
Console.WriteLine("=============================================================================");
Console.WriteLine("  RESPOSTAS ÀS QUESTÕES PARA REFLEXÃO (ATIVIDADE 3 - PARTE 3)                ");
Console.WriteLine("=============================================================================");
Console.WriteLine("1. Por que Documento pode ser o tipo da lista mesmo sendo uma classe abstrata?");
Console.WriteLine("   R: Porque o tipo de uma coleção List<T> restringe os tipos de REFERÊNCIAS aceitos, e não cria instâncias diretas do tipo genérico T. Toda subclasse concreta de Documento é um Documento válido pelo princípio da substituição.");
Console.WriteLine("\n2. Qual é a diferença entre o despacho de Processar() e a ocultação de ExibirResumo() em Planilha?");
Console.WriteLine("   R: Processar() é abstrato na base e implementado com override em Planilha, participando da vtable polimórfica (despacho dinâmico). Já ExibirResumo() foi ocultado com 'new', criando um método desvinculado da vtable que só é visível quando a referência é do tipo estático Planilha.");
Console.WriteLine("\n3. Quais são as diferenças de resultado entre o método ocultado com new e o sobrescrito com override?");
Console.WriteLine("   R: O método sobrescrito com 'override' é executado em qualquer contexto polimórfico (inclusive através de List<Documento>). O método ocultado com 'new' só é executado se a referência for explicitamente do tipo da classe filha (ou após coerção de tipo).");
Console.WriteLine("\n4. Por que a ordem dos documentos muda após Sort(), mas a implementação de seus métodos não muda?");
Console.WriteLine("   R: Sort() apenas rearranja as posições dos ponteiros/referências dos objetos dentro dos índices da lista em memória. Os objetos reais instanciados no Heap continuam exatamente iguais, preservando suas tabelas virtuais e comportamentos originais.");
