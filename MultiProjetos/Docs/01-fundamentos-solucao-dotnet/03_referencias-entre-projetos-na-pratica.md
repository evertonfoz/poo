# Referencias Entre Projetos na Pratica (.NET CLI)

## Objetivo

Mostrar como as referencias entre projetos funcionam na pratica em uma solution .NET, quais comandos usar para configurar dependencias e como isso impacta o build.

## Conceito Principal

Em uma solution com varios projetos, uma referencia entre projetos define quem pode usar o codigo de quem.

Exemplo:

- Se `ConsoleApp` precisa chamar servicos da camada `Application`, entao `ConsoleApp` deve referenciar `Application`.

Sem essa referencia, o compilador nao encontra os tipos usados e o build falha.

## Como Isso Aparece no Projeto

No Sistema Academico, o fluxo de dependencia adotado foi:

```text
ConsoleApp  ->
             Application -> Domain
BlazorApp   ->
```

Ou seja:

1. `CadastroAcademico.Application` referencia `CadastroAcademico.Domain`.
2. `CadastroAcademico.ConsoleApp` referencia `CadastroAcademico.Application`.
3. `CadastroAcademico.BlazorApp` referencia `CadastroAcademico.Application`.

## Comandos Usados na Pratica

## 1) Application -> Domain

```bash
dotnet add src/CadastroAcademico.Application/CadastroAcademico.Application.csproj reference src/CadastroAcademico.Domain/CadastroAcademico.Domain.csproj
```

## 2) ConsoleApp -> Application

```bash
dotnet add src/CadastroAcademico.ConsoleApp/CadastroAcademico.ConsoleApp.csproj reference src/CadastroAcademico.Application/CadastroAcademico.Application.csproj
```

## 3) BlazorApp -> Application

```bash
dotnet add src/CadastroAcademico.BlazorApp/CadastroAcademico.BlazorApp.csproj reference src/CadastroAcademico.Application/CadastroAcademico.Application.csproj
```

## O Que Muda no .csproj

Quando usamos `dotnet add ... reference ...`, o .NET CLI adiciona um bloco como este no projeto que depende do outro:

```xml
<ItemGroup>
  <ProjectReference Include="..\CadastroAcademico.Application\CadastroAcademico.Application.csproj" />
</ItemGroup>
```

Esse `ProjectReference` e o elo formal de compilacao entre os projetos.

## Impacto no Build

Depois que as referencias estao corretas:

1. O compilador sabe a ordem de compilacao dos projetos.
2. Tipos de projetos referenciados passam a ser reconhecidos.
3. `dotnet build` na solution compila o conjunto de forma coerente.

Comando de validacao usado no projeto:

```bash
dotnet build CadastroAcademico.slnx
```

## Impacto no Aprendizado

Esse passo ensina ao aluno que:

1. Arquitetura em camadas depende de relacoes explicitas.
2. Nao basta criar varios projetos: e preciso conecta-los corretamente.
3. O build da solution confirma se a topologia de dependencias esta correta.

## Erros Comuns

1. Referenciar no sentido errado
- Exemplo ruim: `Domain` referenciando `ConsoleApp`.
- Correcao: camadas de nucleo nao devem depender de interface.

2. Tentar usar classes sem `ProjectReference`
- Sintoma: erro de tipo/namespace nao encontrado.
- Correcao: adicionar referencia adequada com `dotnet add reference`.

3. Criar dependencia circular
- Exemplo ruim: `Application` referencia `Domain`, e `Domain` referencia `Application`.
- Correcao: manter fluxo unidirecional de dependencias.

## Checkpoint de Aprendizagem

Depois desta leitura, o aluno deve conseguir responder:

1. Para que serve `ProjectReference` em um `.csproj`?
2. Qual e a diferenca entre adicionar projeto na solution e adicionar referencia entre projetos?
3. Qual o fluxo de dependencias correto no Sistema Academico?
4. Como validar se as referencias estao corretas?
