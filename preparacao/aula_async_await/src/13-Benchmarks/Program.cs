using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

/*
 * Benchmarks: compare sequential vs Task.WhenAll for simulated I/O
 * and compare Task<T> vs ValueTask<T> for a hot-cache scenario.
 *
 * How to run (Release):
 * dotnet run -c Release --project 13-Benchmarks/13-Benchmarks.csproj
 *
 * Pedagogical notes (in-code): see comments below about what to measure and
 * how to interpret results. BenchmarkDotNet will produce mean time, error,
 * standard deviation, and memory stats (if enabled).
 */

[MemoryDiagnoser]
public class Benchmarks
{
    // Number of concurrent I/O operations to simulate
    [Params(10, 50)]
    public int OpsCount { get; set; }

    // Artificial I/O latency (ms) per operation
    private const int IoDelayMs = 50;

    // Cache hit flag used for cache benchmarks
    [Params(true)] // we focus on hot-cache scenario per request
    public bool CacheHit { get; set; }

    // ---------- I/O simulation helpers ----------
    private static async Task<string> SimulatedIoAsync(int id, CancellationToken ct = default)
    {
        // Simulate an I/O-bound operation (e.g., network call, DB)
        await Task.Delay(IoDelayMs, ct);
        return $"OK:{id}";
    }

    // Sequential: await each operation one-by-one
    [Benchmark(Baseline = true, Description = "IO: Sequential await")]
    public async Task<string[]> Io_Sequential()
    {
        var results = new List<string>(OpsCount);
        for (int i = 0; i < OpsCount; i++)
        {
            var r = await SimulatedIoAsync(i);
            results.Add(r);
        }
        return results.ToArray();
    }

    // Parallel-ish: start tasks then await Task.WhenAll
    [Benchmark(Description = "IO: WhenAll (concurrent)")]
    public async Task<string[]> Io_WhenAll()
    {
        var tasks = new Task<string>[OpsCount];
        for (int i = 0; i < OpsCount; i++) tasks[i] = SimulatedIoAsync(i);
        var results = await Task.WhenAll(tasks);
        return results;
    }

    // ---------- Cache scenario helpers ----------
    // Simulated backing store (cold path)
    private static async Task<string> BackingStoreFetchAsync(int id)
    {
        // Simulate slower I/O for cache miss
        await Task.Delay(200);
        return $"Value:{id}";
    }

    // API that returns Task<string>
    private async Task<string> GetValue_AsTaskAsync(int id)
    {
        if (CacheHit)
        {
            // hot cache: very cheap, return completed Task
            return await Task.FromResult($"Cached:{id}");
        }
        return await BackingStoreFetchAsync(id);
    }

    // API that returns ValueTask<string>
    private ValueTask<string> GetValue_AsValueTaskAsync(int id)
    {
        if (CacheHit)
        {
            // hot cache: avoid allocation by returning ValueTask wrapping the result
            return new ValueTask<string>($"Cached:{id}");
        }
        // cold path: wrap the Task in a ValueTask (note: still allocates the Task)
        return new ValueTask<string>(BackingStoreFetchAsync(id));
    }

    [Benchmark(Description = "Cache: Task<T> (hot)")]
    public async Task<string> Cache_Task_Hot()
    {
        // call multiple times to make the measurement stable
        var id = 1;
        return await GetValue_AsTaskAsync(id);
    }

    [Benchmark(Description = "Cache: ValueTask<T> (hot)")]
    public async Task<string> Cache_ValueTask_Hot()
    {
        var id = 1;
        var vt = GetValue_AsValueTaskAsync(id);
        return await vt;
    }

    // ---------- Additional explanation (not benchmarks) ----------
    // The benchmarks above focus on two orthogonal questions:
    // 1) For I/O-bound operations that can run concurrently, does starting
    //    all work and awaiting Task.WhenAll give a measurable benefit over
    //    awaiting each call sequentially? Usually yes for I/O-bound work up to
    //    the point where the shared resource (e.g., network/DB) becomes the
    //    bottleneck.
    // 2) For hot-cache fast-path returns, does returning ValueTask<T> avoid
    //    allocations and produce better performance than Task<T>? Often yes,
    //    for very hot paths, but the difference can be tiny and ValueTask has
    //    usage pitfalls (don't await multiple times, don't store it). Measure
    //    with real workloads before committing.
    //
    // Limitations and biases of these microbenchmarks:
    // - The simulated delays are synthetic; real I/O may have more variance
    //   and additional costs (serialization, sockets, TLS).
    // - BenchmarkDotNet runs isolated process and attempts to minimize noise,
    //   but environment (CPU frequency scaling, background processes) can
    //   still influence results.
    // - For WhenAll, results depend heavily on concurrency limits and
    //   resource contention (max connections, DB pool size). WhenAll may
    //   produce worse results if it causes overload.
    // - ValueTask benefits are visible for very hot paths. If the cold path
    //   is common, ValueTask wrapping a Task doesn't avoid allocation.
    //
    // Questions to reflect on (guide questions):
    // - When might Task.WhenAll NOT help? (when operations are sequentially
    //   dependent, when shared resource becomes the bottleneck, or when
    //   starting many concurrent operations overloads the system)
    // - When is ValueTask a good idea? (very hot cache-return paths where the
    //   allocation of Task is measurable; after profiling)
    // - What else should you measure besides mean latency? (throughput,
    //   GC allocations, p95/p99 latencies, and system-level metrics like
    //   CPU and network utilization)
}

public class Program
{
    public static void Main(string[] args)
    {
        // Run all benchmarks in this assembly
        var summary = BenchmarkRunner.Run<Benchmarks>();
    }
}
