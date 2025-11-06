# 07-CPUvsIO

Este projeto demonstra a diferença entre operações I/O-bound (ligadas a entrada/saída) e CPU-bound (ligadas a processamento), e quando usar `Task.Run` vs. `await` direto.

### O objetivo
- Ensinar a escolher entre `await` para I/O assíncrono e `Task.Run` para trabalho CPU intensivo, evitando overhead desnecessário.

#### O que é I/O-bound?
- Operações que aguardam recursos externos (rede, disco, banco). Use `await` em APIs assíncronas; não envolva em `Task.Run`.

#### O que é CPU-bound?
- Operações que consomem CPU (cálculos). Use `Task.Run` para liberar o thread chamador e permitir paralelismo.

### Como executar

```bash
dotnet run --project src/07-CPUvsIO
```

### O que este projeto resolve
- Mostra por que `Task.Run` para I/O é anti-padrão (overhead sem benefício), e quando compensa para CPU-bound.

### O que entrega
- Comparação executável:
  - I/O simulado com `await Task.Delay` (correto).
  - I/O envolto em `Task.Run` (incorreto, para demonstração).
  - CPU-bound com `Task.Run` (libera thread).
  - CPU-bound síncrono (bloqueia thread).
  - Medição de tempo e IDs de threads.

### Recursos .NET/C# usados (explicação)
- **Task.Run**
  - Executa delegate no thread-pool. Use para CPU-bound longa; evita bloquear chamador (ex.: UI).

- **await**
  - Para operações I/O assíncronas; libera thread enquanto aguarda.

- **Stopwatch**
  - Para medir tempo de execução e comparar overhead.

- **Environment.CurrentManagedThreadId**
  - Mostra em qual thread o código roda, demonstrando liberação de threads.

- **Task.Delay**
  - Simula I/O assíncrono (rede/disco).

### Notas e boas práticas mencionadas no código
- **Não use Task.Run para I/O**: Já é assíncrono; `Task.Run` adiciona overhead sem benefício.
- **Use Task.Run para CPU-bound**: Se operação é longa e você quer liberar thread chamador.
- **Meça sempre**: Overhead de `Task.Run` (context switch) vs. benefício de paralelismo.
- **Escalabilidade**: I/O assíncrono não bloqueia threads; CPU-bound pode precisar de paralelismo.

### Explicação rápida do fluxo do programa
- **I/O-bound correto**: `await SimulatedIoAsync(1000)` — aguarda sem bloquear.
- **I/O incorreto**: `await Task.Run(() => SimulatedIoAsync(1000))` — overhead demonstrativo.
- **CPU com Task.Run**: `await Task.Run(() => CountPrimes(max))` — computa em thread-pool.
- **CPU síncrono**: `CountPrimes(max)` — bloqueia thread atual.

### Exemplo de uso prático
- Em web API: use `await` para chamadas de banco; `Task.Run` para cálculos pesados em background.

### Desafios para os alunos
1. **Meça overhead**: Compare tempos de I/O com/sem `Task.Run` para diferentes delays.

2. **Ajuste workload CPU**: Mude `max` em `CountPrimes` e veja quando `Task.Run` compensa.

3. **Adicione paralelismo**: Use `Parallel.For` dentro de `Task.Run` para acelerar CPU-bound.

4. **Simule I/O real**: Substitua `Task.Delay` por `HttpClient.GetAsync` e compare.

5. **Monitore threads**: Use `ThreadPool.GetAvailableThreads` antes/depois para ver impacto.

### Comentários finais
- Este README explica quando e por que usar cada abordagem. Continuando com os próximos projetos.

### Onde alterar o código

- `Program.cs` contém os exemplos demonstráveis e foi organizado em métodos claros — edite estes métodos para alterar o comportamento das demos:
  - `RunIoBoundExampleAsync()` — I/O assíncrono correto (usa `SimulatedIoAsync`).
  - `RunWrappedIoExampleAsync()` — demonstra o overhead quando se envolve I/O em `Task.Run`.
  - `RunCpuBoundWithTaskRunExampleAsync()` — demonstra CPU-bound executado em `Task.Run` (offload para thread-pool).
  - `RunCpuBoundBlockingExample()` — demonstra a execução CPU-bound síncrona (bloqueante) para comparação.

Altere os parâmetros (por exemplo `max` em `CountPrimes`) ou os tempos (atualmente `1000 ms` no `SimulatedIoAsync`) para criar cenários mais curtos para demonstração em sala de aula.
