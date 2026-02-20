# AsyncPlayground — exemplos de programação assíncrona em C#

Esta solução contém exemplos didáticos de programação assíncrona em C# (async/await, Task, cancelamento, composição de tarefas, streams assíncronos), organizados do básico ao avançado.

Estrutura
- `preparacao/aula_async_await/AsyncPlayground.sln` — solução vazia para agrupar exemplos.
- `preparacao/aula_async_await/src/` — código-fonte dos exemplos (ainda não há projetos).
- `preparacao/aula_async_await/tests/` — projetos de teste para os exemplos (ainda não há testes).

Conteúdos (sugestão de labs numerados)
1. Básico: async/await e Task simples.
2. Cancelamento: uso de CancellationToken e TokenSource.
3. Composição: WhenAll, WhenAny, encadeamento de Tasks.
4. Padrões avançados: ConfigureAwait, sincronização e deadlocks.
5. Streams assíncronos: IAsyncEnumerable, await foreach.

Princípios e boas práticas
- Evitar async void (use apenas para handlers de eventos).
- Nomear métodos assíncronos com o sufixo `Async`.
- Não bloquear com `.Result` ou `.Wait()` — prefira `await`.
- Propagar `CancellationToken` nos métodos e APIs.

Fonte teórica
“Métodos Assíncronos em C#” (fundamentos + labs).

Objetivo
Fornecer um playground organizado para aprender e testar padrões assíncronos em C#, com exemplos crescentes em complexidade.