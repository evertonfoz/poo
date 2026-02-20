using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WhenAllWhenAny
{
    class Program
    {
        /*
         * Padrão de comentários pedagógicos adotado:
         * - explicar por que/como o Main é async (quando aplicável);
         * - descrever cada abordagem (sequencial, WhenAll, WhenAny) e o que
         *   será medido (tempo, resultados e comportamento em caso de exceção);
         * - mencionar detalhes práticos (Task.WhenAll retorna resultados na
         *   mesma ordem das tasks, Task.WhenAny retorna a Task que terminou
         *   primeiro, Random.Shared é thread-safe, await libera a thread, etc.).
         */

        // Método de entrada assíncrono: permite usar 'await' diretamente no Main.
        // Aqui usamos para facilitar a demonstração das chamadas assíncronas.
        static async Task Main()
        {
            Console.WriteLine("Demo: WhenAll vs WhenAny vs Sequencial\n");

            var fontes = new[] { "A", "B", "C" };

            await RunSequentialExampleAsync(fontes);
            await RunWhenAllExampleAsync(fontes);
            await RunWhenAnyExampleAsync(fontes);

            Console.WriteLine("\nObservação: cada chamada usa atraso aleatório entre 200–800 ms; compare os tempos para ver a diferença de padrão de execução.");
            Console.WriteLine("Dica: para cancelar operações ou impor timeout, use CancellationToken e combine Task.WhenAny com Task.Delay(timeout).");
        }

        static async Task RunSequentialExampleAsync(string[] fontes)
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine("(a) Execução sequencial (await um-por-um)");
            var seqResults = new string[fontes.Length];
            for (int i = 0; i < fontes.Length; i++)
            {
                seqResults[i] = await BuscarDadosSimuladoAsync(fontes[i]);
            }
            Console.WriteLine($"Resultados: {string.Join(", ", seqResults)}");
            Console.WriteLine($"Tempo sequencial: {sw.ElapsedMilliseconds} ms\n");
        }

        static async Task RunWhenAllExampleAsync(string[] fontes)
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine("(b) Task.WhenAll (inicia todas e aguarda todas)");
            var tasksAll = fontes.Select(f => BuscarDadosSimuladoAsync(f)).ToArray();
            var allResults = await Task.WhenAll(tasksAll);
            Console.WriteLine($"Resultados: {string.Join(", ", allResults)}");
            Console.WriteLine($"Tempo WhenAll: {sw.ElapsedMilliseconds} ms\n");

            // Observação sobre exceções: se qualquer task lançar, WhenAll propaga
            // a exceção; trate/examine tasks individualmente se falhas isoladas forem esperadas.
        }

        static async Task RunWhenAnyExampleAsync(string[] fontes)
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine("(c) Task.WhenAny (inicia todas e pega a primeira concluída)");
            var tasksAny = fontes.Select(f => BuscarDadosSimuladoAsync(f)).ToArray();
            var firstCompleted = await Task.WhenAny(tasksAny);
            var firstResult = await firstCompleted; // await da task vencedora
            Console.WriteLine($"Primeiro finalizado: {firstResult}");
            Console.WriteLine($"Tempo até o primeiro (WhenAny): {sw.ElapsedMilliseconds} ms");

            // Opcional: aguardar todas para exibir os resultados finais
            var finalAll = await Task.WhenAll(tasksAny);
            Console.WriteLine($"Todos (após completar): {string.Join(", ", finalAll)}");
        }

        // Simula busca em uma fonte com atraso aleatório entre 200 e 800 ms.
        // Nota: Random.Shared é thread-safe (disponível em .NET moderno) e
        // await Task.Delay não bloqueia a thread durante a espera.
        static async Task<string> BuscarDadosSimuladoAsync(string fonte)
        {
            int delay = Random.Shared.Next(200, 801); // 200..800 ms
            Console.WriteLine($"Iniciando fonte {fonte} (delay {delay} ms)");
            await Task.Delay(delay);
            Console.WriteLine($"Concluída fonte {fonte} (delay {delay} ms)");
            return $"Dados-{fonte}({delay}ms)";
        }
    }
}
