using System;

namespace RepositoryAsyncMock.Domain
{
    /// <summary>
    /// Entidade de domínio mínima para demonstrar separação de camadas.
    /// Nota: regras de validação de negócio simples podem estar aqui (invariantes),
    /// mas decisões que dependem de I/O ou de infraestrutura devem ficar na
    /// camada Application (ex.: verificar unicidade no repositório).
    /// </summary>
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public Produto(string nome, decimal preco)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Preco = preco;
        }
    }
}
