# 13-Benchmarks

Este projeto usa BenchmarkDotNet para comparar performance: sequencial vs. `Task.WhenAll` para I/O, e `Task<T>` vs. `ValueTask<T>` em cenários de cache quente.

### O objetivo
- Fornecer medições empíricas para escolher entre padrões assíncronos, focando em latência e alocações de memória.

#### O que são benchmarks em async?
- Testes de performance isolados para comparar overhead de diferentes abordagens assíncronas.

### Como executar

```bash
dotnet run -c Release --project 13-Benchmarks
```

### O que este projeto resolve
- Ajuda a decidir quando paralelismo ou `ValueTask` compensam, baseado em dados reais.

### O que entrega
- Benchmarks executáveis com:
  - I/O sequencial vs. `WhenAll` (10/50 operações).
  - Cache quente: `Task<T>` vs. `ValueTask<T>`.
  - Relatórios de tempo médio, desvio, memória (GC).

### Recursos .NET/C# usados (explicação)
- **BenchmarkDotNet**
  - Biblioteca para benchmarks precisos; roda em processo isolado.

- **[Benchmark]**
  - Marca métodos como benchmarks; compara com baseline.

- **[Params]**
  - Varia parâmetros (ex.: número de operações).

- **[MemoryDiagnoser]**
  - Mede alocações de memória e GC.

- **Task.WhenAll**
  - Aguarda múltiplas tarefas em paralelo.

- **ValueTask<T>**
  - Evita alocações em caminhos quentes.

### Notas e boas práticas mencionadas no código
- **WhenAll**: Beneficia I/O concorrente, mas evite sobrecarga de recursos.
- **ValueTask**: Só para caminhos muito quentes; meça antes.
- **Limitações**: Benchmarks sintéticos; teste com workloads reais.
- **Interpretação**: Foque em p95/p99, não só média.

### Explicação rápida do fluxo do programa
- Executa benchmarks variando parâmetros.
- Gera relatório com tempos e memória.
- Compara sequencial (baseline) vs. paralelo, Task vs. ValueTask.

### Exemplo de uso prático
- Em APIs: use benchmarks para decidir se paralelizar queries independentes.

### Desafios para os alunos
1. **Adicione mais cenários**: Benchmark `async void` vs. `Task` para event handlers.

2. **Meça throughput**: Use `BenchmarkDotNet` para operações/segundo.

3. **Varia delays**: Teste com delays reais de rede.

4. **Profile GC**: Adicione `[GcServer]` para ver impacto em server GC.

5. **Compare plataformas**: Rode em Windows/Linux para ver diferenças.

### Comentários finais
- Este README conclui as atualizações. Todos os READMEs foram padronizados com explicações detalhadas.