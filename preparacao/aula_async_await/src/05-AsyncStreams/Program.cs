using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncStreams
{
    class Program
    {
        /*
         * Padrão pedagógico (async streams):
         * - o que é uma stream assíncrona: um IAsyncEnumerable<T> que produz
         *   elementos com await, permitindo consumir itens à medida que chegam
         *   sem carregar tudo em memória;
         * - quando usar vs carregar tudo em memória: use stream quando o
         *   conjunto for grande, quando os elementos chegam ao longo do tempo
         *   (ex.: leitura de rede, processamento de logs) ou quando queremos
         *   começar a processar antes de ter todos os dados;
         * - impacto em backpressure: consumidores podem controlar a taxa de
         *   consumo (await foreach espera cada item) — producers devem respeitar
         *   isso (p.ex. usando delays ou buffers); ainda assim, um produtor
         *   muito rápido pode sobrecarregar recursos do consumidor se não houver
         *   mecanismos de controle;
         *
         * Perguntas-guia:
         * 1) Qual o ganho de usar await foreach em vez de materializar uma lista? (memória, latência inicial);
         * 2) Onde e como posso cancelar a iteração? (WithCancellation ou passar CancellationToken ao enumerador);
         * 3) O que acontece se o produtor gerar itens mais rápido que o consumidor? (considere buffering/backpressure).
         */

        static async Task Main()
        {
            Console.WriteLine("Demo: IAsyncEnumerable (async streams)\n");

            await RunSimpleConsumeAsync();
            Console.WriteLine();
            await RunWithCancellationUsingExtensionAsync();
            Console.WriteLine();
            await RunProducerCancellationAsync();

            Console.WriteLine("\nFim da demo de async streams.");
        }

        static async Task RunSimpleConsumeAsync()
        {
            Console.WriteLine("1) Consumo simples com await foreach");
            await foreach (var n in ContarAsync(5, 250))
            {
                Console.WriteLine($"Recebido: {n}");
            }
        }

        static async Task RunWithCancellationUsingExtensionAsync()
        {
            Console.WriteLine("2) Cancelamento com token usando WithCancellation");
            using (var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(900)))
            {
                try
                {
                    await foreach (var n in ContarAsync(10, 300).WithCancellation(cts.Token))
                    {
                        Console.WriteLine($"Received: {n}");
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Iteração cancelada via token (OperationCanceledException).");
                }
            }
        }

        static async Task RunProducerCancellationAsync()
        {
            Console.WriteLine("3) Passando token direto para o produtor (com [EnumeratorCancellation])");
            using (var cts2 = new CancellationTokenSource(800))
            {
                try
                {
                    await foreach (var n in ContarAsync(10, 300, cts2.Token))
                    {
                        Console.WriteLine($"Item: {n}");
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Operação cancelada no produtor (OperationCanceledException).");
                }
            }
        }

        // Produz números de 1 até 'ate' com atraso 'atrasoMs' entre eles.
        // O parâmetro CancellationToken anotado com [EnumeratorCancellation]
        // permite que o token seja passado quando o chamador usar
        // GetAsyncEnumerator(token) (por exemplo via WithCancellation).
    public static async IAsyncEnumerable<int> ContarAsync(int ate, int atrasoMs, [EnumeratorCancellation] CancellationToken ct = default)
        {
            for (int i = 1; i <= ate; i++)
            {
                // Use APIs que aceitam o token (Task.Delay) e/ou cheque explicitamente.
                await Task.Delay(atrasoMs, ct);
                ct.ThrowIfCancellationRequested();
                yield return i;
            }
        }
    }

    // Extensão helper: WithCancellation para expor a forma de cancelar a iteração
    // pelo chamador, chamando GetAsyncEnumerator(cancellationToken) internamente.
    static class AsyncEnumerableExtensions
    {
        public static async IAsyncEnumerable<T> WithCancellation<T>(this IAsyncEnumerable<T> source, [EnumeratorCancellation] CancellationToken ct)
        {
            await using var e = source.GetAsyncEnumerator(ct);
            try
            {
                while (await e.MoveNextAsync())
                {
                    yield return e.Current;
                }
            }
            finally
            {
                // o dispose do enumerator será chamado pelo await using acima
            }
        }
    }
}
