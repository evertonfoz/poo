# 09-ValueTaskMicroOptimizations

Este projeto demonstra quando usar `ValueTask<T>` para micro-otimizações, evitando alocações desnecessárias em caminhos quentes (ex.: cache hits).

### O objetivo
- Ensinar a usar `ValueTask<T>` em cenários onde o resultado é frequentemente síncrono, reduzindo alocações de `Task<T>`.

#### O que é `ValueTask<T>`?
- Estrutura leve que pode representar um resultado já disponível (sem alocação) ou um `Task<T>` pendente. Use para otimizar caminhos quentes após profiling.

### Como executar

```bash
dotnet run --project 09-ValueTaskMicroOptimizations
```

### O que este projeto resolve
- Mostra como evitar overhead de alocações em APIs assíncronas que frequentemente retornam imediatamente.

### O que entrega
- Demonstração executável:
  - Cache hit: `ValueTask.FromResult` (sem alocação).
  - Cache miss: Fallback para `Task` (I/O simulado).
  - Comparação de caminhos quente vs. frio.

### Recursos .NET/C# usados (explicação)
- **ValueTask<T>**
  - Tipo valor para resultados assíncronos; evita alocação se resultado já pronto.

- **ValueTask.FromResult**
  - Cria `ValueTask<T>` completada imediatamente, sem `Task` alocado.

- **ConcurrentDictionary**
  - Dicionário thread-safe para simular cache.

- **Task.Delay**
  - Simula I/O no caminho frio.

### Notas e boas práticas mencionadas no código
- **Use após profiling**: Benefício pequeno; só para caminhos muito quentes.
- **Não await duas vezes**: `ValueTask` pode usar recursos pooled.
- **Não armazene**: Converta para `Task<T>` se precisar armazenar.
- **Cenário ideal**: Cache hits frequentes.

### Explicação rápida do fluxo do programa
- **Cache hit**: `ValueTask.FromResult` retorna sem alocação.
- **Cache miss**: Chama `GetFromIoAsync`, retorna `Task`.
- **Await**: Ambos suportam `await` igual.

### Exemplo de uso prático
- Em bibliotecas de cache: use `ValueTask` para lookups que frequentemente acertam.

### Desafios para os alunos
1. **Meça alocações**: Use `BenchmarkDotNet` para comparar `Task<T>` vs. `ValueTask<T>`.

2. **Adicione mais cenários**: Implemente cache com expiração e veja impacto.

3. **Evite armadilhas**: Tente await duas vezes e veja comportamento.

4. **Combine com streams**: Use `ValueTask` em `IAsyncEnumerable` para otimizações.

5. **Profile real**: Use dotTrace ou similar para medir alocações em app real.

### Comentários finais
- Este README explica otimizações avançadas. Próximos projetos serão atualizados.

### Onde alterar o código

- `Program.cs` contém as demos organizadas em métodos claros — edite estes métodos para ajustar os cenários em sala de aula:
  - `RunCacheHitExampleAsync(ConcurrentDictionary<int,string> store)` — demonstra `ValueTask.FromResult` para cache hit (sem alocação de Task).
  - `RunCacheMissExampleAsync(ConcurrentDictionary<int,string> store)` — demonstra fallback para I/O (`Task`) em cache miss.
  - `GetValueAsync(int id, ConcurrentDictionary<int,string> cache)` — implementação que retorna `ValueTask<string>`; altere a lógica de cache se desejar.
  - `GetFromIoAsync(int id)` — simula I/O com `Task.Delay(200)`; ajuste o delay para demos mais curtas (ex.: 100 ms) durante a aula.

Altere os delays e a composição do cache para criar cenários de comparação mais curtos em sala (por exemplo reduzir `Task.Delay(200)` para `100` ou ajustar a taxa de cache hits/misses).
