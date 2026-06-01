# Decisões Técnicas

## 2026-05-23 — Formato da solution

Decisão: usar `CadastroAcademico.slnx`.

Justificativa:
1. Em .NET 10, `dotnet new sln` passa a gerar `slnx` por padrão.
2. Formato simplificado e suportado pelo tooling atual.
3. Mantém alinhamento com comportamento padrão do SDK instalado no ambiente.

Referência oficial consultada:
- Microsoft Learn: `dotnet new sln` defaults to SLNX file format.

## 2026-05-23 — Segredo da Context7

Decisão: usar variável de ambiente no `.vscode/mcp.json`.

Implementação:
1. `CONTEXT7_API_KEY` configurada como `${env:CONTEXT7_API_KEY}`.
2. Chave registrada localmente em `.env`.
3. `.env` adicionado ao `.gitignore` para não versionar segredo.

Justificativa:
1. Evitar chave em texto puro no repositório.
2. Permitir execução local segura e simples.

## 2026-05-23 — Lifetime do store em memória no Blazor

Decisão: usar `AddScoped<InMemoryAcademicStore>()` na BlazorApp.

Implementação:
1. `InMemoryAcademicStore` registrado como `scoped` no `Program.cs` da BlazorApp.
2. `CourseService`, `StudentService` e `EnrollmentService` mantidos como `scoped`.
3. ConsoleApp continua instanciando o store manualmente no `Program.cs`.

Justificativa:
1. Em Blazor Interactive Server, `scoped` acompanha o circuito do usuário.
2. Evita compartilhar estado mutável entre usuários da interface web.
3. Mantém o comportamento didático de persistência em memória sem introduzir banco de dados.

## 2026-05-23 — Validação de unicidade na criação

Decisão: validar conflitos de unicidade também nas operações de criação.

Implementação:
1. `CourseService.CreateCourse(...)` passa a bloquear sigla duplicada.
2. `StudentService.CreateStudent(...)` passa a bloquear e-mail duplicado.
3. `StudentService.CreateStudent(...)` passa a bloquear registro acadêmico duplicado.

Justificativa:
1. A mesma regra já existia na atualização e precisava valer desde a entrada do dado.
2. Evita que o estado em memória aceite dados contraditórios logo no cadastro.
3. Deixa os exemplos de Console e Blazor coerentes com as invariantes de uso esperadas.

## 2026-05-23 — Exclusão simples mantida por decisão didática

Decisão: manter a exclusão atual em memória sem sincronizar remoção histórica nas coleções internas de todas as entidades.

Implementação:
1. `DeleteCourse`, `DeleteStudent` e `DeleteEnrollment` permanecem simples nesta etapa.
2. A remoção continua focada no `InMemoryAcademicStore`.
3. O tradeoff fica registrado explicitamente na documentação.

Justificativa:
1. O objetivo desta fase é ensinar fluxo entre Domain, Application, Console e Blazor com baixa complexidade.
2. A modelagem de remoção histórica consistente pode ser tratada em uma etapa futura de refino de domínio.
3. Para o contexto didático atual, a simplificação é aceitável desde que esteja documentada como escolha consciente.
