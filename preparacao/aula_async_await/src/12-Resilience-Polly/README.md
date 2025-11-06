## 12-Resilience-Polly

Este projeto demonstra padrões de resiliência para operações assíncronas usando a biblioteca Polly e compara com uma implementação manual. O foco é apresentar políticas como Retry, Circuit Breaker, Timeout e Bulkhead, além de como combiná-las com PolicyWrap.

### O objetivo
- Ensinar a projetar camadas resilientes que lidam com falhas transitórias (timeouts, exceções), controlar tentativas (retries) e evitar sobrecarga do sistema (bulkhead, circuit-breaker).

### Como executar

```bash
dotnet run --project 12-Resilience-Polly
```

> Observação: o `.csproj` do projeto está em `12-Resilience-Polly/12-Resilience-Polly.csproj` — se não aparecer no Solution Explorer, adicione-o à solution.

### O que este projeto resolve
- Demonstra como tornar chamadas I/O mais robustas contra falhas transitórias, reduzindo retries descontrolados, evitando falhas em cascata e melhorando a disponibilidade de serviços.

### O que entrega
- Execução comparativa entre abordagens:
  - Polly: políticas declarativas (Retry com backoff exponencial + jitter, Timeout, Circuit Breaker, Bulkhead, PolicyWrap).
  - Manual: loops de retry usando `CancellationTokenSource`, backoff explícito e tratamento ad-hoc.
  - Operação instável simulada que evidencia comportamento de cada política.

### Principais políticas cobertas (explicação)
- **Retry**: reexecuta uma operação que falhou, com estratégia de backoff (exponencial + jitter). Importante exigir idempotência antes de aplicar.
- **Circuit Breaker**: corta tentativas para um recurso que está consistentemente falhando, permitindo recuperação e evitando sobrecarregar o recurso.
- **Timeout**: define um limite máximo para uma operação; pode ser aplicado como policy (Polly) ou via `CancellationTokenSource(TimeSpan)` manualmente.
- **Bulkhead Isolation**: limita concorrência (número de execuções simultâneas) para isolar falhas e preservar capacidade para outras partes do sistema.
- **PolicyWrap**: compõe múltiplas políticas (ex.: Timeout dentro de Retry dentro de CircuitBreaker) para aplicar comportamentos combinados de forma previsível.

### Boas práticas mencionadas no código
- Centralize políticas em pontos de configuração (por cliente HttpClientFactory) para evitar duplicação.
- Garanta idempotência e efeitos colaterais controlados antes de aplicar Retry.
- Use backoff exponencial com jitter para reduzir contenção.
- Monitore métricas (retries, falhas, circuit-breaker openings) e alerte quando limites são atingidos.

### Fluxo rápido do programa
- O demo executa a operação instável com diferentes estratégias e imprime métricas: número de tentativas, sucessos, falhas e tempo gasto para cada abordagem.

### Exemplos de uso prático
- Em chamadas HTTP a serviços externos: combine Timeout + Retry + CircuitBreaker para tolerar falhas transitórias sem sobrecarregar o serviço.
- Em filas/processamento em lote: use Bulkhead para limitar concorrência de operações caras.

### Desafios para os alunos
1. Adicione e configure `CircuitBreaker` (Polly) para proteger contra falhas contínuas e demonstre como ele abre/fecha.
2. Implemente Bulkhead e simule alta concorrência para ver a proteção da fila de trabalho.
3. Compare comportamento com/sem jitter em backoff exponencial em cenários com muitos clientes concorrentes.
4. Integre `HttpClientFactory` com Polly e configure políticas por cliente.
5. Meça e exponha métricas (contador de retries, tempo médio) e gere um pequeno relatório.

### Observações finais
- Este README foi padronizado para facilitar o ensino dos conceitos de resiliência. Posso agora adicionar o projeto à solution e executar build + testes.
 
### Onde alterar o código

- `Program.cs` contém as demos organizadas em métodos:
  - `RunPollyPolicyExampleAsync()` — configura `Timeout` + `Retry` (no exemplo: 1s timeout, 3 retries com backoff). Edite aqui para adicionar `CircuitBreaker`, `Bulkhead` ou `Fallback` e para ajustar o backoff/jitter.
  - `RunManualRetryExampleAsync()` — mostra abordagem manual com `CancellationTokenSource(TimeSpan)` e retry manual; ajuste número de tentativas, timeout e delays de backoff aqui.
  - `UnreliableOperationAsync(CancellationToken)` — simula operação instável; ajuste probabilidades e tempos (`1500ms`, `100ms`, `200ms`) para criar cenários mais curtos ou mais agressivos.

Altere os tempos/tentativas para tornar os demos adequados à sua aula (ex.: reduzir timeout de 1s para 500ms, reduzir delays de backoff). Posso, se preferir, adicionar um exemplo de `CircuitBreaker` e um `PolicyWrap` com `Bulkhead` para demonstrar proteção sob alta concorrência.
## 12-Resilience-Polly

Este projeto demonstra padrões de resiliência aplicados a operações assíncronas, comparando políticas da biblioteca Polly com uma implementação manual (retry + timeout). Ideal para aprender quando usar políticas reutilizáveis e como evitar efeitos colaterais em retries.

### O objetivo
- Ensinar como configurar e usar políticas de resiliência (Retry, Timeout, Circuit Breaker, Fallback) em operações I/O assíncronas, e comparar com soluções manuais.

### Como executar

```bash
dotnet run --project src/12-Resilience-Polly
```

> Observação: o projeto existe no diretório, mas originalmente não estava incluído na solução (`AsyncPlayground.sln`). Posso adicioná-lo à `.sln` para aparecer no Solution Explorer (faco isso ao rodar os comandos abaixo).

### O que este projeto resolve
- Demonstra como tornar chamadas I/O mais robustas contra falhas transitórias (timeouts, exceções transitórias), evitando falhas em cascata e melhorando disponibilidade.

### O que entrega
- Executável de comparação com:
  - Polly: políticas declarativas (ex.: Timeout 1s + Retry 3x com backoff exponencial e jitter).
  - Manual: loops de retry com `CancellationTokenSource` e backoff explícito.
  - Operação instável que simula falhas e tempos de resposta para evidenciar comportamento das políticas.

### Recursos .NET/C# usados (explicação)
- **Polly** — Biblioteca para políticas de resiliência: Retry, Timeout, Circuit Breaker, Fallback e PolicyWrap.
- **Policy.WrapAsync** — Combina políticas (ex.: retry + timeout + circuit-breaker).
- **CancellationTokenSource(TimeSpan)** — Timeout automático usado na abordagem manual.
- **Random.Shared** — Simula comportamento instável (falhas/tempos de resposta).

### Boas práticas mencionadas
- Prefira políticas centralizadas (Polly) para evitar duplicação de lógica.
- Garanta idempotência dos endpoints/operações antes de aplicar retry.
- Use backoff exponencial com jitter para reduzir contenda entre clientes.
- Monitore e exponha métricas (tentativas, falhas, tempos) para observabilidade.

### Fluxo rápido do programa
- Executa a operação instável com a abordagem Polly e em seguida com a abordagem manual, mostrando número de tentativas, sucessos e falhas.

### Exemplo de uso prático
- Em chamadas HTTP a serviços externos: combine Timeout + Retry + CircuitBreaker para lidar com falhas transitórias de forma segura.

### Desafios para os alunos
1. Adicione `CircuitBreaker` (Polly) e demonstre proteção contra falhas contínuas.
2. Introduza jitter no backoff e compare o número de conflitos em alta concorrência.
3. Implemente `Fallback` para retornar um valor alternativo quando as tentativas falharem.
4. Integre `HttpClientFactory` e Polly para políticas por cliente.
5. Meça latência e número de tentativas com/sem Polly usando logs ou BenchmarkDotNet.

### Comentários finais
- Atualizei este README para o formato padronizado. Vou adicionar o projeto à solução e rodar build/test a seguir.
