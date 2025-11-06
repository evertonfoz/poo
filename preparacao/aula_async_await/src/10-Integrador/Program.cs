using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/*
 * Tour final: integra padrões estudados.
 * Estrutura: quatro seções independentes, cada uma com objetivo, leitura do output,
 * armadilhas e perguntas-guia.
 */

class Program
{
    static async Task Main()
    {
        Console.WriteLine("=== Tour final: async patterns integrator ===\n");

        await SectionWhenAll();
        await SectionCancellationAndTimeout();
        await SectionAsyncStreams();
        await SectionSequentialVsParallel();

        Console.WriteLine("\n=== End of tour ===");
    }

    // -------------------------
    // Section: Task.WhenAll
    // Objective: show parallel startup of tasks and waiting for all results.
    // Read output: times, order of completion vs order of results.
    // Pitfalls: exceptions aggregate in WhenAll; starting tasks inside loop with await defeats concurrency.
    // Questions: 1) When prefer WhenAll vs sequential awaits? 2) How to inspect exceptions from WhenAll?
    static async Task SectionWhenAll()
    {
        Console.WriteLine("--- SECTION: WhenAll ---");
        Console.WriteLine("Objective: run many independent tasks concurrently and await all.");

        var sw = Stopwatch.StartNew();
        var tasks = Enumerable.Range(1, 3).Select(i => SimulatedWorkAsync(i * 300, i)).ToArray();

        // start all then await
        var results = await Task.WhenAll(tasks);
        sw.Stop();

        Console.WriteLine($"Results: {string.Join(", ", results)}");
        Console.WriteLine($"Elapsed (WhenAll): {sw.ElapsedMilliseconds} ms");
        Console.WriteLine("Reading: elapsed should be close to the slowest task, not sum of all.");
        Console.WriteLine("Pitfall: if one task throws, WhenAll will throw and you should inspect AggregateException.InnerExceptions (or catch and inspect the Task exceptions).");
        Console.WriteLine("Questions: When prefer Task.WhenAll? How to handle partial failures?\n");
    }

    // -------------------------
    // Section: Cancellation + Timeout
    // Objective: show cancellation via CancellationTokenSource and using timeouts.
    // Read output: cancelled vs completed messages; observe OperationCanceledException.
    // Pitfalls: always pass token to cancellable APIs; don't swallow OperationCanceledException unless intended.
    // Questions: 1) When to use cooperative cancellation? 2) How to design timeouts at API boundaries?
    static async Task SectionCancellationAndTimeout()
    {
        Console.WriteLine("--- SECTION: Cancellation + Timeout ---");
        Console.WriteLine("Objective: demonstrate cancellation using CancellationToken and timeouts.");

        using var cts = new CancellationTokenSource(700);
        try
        {
            await CancellableOperationAsync(cts.Token);
            Console.WriteLine("Operation completed (unexpected)");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation cancelled as expected (OperationCanceledException)");
        }

        Console.WriteLine("Reading: check that the operation observed the token and exited promptly.");
        Console.WriteLine("Pitfall: forgetting to pass the token to sub-operations or Task.Delay(..., token).");
        Console.WriteLine("Questions: Where set timeouts? Who owns cancellation responsibility?\n");
    }

    // -------------------------
    // Section: Async Streams (await foreach)
    // Objective: produce items over time with IAsyncEnumerable and consume with await foreach.
    // Read output: items appear incrementally; the consumer can cancel iteration.
    // Pitfalls: care with buffering and producer speed vs consumer speed; cancellation should be supported.
    // Questions: 1) When use async streams vs returning a collection? 2) How to cancel an ongoing stream?
    static async Task SectionAsyncStreams()
    {
        Console.WriteLine("--- SECTION: Async Streams (await foreach) ---");
        Console.WriteLine("Objective: demonstrate IAsyncEnumerable and await foreach (streaming results).");

        await foreach (var n in CountdownAsync(5, 200))
        {
            Console.WriteLine($"Streamed item: {n}");
        }

        Console.WriteLine("Reading: items should be printed as they are produced (incremental). Use WithCancellation or pass token to producer to cancel.");
        Console.WriteLine("Pitfall: do not materialize the whole stream if you want streaming semantics.");
        Console.WriteLine("Questions: Where is streaming better than batch? How to backpressure?\n");
    }

    // -------------------------
    // Section: Sequential vs Parallel
    // Objective: contrast awaiting each task (sequential) vs starting tasks then awaiting (parallel).
    // Read output: elapsed times show difference. Pitfalls: parallelism increases contention and resource usage.
    // Questions: 1) When parallelism harms performance? 2) How to limit degree of parallelism?
    static async Task SectionSequentialVsParallel()
    {
        Console.WriteLine("--- SECTION: Sequential vs Parallel ---");
        Console.WriteLine("Objective: compare sequential awaits vs concurrent tasks.");

        var sw = Stopwatch.StartNew();
        for (int i = 1; i <= 3; i++)
        {
            await SimulatedWorkAsync(300, i);
        }
        sw.Stop();
        Console.WriteLine($"Elapsed sequential: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        var tasks = Enumerable.Range(1, 3).Select(i => SimulatedWorkAsync(300, i)).ToArray();
        await Task.WhenAll(tasks);
        sw.Stop();
        Console.WriteLine($"Elapsed parallel (WhenAll): {sw.ElapsedMilliseconds} ms");

        Console.WriteLine("Reading: sequential should be ~ sum; parallel close to max. Be mindful of CPU, IO capacity and throttling.");
        Console.WriteLine("Pitfall: unbounded parallelism can cause threadpool starvation, increased memory and I/O saturation.");
        Console.WriteLine("Questions: How to cap parallelism? When to prefer dataflow / parallel loops?\n");
    }

    // Helper methods
    static async Task<string> SimulatedWorkAsync(int delayMs, int id)
    {
        await Task.Delay(delayMs);
        return $"task-{id}-done";
    }

    static async Task CancellableOperationAsync(CancellationToken ct)
    {
        // loop doing small work and checking token
        for (int i = 0; i < 10; i++)
        {
            ct.ThrowIfCancellationRequested();
            await Task.Delay(200, ct);
        }
    }

    static async IAsyncEnumerable<int> CountdownAsync(int from, int delayMs)
    {
        for (int i = from; i >= 1; i--)
        {
            await Task.Delay(delayMs);
            yield return i;
        }
    }
}
