# Projeto Isolado vs Solution Completa no .NET

## Objetivo

Explicar, de forma pratica, a diferenca entre compilar um projeto isolado e compilar a solution inteira, incluindo cenarios reais de erro e como diagnosticar cada situacao.

## Visao Rapida

No .NET, os dois comandos abaixo sao validos, mas respondem perguntas diferentes:

```bash
dotnet build src/CadastroAcademico.Application/CadastroAcademico.Application.csproj
dotnet build CadastroAcademico.slnx
```

Resumo:

1. Build de projeto isolado valida um recorte especifico.
2. Build da solution valida a integracao de todos os projetos.

## Quando Usar Cada Um

## 1) Build de Projeto Isolado

Use quando voce quer:

1. Validar rapidamente uma camada em desenvolvimento.
2. Testar um ajuste localizado.
3. Reduzir tempo de feedback durante implementacao.

Exemplo:

```bash
dotnet build src/CadastroAcademico.Domain/CadastroAcademico.Domain.csproj
```

Interpretacao:

- Bom para verificar se entidades e regras de dominio estao consistentes.
- Nao garante que ConsoleApp e BlazorApp continuam integrando corretamente.

## 2) Build da Solution Completa

Use quando voce quer:

1. Garantir consistencia arquitetural do sistema inteiro.
2. Validar impacto transversal de mudancas.
3. Confirmar prontidao para entrega/commit/pipeline.

Exemplo:

```bash
dotnet build CadastroAcademico.slnx
```

Interpretacao:

- Valida o grafo completo de dependencias.
- Detecta quebras de integracao entre projetos.

## Tabela Comparativa

| Aspecto | Projeto Isolado | Solution Completa |
|---|---|---|
| Escopo | Local | Global |
| Velocidade | Mais rapida | Mais completa (pode ser mais lenta) |
| Detecta erro interno da camada | Sim | Sim |
| Detecta erro entre camadas | Parcial | Sim |
| Ideal para | Ciclo curto de desenvolvimento | Validacao final e integracao |

## Cenarios Praticos de Erro

## Cenario 1: Dominio compila, mas UI quebra

Situacao:

1. `Domain` builda sem erro.
2. `Application` mudou assinatura de metodo.
3. `ConsoleApp` ainda chama assinatura antiga.

Resultado:

- Build isolado de `Domain` passa.
- Build da solution falha.

Diagnostico recomendado:

```bash
dotnet build CadastroAcademico.slnx
```

## Cenario 2: Projeto adicionado, mas nao referenciado

Situacao:

1. Projeto esta na solution.
2. Faltou `ProjectReference` no projeto consumidor.

Resultado:

- Pode haver falsa sensacao de configuracao completa.
- Erros de tipo/namespace nao encontrado ao compilar consumidor.

Diagnostico recomendado:

1. Verificar `ProjectReference` no `.csproj`.
2. Rodar build da solution.

## Cenario 3: Dependencia circular acidental

Situacao:

1. `Application` referencia `Domain`.
2. Alguem adiciona referencia inversa (`Domain` -> `Application`).

Resultado:

- Falhas no grafo de compilacao.
- Violacao arquitetural.

Diagnostico recomendado:

1. Revisar referencias entre `.csproj`.
2. Executar build da solution para validar o grafo completo.

## Fluxo Recomendado no Dia a Dia

Para produtividade com seguranca, use este ciclo:

1. Durante implementacao: build isolado da camada alterada.
2. Antes de finalizar tarefa: build da solution inteira.
3. Antes de commit/pipeline: build da solution + testes (quando houver).

Exemplo:

```bash
dotnet build src/CadastroAcademico.Application/CadastroAcademico.Application.csproj
dotnet build CadastroAcademico.slnx
```

## Diagnostico Estruturado (Passo a Passo)

Se houver erro no build:

1. Identifique em qual projeto o erro ocorreu.
2. Veja se e erro local (codigo interno) ou de integracao (referencia/assinatura).
3. Se for integracao, valide primeiro o grafo de dependencias.
4. Recompile a solution apos ajuste para confirmar estabilidade geral.

## Aplicacao no Sistema Academico

No nosso contexto:

1. Build isolado acelera o trabalho por camada (`Domain`, `Application`).
2. Build da solution confirma que Console e Blazor continuam consumindo o nucleo corretamente.
3. Isso evita regressao silenciosa e reforca a disciplina arquitetural.

## Conclusao

Projeto isolado e build de solution nao competem; eles se complementam.

1. Um oferece velocidade.
2. O outro oferece confianca sistemica.

Maturidade tecnica e usar ambos no momento certo.

## Checkpoint de Aprendizagem

Depois desta leitura, o aluno deve conseguir responder:

1. Qual problema o build isolado resolve melhor?
2. Qual problema apenas o build da solution detecta com seguranca?
3. Como montar um fluxo de validacao eficiente e confiavel no dia a dia?
4. Em qual momento do trabalho usar cada tipo de build?
