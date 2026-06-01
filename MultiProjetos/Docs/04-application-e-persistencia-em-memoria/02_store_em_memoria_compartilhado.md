# Store em Memoria Compartilhado

## Contexto

Nesta fase do projeto, o objetivo e aprender fluxo de arquitetura sem adicionar complexidade de banco de dados.

## Solucao adotada

Foi criado um store em memoria compartilhado na Application:

- InMemoryAcademicStore

Esse store mantem listas em memoria para:

1. Cursos
2. Alunos
3. Matriculas

Na ConsoleApp, o store e criado manualmente no `Program.cs` e compartilhado pelos menus durante a execucao.

Na BlazorApp, o store e registrado como `scoped`, o que faz cada circuito do usuario manter seu proprio estado em memoria durante a sessao interativa.

## Vantagens para aprendizado

1. Permite testar o fluxo completo sem persistencia externa.
2. Reduz atrito para focar em separacao de responsabilidades.
3. Facilita depuracao durante aulas praticas.
4. Em Blazor, evita compartilhar estado mutavel entre usuarios quando usado com lifetime por circuito.

## Limites

1. Dados sao perdidos ao encerrar o processo.
2. Nao ha concorrencia real de multiplos usuarios.
3. Nao substitui repositorio persistente em producao.
4. Nesta etapa, a exclusao foi mantida propositalmente simples: a remocao acontece no store, sem sincronizar toda a relacao historica entre colecoes internas das entidades.

## Observacao didatica importante

O item 4 da secao anterior e uma simplificacao consciente do projeto.

Ela foi mantida para preservar foco em:

1. fluxo entre camadas;
2. uso de DTOs e services;
3. integracao entre Console e Blazor.

Em uma evolucao futura, a exclusao pode ser remodelada para garantir consistencia historica mais forte no grafo de objetos em memoria.

## Proxima evolucao natural

Quando a turma dominar o fluxo atual, o store em memoria pode ser trocado por repositorios persistentes sem mudar o contrato da interface.
