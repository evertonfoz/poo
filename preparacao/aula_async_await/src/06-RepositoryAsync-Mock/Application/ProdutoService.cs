using System;
using System.Threading.Tasks;
using RepositoryAsyncMock.Domain;
using RepositoryAsyncMock.Infra;

namespace RepositoryAsyncMock.Application
{
    /// <summary>
    /// Camada de Application: orquestra casos de uso, aplica regras de negócio
    /// (validações, invariantes) e delega I/O para a camada Infra.
    ///
    /// Regra prática: mantenha o modelo de domínio puro e execute validações
    /// síncronas localmente quando possível; deixe as chamadas assíncronas para
    /// os limites, como repositórios e serviços externos. Isso facilita testes
    /// unitários do domínio (sem precisar de infraestrutura assíncrona).
    ///
    /// Perguntas-guia:
    /// - O que testar no domínio? (validações, invariantes, regras que não dependem de I/O).
    /// - Onde entra a validação de dados? (validações simples no domínio; validações que precisam de dados existentes — ex.: unicidade — no Application antes do repositório).
    /// - Como tratar erros de infraestrutura? (Application deve mapear/decidir política de retry/compensação ou propagar para camadas superiores).
    /// </summary>
    public class ProdutoService
    {
        private readonly IProdutoRepository _repo;

        public ProdutoService(IProdutoRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task AddProdutoAsync(Produto produto)
        {
            // Validações síncronas de negócio
            if (produto is null) throw new ArgumentNullException(nameof(produto));
            if (string.IsNullOrWhiteSpace(produto.Nome))
                throw new ArgumentException("Nome é obrigatório", nameof(produto.Nome));
            if (produto.Preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero", nameof(produto.Preco));

            // Operação de I/O: delega ao repositório (assíncrono). Aqui ocorre a
            // transição para async — a camada Application orquestra, mas o trabalho
            // de I/O está na Infra.
            await _repo.AddAsync(produto);
        }

        public Task<Produto?> GetByIdAsync(int id)
        {
            // Validações simples
            if (id <= 0) throw new ArgumentException("Id inválido", nameof(id));

            return _repo.GetByIdAsync(id);
        }
    }
}
