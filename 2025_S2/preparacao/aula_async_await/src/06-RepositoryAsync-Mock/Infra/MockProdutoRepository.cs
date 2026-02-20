using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using RepositoryAsyncMock.Domain;

namespace RepositoryAsyncMock.Infra
{
    /// <summary>
    /// Implementação em memória que simula I/O com Task.Delay.
    /// Use esta implementação para testes manuais e para ensinar onde o async
    /// entra: aqui, na infraestrutura. Em produção, esta camada faria chamadas
    /// ao banco de dados, serviços externos, arquivos etc.
    /// </summary>
    public class MockProdutoRepository : IProdutoRepository
    {
        private readonly List<Produto> _store = new();
        private int _nextId = 1;
        private readonly SemaphoreSlim _lock = new(1,1);

        public async Task AddAsync(Produto produto)
        {
            // Simula latência de I/O
            await Task.Delay(200);

            await _lock.WaitAsync();
            try
            {
                produto.Id = _nextId++;
                _store.Add(produto);
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<Produto?> GetByIdAsync(int id)
        {
            // Simula latência de I/O
            await Task.Delay(150);

            // busca simples em memória
            // Nota pedagógica: aqui fazemos I/O (simulado) — o Application chama
            // este método e aguarda o resultado. A lógica de negócio já deve
            // estar validada antes desta chamada, se possível.
            return _store.FirstOrDefault(p => p.Id == id);
        }
    }
}
