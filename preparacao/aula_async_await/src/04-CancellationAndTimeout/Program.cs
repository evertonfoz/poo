using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationAndTimeout
{
    class Program
    {
        /*
         * Padrão pedagógico: documentação explicativa logo após a implementação.
         * Este arquivo demonstra:
         * - diferença entre "cancelamento" (ação solicitada pela aplicação) e "timeout"
         *   (cancelamento automático após um período);
         * - como propagar CancellationToken entre camadas e por que lançar
         *   OperationCanceledException para sinalizar cancelamento;
         * - uso de CancellationTokenSource(TimeSpan) para timeout e
         *   CancellationTokenSource.CreateLinkedTokenSource para combinar tokens.
         *
         * Perguntas-guia para alunos:
         * 1) Onde o cancelamento é verificado no fluxo da operação? (procure ct.ThrowIfCancellationRequested ou await Task.Delay(..., ct)).
         * 2) O que acontece se não propagarmos o token para operações internas? (a operação continuará executando até o fim, consumindo recursos).
         * 3) Por que lançar OperationCanceledException em vez de retornar um valor especial? (permite ao runtime e às APIs tratar cancelamento consistentemente e liberar recursos corretamente).
         */

        static async Task Main()
        {
            Console.WriteLine("Demo: CancellationToken, timeout e linked tokens\n");

            await RunTimeoutExampleAsync();
            Console.WriteLine();
            await RunExternalCancellationExampleAsync();
            Console.WriteLine();
            await RunLinkedTokensExampleAsync();

            Console.WriteLine("\nFim da demo de cancelamento e timeout.");
        }

        static async Task RunTimeoutExampleAsync()
        {
            Console.WriteLine("1) Timeout-based cancellation (CancellationTokenSource with 1s timeout)");
            using (var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(1)))
            {
                try
                {
                    await OperacaoCancelavelAsync(timeoutCts.Token);
                    Console.WriteLine("Operação completou sem cancelamento (unexpected)");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Operação cancelada por timeout (OperationCanceledException).");
                }
            }
        }

        static async Task RunExternalCancellationExampleAsync()
        {
            Console.WriteLine("2) External cancellation (Cancel called from outside)");
            using (var cts = new CancellationTokenSource())
            {
                var task = OperacaoCancelavelAsync(cts.Token);
                // cancela após 700 ms
                _ = Task.Run(async () => { await Task.Delay(700); cts.Cancel(); });

                try
                {
                    await task;
                    Console.WriteLine("Operação completou sem cancelamento (unexpected)");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Operação cancelada externamente (OperationCanceledException).");
                }
            }
        }

        static async Task RunLinkedTokensExampleAsync()
        {
            Console.WriteLine("3) Linked tokens (timeout + external token)");
            using (var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(2)))
            using (var external = new CancellationTokenSource())
            using (var linked = CancellationTokenSource.CreateLinkedTokenSource(timeout.Token, external.Token))
            {
                // Simula cancelamento externo após 500 ms
                _ = Task.Run(async () => { await Task.Delay(500); external.Cancel(); });

                try
                {
                    await OperacaoCancelavelAsync(linked.Token);
                    Console.WriteLine("Operação completou sem cancelamento (unexpected)");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine($"Operação cancelada. timeout.IsCancellationRequested={timeout.IsCancellationRequested}, external.IsCancellationRequested={external.IsCancellationRequested}");
                }
            }
        }

        // Operação que verifica o token em loop e usa Task.Delay(ct) para respeitar cancelamento.
        static async Task OperacaoCancelavelAsync(CancellationToken ct)
        {
            // Em loops longos, cheque frequentemente o CancellationToken e/ou utilize APIs que aceitam token.
            for (int i = 0; i < 10; i++)
            {
                // Exemplo: await Task.Delay com token — se cancelado, Task.Delay lançará OperationCanceledException
                await Task.Delay(300, ct);

                // Verificação explícita — lança OperationCanceledException se o token foi cancelado.
                ct.ThrowIfCancellationRequested();

                Console.WriteLine($"Passo {i + 1} concluído (thread {Environment.CurrentManagedThreadId})");
            }

            // Se quisermos sinalizar cancelamento manualmente, podemos lançar OperationCanceledException
            // com o token: throw new OperationCanceledException(ct);
        }
    }
}
