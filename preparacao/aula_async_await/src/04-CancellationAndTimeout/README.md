## 04-CancellationAndTimeout

Este projeto demonstra o uso de `CancellationToken`, timeout e tokens encadeados (linked tokens) para cancelamento cooperativo em operações assíncronas.

### O objetivo
- Ensinar como implementar cancelamento cooperativo em métodos assíncronos, incluindo timeout automático, cancelamento externo e combinação de tokens.

#### O que é `CancellationToken`?
- `CancellationToken` é uma estrutura leve que representa um sinal de cancelamento. Ele permite que operações assíncronas sejam canceladas de forma cooperativa, verificando se o cancelamento foi solicitado (via propriedade `IsCancellationRequested`) e lançando `OperationCanceledException` se necessário.

#### O que é `CancellationTokenSource`?
- `CancellationTokenSource` é a classe que cria e gerencia o `CancellationToken`. Permite disparar cancelamento manualmente (método `Cancel`) ou após um tempo (método `CancelAfter`), útil para implementar timeouts em operações assíncronas.

### Como executar

```bash
dotnet run --project src/04-CancellationAndTimeout
```

### O que este projeto resolve
- Ajuda a entender como cancelar operações assíncronas de forma controlada, evitando recursos desperdiçados, e como combinar sinais de cancelamento (ex.: timeout + cancelamento externo).

### O que entrega
- Um exemplo executável que demonstra:
  - Timeout automático com `CancellationTokenSource(TimeSpan)` — cancela após 1 segundo.
  - Cancelamento externo via `cts.Cancel()` — simula interrupção manual.
  - Tokens encadeados com `CreateLinkedTokenSource` — combina timeout e cancelamento externo, cancelando pelo primeiro sinal.
  - Verificação de cancelamento com `ct.ThrowIfCancellationRequested()` e `Task.Delay(..., ct)`.

### Recursos .NET/C# usados (explicação)
- **CancellationTokenSource**
  - Classe para criar `CancellationToken`. Construtores permitem timeout (ex.: `new CancellationTokenSource(TimeSpan)`), e métodos como `Cancel()` ou `CancelAfter()` disparam cancelamento.

- **CancellationToken**
  - Estrutura passada para métodos assíncronos. Propriedade `IsCancellationRequested` para checagem manual; `ThrowIfCancellationRequested()` lança `OperationCanceledException`.

- **OperationCanceledException**
  - Exceção lançada quando cancelamento é detectado. Indica cancelamento cooperativo, permitindo ao runtime liberar recursos.

- **CancellationTokenSource.CreateLinkedTokenSource**
  - Cria um token que combina múltiplos tokens fonte. Cancela quando qualquer um dos tokens fonte for cancelado.

- **Task.Delay (com CancellationToken)**
  - Versão sobrecarregada que aceita `CancellationToken`. Se cancelado, lança `OperationCanceledException` em vez de completar.

### Notas e boas práticas mencionadas no código
- **Cancelamento cooperativo**: Sempre verifique o token em loops ou operações longas; não force terminação abrupta.
- **Propagação de token**: Passe `CancellationToken` para todas as operações internas para respeitar cancelamento em cascata.
- **OperationCanceledException**: Lançada para sinalizar cancelamento; permite tratamento consistente pelo runtime.
- **Linked tokens**: Úteis para combinar sinais (ex.: timeout + botão cancelar); o primeiro a cancelar vence.

### Explicação rápida do fluxo do programa
- **Timeout**: `using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1)))` — cancela automaticamente após 1s.
- **Cancelamento externo**: `cts.Cancel()` chamado de outra task — interrompe a operação em andamento.
- **Linked tokens**: `CreateLinkedTokenSource(timeout.Token, external.Token)` — cancela pelo primeiro sinal (timeout ou externo).
- **Verificação**: `await Task.Delay(300, ct)` lança se cancelado; `ct.ThrowIfCancellationRequested()` checa manualmente.

### Exemplo de uso prático
- Em um download de arquivo, use timeout para evitar travamentos e cancelamento externo (botão "cancelar") para interrupção manual, combinados com linked tokens.

### Desafios para os alunos
1. **Implemente cancelamento em cascata**: Modifique `OperacaoCancelavelAsync` para chamar outro método assíncrono, passando o token. Demonstre como o cancelamento se propaga.

2. **Adicione progresso com cancelamento**: Use um loop para reportar progresso (ex.: "Passo X de Y") e permita cancelamento a qualquer momento.

3. **Combine com Task.WhenAny para timeout**: Use `Task.WhenAny` com `Task.Delay(timeout)` para implementar timeout manual, sem `CancellationTokenSource(TimeSpan)`.

4. **Trate OperationCanceledException**: Capture a exceção e demonstre diferença entre cancelamento por timeout vs. externo (use linked tokens).

5. **Simule operação longa**: Aumente o loop para 50 passos e teste cancelamento rápido; observe como `ThrowIfCancellationRequested()` responde imediatamente.

### Comentários finais
- Este README foi atualizado para explicar por que as construções usadas aparecem no código e quando aplicá-las em projetos reais. Se quiser, eu aplico o mesmo padrão aos outros READMEs do repositório.

### Onde alterar o código (rápido)

- O código principal está em `src/04-CancellationAndTimeout/Program.cs`. Os exemplos foram extraídos em métodos para facilitar experimentação:
  - `RunTimeoutExampleAsync()` — demonstra `CancellationTokenSource(TimeSpan)` para timeout automático.
  - `RunExternalCancellationExampleAsync()` — demonstra cancelamento explícito via `cts.Cancel()` disparado externamente.
  - `RunLinkedTokensExampleAsync()` — demonstra `CancellationTokenSource.CreateLinkedTokenSource` para combinar timeout + cancelamento externo.

- Helpers:
  - `OperacaoCancelavelAsync(CancellationToken ct)` — loop que usa `await Task.Delay(..., ct)` e `ct.ThrowIfCancellationRequested()` para respeitar cancelamento.

- Para tornar a demonstração mais visível em sala, ajuste os delays e os tempos de cancelamento (por exemplo, reduzindo 700 ms para 300 ms no cancelamento externo, ou aumentando os delays na operação).

- Execução rápida:

```bash
dotnet run --project src/04-CancellationAndTimeout
```
