## 01-AsyncBasics

Este projeto é uma demo mínima para explicar e demonstrar os conceitos básicos de programação assíncrona em C#/.NET.

> Nota: este repositório foi organizado em dois níveis didáticos. `01-AsyncBasics` é um mini-intro com exemplos mínimos e rápidos. Para demos mais completas (WhenAny, cancelamento, tratamento de exceções e comparações maiores), consulte `02-WhenAllWhenAny`.

### O objetivo
- Mostrar a diferença entre executar operações I/O-bound de forma sequencial vs. executá-las de forma assíncrona/concorrente usando async/await e `Task`.

#### O que é `async/await`?
- `async` e `await` são palavras-chave em C# para programação assíncrona. `async` marca um método como assíncrono, permitindo o uso de `await`. `await` suspende a execução do método até que uma `Task` seja concluída, sem bloquear a thread, permitindo que o controle retorne ao chamador.

#### O que é `Task`?
- Um `Task` é a abstração principal em .NET para representar uma operação assíncrona que será concluída no futuro. Em C# existem dois tipos principais:
  - `Task` — representa uma operação assíncrona que não retorna valor (equivalente a um 'void' assíncrono);
  - `Task<T>` — representa uma operação assíncrona que produzirá um resultado do tipo `T` quando completar.
- Tasks não significam necessariamente uma thread dedicada: elas representam trabalho/espera assíncrona (uma promessa/futuro). A execução pode usar o thread pool, I/O assíncrono do sistema operativo ou continuar em outro contexto de sincronização quando a operação completar.

### Como executar

```bash
dotnet run --project src/01-AsyncBasics
```

### O que este projeto resolve
- Ajuda a entender por que, em operações que aguardam I/O (por exemplo chamadas HTTP, leitura de arquivos, esperas), iniciar várias operações e aguardar todas com `Task.WhenAll` pode reduzir drasticamente o tempo total comparado a aguardar cada operação sequencialmente.

#### O que é `Task.WhenAll`?
- `Task.WhenAll` é um método estático que cria uma `Task` que completa quando todas as `Task`s fornecidas como argumentos terminarem. Permite executar operações em paralelo e aguardar a conclusão de todas de uma vez, otimizando tempo em cenários I/O-bound.

### O que entrega
- Um exemplo simples e reproduzível que:
  - Executa duas chamadas simuladas que usam `Task.Delay` para representar latência.

#### O que é `Task.Delay`?
- `Task.Delay` cria uma `Task` que completa após um atraso especificado (em milissegundos). É usado para simular esperas assíncronas sem bloquear threads, útil em testes e demos para representar operações I/O-bound.

  - Mede o tempo das duas abordagens (sequencial e concorrente) e mostra a diferença prática no tempo de execução.
  - Demonstra mensagens de início/conclusão com o `Thread.ManagedThreadId` para ilustrar como o fluxo pode alternar threads quando `await` é usado.

#### O que é `Thread.ManagedThreadId`?
- `Thread.ManagedThreadId` é uma propriedade que retorna o identificador único da thread gerenciada atual. É usado para demonstrar como `await` pode fazer a continuação executar em uma thread diferente do thread pool, especialmente em aplicações console sem contexto de sincronização.

### Recursos .NET/C# usados (explicação)
- **async Main (C# 7.1+)**
  - A assinatura `static async Task Main()` permite usar `await` diretamente no método de entrada da aplicação console.

- **Contexto de sincronização / threads**
  - Em aplicações console normalmente não há um `SynchronizationContext` que capture o contexto. Portanto, após um `await` a continuação pode executar em outro thread do thread pool.
  - O exemplo imprime `Thread.CurrentThread.ManagedThreadId` para demonstrar isso.

### Notas e boas práticas mencionadas no código
- **Cancelamento/timeout**: para produzir cancelamento cooperativo use `CancellationToken` e `Task.WhenAny`/`CancellationTokenSource` quando aplicar timeouts.
- **ConfigureAwait**: em bibliotecas ou UIs é comum usar `ConfigureAwait(false)` para evitar capturar o contexto; neste exemplo didático não é usado nem necessário.
- **Quando preferir concorrência**: quando operações são I/O-bound (esperas, chamadas de rede, E/S), concorrência com `Task.WhenAll` costuma reduzir o tempo total; para trabalho CPU-bound, prefira paralelismo controlado (e.g., `Parallel` ou `Task.Run` com cuidado) ou reescreva em termos de operações síncronas distribuídas.

### Explicação rápida do fluxo do programa
- **Execução sequencial**: chama `await BuscarDadosSimuladoAsync("A")` e só depois inicia a chamada para `B`. Tempo total ≈ soma das latências.
- **Execução assíncrona/concorrente**: inicia `var t1 = BuscarDadosSimuladoAsync("A")` e `var t2 = BuscarDadosSimuladoAsync("B")`, depois `await Task.WhenAll(t1, t2)` — tempo total ≈ máximo das latências (quando não há dependência entre as operações).

### Exemplo de uso prático
- Em um serviço que precisa buscar dados de vários endpoints independentes, usar `Task.WhenAll` reduz latência percebida e melhora throughput.

### Desafios para os alunos
1. **Modifique o código para usar `Task.WhenAny`**: Em vez de aguardar todas as tarefas com `Task.WhenAll`, use `Task.WhenAny` para processar a primeira tarefa que completar e cancele as outras. Meça o tempo e compare com a versão original.

   #### O que é `Task.WhenAny`?
   - `Task.WhenAny` é um método estático que cria uma `Task<Task>` que completa quando qualquer uma das `Task`s fornecidas como argumentos terminar primeiro. Útil para cenários onde você quer reagir à primeira conclusão, como timeouts ou seleção de resultados.

2. **Adicione cancelamento cooperativo**: Implemente um `CancellationToken` que permita cancelar as operações após um timeout (ex.: 1 segundo). Use `CancellationTokenSource` e passe o token para `BuscarDadosSimuladoAsync`.

  Sugestão para demonstração em sala: reduza o timeout para ~300 ms no `CancellationTokenSource` (ex.: `cts.CancelAfter(300)`) para garantir que as tarefas mais lentas sejam canceladas durante a atividade — isso torna o cancelamento visível para os alunos.

  **O que é um Token?**  
  Um token, no contexto de cancelamento em .NET, é um objeto que representa um sinal para interromper operações de forma controlada. Ele não força a parada imediata, mas permite que o código verifique se deve parar e limpe recursos adequadamente.

  Um token de cancelamento é um mecanismo leve em .NET para sinalizar que uma operação assíncrona deve ser interrompida de forma cooperativa. Ele permite que o código verifique periodicamente se o cancelamento foi solicitado, evitando bloqueios ou terminação forçada.

  #### O que é `CancellationToken` e `CancellationTokenSource`?
  - `CancellationToken` é uma estrutura leve que representa um sinal de cancelamento. Ele permite que operações assíncronas sejam canceladas de forma cooperativa, verificando se o cancelamento foi solicitado (via propriedade `IsCancellationRequested`) e lançando `OperationCanceledException` se necessário.
  - `CancellationTokenSource` é a classe que cria e gerencia o `CancellationToken`. Permite disparar cancelamento manualmente (método `Cancel`) ou após um tempo (método `CancelAfter`), útil para implementar timeouts em operações assíncronas.

3. **Simule operações com durações variáveis**: Altere `BuscarDadosSimuladoAsync` para aceitar uma duração variável (ex.: passe um parâmetro de tempo) e execute 3 operações simultâneas. Observe como `Task.WhenAll` se comporta com tempos diferentes.

4. **Use `ConfigureAwait(false)`**: Adicione `ConfigureAwait(false)` nas chamadas `await` e explique o impacto em aplicações com UI (embora este seja um console, discuta o conceito).

   #### O que é `ConfigureAwait`?
   - `ConfigureAwait(false)` indica que a continuação após `await` não precisa capturar o contexto de sincronização atual, evitando deadlocks em UIs e melhorando performance em bibliotecas, mas perdendo marshaling automático para threads de UI.

5. **Trate exceções em operações simultâneas**: Modifique uma das chamadas simuladas para lançar uma exceção (ex.: após o delay, `throw new Exception("erro simulado")`). Use `Task.WhenAll` e demonstre como capturar `AggregateException` ou exceções individuais.

  Sugestão prática: adicione um pequeno cenário onde uma das tarefas lança exceção e outra completa com sucesso. Mostre duas abordagens:
  - `try { await Task.WhenAll(t1, t2); } catch (Exception ex) { /* mostrar ex e, se for AggregateException, iterar InnerExceptions */ }` — aqui os alunos veem `AggregateException`/propagação.
  - `var completed = await Task.WhenAny(t1, t2); try { var result = await completed; } catch (Exception ex) { /* tratar exceção da tarefa específica */ }` — mostra como isolar/examinar a exceção de uma tarefa individual.

  #### O que é `AggregateException`?
  - `AggregateException` é uma exceção que agrupa múltiplas exceções lançadas por operações simultâneas (como em `Task.WhenAll`). Permite inspecionar e tratar exceções individuais dentro dela.

### Onde alterar o código (rápido)

- Os exemplos estão organizados em métodos no arquivo `src/01-AsyncBasics/Program.cs` para facilitar a leitura e experimentação:
  - `RunSequentialExampleAsync()` — demonstra execução sequencial.
  - `RunWhenAllExampleAsync()` — demonstra `Task.WhenAll` e tratamento de exceções agregadas.
  - `RunWhenAnyExampleAsync()` — demonstra `Task.WhenAny` (pegar o primeiro que completar).
  - `RunCancellationExampleAsync(int timeoutMs = 700)` — demonstra cancelamento via `CancellationTokenSource`. Para aula, altere para `RunCancellationExampleAsync(300)` ou mude o valor padrão para `300` para ver cancelamento visível.
  - `RunExceptionExamplesAsync()` — contém os exemplos com `SimulateErrorAsync` que lançam exceções para demonstrar `WhenAll` vs `WhenAny`.

- Helpers úteis:
  - `BuscarDadosSimuladoAsync(string id, CancellationToken ct = default)` — simula I/O com `Task.Delay` e suporta cancelamento.
  - `SimulateErrorAsync(string id)` — simula uma task que lança `Exception` (útil para demonstrar captura e inspeção de exceções).

- Para testar rapidamente no terminal:

```bash
dotnet run --project src/01-AsyncBasics
```

Altere o timeout para `300` ms ou ajuste os delays em `BuscarDadosSimuladoAsync`/`SimulateErrorAsync` para observar diferentes comportamentos em sala.

### Comentários finais
- Este README foi atualizado para explicar por que as construções usadas aparecem no código e quando aplicá-las em projetos reais. Se quiser, eu aplico o mesmo padrão aos outros READMEs do repositório.
