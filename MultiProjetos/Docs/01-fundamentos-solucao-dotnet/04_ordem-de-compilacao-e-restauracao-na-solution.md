# Ordem de Compilacao e Restauracao na Solution (.NET)

## Objetivo

Explicar como o .NET decide a ordem de compilacao entre projetos de uma solution e como funciona a restauracao de dependencias antes do build.

## Visao Geral

Quando executamos build na solution, o .NET nao compila projetos aleatoriamente.

Ele usa o grafo de dependencias definido pelos `ProjectReference` para:

1. Restaurar o que for necessario.
2. Compilar primeiro os projetos base.
3. Compilar depois os projetos que dependem dos anteriores.

No Sistema Academico, isso significa:

1. `Domain` antes de `Application`.
2. `Application` antes de `ConsoleApp` e `BlazorApp`.

## Grafo de Dependencias (Projeto Atual)

```text
CadastroAcademico.Domain
        -> CadastroAcademico.Application
                -> CadastroAcademico.ConsoleApp
                -> CadastroAcademico.BlazorApp
```

## Sequencia Pratica no Build da Solution

Comando:

```bash
dotnet build CadastroAcademico.slnx
```

Fluxo conceitual:

1. Resolve a solution e lista os projetos.
2. Monta o grafo de dependencias via `ProjectReference`.
3. Executa restore (quando necessario).
4. Compila projetos em ordem topologica (base -> dependentes).
5. Gera outputs (`bin/`, `obj/`) de cada projeto.

## Restore: o Que e e Quando Acontece

`dotnet restore` recupera dependencias (principalmente pacotes NuGet) exigidas pelos projetos.

Mesmo em projetos sem pacote externo, o restore faz parte do fluxo padrao do SDK.

Comandos comuns:

```bash
dotnet restore CadastroAcademico.slnx
dotnet build CadastroAcademico.slnx
```

Observacao importante:

- `dotnet build` normalmente aciona restore automaticamente.
- Em CI/CD, e comum separar restore e build para tornar o pipeline mais previsivel.

## Build Incremental (Impacto no Dia a Dia)

O build do .NET e incremental. Isso significa:

1. Se nada mudou, o proximo build tende a ser mais rapido.
2. Se voce altera `Domain`, projetos dependentes podem ser recompilados.
3. Se altera apenas `ConsoleApp`, em geral nao recompila todo o restante.

Isso acelera ciclos de desenvolvimento e reforca o valor da separacao em projetos.

## Comandos Uteis para Diagnostico

## 1) Build da solution inteira

```bash
dotnet build CadastroAcademico.slnx
```

## 2) Build de um projeto especifico

```bash
dotnet build src/CadastroAcademico.Application/CadastroAcademico.Application.csproj
```

## 3) Restore explicito da solution

```bash
dotnet restore CadastroAcademico.slnx
```

## 4) Limpar artefatos de compilacao

```bash
dotnet clean CadastroAcademico.slnx
```

## Erros Comuns

1. Dependencia circular entre projetos
- Sintoma: falha de resolucao/compilacao no grafo.
- Correcao: manter fluxo de dependencia unidirecional.

2. Achar que adicionar na solution cria referencia automaticamente
- Sintoma: projeto aparece na solution, mas tipos nao sao encontrados.
- Correcao: adicionar `ProjectReference` explicito com `dotnet add reference`.

3. Ignorar restore em ambiente novo
- Sintoma: build falha por falta de pacotes/ativos restaurados.
- Correcao: executar `dotnet restore` antes do build, especialmente em CI ou maquina nova.

## Aplicacao no Sistema Academico

No contexto deste projeto, a ordem correta de compilacao garante que:

1. Entidades e regras em `Domain` existam antes dos servicos.
2. Servicos da `Application` estejam prontos antes das interfaces.
3. Console e Blazor compartilhem o mesmo nucleo com consistencia.

Isso reduz bugs, evita duplicacao e prepara o caminho para proximas etapas (casos de uso, persistencia e testes).

## Checkpoint de Aprendizagem

Depois desta leitura, o aluno deve conseguir responder:

1. Como o .NET determina a ordem de compilacao entre projetos?
2. Qual o papel de `restore` no ciclo de build?
3. Qual a diferenca entre build incremental e build completo?
4. Por que o grafo de dependencias precisa estar bem definido?
