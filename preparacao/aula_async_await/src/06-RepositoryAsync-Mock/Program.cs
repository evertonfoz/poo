using System;
using System.Threading.Tasks;
using RepositoryAsyncMock.Domain;
using RepositoryAsyncMock.Infra;
using RepositoryAsyncMock.Application;

namespace RepositoryAsyncMock
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Demo: Repository async (mock) - separação de camadas\n");

            var repo = new MockProdutoRepository();
            var service = new ProdutoService(repo);

            await RunAddAndFindExamplesAsync(service);
            Console.WriteLine();
            await RunInvalidProductExampleAsync(service);

            Console.WriteLine("Fim do demo.");
        }

        static async Task RunAddAndFindExamplesAsync(ProdutoService service)
        {
            try
            {
                var p1 = new Produto("Caneta", 3.5m);
                var p2 = new Produto("Caderno", 25.0m);

                Console.WriteLine("Adicionando produtos...");
                await service.AddProdutoAsync(p1);
                await service.AddProdutoAsync(p2);

                Console.WriteLine($"Produto 1 adicionado com Id: {p1.Id}");
                Console.WriteLine($"Produto 2 adicionado com Id: {p2.Id}");

                Console.WriteLine("Buscando produto por id (1)...");
                var found = await service.GetByIdAsync(1);
                Console.WriteLine(found is null ? "Não encontrado" : $"Encontrado: {found.Nome} ({found.Preco:C})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado ao adicionar/buscar: {ex.GetType().Name} - {ex.Message}");
            }
        }

        static async Task RunInvalidProductExampleAsync(ProdutoService service)
        {
            Console.WriteLine("Tentando adicionar produto inválido (preço negativo)...");
            var bad = new Produto("Lapiseira", -1);
            try
            {
                await service.AddProdutoAsync(bad);
                Console.WriteLine("(não esperado) produto inválido adicionado");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro esperado ao validar/operar: {ex.GetType().Name} - {ex.Message}");
            }
        }
    }
}
