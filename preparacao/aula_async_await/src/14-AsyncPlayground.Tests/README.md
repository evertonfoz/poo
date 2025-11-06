## 14-AsyncPlayground.Tests

Este projeto contém testes automatizados que validam comportamentos assíncronos das demos (unit/integration), com foco em cancelamento, agregação de tarefas e streams assíncronos.

### O objetivo
- Mostrar como escrever e organizar testes para cenários assíncronos: validação de orquestração (Task.WhenAll), cancelamento cooperativo e consumo de `IAsyncEnumerable`.

### Como executar

```bash
dotnet test --project 14-AsyncPlayground.Tests
```

ou, a partir da raiz da solução, para rodar todos os testes:

```bash
dotnet test
```

### O que este projeto resolve
- Garante que mudanças nas demos não introduzam regressões em comportamento assíncrono — por exemplo, cancelamentos que não propagam, tarefas que não retornam resultados ou streams que falham.

### O que entrega
- Testes que cobrem cenários como:
  - Agregação de resultados com `Task.WhenAll`.
  - Cancelamento e timeouts (validação de `OperationCanceledException`).
  - Consumo e interrupção de `IAsyncEnumerable`.
  - Exemplos de armadilhas (deadlocks) documentados e evitados nos testes.

### Recursos .NET/C# usados (explicação)
- **xUnit** — Framework de testes. Use `[Fact]` e `[Theory]` para marcar testes.
- **Assert / Assert.ThrowsAsync** — Verificações e validação de exceções assíncronas.
- **Task.WhenAll** — Para orquestrar múltiplas tarefas no teste e validar resultados agregados.
- **CancellationTokenSource** — Simula timeouts e cancelamento cooperativo nos testes.
- **IAsyncEnumerable / await foreach** — Para validar streams assíncronos e sua interrupção via token.

### Boas práticas e notas (no contexto dos testes)
- Prefira `async Task` como assinatura de teste; evite `void`.
- Evite usar `.Result`/`.Wait()` em testes — use `await` para não introduzir deadlocks.
- Use tokens curtos para simular timeouts e manter testes rápidos.
- Mantenha testes determinísticos: substitua latência real por `Task.Delay` controlado ou mocks.

### Fluxo resumido dos testes
- Cada teste configura os cenários (cts, inputs), executa a operação assíncrona com `await` e verifica resultados/exceções esperadas com `Assert`.

### Exemplos de cenários a validar
1. `WhenAll_ReturnsAllResults`: cria várias tarefas que retornam valores e valida que `Task.WhenAll` reúne todos.
2. `Operation_IsCancelled_ByTimeout`: usa `CancellationTokenSource` curto e verifica `OperationCanceledException`.
3. `AsyncStream_ProducesSequence`: consome `IAsyncEnumerable` com `await foreach` e valida sequência.

### Desafios para alunos (estender a suíte)
1. Adicione um teste que cause uma `AggregateException` via `WhenAll` com uma tarefa lançando.
2. Teste cancelamento manual chamando `cts.Cancel()` no meio da operação.
3. Escreva testes para `ValueTask` e documente cuidados ao awaitar múltiplas vezes.
4. Integre coverage (coverlet) e verifique linhas críticas cobertas.
5. Configure um workflow de CI (GitHub Actions) para rodar `dotnet test` em PRs.

### Observações finais
- Este README documenta o propósito do projeto de testes e como executar/estender os casos. Mantê-lo atualizado ajuda na manutenção das demos assíncronas.
## 14-AsyncPlayground.Tests

Este projeto contém testes unitários usando xUnit para validar comportamentos assíncronos dos projetos anteriores, incluindo `Task.WhenAll`, cancelamento, async streams e armadilhas comuns.

### O objetivo
- Demonstrar testes de código assíncrono, validando que operações cooperativas funcionam corretamente e evitando regressões.

#### O que são testes unitários em async?
- Testes que verificam comportamentos assíncronos, como agregação de resultados, cancelamento cooperativo e produção de sequências.

### Como executar

```bash
dotnet test --project 14-AsyncPlayground.Tests
```

### O que este projeto resolve
- Garante que mudanças nos projetos não quebrem comportamentos assíncronos esperados, especialmente em cenários de concorrência e cancelamento.

### O que entrega
- Testes executáveis com:
  - Validação de `Task.WhenAll` retornando todos os resultados.
  - Cancelamento por timeout lançando `OperationCanceledException`.
  - Produção de sequência correta em async streams.
  - Demonstração de armadilha com `.Result` (skipped para evitar deadlocks).

### Recursos .NET/C# usados (explicação)
- **xUnit**
  - Framework de testes; `[Fact]` marca métodos de teste.

- **Assert**
  - Métodos para verificações (ex.: `Assert.Equal`, `Assert.ThrowsAsync`).

- **Task.WhenAll**
  - Agrega resultados de múltiplas tarefas.

- **CancellationTokenSource**
  - Para simular timeouts em testes.

- **IAsyncEnumerable**
  - Para testar streams assíncronos com `await foreach`.

- **TaskCanceledException**
  - Subtipo de `OperationCanceledException` lançado por `Task.Delay` cancelado.

### Notas e boas práticas mencionadas no código
- **Teste async**: Use `async Task` em testes; aguarde com `await`.
- **Cancelamento**: Teste exceções de cancelamento com `Assert.ThrowsAsync`.
- **Deadlocks**: Evite `.Result` em testes; pode travar em contextos com sync.
- **Timeouts**: Use tokens curtos para testes rápidos.

### Explicação rápida do fluxo do programa
- Executa testes isoladamente; falha se assert falhar.
- Simula I/O com `Task.Delay`; valida resultados agregados.
- Testa cancelamento lançando exceções esperadas.
- Demonstra armadilhas sem executá-las (skipped).

### Exemplo de uso prático
- Em projetos reais: teste cancelamento em APIs, agregação em paralelismo, streams em processamento de dados.

### Desafios para os alunos
1. **Adicione mais testes**: Teste `AggregateException` em `WhenAll` com falhas.

2. **Teste cancelamento manual**: Use `cts.Cancel()` em vez de timeout.

3. **Valide streams com cancelamento**: Passe token para `ContarAsync` e teste interrupção.

4. **Meça cobertura**: Use coverlet para verificar cobertura dos testes.

5. **Integre CI**: Configure GitHub Actions para rodar testes automaticamente.

### Comentários finais
- Este README foi criado para o projeto de testes, explicando validações assíncronas. Todos os READMEs estão agora padronizados.