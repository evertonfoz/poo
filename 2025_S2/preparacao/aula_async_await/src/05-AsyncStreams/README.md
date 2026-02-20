## 05-AsyncStreams

Este projeto demonstra o uso de async streams com `IAsyncEnumerable<T>` para processar sequências de dados de forma assíncrona, permitindo consumo item por item sem carregar tudo em memória.

### O objetivo
- Ensinar como implementar e consumir streams assíncronos, incluindo cancelamento cooperativo e controle de fluxo.

#### O que é `IAsyncEnumerable<T>`?
- `IAsyncEnumerable<T>` é uma interface para sequências assíncronas, permitindo produzir e consumir elementos com `await`. Diferente de `IEnumerable<T>`, suporta operações assíncronas como `await foreach`.

#### O que é `await foreach`?
- Sintaxe para iterar sobre `IAsyncEnumerable<T>`, aguardando cada elemento. Permite processamento incremental, ideal para dados grandes ou que chegam ao longo do tempo.

### Como executar

```bash
dotnet run --project src/05-AsyncStreams
```

### O que este projeto resolve
- Mostra como lidar com dados sequenciais assíncronos (ex.: leitura de rede, logs) sem bloquear memória, permitindo começar processamento antes de ter todos os dados.

### O que entrega
- Um exemplo executável que demonstra:
  - Consumo simples com `await foreach` — itera sobre números gerados assincronamente.
  - Cancelamento com `WithCancellation` — interrompe iteração via token externo.
  - Cancelamento direto no produtor — passa `CancellationToken` para o método gerador.
  - Extensão helper `WithCancellation` — facilita cancelamento em iteradores.

### Recursos .NET/C# usados (explicação)
- **IAsyncEnumerable<T>**
  - Interface para streams assíncronos. Métodos como `GetAsyncEnumerator` retornam enumeradores assíncronos.

- **await foreach**
  - Sintaxe para iterar sobre `IAsyncEnumerable<T>`. Aguarda cada `MoveNextAsync()` e acessa `Current`.

- **yield return**
  - Palavra-chave para produzir elementos em métodos assíncronos, criando enumeradores sob demanda.

- **[EnumeratorCancellation]**
  - Atributo em parâmetros `CancellationToken` de métodos `IAsyncEnumerable`. Permite passar token via `GetAsyncEnumerator(token)`.

- **WithCancellation**
  - Extensão para `IAsyncEnumerable<T>` que aceita `CancellationToken`, chamando `GetAsyncEnumerator(ct)` internamente.

- **Task.Delay (com CancellationToken)**
  - Usado no produtor para simular atraso assíncrono, respeitando cancelamento.

### Notas e boas práticas mencionadas no código
- **Streams vs. listas**: Use streams para dados grandes ou infinitos; listas para pequenos conjuntos.
- **Backpressure**: Consumidores controlam taxa via `await`; produtores devem respeitar (ex.: delays).
- **Cancelamento**: Sempre passe tokens para operações canceláveis; use `WithCancellation` ou `[EnumeratorCancellation]`.
- **Dispose**: `await using` garante limpeza do enumerador.

### Explicação rápida do fluxo do programa
- **Consumo simples**: `await foreach (var n in ContarAsync(5, 250))` — imprime números 1 a 5 com atraso.
- **Cancelamento externo**: `WithCancellation(cts.Token)` — cancela após 900ms, lançando `OperationCanceledException`.
- **Cancelamento no produtor**: Passa token direto para `ContarAsync`, cancelando no `Task.Delay` ou `ThrowIfCancellationRequested`.
- **Extensão**: `WithCancellation` chama `GetAsyncEnumerator(ct)` para propagar token.

### Exemplo de uso prático
- Processar logs de servidor em tempo real: use async stream para ler linhas de arquivo/network, processando cada uma sem carregar tudo.

### Desafios para os alunos
1. **Implemente filtro assíncrono**: Modifique `ContarAsync` para filtrar números pares, usando `yield return` condicional.

2. **Adicione buffering**: Use `System.Threading.Channels` para bufferizar itens, simulando backpressure controlado.

3. **Combine com Task.WhenAll**: Crie múltiplos streams e processe em paralelo, mas consuma sequencialmente.

4. **Trate exceções no produtor**: Lance uma exceção no loop de `ContarAsync` e veja como `await foreach` a propaga.

5. **Simule stream infinito**: Remova limite `ate` e use cancelamento para parar; teste com timeout.

### Comentários finais
- Este README foi criado para explicar as construções de async streams e quando aplicá-las. O padrão será aplicado aos outros projetos.

### Onde alterar o código (rápido)

- O código principal está em `src/05-AsyncStreams/Program.cs`. Os exemplos foram extraídos em métodos para facilitar experimentação:
  - `RunSimpleConsumeAsync()` — demonstra consumo simples com `await foreach`.
  - `RunWithCancellationUsingExtensionAsync()` — demonstra cancelamento via extensão `WithCancellation(ct)`.
  - `RunProducerCancellationAsync()` — demonstra passar o `CancellationToken` direto ao produtor (`[EnumeratorCancellation]`).

- Helpers:
  - `ContarAsync(int ate, int atrasoMs, CancellationToken ct = default)` — produtor `IAsyncEnumerable<int>` que usa `Task.Delay(atrasoMs, ct)` e `ct.ThrowIfCancellationRequested()`.
  - `AsyncEnumerableExtensions.WithCancellation<T>` — extensão que chama `GetAsyncEnumerator(ct)` internamente para propagar token.

- Para demo em sala: ajuste os delays e os tempos de cancelamento (por exemplo, reduza 900ms para 300ms) para visualizar cancelamento de forma mais evidente.

- Execução rápida:

```bash
dotnet run --project src/05-AsyncStreams
```