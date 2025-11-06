using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncBasics
{
    class Program
    {
        // Mini-intro: exemplo mínimo que demonstra diferença prática entre
        // aguardar sequencialmente e iniciar tarefas em paralelo com Task.WhenAll.
        // Para demos mais completas (WhenAny, cancelamento, exceções), veja `src/02-WhenAllWhenAny`.
        static async Task Main()
        {
            Console.WriteLine("Mini-intro: async/await — sequencial vs concorrente\n");

            var sw = System.Diagnostics.Stopwatch.StartNew();
            // Execução sequencial (await um-por-um)
            var r1 = await BuscarDadosSimuladoAsync("A");
            var r2 = await BuscarDadosSimuladoAsync("B");
            Console.WriteLine($"Sequencial: {r1}, {r2}");
            Console.WriteLine($"Tempo sequencial: {sw.ElapsedMilliseconds} ms\n");

            // Execução concorrente com WhenAll
            sw.Restart();
            var t1 = BuscarDadosSimuladoAsync("A");
            var t2 = BuscarDadosSimuladoAsync("B");
            var results = await Task.WhenAll(t1, t2);
            Console.WriteLine($"Concorrente: {results[0]}, {results[1]}");
            Console.WriteLine($"Tempo concorrente (WhenAll): {sw.ElapsedMilliseconds} ms\n");

            Console.WriteLine("Para exemplos completos (WhenAny, cancelamento, exceções) veja: src/02-WhenAllWhenAny");
        }

        // Simula I/O assíncrono com suporte a CancellationToken.
        static async Task<string> BuscarDadosSimuladoAsync(string id, CancellationToken ct = default)
        {
            Console.WriteLine($"Iniciando busca {id} (Thread {System.Threading.Thread.CurrentThread.ManagedThreadId})");
            await Task.Delay(500, ct);
            ct.ThrowIfCancellationRequested();
            Console.WriteLine($"Concluída busca {id} (Thread {System.Threading.Thread.CurrentThread.ManagedThreadId})");
            return $"Dados-{id}";
        }

        // Task que simula trabalho e, no final, lança uma exceção para demonstração
        static async Task<string> SimulateErrorAsync(string id)
        {
            Console.WriteLine($"Iniciando tarefa com erro {id} (Thread {System.Threading.Thread.CurrentThread.ManagedThreadId})");
            await Task.Delay(200);
            Console.WriteLine($"Tarefa {id} prestes a lançar exceção");
            throw new Exception($"erro simulado {id}");
        }
    }
}
