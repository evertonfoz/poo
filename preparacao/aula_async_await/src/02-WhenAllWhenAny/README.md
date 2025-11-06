## 02-WhenAllWhenAny

Este projeto demonstra e compara diferentes padrões de espera de tarefas assíncronas em C#.

### O objetivo
- Comparar três abordagens para executar múltiplas operações assíncronas: execução sequencial, `Task.WhenAll` (aguardar todas) e `Task.WhenAny` (obter a primeira concluída).

#### O que é `Task.WhenAll`?
- `Task.WhenAll` é um método estático que cria uma `Task` que completa quando todas as `Task`s fornecidas como argumentos terminarem. Permite executar operações em paralelo e aguardar a conclusão de todas de uma vez, otimizando tempo em cenários I/O-bound.

#### O que é `Task.WhenAny`?
- `Task.WhenAny` é um método estático que cria uma `Task<Task>` que completa quando qualquer uma das `Task`s fornecidas como argumentos terminar primeiro. Útil para cenários onde você quer reagir à primeira conclusão, como timeouts ou seleção de resultados.

### Como executar

```bash
dotnet run --project src/02-WhenAllWhenAny
```

### O que este projeto resolve
- Ajuda a entender as diferenças práticas entre aguardar tarefas uma por vez (sequencial), aguardar todas simultaneamente (`Task.WhenAll`) ou reagir à primeira conclusão (`Task.WhenAny`), incluindo impacto no tempo total e ordem de resultados.

### O que entrega
- Um exemplo executável que:
  - Executa três operações simuladas com atrasos aleatórios (200-800 ms) usando cada padrão.
  - Mede e exibe os tempos para cada abordagem, mostrando como `Task.WhenAll` reduz tempo comparado ao sequencial, e `Task.WhenAny` responde rapidamente ao primeiro resultado.
  - Demonstra a ordem de resultados: `Task.WhenAll` mantém a ordem das tasks, enquanto `Task.WhenAny` destaca a primeira concluída.

### Recursos .NET/C# usados (explicação)
- **async Main (C# 7.1+)**
  - A assinatura `static async Task Main()` permite usar `await` diretamente no método de entrada da aplicação console.

- **Task.WhenAll**
  - Permite aguardar a conclusão de várias `Task`s em paralelo. Retorna uma `Task` que completa quando todas as tasks fornecidas terminarem.
  - Se alguma Task lançar exceção, `WhenAll` propaga as exceções (em contextos async/await a exceção é re-lançada diretamente; internamente pode vir como AggregateException).

- **Task.WhenAny**
  - Permite aguardar a conclusão da primeira `Task` entre várias. Retorna uma `Task<Task>` que completa quando qualquer task terminar, permitindo acesso ao resultado da primeira concluída.

- **Task.Delay**
  - Usado para simular latência/espera sem bloquear threads. É útil em demos e testes para representar I/O-bound work.

- **Random.Shared**
  - Propriedade estática que fornece uma instância thread-safe de `Random`, ideal para gerar números aleatórios em contextos assíncronos ou multithread.

- **Stopwatch**
  - Classe para medir intervalos de tempo de alta precisão, usada aqui para comparar performance das abordagens.

### Notas e boas práticas mencionadas no código
- **Exceções em WhenAll**: Se qualquer task lançar exceção, `WhenAll` propaga como `AggregateException` (ou diretamente em async/await); inspecione tasks individualmente se falhas isoladas são esperadas.
- **Uso de WhenAny**: Ideal para "race conditions" (primeiro a terminar vence) ou timeouts (combine com `Task.Delay`).
- **Random.Shared**: Thread-safe, evita problemas de concorrência em geração de números aleatórios.
- **Aguardar libera thread**: `await Task.Delay` não bloqueia a thread, permitindo escalabilidade.

### Explicação rápida do fluxo do programa
- **Sequencial**: Loop `for` com `await` em cada chamada — tempo total ≈ soma dos atrasos.
- **Task.WhenAll**: Cria array de tasks, `await Task.WhenAll(tasks)` — tempo total ≈ máximo dos atrasos, resultados na ordem original.
- **Task.WhenAny**: Cria array de tasks, `await Task.WhenAny(tasks)` retorna a primeira task concluída — tempo até o primeiro resultado, depois opcionalmente `await Task.WhenAll` para os demais.

### Exemplo de uso prático
- Em um agregador de feeds, use `Task.WhenAll` para buscar dados de múltiplas fontes em paralelo e aguardar todos; use `Task.WhenAny` para responder ao primeiro feed disponível ou implementar timeout.

### Desafios para os alunos
1. **Implemente timeout com Task.WhenAny**: Combine `Task.WhenAny` com `Task.Delay` para impor um timeout (ex.: 500 ms) nas operações. Se `Task.Delay` completar primeiro, cancele as outras tasks usando `CancellationToken`.

   #### O que é `CancellationToken`?
   - `CancellationToken` é uma estrutura leve que representa um sinal de cancelamento. Ele permite que operações assíncronas sejam canceladas de forma cooperativa, verificando se o cancelamento foi solicitado (via propriedade `IsCancellationRequested`) e lançando `OperationCanceledException` se necessário.

2. **Trate exceções em Task.WhenAll**: Modifique uma operação para lançar uma exceção. Demonstre como capturar e inspecionar `AggregateException` ou exceções individuais após `await Task.WhenAll`.

   #### O que é `AggregateException`?
   - `AggregateException` é uma exceção que agrupa múltiplas exceções lançadas por operações simultâneas (como em `Task.WhenAll`). Permite inspecionar e tratar exceções individuais dentro dela.

3. **Compare com Parallel.ForEach**: Substitua as operações assíncronas por síncronas e use `Parallel.ForEach` para paralelismo CPU-bound. Compare tempos e explique quando usar async vs. paralelo.

4. **Adicione logging de progresso**: Use `Task.WhenAny` em um loop para processar tasks à medida que completam, logando cada conclusão sem aguardar todas.

5. **Simule dependências**: Modifique para que uma operação dependa do resultado de outra (ex.: buscar "B" só após "A"). Compare sequencial obrigatório vs. paralelo onde possível.

### Comentários finais
- Este README foi atualizado para explicar por que as construções usadas aparecem no código e quando aplicá-las em projetos reais. Se quiser, eu aplico o mesmo padrão aos outros READMEs do repositório.

### Onde alterar o código (rápido)

- O código principal está em `src/02-WhenAllWhenAny/Program.cs`. Os exemplos foram extraídos em métodos para facilitar experimentação:
  - `RunSequentialExampleAsync(string[] fontes)` — demonstra execução sequencial (await por item).
  - `RunWhenAllExampleAsync(string[] fontes)` — demonstra `Task.WhenAll` (iniciar todas e aguardar todas).
  - `RunWhenAnyExampleAsync(string[] fontes)` — demonstra `Task.WhenAny` (pegar a primeira que completar).

- Helpers:
  - `BuscarDadosSimuladoAsync(string fonte)` — simula I/O com delays aleatórios (200–800 ms). Ajuste os valores de `Random.Shared.Next(...)` para experimentar diferentes cenários.

- Para tornar o timeout visível em sala, combine `Task.WhenAny` com `Task.Delay(timeout)` ou ajuste os delays para que algumas tasks demorem mais (por exemplo, `Task.Delay(1000)` numa das fontes).

- Execução rápida:

```bash
dotnet run --project src/02-WhenAllWhenAny
```
