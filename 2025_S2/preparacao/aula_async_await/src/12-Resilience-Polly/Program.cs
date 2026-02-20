using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.Timeout;

/*
 * Demo: Resilience with Polly vs internal CancellationToken timeouts
 * - mostra Timeout + Retry policies com Polly
 * - compara com abordagem manual usando CancellationTokenSource e retry manual
 *
 * Pontos pedagógicos:
 * - Polly centraliza políticas cross-cutting (timeout, retry, circuit-breaker)
 * - Idempotência importa: retries sobre operações não idempotentes podem duplicar efeitos
 * - Configure backoff (exponential, jitter) para diminuir contenda em retrys
 * - Use políticas externas quando quiser aplicar comportamento consistente across callers
 *
 * Perguntas-guia:
 * 1) Quem deve decidir retry: a camada de chamada (cliente) ou a biblioteca/serviço? (depende, mas prefira políticas externas e configuráveis)
 * 2) Por que idempotência importa em retries? (para evitar efeitos colaterais duplicados)
 * 3) Quando preferir CancellationToken interno? (quando o caller precisa cancelar a operação explicitamente e cooperativamente)
 */

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Demo: Polly Timeout + Retry vs CancellationToken timeout/retry\n");

        await RunPollyPolicyExampleAsync();

        Console.WriteLine();

        await RunManualRetryExampleAsync();

        Console.WriteLine();
        // run the policy wrap demo after the other examples
        await RunPolicyWrapWithBulkheadExampleAsync();

        Console.WriteLine();
        Console.WriteLine("Notes: Polly gives a declarative centralized approach. Manual approach is straightforward but you must repeat patterns and be careful with idempotency and backoff.");
    }

    static async Task RunPollyPolicyExampleAsync()
    {
        // Polly policy: timeout of 1s + retry 3 times with exponential backoff
        var timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromSeconds(1), TimeoutStrategy.Pessimistic);
        var retryPolicy = Policy.Handle<Exception>()
                                 .WaitAndRetryAsync(new[] { TimeSpan.FromMilliseconds(200), TimeSpan.FromMilliseconds(400), TimeSpan.FromMilliseconds(800) },
                                     (ex, ts, ctx) => Console.WriteLine($"Polly retry due to {ex.GetType().Name}, waiting {ts.TotalMilliseconds}ms"));

        var combined = Policy.WrapAsync(retryPolicy, timeoutPolicy);

        Console.WriteLine("1) Polly policy execution (will retry on timeout/exception)");
        await combined.ExecuteAsync(async ct =>
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine($"Polly executing operation (thread {Environment.CurrentManagedThreadId})");
            await UnreliableOperationAsync(ct);
            sw.Stop();
            Console.WriteLine($"Polly operation succeeded in {sw.ElapsedMilliseconds} ms");
            return Task.CompletedTask;
        }, CancellationToken.None);
    }

    static async Task RunManualRetryExampleAsync()
    {
        Console.WriteLine("2) Manual timeout with CancellationTokenSource + manual retry");
        for (int attempt = 1; attempt <= 3; attempt++)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            try
            {
                Console.WriteLine($"Manual attempt {attempt}");
                var sw = Stopwatch.StartNew();
                await UnreliableOperationAsync(cts.Token);
                sw.Stop();
                Console.WriteLine($"Manual operation succeeded in {sw.ElapsedMilliseconds} ms");
                break;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"Manual attempt {attempt} cancelled (timeout). Backing off before next attempt");
                await Task.Delay(200 * attempt);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Manual attempt {attempt} failed: {ex.GetType().Name} - {ex.Message}");
                await Task.Delay(200 * attempt);
            }
        }
    }

    // -------------------------
    // Additional demo: Circuit Breaker + Bulkhead (PolicyWrap)
    // Objective: show how Bulkhead limits concurrent executions and how CircuitBreaker
    // opens when too many failures occur. This is useful to protect downstream systems.
    static async Task RunPolicyWrapWithBulkheadExampleAsync()
    {
        Console.WriteLine("3) PolicyWrap: Bulkhead + CircuitBreaker + Retry + Timeout demonstration");

        // Bulkhead: allow 2 concurrent executions and queue up to 4
        var bulkhead = Policy.BulkheadAsync(maxParallelization: 2, maxQueuingActions: 4,
            onBulkheadRejectedAsync: ctx =>
            {
                Console.WriteLine("Bulkhead rejected an execution (queue full)");
                return Task.CompletedTask;
            });

        // Circuit breaker: open after 2 consecutive failures, keep open for 5 seconds
        var circuitBreaker = Policy.Handle<Exception>()
            .CircuitBreakerAsync(2, TimeSpan.FromSeconds(5),
                onBreak: (ex, ts) => Console.WriteLine($"Circuit opened due to {ex.GetType().Name}, staying open for {ts.TotalSeconds}s"),
                onReset: () => Console.WriteLine("Circuit closed (reset)"),
                onHalfOpen: () => Console.WriteLine("Circuit is half-open; testing next call"));

        var retry = Policy.Handle<Exception>()
                          .WaitAndRetryAsync(new[] { TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(200) },
                              (ex, ts, ctx) => Console.WriteLine($"Retrying due to {ex.GetType().Name}, waiting {ts.TotalMilliseconds}ms"));

        var timeout = Policy.TimeoutAsync(TimeSpan.FromSeconds(1), TimeoutStrategy.Pessimistic);

        var wrapped = Policy.WrapAsync(bulkhead, circuitBreaker, retry, timeout);

        // Launch multiple concurrent operations to observe bulkhead behavior and circuit transitions
        var tasks = new List<Task>();
        for (int i = 0; i < 8; i++)
        {
            var idx = i;
            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    await wrapped.ExecuteAsync(async ct =>
                    {
                        Console.WriteLine($"Wrapped: starting op {idx}");
                        await UnreliableOperationAsync(ct);
                        Console.WriteLine($"Wrapped: op {idx} succeeded");
                        return Task.CompletedTask;
                    }, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Wrapped: op {idx} failed/was rejected: {ex.GetType().Name} - {ex.Message}");
                }
            }));
        }

        await Task.WhenAll(tasks);
    }

    // Simula operação instável que aleatoriamente demora (gera timeout) ou tem sucesso rápido.
    static async Task UnreliableOperationAsync(CancellationToken ct)
    {
        var r = Random.Shared.NextDouble();
        if (r < 0.5)
        {
            // slow path: longer than 1s to force timeout in some executions
            Console.WriteLine("UnreliableOperation: slow path (simulate I/O)");
            await Task.Delay(1500, ct);
            ct.ThrowIfCancellationRequested();
            Console.WriteLine("UnreliableOperation: slow path completed");
        }
        else if (r < 0.8)
        {
            // fail path
            Console.WriteLine("UnreliableOperation: throwing transient exception");
            await Task.Delay(100, ct);
            throw new HttpRequestException("simulated transient failure");
        }
        else
        {
            Console.WriteLine("UnreliableOperation: fast success path");
            await Task.Delay(200, ct);
        }
    }
}
