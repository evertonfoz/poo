# 06-RepositoryAsync-Mock

Este projeto demonstra a separação de camadas (Domain, Application, Infra) em uma arquitetura assíncrona, usando um repositório mock que simula I/O com `Task.Delay`.

### O objetivo
- Ensinar como estruturar aplicações assíncronas em camadas, aplicando async apenas onde necessário (na infraestrutura de I/O).

#### O que é separação de camadas?
- **Domain**: Modelos e regras de negócio puras (síncronas).
- **Application**: Orquestra casos de uso, validações e delega I/O.
- **Infra**: Implementa acesso a dados externos (banco, rede) de forma assíncrona.

### Como executar

```bash
dotnet run --project src/06-RepositoryAsync-Mock
```

### O que este projeto resolve
- Mostra como manter o domínio síncrono e testável, enquanto a infraestrutura lida com async para I/O, evitando complexidade desnecessária.

### O que entrega
- Um exemplo executável com:
  - Entidade `Produto` no domínio.
  - Serviço `ProdutoService` na aplicação (validações e orquestração).
  - Repositório mock na infra (simula I/O assíncrono com `Task.Delay`).
  - Demonstração de adição e busca de produtos, com tratamento de erros.

### Recursos .NET/C# usados (explicação)
- **Task e async/await**
  - Usados na infra para simular I/O assíncrono; domínio e aplicação permanecem síncronos onde possível.

- **SemaphoreSlim**
  - Para thread-safety em operações concorrentes no mock (simula locks de banco).

- **Interfaces assíncronas**
  - `IProdutoRepository` define contratos assíncronos para acesso a dados.

- **Task.Delay**
  - Simula latência de I/O (rede/banco) no repositório mock.

- **ArgumentException/ArgumentNullException**
  - Para validações de negócio na aplicação.

### Notas e boas práticas mencionadas no código
- **Async na borda**: Use async apenas para I/O; mantenha domínio puro e testável.
- **Validações**: Síncronas no domínio/aplicação; assíncronas apenas se dependerem de I/O.
- **Thread-safety**: Use locks (ex.: `SemaphoreSlim`) para proteger estado compartilhado.
- **Testabilidade**: Separe camadas para testar domínio sem infraestrutura.

### Explicação rápida do fluxo do programa
- Cria repositório mock e serviço.
- Adiciona produtos válidos (validações síncronas, I/O assíncrono).
- Busca por ID (I/O simulado).
- Tenta adicionar inválido (lança `ArgumentException`).

### Exemplo de uso prático
- Em uma API web, use camadas similares: domínio para regras, aplicação para lógica, infra para Entity Framework assíncrono.

### Desafios para os alunos
1. **Adicione validação assíncrona**: Modifique `AddProdutoAsync` para verificar unicidade de nome no repositório (chamada assíncrona).

2. **Implemente delete**: Adicione método `DeleteAsync` na interface e implementações, com validações na aplicação.

3. **Use CancellationToken**: Passe token para métodos assíncronos e cancele operações longas.

4. **Teste unitário**: Escreva teste para `ProdutoService` usando mock de repositório (sem I/O real).

5. **Adicione logging**: Inclua logs assíncronos na infra (ex.: escrever em arquivo).

### Comentários finais
- Este README explica a arquitetura e recursos usados. Aplicando o padrão aos próximos projetos.

### Onde alterar o código (rápido)

- O código principal está em `src/06-RepositoryAsync-Mock/Program.cs`. Os exemplos foram extraídos em métodos para facilitar experimentação:
  - `RunAddAndFindExamplesAsync(ProdutoService service)` — demonstra adicionar produtos e buscar por id.
  - `RunInvalidProductExampleAsync(ProdutoService service)` — demonstra tentativa de adicionar produto inválido e tratamento de validação.

- Camadas e helpers:
  - `Domain/Produto.cs` — entidade do domínio com validações básicas.
  - `Application/ProdutoService.cs` — orquestra validações e delega I/O ao repositório.
  - `Infra/MockProdutoRepository.cs` — repositório mock que simula I/O via `Task.Delay` e usa `SemaphoreSlim` para thread-safety.

- Para demonstração em sala:
  - Ajuste os delays em `MockProdutoRepository` para simular latências maiores/menores.
  - Experimente adicionar validação assíncrona (ex.: checar unicidade com chamada ao repositório) e observe como a aplicação permanece testável.

- Execução rápida:

```bash
dotnet run --project src/06-RepositoryAsync-Mock
```
