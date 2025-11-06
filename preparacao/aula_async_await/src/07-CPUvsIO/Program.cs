using System;
using System.Diagnostics;
using System.Threading.Tasks;

// Demonstração pedagógica: I/O-bound vs CPU-bound
// - I/O-bound: espere usando 'await' em APIs assíncronas (Task.Delay usado aqui como simulação).
//   NÃO use Task.Run para I/O; envolver I/O em Task.Run apenas move a espera para um thread-pool thread,
//   desperdiçando recursos e custos de troca de contexto.
// - CPU-bound: operações que consomem CPU (cálculos intensos) podem ser executadas fora do thread de
//   chamada usando Task.Run para liberar a thread de chamada (por exemplo, UI) e explorar paralelismo.
//
// Custo a considerar:
// - Task.Run tem overhead: alocação, agendamento no thread-pool, possivelmente troca de contexto, sincronização.
// - Não acelera operações I/O que já são assíncronas (a vantagem do async I/O é não bloquear threads);
//   colocar I/O em Task.Run pode reduzir a escalabilidade.
//
// Perguntas-guia:
// 1) Por que Task.Run não acelera chamadas I/O? (Porque I/O assíncrono libera o thread enquanto o kernel/IO aguarda; Task.Run apenas muda o bloqueio para outro thread.)
// 2) Quando considerar Task.Run para operações CPU-bound? (Quando operação é longa e você quer evitar bloquear o thread atual, ex.: UI, request thread.)
// 3) Quais custos devemos medir? (latência, uso de CPU, contagem de threads, custo de contexto/tarefas adicionais.)

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Demo: I/O-bound vs CPU-bound\n");

        await RunIoBoundExampleAsync();
        await RunWrappedIoExampleAsync();
        await RunCpuBoundWithTaskRunExampleAsync();
        RunCpuBoundBlockingExample();

        PrintSummary();
    }

    static async Task RunIoBoundExampleAsync()
    {
        Console.WriteLine("1) I/O-bound (simulado) - usando await Task.Delay (sem Task.Run)");
        var sw = Stopwatch.StartNew();
        Console.WriteLine($"Main thread id before I/O: {Environment.CurrentManagedThreadId}");
        await SimulatedIoAsync(1000);
        sw.Stop();
        Console.WriteLine($"I/O completed in {sw.ElapsedMilliseconds} ms\n");
    }

    static async Task RunWrappedIoExampleAsync()
    {
        Console.WriteLine("2) Não recomendado: wrapping I/O em Task.Run (exemplo apenas para comparação)");
        var sw = Stopwatch.StartNew();
        Console.WriteLine($"Main thread id before wrapped I/O: {Environment.CurrentManagedThreadId}");
        // Observe o overhead de agendar no thread-pool — não costuma ser vantajoso para I/O
        await Task.Run(() => SimulatedIoAsync(1000));
        sw.Stop();
        Console.WriteLine($"Wrapped I/O completed in {sw.ElapsedMilliseconds} ms (note: overhead de Task.Run)\n");
    }

    static async Task RunCpuBoundWithTaskRunExampleAsync()
    {
        Console.WriteLine("3) CPU-bound: executar cálculo pesado dentro de Task.Run para não bloquear o chamador");
        const int max = 100_000; // ajuste para workload
        Console.WriteLine($"Main thread id before CPU work: {Environment.CurrentManagedThreadId}");
        var sw = Stopwatch.StartNew();
        var primesCount = await Task.Run(() => CountPrimes(max));
        sw.Stop();
        Console.WriteLine($"Found {primesCount} primes up to {max} in {sw.ElapsedMilliseconds} ms (computed on thread {Environment.CurrentManagedThreadId})\n");
    }

    static void RunCpuBoundBlockingExample()
    {
        Console.WriteLine("4) CPU-bound executado síncrono (bloqueando o chamador) — comparação");
        const int max = 100_000;
        Console.WriteLine($"Main thread id before blocking CPU work: {Environment.CurrentManagedThreadId}");
        var sw = Stopwatch.StartNew();
        var primesDirect = CountPrimes(max);
        sw.Stop();
        Console.WriteLine($"Blocking computed {primesDirect} primes in {sw.ElapsedMilliseconds} ms (thread {Environment.CurrentManagedThreadId})\n");
    }

    static void PrintSummary()
    {
        Console.WriteLine("Resumo:");
        Console.WriteLine("- Use APIs assíncronas para I/O e apenas await (não envolva I/O em Task.Run).");
        Console.WriteLine("- Use Task.Run para CPU-bound se precisar evitar bloquear o chamador e se houver benefício em paralelismo.");
        Console.WriteLine("- Meça: Task.Run tem overhead; use quando os ganhos superarem o custo.");
    }

    // Simula uma operação I/O-bound que já é naturalmente assíncrona.
    static async Task SimulatedIoAsync(int delayMs)
    {
        Console.WriteLine($"SimulatedIoAsync starting on thread {Environment.CurrentManagedThreadId}");
        await Task.Delay(delayMs);
        Console.WriteLine($"SimulatedIoAsync finished on thread {Environment.CurrentManagedThreadId}");
    }

    // Contador simples de primos (CPU-bound) — algoritmo ingênuo para forçar carga CPU
    static int CountPrimes(int max)
    {
        Console.WriteLine($"CountPrimes starting on thread {Environment.CurrentManagedThreadId}");
        int count = 0;
        for (int i = 2; i <= max; i++)
        {
            if (IsPrime(i)) count++;
        }
        Console.WriteLine($"CountPrimes finished on thread {Environment.CurrentManagedThreadId}");
        return count;
    }

    static bool IsPrime(int n)
    {
        if (n <= 1) return false;
        if (n <= 3) return true;
        if (n % 2 == 0) return false;
        int r = (int)Math.Sqrt(n);
        for (int i = 3; i <= r; i += 2)
            if (n % i == 0) return false;
        return true;
    }
}
