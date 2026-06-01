# Revisão Técnica — 2026-05-23

## Contexto

Revisão realizada com Claude Code (claude-sonnet-4-6) sobre todo o código-fonte da solution.
Build executado antes da análise: `dotnet build CadastroAcademico.slnx` — **0 erros, 0 avisos**.

---

## Resultado geral

O projeto está **atualizado e correto** para .NET 10. Sem pacotes externos desatualizados, sem avisos de compilação e com arquitetura bem separada em camadas. Os pontos abaixo são observações de qualidade de código — nenhum deles impede o funcionamento do sistema.

---

## O que está correto

### Framework e SDK
- Todos os 4 projetos alvos `net10.0`, alinhado com o SDK instalado (10.0.103).
- Nenhum `<PackageReference>` externo — sem risco de versão desatualizada.

### Encapsulamento e Domínio
- `Aluno`, `Curso` e `Matricula` usam `private set` em todas as propriedades.
- Validações de invariantes ficam nos construtores e métodos privados das entidades.
- Coleções internas expostas como `IReadOnlyCollection<T>` / `IReadOnlyList<T>`.
- `Matricula` tem construtora `internal`, impedindo criação fora do Domain.
- `PerfilAcademico` modelado como Value Object (sem `Id`).

### Padrões de resultado
- `OperationResult` e `OperationResult<T>` usados consistentemente em todos os services.
- Exceções do Domain capturadas nos services e convertidas em `Failure(...)` — a interface nunca recebe exceção não tratada.

### Blazor (net10.0)
- `MapStaticAssets()` em vez de `UseStaticFiles()` — API correta para .NET 9+.
- `ResourcePreloader` e `ImportMap` no `App.razor` — estrutura atual do template Blazor Server.
- `UseStatusCodePagesWithReExecute("/not-found")` — tratamento de 404 correto.
- `AddInteractiveServerComponents()` — modo interativo configurado adequadamente.
- `InMemoryAcademicStore` ajustado para lifetime `scoped` na BlazorApp, evitando compartilhamento indevido de estado mutável entre circuitos.
- Criação de cursos e alunos agora valida unicidade antes de inserir dados no store em memória.

---

## Pontos de atenção

### 1. IDs gerados por contadores estáticos

**Onde:** [`Aluno.cs`](../../src/CadastroAcademico.Domain/Entities/Aluno.cs), [`Curso.cs`](../../src/CadastroAcademico.Domain/Entities/Curso.cs), [`Matricula.cs`](../../src/CadastroAcademico.Domain/Entities/Matricula.cs)

```csharp
private static int _ultimoId;
public int Id { get; }

public Aluno(...)
{
    Id = ++_ultimoId;
    ...
}
```

**O que acontece:** O contador é compartilhado por todas as instâncias da classe na mesma execução do processo. Se a aplicação for reiniciada, os IDs recomeçam do 1. Em testes unitários que criam múltiplos objetos, os IDs acumulam entre testes (não resetam).

**Impacto:** Nenhum no funcionamento atual. Em uma aplicação com banco de dados, o ID viria de uma sequência do banco e esse padrão seria substituído.

**Por que foi feito assim:** Para fins didáticos é a forma mais simples de garantir IDs únicos sem dependência de banco. É uma decisão consciente neste contexto.

---

### 2. `PerfilAcademico` não é imutável

**Onde:** [`PerfilAcademico.cs`](../../src/CadastroAcademico.Domain/ValueObjects/PerfilAcademico.cs)

```csharp
public class PerfilAcademico
{
    public string RegistroAcademico { get; private set; }
    public string Periodo { get; private set; }

    public void AtualizarPeriodo(string periodo) { ... }
}
```

**O que acontece:** Value Objects puros (no sentido de DDD) são imutáveis — uma mudança de valor cria um novo objeto, em vez de alterar o existente. O `PerfilAcademico` atual tem um método mutável `AtualizarPeriodo`.

**Como ficaria imutável:**
```csharp
public class PerfilAcademico
{
    public string RegistroAcademico { get; }
    public string Periodo { get; }

    public PerfilAcademico(string registroAcademico, string periodo) { ... }

    // Em vez de AtualizarPeriodo, criaria um novo objeto:
    public PerfilAcademico ComPeriodo(string periodo) =>
        new PerfilAcademico(RegistroAcademico, periodo);
}
```

**Impacto:** Nenhum no funcionamento atual. O `StudentService` já cria um novo `PerfilAcademico` na atualização (`new PerfilAcademico(...)`), então o comportamento externo é equivalente.

**Por que foi feito assim:** Para simplificar a explicação didática. Tornar o Value Object imutável seria o próximo passo natural em uma evolução do projeto.

---

### 3. Exclusão em memória mantida simples por decisão didática

**Onde:** `CourseService`, `StudentService`, `EnrollmentService` e `InMemoryAcademicStore`

**O que acontece:** as exclusões continuam focadas no store em memória e não remodelam a consistência histórica completa entre todas as coleções internas das entidades.

**Impacto:** Baixo para o objetivo atual do projeto. A solução segue funcional para o fluxo de ensino, mas essa simplificação não deve ser confundida com um desenho final de domínio para produção.

**Por que foi mantido assim:** escolha consciente para preservar simplicidade didática nesta etapa, com o tradeoff agora registrado formalmente.

---

### 4. Idioma do `lang` no `App.razor`

**Onde:** [`App.razor`](../../src/CadastroAcademico.BlazorApp/Components/App.razor), linha 2

```html
<html lang="en">
```

**O que acontece:** O atributo `lang` informa ao navegador e às ferramentas de acessibilidade o idioma da página. Como o projeto é em português, o correto seria `lang="pt-BR"`.

**Impacto:** Pequeno — afeta leitores de tela, corretor ortográfico do navegador e ferramentas de SEO. Não causa erro funcional.

**Correção sugerida:**
```html
<html lang="pt-BR">
```

---

## Resumo executivo

| Item | Status | Severidade |
|---|---|---|
| Build da solution | 0 erros, 0 avisos | — |
| Framework alvo | net10.0 em todos os projetos | — |
| Encapsulamento das entidades | Correto | — |
| Padrão OperationResult | Aplicado consistentemente | — |
| APIs Blazor (net10) | Atualizadas | — |
| IDs por contador estático | Aceitável para fins didáticos | Baixa |
| PerfilAcademico mutável | Diverge do Value Object puro | Baixa |
| Exclusão simples em memória | Escolha didática explícita | Baixa |
| `lang="en"` no App.razor | Deveria ser `pt-BR` | Baixa |
