using System.Diagnostics;

// Objetivo da aula:
// 1) Entender o que bloqueia a aplicacao (sync)
// 2) Entender o que nao bloqueia (async/await)
// 3) Ver quando ha ganho real de tempo (WhenAll)
Console.WriteLine("=== Introducao a async/await (uso real) ===");
Console.WriteLine("Cenario: montar uma tela consultando Perfil, Pedidos e Notificacoes.");

// Executa as tres abordagens para comparacao direta.
ExecutarVersaoSincrona();
await ExecutarVersaoAsyncSequencial();
await ExecutarVersaoAsyncParalela();

static void ExecutarVersaoSincrona()
{
    Console.WriteLine("\n1) Versao SINCRONA (bloqueia a thread)");
    var cronometro = Stopwatch.StartNew();

    // Aqui cada chamada termina para so depois iniciar a proxima.
    // O tempo total tende a ser a soma de todos os tempos.
    var perfil = BuscarDadosSincrono("Perfil", 2000);
    var pedidos = BuscarDadosSincrono("Pedidos", 2500);
    var notificacoes = BuscarDadosSincrono("Notificacoes", 1500);

    cronometro.Stop();
    Console.WriteLine($"Resultado: {perfil}, {pedidos}, {notificacoes}");
    Console.WriteLine($"Tempo total (sincrono): {cronometro.ElapsedMilliseconds} ms");
}

static async Task ExecutarVersaoAsyncSequencial()
{
    Console.WriteLine("\n2) Versao ASYNC + AWAIT sequencial (nao bloqueia, mas ainda soma tempos)");
    var cronometro = Stopwatch.StartNew();

    // await libera a thread enquanto aguarda a operacao de I/O.
    // Porem, como o await esta em sequencia, o tempo total ainda soma.
    var perfil = await BuscarDadosAsync("Perfil", 2000);
    Console.WriteLine("Aplicacao segue responsiva enquanto aguarda...");
    var pedidos = await BuscarDadosAsync("Pedidos", 2500);
    var notificacoes = await BuscarDadosAsync("Notificacoes", 1500);

    cronometro.Stop();
    Console.WriteLine($"Resultado: {perfil}, {pedidos}, {notificacoes}");
    Console.WriteLine($"Tempo total (async sequencial): {cronometro.ElapsedMilliseconds} ms");
}

static async Task ExecutarVersaoAsyncParalela()
{
    Console.WriteLine("\n3) Versao ASYNC paralela com Task.WhenAll (uso comum em APIs/arquivos)");
    var cronometro = Stopwatch.StartNew();

    // Inicia as tres tarefas sem aguardar imediatamente.
    // Isso faz as operacoes acontecerem em paralelo (concorrencia de I/O).
    var perfilTask = BuscarDadosAsync("Perfil", 2000);
    var pedidosTask = BuscarDadosAsync("Pedidos", 2500);
    var notificacoesTask = BuscarDadosAsync("Notificacoes", 1500);

    // Aguarda todas juntas.
    // O tempo total tende a se aproximar da tarefa mais demorada.
    var resultados = await Task.WhenAll(perfilTask, pedidosTask, notificacoesTask);

    cronometro.Stop();
    Console.WriteLine($"Resultado: {string.Join(", ", resultados)}");
    Console.WriteLine($"Tempo total (async paralelo): {cronometro.ElapsedMilliseconds} ms");
}

static string BuscarDadosSincrono(string recurso, int tempoMs)
{
    Console.WriteLine($"[SYNC] Buscando {recurso}...");
    // Sleep bloqueia a thread atual: a aplicacao "para" aqui.
    Thread.Sleep(tempoMs);
    Console.WriteLine($"[SYNC] {recurso} carregado.");
    return recurso;
}

static async Task<string> BuscarDadosAsync(string recurso, int tempoMs)
{
    Console.WriteLine($"[ASYNC] Buscando {recurso}...");
    // Delay simula espera de I/O sem bloquear a thread.
    await Task.Delay(tempoMs);
    Console.WriteLine($"[ASYNC] {recurso} carregado.");
    return recurso;
}