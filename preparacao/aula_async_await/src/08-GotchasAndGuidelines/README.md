# 08-GotchasAndGuidelines

Este projeto apresenta um checklist executável de anti-patterns e boas práticas em async/await, com demonstrações de armadilhas comuns.

### O objetivo
- Ensinar diretrizes para usar async/await corretamente, evitando deadlocks, exceções perdidas e overhead desnecessário.

#### O que são gotchas em async/await?
- Problemas comuns como deadlocks com `.Result`, exceções perdidas em `async void`, e captura de contexto desnecessária.

### Como executar

```bash
dotnet run --project src/08-GotchasAndGuidelines
```

### O que este projeto resolve
- Ajuda a identificar e evitar erros comuns em código assíncrono, melhorando robustez e performance.

### O que entrega
- Demonstrações executáveis:
  - Exceções com `await` vs. `AggregateException` com `.Wait()`.
  - Bloqueio com `.Result` (anti-padrão).
  - `ConfigureAwait(false)` em bibliotecas.
  - `async void` (anti-padrão, com alternativa segura).

### Recursos .NET/C# usados (explicação)
- **ConfigureAwait(false)**
  - Evita capturar `SynchronizationContext` do chamador; use em bibliotecas para não agendar de volta ao contexto (ex.: UI).

- **.Result e .Wait()**
  - Bloqueiam thread atual; podem causar deadlocks em contextos com sync (ex.: ASP.NET). Evite; use `await`.

- **AggregateException**
  - Embrulha exceções de tarefas; `.Wait()` lança isso em vez da exceção original.

- **async void**
  - Permite métodos assíncronos sem retorno; exceções se perdem. Use apenas em event handlers; prefira `Task`.

- **SynchronizationContext**
  - Contexto de sincronização (ex.: UI thread); `await` tenta retornar a ele, causando deadlocks se thread estiver bloqueada.

### Notas e boas práticas mencionadas no código
- **Evite async void**: Exceto event handlers; use wrapper com try/catch para capturar exceções.
- **Evite .Result/.Wait()**: Podem deadlockar; sempre prefira `await`.
- **Sufixo Async**: Convenção para métodos assíncronos (clareza).
- **ConfigureAwait(false)**: Em bibliotecas, evita overhead de contexto.

### Explicação rápida do fluxo do programa
- **Exceções**: `await` lança direto; `.Wait()` lança `AggregateException`.
- **Bloqueio**: `.Result` bloqueia thread (demonstra anti-padrão).
- **Biblioteca**: `ConfigureAwait(false)` simula chamada de lib.
- **Async void**: Demonstra alternativa segura com captura de exceções.

### Exemplo de uso prático
- Em ASP.NET: use `await` em controllers; `ConfigureAwait(false)` em services para evitar deadlocks.

### Desafios para os alunos
1. **Simule deadlock**: Use `.Result` em ASP.NET controller e demonstre deadlock.

2. **Capture exceções async void**: Modifique `FireAndForgetSafe` para lançar e ver captura.

3. **Compare contextos**: Use `SynchronizationContext.Current` antes/depois de `ConfigureAwait`.

4. **Refatore .Wait()**: Substitua `.Wait()` por `await` no código e compare.

5. **Adicione logging**: Logue exceções em `FireAndForgetInternalAsync` em vez de imprimir.

### Comentários finais
- Este README explica as diretrizes e demonstrações. Próximos projetos serão atualizados em seguida.

### Onde alterar o código

- `Program.cs` contém as demos organizadas em métodos claros — edite estes métodos para ajustar os cenários em sala de aula:
  - `PrintChecklist()` — lista as boas práticas/armadilhas exibidas no início.
  - `RunExceptionAwaitVsWaitAsync()` — demonstra diferença entre `await` (lança direto) e `.Wait()` (gera `AggregateException`).
  - `RunBlockingResultExample()` — demonstra o bloqueio causado por `.Result` (ajuste o `700` ms para tempos menores em demos rápidas).
  - `RunConfigureAwaitExampleAsync()` — simula uma chamada de biblioteca que usa `ConfigureAwait(false)`.
  - `RunFireAndForgetExampleAsync()` — demonstra uma alternativa segura ao `async void`.

Altere os tempos (`700 ms`, `100 ms`, etc.) e os pontos de medição (`Stopwatch`) para criar versões mais curtas e apropriadas para a sua aula.
