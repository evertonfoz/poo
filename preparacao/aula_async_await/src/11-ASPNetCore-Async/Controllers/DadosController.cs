using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ASPNetCoreAsync.Controllers
{
    // Controller pedagógico: aceita CancellationToken para propagar cancelamento
    // para baixo nas camadas. O controller não implementa lógica de I/O — ele
    // apenas orquestra chamadas assíncronas ao repositório.
    [ApiController]
    [Route("dados")]
    public class DadosController : ControllerBase
    {
        private readonly Repositories.IAsyncDataRepository _repo;
        private readonly ILogger<DadosController> _logger;

        public DadosController(Repositories.IAsyncDataRepository repo, ILogger<DadosController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET /dados
        // - Recebe CancellationToken (injetado automaticamente pelo framework a partir
        //   do contexto da requisição). Quando o cliente cancela a requisição (fechar
        //   conexão) o token será sinalizado.
        // - O repositório deve aceitar o token e observar cancelamento.
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            _logger.LogInformation("Receiving /dados request (thread {ThreadId})", System.Environment.CurrentManagedThreadId);

            // Regras pedagógicas: validações leves no controller; delegue I/O e regras
            // de negócio para Application/Repository. Aqui só orquestramos.
            var data = await _repo.GetDataAsync(ct);

            return Ok(new { data, timestamp = System.DateTimeOffset.UtcNow });
        }
    }
}
