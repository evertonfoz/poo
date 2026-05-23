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

## Vantagens para aprendizado

1. Permite testar o fluxo completo sem persistencia externa.
2. Reduz atrito para focar em separacao de responsabilidades.
3. Facilita depuracao durante aulas praticas.

## Limites

1. Dados sao perdidos ao encerrar o processo.
2. Nao ha concorrencia real de multiplos usuarios.
3. Nao substitui repositorio persistente em producao.

## Proxima evolucao natural

Quando a turma dominar o fluxo atual, o store em memoria pode ser trocado por repositorios persistentes sem mudar o contrato da interface.
