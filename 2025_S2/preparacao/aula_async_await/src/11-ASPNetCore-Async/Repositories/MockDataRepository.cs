using System.Threading;
using System.Threading.Tasks;

namespace Repositories
{
    /// <summary>
    /// Implementação de repositório que simula I/O com Task.Delay e observa
    /// CancellationToken para cancelar prontamente quando solicitado.
    /// Comentário pedagógico: mantenha I/O assíncrono na camada de infra; o domínio
    /// e controllers apenas orquestram e propagam tokens.
    /// </summary>
    public class MockDataRepository : IAsyncDataRepository
    {
        public async Task<string> GetDataAsync(CancellationToken ct)
        {
            // Simula latência de I/O (2s) e respeita o token
            await Task.Delay(2000, ct);
            ct.ThrowIfCancellationRequested();
            return "dados-simulados";
        }
    }
}
