using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsyncStreams;
using Xunit;

namespace AsyncPlayground.Tests
{
    public class ContingencyTests
    {
        /*
         * Test: WhenAll returns all results
         * - O que valida: que quando você inicia várias Tasks de I/O e aguarda
         *   Task.WhenAll, todas as Tasks completam e seus resultados são
         *   retornados no array resultante.
         * - Por que importa: muitos erros vêm de confiar que iniciando Tasks
         *   elas serão executadas independentemente — esse teste garante a
         *   agregação correta dos resultados.
         * - Armadilhas: Task.WhenAll falha rápido se qualquer Task lançar;
         *   em cenários reais você pode querer tratamento por-Task ou
         *   WhenAll(handle exceptions) explícito.
         */
        [Fact]
        public async Task WhenAll_ReturnsAllResults()
        {
            var tasks = Enumerable.Range(1, 5).Select(i => SimulatedIoAsync(i)).ToArray();
            var results = await Task.WhenAll(tasks);
            Assert.Equal(5, results.Length);
            Assert.All(results, s => Assert.StartsWith("OK:", s));
            Assert.Equal(new[] { "OK:1", "OK:2", "OK:3", "OK:4", "OK:5" }, results);
        }

        private static async Task<string> SimulatedIoAsync(int id)
        {
            await Task.Delay(10);
            return $"OK:{id}";
        }

        /*
         * Test: Timeout cancellation throws OperationCanceledException
         * - O que valida: que operações cooperativas respeitam o
         *   CancellationToken e propagam OperationCanceledException quando
         *   canceladas por timeout.
         * - Por que importa: cancelar operações longas é essencial para
         *   evitar recursos presos e para obedecer a SLAs/requests abortadas.
         * - Armadilhas: alguns métodos não aceitam tokens; nesse caso não
         *   haverá exceção de cancelamento e a operação continuará.
         */
        [Fact]
        public async Task Cancellation_ByTimeout_Throws_OperationCanceledException()
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(20));
            Func<Task> act = async () => await Task.Delay(500, cts.Token);
            // In practise Task.Delay cancellation throws TaskCanceledException
            // (a subclass of OperationCanceledException). Assert the concrete
            // type to avoid test framework strict-matching differences.
            await Assert.ThrowsAsync<TaskCanceledException>(act);
        }

        /*
         * Test: ContarAsync produces expected sequence
         * - O que valida: o IAsyncEnumerable expõe a sequência esperada de
         *   1..N e pode ser consumida com await foreach.
         * - Por que importa: garante semântica da API de streams assincronas
         *   (iteração e cancelabilidade).
         * - Armadilhas: lembre de usar tokens ou timeouts em testes que
         *   dependem de delays para evitar travamentos longos.
         */
        [Fact]
        public async Task ContarAsync_Produces_Expected_Sequence()
        {
            var items = new List<int>();
            // Use pequeno atraso para teste ser rápido
            await foreach (var n in AsyncStreamsApi.ContarAsyncPublic(5, 10))
            {
                items.Add(n);
            }

            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, items.ToArray());
        }

        /*
         * Illustrative test (skipped): blocking on .Result
         * - Por que existe: bloquear em Tasks com .Result/.Wait pode levar a
         *   deadlocks em contextos com single-threaded synchronization
         *   contexts (p.ex. UI, ASP.NET classic) — este teste está marcado
         *   Skip para explicar o porquê em vez de falhar.
         */
        [Fact(Skip = "Demonstration only: .Result can deadlock when synchronization context is present; skip in automated CI")]
        public void BlockingOnResult_CanDeadlock()
        {
            // Em ambientes com SynchronizationContext (UI/legacy ASP.NET),
            // usar .Result para esperar Tasks pode causar deadlock porque o
            // continuations são enfileirados no contexto que está bloqueado.
            // Evite esse padrão e prefira async/await.
            var task = Task.Run(async () =>
            {
                await Task.Delay(10);
                return 42;
            });

            // This could deadlock in certain synchronization contexts.
            var result = task.Result;
            Assert.Equal(42, result);
        }
    }
}
