using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AsyncExceptions
{
    class Program
    {
        // Demo de tratamento de exceções assíncronas.
        // Pedagogia aplicada (padrão do workspace):
        // - explique o que o programa demonstra (ex.: exceções individuais vs agregadas);
        // - mostre exemplos concretos de try/catch com await e de inspeção de AggregateException;
        // - comente sobre comportamento do runtime: await em Task.WhenAll propaga a
        //   primeira exceção observada como exceção assincrônica, e a Task agregada
        //   mantém a AggregateException em sua propriedade Exception;
        // - indique práticas recomendadas (tratar exceções por tarefa quando necessário,
        //   usar CancellationToken para abortar operações, log/telemetria das inner exceptions).
        //
        // Neste arquivo:
        // 1) Demonstramos try/catch em await individual (captura direta de TimeoutException).
        // 2) Executamos várias tasks em paralelo com Task.WhenAll e demonstramos como
        //    inspecionar whenAllTask.Exception (AggregateException.InnerExceptions)
        //    para obter todas as falhas ocorridas.
        // 3) Mostramos também como inspecionar tasks individuais (t.IsFaulted) caso
        //    deseje tratar falhas por fonte específica.
        static async Task Main()
        {
            Console.WriteLine("Demo: Exceções assíncronas (TimeoutException e AggregateException)\n");

            await RunAwaitIndividualExampleAsync();
            await RunWhenAllExampleAsync();

            Console.WriteLine("\nFim da demo.");
        }

        static async Task RunAwaitIndividualExampleAsync()
        {
            Console.WriteLine("1) Teste await individual com try/catch");
            try
            {
                var r = await BuscarDadosSimuladoAsync("Single");
                Console.WriteLine($"Resultado: {r}");
            }
            catch (TimeoutException tex)
            {
                Console.WriteLine($"Capturada TimeoutException: {tex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Outra exceção capturada: {ex.GetType().Name}: {ex.Message}");
            }
        }

        static async Task RunWhenAllExampleAsync()
        {
            Console.WriteLine("\n2) Task.WhenAll com múltiplas tarefas (captura de AggregateException.InnerExceptions)");
            var fontes = new[] { "A", "B", "C", "D" };

            // Inicia as tasks (não await ainda)
            var tasks = fontes.Select(f => BuscarDadosSimuladoAsync(f)).ToArray();

            // Obtem a task agregada
            var whenAllTask = Task.WhenAll(tasks);

            try
            {
                // Ao await, uma exceção será lançada caso qualquer task falhe.
                var results = await whenAllTask;
                Console.WriteLine($"Todos concluíram com sucesso: {string.Join(", ", results)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WhenAll lançou: {ex.GetType().Name}: {ex.Message}");

                // A Task retornada (whenAllTask) contém AggregateException com todas as exceções
                var agg = whenAllTask.Exception;
                if (agg != null)
                {
                    Console.WriteLine($"AggregateException com {agg.InnerExceptions.Count} inner exception(s):");
                    foreach (var inner in agg.InnerExceptions)
                    {
                        Console.WriteLine($" - {inner.GetType().Name}: {inner.Message}");
                    }
                }
                else
                {
                    // Alternativa: inspecionar cada task individualmente
                    var faults = tasks.Where(t => t.IsFaulted).ToArray();
                    Console.WriteLine($"Tasks com falha: {faults.Length}");
                    foreach (var t in faults)
                    {
                        var ie = t.Exception?.InnerExceptions ?? Enumerable.Empty<Exception>();
                        foreach (var e in ie)
                            Console.WriteLine($" - {e.GetType().Name}: {e.Message}");
                    }
                }
            }
        }

        // Simula busca e lança TimeoutException aleatoriamente (~50% de chance).
        static async Task<string> BuscarDadosSimuladoAsync(string fonte)
        {
            int delay = Random.Shared.Next(200, 801);
            Console.WriteLine($"Iniciando fonte {fonte} (delay {delay} ms)");
            await Task.Delay(delay);
            // 50% chance de lançar TimeoutException — isso permite demonstrar como
            // exceções assíncronas se propagam e como agrupá-las com WhenAll.
            if (Random.Shared.NextDouble() < 0.5)
            {
                // Lançando TimeoutException intencionalmente para o exemplo.
                throw new TimeoutException($"Timeout na fonte {fonte} após {delay}ms");
            }

            Console.WriteLine($"Concluída fonte {fonte} (delay {delay} ms)");
            return $"Dados-{fonte}({delay}ms)";
        }
    }
}
