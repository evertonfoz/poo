## 03-AsyncExceptions

Este projeto demonstra o tratamento de exceções em operações assíncronas em C#.

### O objetivo
- Mostrar como capturar e tratar exceções lançadas em métodos assíncronos, incluindo casos individuais (`await` com try/catch) e múltiplas (`Task.WhenAll` com `AggregateException`).

#### O que é `AggregateException`?
- `AggregateException` é uma exceção que agrupa múltiplas exceções lançadas por operações simultâneas (como em `Task.WhenAll`). Permite inspecionar e tratar exceções individuais dentro dela.

### Como executar

```bash
dotnet run --project src/03-AsyncExceptions
```

### O que este projeto resolve
- Ajuda a entender como exceções assíncronas se propagam: em `await` individual, a exceção é lançada diretamente; em `Task.WhenAll`, múltiplas exceções são agrupadas em `AggregateException`, permitindo inspeção de todas as falhas.

### O que entrega
- Um exemplo executável que:
  - Demonstra try/catch em `await` individual, capturando `TimeoutException` lançada aleatoriamente.
  - Executa múltiplas operações com `Task.WhenAll` e mostra como inspecionar `AggregateException.InnerExceptions` para todas as exceções ocorridas.
  - Alternativamente, inspeciona `Task.IsFaulted` em tasks individuais para tratamento granular.

### Recursos .NET/C# usados (explicação)
- **async Main (C# 7.1+)**
  - A assinatura `static async Task Main()` permite usar `await` diretamente no método de entrada da aplicação console.

- **Task.WhenAll**
  - Permite aguardar a conclusão de várias `Task`s em paralelo. Retorna uma `Task` que completa quando todas as tasks fornecidas terminarem.
  - Se alguma Task lançar exceção, `WhenAll` propaga as exceções (em contextos async/await a exceção é re-lançada diretamente; internamente pode vir como AggregateException).

- **AggregateException**
  - Agrupa múltiplas exceções de operações simultâneas. A propriedade `InnerExceptions` contém a lista de exceções individuais.

- **Task.IsFaulted**
  - Propriedade booleana que indica se uma `Task` falhou (lançou exceção). Útil para inspecionar tasks individuais sem `await`.

- **TimeoutException**
  - Exceção lançada quando uma operação excede o tempo limite esperado. Usada aqui para simular falhas assíncronas.

- **Random.Shared**
  - Propriedade estática que fornece uma instância thread-safe de `Random`, ideal para gerar números aleatórios em contextos assíncronos ou multithread.

### Notas e boas práticas mencionadas no código
- **Propagação de exceções**: Em `await`, a primeira exceção é relançada diretamente; em `Task.WhenAll`, use `AggregateException` para acessar todas.
- **Inspeção granular**: Use `Task.IsFaulted` e `Task.Exception` para tratar falhas por task específica.
- **Práticas recomendadas**: Log/telemetria das inner exceptions, use `CancellationToken` para abortar operações falhidas, evite swallow de exceções sem tratamento.

### Explicação rápida do fluxo do programa
- **Await individual**: `try { await task; } catch (TimeoutException ex) { ... }` — captura direta da exceção lançada.
- **Task.WhenAll**: `await Task.WhenAll(tasks)` lança se qualquer falhe; inspecione `whenAllTask.Exception.InnerExceptions` para todas as exceções.
- **Inspeção alternativa**: Verifique `task.IsFaulted` e `task.Exception.InnerExceptions` para tratamento por task.

### Exemplo de uso prático
- Em um serviço que busca dados de múltiplas APIs, use `AggregateException` para logar todas as falhas (ex.: timeouts em diferentes endpoints) e decidir se retry ou fallback.

### Desafios para os alunos
1. **Adicione retry com AggregateException**: Após capturar `AggregateException`, implemente retry automático para tasks falhadas (ex.: até 3 tentativas por fonte).

2. **Combine com CancellationToken**: Modifique para usar `CancellationToken` e cancele operações restantes se uma falhar, evitando processamento desnecessário.

   #### O que é `CancellationToken`?
   - `CancellationToken` é uma estrutura leve que representa um sinal de cancelamento. Ele permite que operações assíncronas sejam canceladas de forma cooperativa, verificando se o cancelamento foi solicitado (via propriedade `IsCancellationRequested`) e lançando `OperationCanceledException` se necessário.

3. **Trate tipos específicos de exceção**: Lance diferentes tipos de exceções (ex.: `HttpRequestException`, `InvalidOperationException`) e demonstre como filtrar `InnerExceptions` por tipo.

4. **Use Task.WhenAny para primeira falha**: Combine `Task.WhenAny` com inspeção de falhas para reagir à primeira exceção lançada, abortando outras tasks.

5. **Implemente logging estruturado**: Adicione logging (ex.: console ou arquivo) das exceções capturadas, incluindo stack traces de `InnerExceptions`.

### Comentários finais
- Este README foi atualizado para explicar por que as construções usadas aparecem no código e quando aplicá-las em projetos reais. Se quiser, eu aplico o mesmo padrão aos outros READMEs do repositório.

### Onde alterar o código (rápido)

- O código está em `src/03-AsyncExceptions/Program.cs`. Os exemplos foram extraídos em métodos para facilitar experimentação e demonstração em sala:
  - `RunAwaitIndividualExampleAsync()` — demonstra `await` individual com `try/catch` para `TimeoutException`.
  - `RunWhenAllExampleAsync()` — demonstra `Task.WhenAll` e inspeção de `whenAllTask.Exception` (`AggregateException.InnerExceptions`).

- Helpers:
  - `BuscarDadosSimuladoAsync(string fonte)` — simula I/O com delay aleatório e taxa de falha (~50% por padrão). Para tornar demonstração mais determinística, ajuste a condição de falha ou parametriza a taxa de falha no código.

- Para executar rapidamente:

```bash
dotnet run --project src/03-AsyncExceptions
```

Altere a taxa de falha (valor em `Random.Shared.NextDouble() < 0.5`) ou os delays para controlar quantas tasks falham e demonstrar `AggregateException` de forma controlada.
