# 11-ASPNetCore-Async

Este projeto demonstra uma pequena Web API ASP.NET Core com cancelamento cooperativo: aceita `CancellationToken` em controller, repositório que respeita cancelamento, e middleware que mapeia cancelamentos para HTTP 499.

### O objetivo
- Ensinar como integrar async/await em APIs web, propagando cancelamento do cliente para operações I/O.

#### O que é cancelamento em ASP.NET Core?
- Controllers podem aceitar `CancellationToken` injetado automaticamente; sinalizado quando cliente fecha conexão ou servidor aplica timeout.

### Como executar

```bash
dotnet run --project 11-ASPNetCore-Async
```

### O que este projeto resolve
- Mostra como evitar trabalho desnecessário em requisições canceladas, melhorando escalabilidade e responsividade.

### O que entrega
- API executável com:
  - Endpoint `GET /dados` que aceita token e simula I/O de 2s.
  - Middleware que converte `OperationCanceledException` em 499 (Client Closed Request).
  - Repositório mock assíncrono que respeita cancelamento.
  - Swagger para testar.

### Recursos .NET/C# usados (explicação)
- **CancellationToken em controller**
  - Injetado pelo framework; sinaliza quando requisição é cancelada.

- **Middleware**
  - Intercepta exceções; mapeia cancelamento para status 499 e loga.

- **OperationCanceledException**
  - Lançada por `Task.Delay(ct)` quando cancelado; tratada como cancelamento legítimo.

- **Swagger**
  - Gera documentação interativa para testar endpoints.

- **Task.Delay com CancellationToken**
  - Cancela delay se token for sinalizado.

### Notas e boas práticas mencionadas no código
- **Propague tokens**: Passe para todas as operações I/O.
- **Middleware para cancelamento**: Traduza exceções em códigos HTTP apropriados.
- **Evite retry em controller**: Deixe para camadas inferiores.
- **Log cancelamentos**: Para observabilidade.

### Explicação rápida do fluxo do programa
- Requisição chega; controller recebe token.
- Chama repositório com token; simula I/O de 2s.
- Se cliente cancela, token sinaliza; `Task.Delay` lança `OperationCanceledException`.
- Middleware captura e retorna 499.

### Exemplo de uso prático
- Em APIs reais: use para cancelar queries de banco longas quando cliente desiste.

### Desafios para os alunos
1. **Adicione timeout**: Use `CancellationTokenSource` no controller para timeout customizado.

2. **Teste cancelamento**: Use curl com timeout para simular cliente fechando conexão.

3. **Adicione mais endpoints**: Implemente POST com validações e cancelamento.

4. **Log avançado**: Use Serilog para estruturar logs de cancelamento.

5. **Métricas**: Adicione Prometheus para contar cancelamentos.

### Comentários finais
- Este README explica integração web. Próximos projetos serão atualizados.
