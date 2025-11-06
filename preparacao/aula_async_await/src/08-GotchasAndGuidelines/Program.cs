using System;
using System.Threading.Tasks;
using System.Diagnostics;

/*
 * Checklist executável de anti-patterns e boas práticas (async/await)
 * - evitar async void (exceto em handlers de evento em UIs) — exceções ficam fora do fluxo de chamada;
 * - evitar .Result / .Wait() em código que pode estar em presença de SynchronizationContext (pode deadlockar);
 * - sufixar métodos assíncronos com 'Async' para clareza (FindAsync, GetByIdAsync);
 * - em bibliotecas, use ConfigureAwait(false) para não capturar contexto do chamador;
 *
 * Perguntas-guia:
 * 1) Por que .Result pode deadlockar? (em ambientes com SynchronizationContext, o await tenta retornar ao contexto, mas a thread está bloqueada por .Result);
 * 2) Quando é aceitável usar async void? (somente em handlers de eventos que exigem assinatura void e não têm chamador para await);
 * 3) O que ConfigureAwait(false) evita? (captura de contexto desnecessária em bibliotecas que não dependem de um contexto específico).
 */

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Demo: Gotchas and Guidelines for async/await\n");
        PrintChecklist();

        await RunExceptionAwaitVsWaitAsync();
        RunBlockingResultExample();
        await RunConfigureAwaitExampleAsync();
        await RunFireAndForgetExampleAsync();

        Console.WriteLine("Fim do demo. Revise o checklist e as perguntas-guia no cabeçalho.");
    }

    static void PrintChecklist()
    {
        Console.WriteLine("Checklist (printed):");
        Console.WriteLine(" - Evitar async void (exceto eventos UI)");
        Console.WriteLine(" - Evitar .Result / .Wait() quando possível");
        Console.WriteLine(" - Sufixar métodos assíncronos com 'Async'");
        Console.WriteLine(" - Usar ConfigureAwait(false) em bibliotecas\n");
    }

    static async Task RunExceptionAwaitVsWaitAsync()
    {
        Console.WriteLine("1) Demonstrando exceção em Task observada com await vs AggregateException com .Wait()/ .Result");
        try
        {
            await TaskThatThrowsAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught by await: {ex.GetType().Name} - {ex.Message}");
        }

        try
        {
            // .Wait() causa AggregateException que embrulha a exceção original
            TaskThatThrowsAsync().Wait();
        }
        catch (AggregateException ae)
        {
            Console.WriteLine($"Caught AggregateException via Wait(): {ae.InnerException?.GetType().Name} - {ae.InnerException?.Message}");
        }

        Console.WriteLine();
    }

    static void RunBlockingResultExample()
    {
        Console.WriteLine("2) Demonstração do custo de bloquear com .Result (bloqueante)");
        var sw = Stopwatch.StartNew();
        var t = SimulatedIoAsync(700);
        Console.WriteLine($"Calling .Result on task (will block current thread) - thread {Environment.CurrentManagedThreadId}");
        var r = t.Result; // blocking; in console it won't deadlock but will block this thread
        sw.Stop();
        Console.WriteLine($"Result: {r} (blocked {sw.ElapsedMilliseconds} ms)\n");
    }

    static async Task RunConfigureAwaitExampleAsync()
    {
        Console.WriteLine("3) Demonstrando pattern recomendado: await e ConfigureAwait(false) em biblioteca (simulado)");
        var libVal = await LibraryCallAsync();
        Console.WriteLine($"LibraryCallAsync returned: {libVal}\n");
    }

    static async Task RunFireAndForgetExampleAsync()
    {
        Console.WriteLine("4) Async void (antipadrão) demonstration - do NOT use except event handlers");
        Console.WriteLine("   (we'll call a safe wrapper that captures exceções instead of crashing the process)");
        FireAndForgetSafe();
        await Task.Delay(200); // let fire-and-forget run
    }

    static async Task TaskThatThrowsAsync()
    {
        await Task.Delay(50);
        throw new InvalidOperationException("erro de demonstração");
    }

    static async Task<string> SimulatedIoAsync(int ms)
    {
        await Task.Delay(ms);
        return $"done {ms}ms";
    }

    // Simula chamada de biblioteca que usa ConfigureAwait(false)
    static async Task<string> LibraryCallAsync()
    {
        // Em bibliotecas, evite capturar o contexto do chamador, assim callers (UI) não têm a execução agendada de volta ao contexto desnecessariamente.
        await Task.Delay(100).ConfigureAwait(false);
        return "lib-result";
    }

    // Não chame async void — aqui mostramos uma alternativa segura que captura exceções
    static void FireAndForgetSafe()
    {
        _ = FireAndForgetInternalAsync();
        async Task FireAndForgetInternalAsync()
        {
            try
            {
                await Task.Delay(100);
                Console.WriteLine("FireAndForgetInternalAsync completed safely");
            }
            catch (Exception ex)
            {
                // logue a exceção em vez de permitir que ela se perca
                Console.WriteLine($"FireAndForget exception captured: {ex.Message}");
            }
        }
    }
}
