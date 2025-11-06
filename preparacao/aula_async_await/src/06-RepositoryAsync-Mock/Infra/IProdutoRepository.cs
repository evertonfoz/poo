using System.Threading.Tasks;
using RepositoryAsyncMock.Domain;

namespace RepositoryAsyncMock.Infra
{
    /// <summary>
    /// Contrato de acesso a dados (I/O). Métodos são assíncronos porque representam
    /// operações de I/O (banco, rede, arquivos). A camada de infraestrutura é a
    /// borda onde o async faz sentido — o domínio e regras de negócio não precisam
    /// ser assíncronas por si só.
    /// </summary>
    public interface IProdutoRepository
    {
        Task<Produto?> GetByIdAsync(int id);

        Task AddAsync(Produto produto);
    }
}
