# Sessao 005

## Data

18 de maio de 2026

## Objetivo

Implementar a entidade `Estudante`, centralizar validacoes reutilizaveis, introduzir `Curso` como tipo proprio e encerrar a sessao com registro documental e commit.

## Decisoes tomadas

- Implementar `Estudante` em `Models/EntidadesBase`, por ser uma entidade independente do ciclo de vida de `ProjetoIncubado`.
- Centralizar validacoes compartilhadas na classe `ValidadorDominio` para reaproveitamento nas proximas entidades.
- Evoluir `Curso` de `string` para tipo proprio de dominio por decisao do usuario, mantendo compatibilidade no construtor de `Estudante` por meio de overload.
- Tratar `Curso` como objeto imutavel, sem API de alteracao apos a criacao.

## Alteracoes realizadas

- Criada a classe `TrabalhoPOO/Models/EntidadesBase/Estudante.cs` com propriedades validadas, construtor consistente e metodo `AtualizarNome`.
- Criada a classe `TrabalhoPOO/Models/EntidadesBase/ValidadorDominio.cs` com validacao de campo obrigatorio e validacao de email por expressao regular.
- Criada a classe `TrabalhoPOO/Models/EntidadesBase/Curso.cs` como tipo proprio imutavel.
- Refatorada a classe `Estudante` para usar `Curso` como tipo de dominio, preservando o construtor que recebe `string`.
- Validado o projeto com `dotnet build` apos cada ajuste relevante.
- Atualizado o indice de sessoes em `TrabalhoPOO/docs/sessoes/README.md` com esta nova sessao.

## Status final

Encerrada e aprovada.

## Proximos passos

- Implementar `Professor` e `Laboratorio` reutilizando `ValidadorDominio` e o mesmo padrao de modelagem.
- Definir se outros conceitos textuais do dominio devem permanecer como tipos primitivos ou evoluir para tipos proprios.
- Iniciar a implementacao de `ProjetoIncubado` com suas associacoes obrigatorias e invariantes.