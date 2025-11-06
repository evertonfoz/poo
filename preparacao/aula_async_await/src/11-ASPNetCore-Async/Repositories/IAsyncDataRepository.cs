using System.Threading;
using System.Threading.Tasks;

namespace Repositories
{
    /// <summary>
    /// Contrato de repositório assíncrono. Métodos aceitam CancellationToken
    /// para permitir que I/O seja cancelado cooperativamente.
    /// </summary>
    public interface IAsyncDataRepository
    {
        Task<string> GetDataAsync(CancellationToken ct);
    }
}
