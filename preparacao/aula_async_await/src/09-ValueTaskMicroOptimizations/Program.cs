using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

/*
 * Demo: quando ValueTask<T> faz sentido
 * - cenário: quando a resposta é frequentemente já disponível (cache), evitar alocar Task<T> para o caminho quente.
 * - se não houver cache, usamos I/O (Task<T>) normalmente.
 *
 * Boas práticas / restrições:
 * - não awaitar duas vezes um ValueTask retornado (ele pode estar backed by a pooled resource);
 * - não armazenar ValueTask<T> em campos/coleções para uso posterior; converta para Task<T> se precisar reusar/armazenar;
 * - prefira ValueTask apenas após profiling (benefício apenas em caminhos muito quentes).
 *
 * Perguntas-guia:
 * 1) Por que usar ValueTask só após profiling? (o ganho de evitar uma alocação de Task é pequeno e aumenta a complexidade/risco de uso incorreto);
 * 2) Quando ValueTask traz ganho real? (quando a maioria das chamadas retorna um resultado já disponível e a alocação de Task seria acessível);
 * 3) Quais armadilhas evitar? (armazenar o ValueTask, await múltiplas vezes, esquecer que nem todos os ValueTask são hot-path safe).
 */

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Demo: ValueTask micro-optimizations\n");

        var store = new ConcurrentDictionary<int, string>();
        store[1] = "cached-value-1";

        // hot path: cache hit -> ValueTask.FromResult (no Task allocation)
        var vtHit = GetValueAsync(1, store);
        Console.WriteLine($"Cached lookup (ValueTask) result: {await vtHit}");

        // cold path: cache miss -> fallback para I/O (Task)
        var vtMiss = GetValueAsync(42, store);
        Console.WriteLine($"Non-cached lookup result: {await vtMiss}\n");

        Console.WriteLine("Observação: o método GetValueAsync retorna ValueTask<string> e internamente evita alocações se o item estiver em cache.");
    }

    // Retorna ValueTask<string>: quando o dado está em cache devolvemos um ValueTask já completado;
    // caso contrário, delegamos para um método assíncrono que realiza I/O e retorna Task<string>.
    static ValueTask<string> GetValueAsync(int id, ConcurrentDictionary<int,string> cache)
    {
        if (cache.TryGetValue(id, out var value))
        {
            // caminho quente: retorno sincrono completado, sem alocar Task
            return ValueTask.FromResult(value);
        }

        // caminho frio: realizamos I/O que produz um Task<string>
        return new ValueTask<string>(GetFromIoAsync(id));
    }

    static async Task<string> GetFromIoAsync(int id)
    {
        // simula I/O
        await Task.Delay(200);
        return $"fetched-{id}";
    }
}
