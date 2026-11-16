using System.Globalization;
using Atividade02_PersonagensJogo.Modelo;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

Console.WriteLine("=============================================================================");
Console.WriteLine("  ATIVIDADE 2 - SISTEMA DE PERSONAGENS DE UM JOGO: COLEÇÕES E POLIMORFISMO   ");
Console.WriteLine("=============================================================================\n");

// ============================================================================
// 1. PREPARAÇÃO: Criando a List<Personagem> com pelo menos 8 personagens
// distribuídos entre as classes implementadas
// ============================================================================
Console.WriteLine("--- 1. Preparação da Coleção List<Personagem> (8 Personagens) ---");

List<Personagem> personagens = new()
{
    new Personagem(nome: "Arthur (Aldeão)", pontosVida: 80, forcaAtaque: 12),
    new Guerreiro(nome: "Conan (Guerreiro)", pontosVida: 180, forcaAtaque: 35, bonusEspada: 20),
    new Guerreiro(nome: "Ragnar (Guerreiro)", pontosVida: 160, forcaAtaque: 32, bonusEspada: 18),
    new Mago(nome: "Gandalf (Mago)", pontosVida: 110, forcaAtaque: 18, manaAtual: 50, custoManaAtaque: 20, bonusMagico: 35),
    new Mago(nome: "Merlin (Mago)", pontosVida: 95, forcaAtaque: 15, manaAtual: 30, custoManaAtaque: 20, bonusMagico: 40),
    new Mago(nome: "Saruman (Mago)", pontosVida: 105, forcaAtaque: 22, manaAtual: 15, custoManaAtaque: 20, bonusMagico: 30), // Mana insuficiente para 1º ataque
    new Arqueiro(nome: "Legolas (Arqueiro)", pontosVida: 130, forcaAtaque: 28, bonusCritico: 25),
    new Paladino(nome: "Uther (Paladino)", pontosVida: 210, forcaAtaque: 30, bonusEspada: 15, bonusSagrado: 25)
};

Console.WriteLine($"[Contagem Inicial] Total de personagens no grupo: {personagens.Count}\n");

// ============================================================================
// 2. OPERAÇÕES OBRIGATÓRIAS
// ============================================================================

// 2.1: Listar com foreach e executar ExibirStatus()
Console.WriteLine("--- 2. Listagem de Status de Todos os Personagens ---");
foreach (Personagem p in personagens)
{
    p.ExibirStatus();
}

// 2.2: Atacar no percurso e apresentar dano devolvido (observando consumo de mana dos magos)
Console.WriteLine("\n--- 3. Execução dos Ataques em Lote (Método Virtual/Override Atacar) ---");
foreach (Personagem p in personagens)
{
    Console.Write($"[{p.GetType().Name}] {p.Nome} atacou -> ");
    int dano = p.Atacar();
    Console.WriteLine($"   => Dano final desferido: {dano}");
}

// 2.3: Buscar com Find() pelo nome: teste positivo e teste negativo
Console.WriteLine("\n--- 4. Busca de Personagem por Nome com Find() ---");

void BuscarPersonagem(string nomeBuscado)
{
    Console.WriteLine($"-> Buscando personagem com nome: \"{nomeBuscado}\"...");
    Personagem? encontrado = personagens.Find(p => p.Nome == nomeBuscado);

    if (encontrado is not null)
    {
        Console.Write("   [Sucesso]: ");
        encontrado.ExibirStatus();
    }
    else
    {
        Console.WriteLine($"   [Não Encontrado]: Nenhum personagem com o nome \"{nomeBuscado}\" foi localizado.");
    }
}

BuscarPersonagem("Legolas (Arqueiro)");
BuscarPersonagem("Voldemort (Inexistente)");

// 2.4: Comparar para encontrar o personagem com maior ForcaAtaque básica
Console.WriteLine("\n--- 5. Comparação: Personagem com Maior ForcaAtaque Básica ---");
Personagem maisForte = personagens[0];

for (int i = 1; i < personagens.Count; i++)
{
    if (personagens[i].ForcaAtaque > maisForte.ForcaAtaque)
    {
        maisForte = personagens[i];
    }
}

Console.WriteLine($"Personagem com maior força de ataque básica: {maisForte.Nome}");
Console.WriteLine($"Força básica de ataque: {maisForte.ForcaAtaque} (Atenção: força básica não se confunde com o dano de ataque especializado)");

// 2.5: Filtrar sobreviventes com FindAll() (PontosVida > 0)
Console.WriteLine("\n--- 6. Filtragem de Sobreviventes com FindAll() (PontosVida > 0) ---");
// Simulando combate prévio: um personagem sofre dano fatal
Console.WriteLine("Simulando batalha: Arthur (Aldeão) recebe 80 de dano fatal...");
Personagem? aldeao = personagens.Find(p => p.Nome.StartsWith("Arthur"));
aldeao?.ReceberDano(80);

List<Personagem> sobreviventes = personagens.FindAll(p => p.PontosVida > 0);

Console.WriteLine($"\nTotal na lista original: {personagens.Count}");
Console.WriteLine($"Total de sobreviventes (PontosVida > 0): {sobreviventes.Count}");
Console.WriteLine("Integrantes sobreviventes:");
foreach (Personagem s in sobreviventes)
{
    Console.WriteLine($"  - {s.Nome} | Vida: {s.PontosVida}");
}

// 2.6: Remover da lista original com Remove()
Console.WriteLine("\n--- 7. Remoção de Personagem com Remove() ---");
string nomeParaRemover = "Ragnar (Guerreiro)";
Console.WriteLine($"Tentando remover \"{nomeParaRemover}\" da lista principal...");
Console.WriteLine($"Contagem antes da remoção: {personagens.Count}");

Personagem? personagemParaRemover = personagens.Find(p => p.Nome == nomeParaRemover);
if (personagemParaRemover is not null)
{
    bool removido = personagens.Remove(personagemParaRemover);
    Console.WriteLine($"Removido com sucesso? {removido}");
}
Console.WriteLine($"Contagem após a remoção: {personagens.Count}");

// Verificando que o objeto continua existindo na lista de sobreviventes
bool aindaEstaNosSobreviventes = sobreviventes.Contains(personagemParaRemover!);
Console.WriteLine($"O personagem \"{nomeParaRemover}\" ainda existe na lista de sobreviventes? {aindaEstaNosSobreviventes}");
Console.WriteLine("-> Demonstração: FindAll() copia referências para uma nova lista; remover de uma não afeta a outra.");

// 2.7: Identificar tipos com o operador 'is'
Console.WriteLine("\n--- 8. Contagem de Tipos Concretos com Operador 'is' ---");
int contagemMagos = 0;
int contagemGuerreiros = 0;
int contagemArqueiros = 0;
int contagemPaladinos = 0;
int contagemComuns = 0;

foreach (Personagem p in personagens)
{
    if (p is Paladino) contagemPaladinos++;
    else if (p is Guerreiro) contagemGuerreiros++;
    else if (p is Mago) contagemMagos++;
    else if (p is Arqueiro) contagemArqueiros++;
    else contagemComuns++;
}

Console.WriteLine($"Total de Magos no grupo: {contagemMagos}");
Console.WriteLine($"Total de Guerreiros (não Paladinos): {contagemGuerreiros}");
Console.WriteLine($"Total de Paladinos: {contagemPaladinos}");
Console.WriteLine($"Total de Arqueiros: {contagemArqueiros}");
Console.WriteLine($"Total de Personagens comuns: {contagemComuns}");

// ============================================================================
// 3. CONSOLIDAÇÃO DA PARTE 2: Sobrecarga do Arqueiro e Validações
// ============================================================================
Console.WriteLine("\n--- 9. Consolidação da Parte 2: Sobrecarga do Arqueiro e Validações ---");
Arqueiro? legolas = personagens.Find(p => p is Arqueiro) as Arqueiro;
if (legolas is not null)
{
    Console.WriteLine("Ataque normal do arqueiro: " + legolas.Atacar());
    Console.WriteLine("Ataque com sobrecarga sem crítico Atacar(false): " + legolas.Atacar(false));
    Console.WriteLine("Ataque com sobrecarga com crítico Atacar(true): " + legolas.Atacar(true));
}

// ============================================================================
// 4. QUESTÕES PARA REFLEXÃO (PARTE 3)
// ============================================================================
Console.WriteLine("\n=============================================================================");
Console.WriteLine("  RESPOSTAS ÀS QUESTÕES PARA REFLEXÃO (ATIVIDADE 2 - PARTE 3)                ");
Console.WriteLine("=============================================================================");
Console.WriteLine("1. Por que um percurso de List<Personagem> permite executar Atacar() com implementações diferentes?");
Console.WriteLine("   R: Porque o método Atacar() foi declarado 'virtual' na classe base Personagem e 'override' nas subclasses. Em cada iteração, o runtime consulta a tabela virtual (vtable) do objeto instanciado, executando a lógica especializada de cada subclasse por polimorfismo dinâmico.");
Console.WriteLine("\n2. Qual é a diferença entre Find() e FindAll()?");
Console.WriteLine("   R: Find() avalia os elementos até encontrar o primeiro que satisfaz a condição lambda e o retorna individualmente (ou null se não encontrar). Já FindAll() percorre toda a coleção e retorna uma NOVA List<T> contendo as referências de todos os elementos que atenderam ao critério.");
Console.WriteLine("\n3. O que acontece com um objeto quando removemos sua referência de uma lista, mas ele continua referenciado em outra?");
Console.WriteLine("   R: O objeto permanece vivo e acessível na memória enquanto houver pelo menos uma referência ativa apontando para ele (como na lista de sobreviventes). Ele só será elegível para coleta de lixo (Garbage Collector) quando todas as referências forem desfeitas.");
Console.WriteLine("\n4. Por que uma verificação 'is Mago' pode ser útil para contar tipos, mas não é necessária para chamar o método virtual Atacar()?");
Console.WriteLine("   R: Porque a contagem exige inspeção de metadados em tempo de compilação/execução sobre a classe concreta. Já a chamada a Atacar() é polimórfica: o próprio mecanismo virtual do C# se encarrega de encontrar e invocar o método correto sem necessidade de checagens manuais com 'is' ou 'switch'.");
