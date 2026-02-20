# 10-Integrador

Projeto integrador que combina e demonstra os padrões estudados: `Task.WhenAll`, Cancellation+Timeout, Async Streams e Sequential vs Parallel.

### O objetivo
- Integrar conceitos de async/await em um tour final, mostrando aplicação prática de múltiplos padrões em seções independentes.

#### O que é um projeto integrador?
- Demonstração combinada de técnicas assíncronas, contrastando abordagens e destacando quando usar cada uma.

### Como executar

```bash
dotnet run --project src/10-Integrador
```

### O que este projeto resolve
- Ajuda a consolidar aprendizado, mostrando como combinar padrões para resolver problemas reais de concorrência e I/O.

### O que entrega
- Quatro seções executáveis:
  - **WhenAll**: Execução paralela de tarefas independentes.
  - **Cancellation+Timeout**: Cancelamento cooperativo com tokens.
  - **Async Streams**: Produção/consumo incremental com `IAsyncEnumerable`.
  - **Sequential vs Parallel**: Comparação de tempos entre sequencial e concorrente.

### Recursos .NET/C# usados (explicação)
- **Task.WhenAll**
  - Aguarda múltiplas tarefas em paralelo; retorna array de resultados.

- **CancellationToken**
  - Para cancelamento cooperativo; passado para `Task.Delay` e checado em loops.

- **IAsyncEnumerable e await foreach**
  - Para streams assíncronos; consome itens incrementalmente.

- **Stopwatch**
  - Mede tempos para comparar sequencial vs. paralelo.

- **OperationCanceledException**
  - Lançada quando cancelamento é solicitado.

### Notas e boas práticas mencionadas no código
- **Paralelismo**: Use `WhenAll` para tarefas independentes; evite para dependentes.
- **Cancelamento**: Sempre passe token para operações canceláveis.
- **Streams**: Use para dados grandes/incrementais; evite materializar tudo.
- **Limite paralelismo**: Paralelismo excessivo pode saturar recursos.

### Explicação rápida do fluxo do programa
- **WhenAll**: Inicia tarefas, aguarda todas; tempo próximo ao mais lento.
- **Cancellation**: Cancela após 700ms; lança `OperationCanceledException`.
- **Streams**: `await foreach` consome contagem regressiva incrementalmente.
- **Sequential/Parallel**: Compara tempos; paralelo é mais rápido para independentes.

### Exemplo de uso prático
- Em processamento de dados: use streams para leitura, paralelismo para cálculos independentes, cancelamento para timeouts.

### Desafios para os alunos
1. **Adicione exceções**: Faça uma tarefa em `WhenAll` lançar e veja `AggregateException`.

2. **Combine cancelamento**: Use token em streams com `WithCancellation`.

3. **Limite paralelismo**: Use `SemaphoreSlim` para controlar grau de paralelismo em `WhenAll`.

4. **Meça recursos**: Monitore CPU/memória durante sequencial vs. paralelo.

5. **Adicione progresso**: Reporte progresso em streams ou paralelismo.

### Comentários finais
- Este README integra os conceitos. Agora, os projetos na raiz (09, 11, etc.) serão atualizados.

### Onde alterar o código

- `Program.cs` já organiza o tour em seções bem nomeadas — edite os métodos abaixo para ajustar exemplos e tempos para suas demonstrações:
  - `SectionWhenAll()` — altera a quantidade de tarefas e delays (aqui usa 3 tarefas com delays `i * 300`).
  - `SectionCancellationAndTimeout()` — ajusta o timeout do `CancellationTokenSource` (atualmente `700 ms`) e os delays na operação cancelável (`200 ms` por iteração).
  - `SectionAsyncStreams()` — altera o `CountdownAsync(from, delayMs)` para ajustar número de itens e intervalo entre eles (ex.: `from=5`, `delayMs=200`).
  - `SectionSequentialVsParallel()` — ajuste o `300 ms` por tarefa e o número de iterações para tornar as demos mais rápidas em sala.

Altere estes parâmetros para criar versões mais curtas (por exemplo, reduzir `300 ms` para `100–200 ms`) quando precisar de demonstrações mais rápidas. Para demonstrações de falhas, adicione uma tarefa que lance uma exceção em `SectionWhenAll()` e observe o comportamento do `Task.WhenAll`.
