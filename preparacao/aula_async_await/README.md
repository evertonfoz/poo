# AsyncPlayground

Esta pasta contém a solução `AsyncPlayground` para a trilha de estudos sobre programação assíncrona em .NET.

Objetivo da trilha

- Introduzir e aprofundar conceitos e práticas de programação assíncrona em .NET, do básico ao avançado.

Tópicos / sequência (projetos numerados 01, 02, ...)

01. Conceitos básicos: Task, async/await, modelo de execução e sincronização básica.
02. Padrões de composição de Tasks: WhenAll, WhenAny, ContinueWith e encadeamento.
03. Cancelamento: CancellationToken, propagação e melhores práticas para cancelar operações.
04. Tratamento de exceções em código assíncrono: AggregateException, exceções observadas e padrão de propagação.
05. Performance e otimizações: use de ConfigureAwait, pooling de Tasks e quando usar `ValueTask`.
06. I/O assíncrono e IAsyncEnumerable: leitura/streaming assíncrono, `await foreach` e produção/consumo.
07. Paralelismo vs assíncrono: quando usar `Task.Run`, `Parallel` e abordagens reativas.
08. Boas práticas e anti-patterns: evitar bloqueios, captura de contextos, leaks de CancellationToken, etc.
09+. Exemplos avançados e integração: bibliotecas, patterns de retry backoff, pipelines e testes assíncronos.

Como os projetos serão organizados

- Haverá dois diretórios principais:
  - `src/` — projetos de exemplo/implementação, numerados `Project01`, `Project02`, ...
  - `tests/` — projetos de testes para cada exemplo, numerados de forma correspondente.

Próximos passos

- Criar projetos `.csproj` em `src/Project01`, `src/Project02`, ... com exemplos incrementais.
- Implementar testes em `tests/` para validar os comportamentos e demonstrar casos de uso.

Boas práticas (resumo rápido)

- Prefira `async`/`await` para composição clara de operações assíncronas.
- Use `CancellationToken` para permitir cancelamento cooperativo.
- Use `ConfigureAwait(false)` em bibliotecas onde não é necessária a captura de contexto.
- Avalie `ValueTask` apenas quando houver necessidade de reduzir alocações em hot-paths e sabendo as restrições.
- Prefira `IAsyncEnumerable<T>` para streaming assíncrono de dados.

Se quiser que eu já crie os primeiros projetos (01 Básico com exemplos, e testes) eu posso fazê-lo agora.