# Guia Pratico Para Novas Classes

## Pergunta 1

A classe representa um conceito de negocio com identidade?

- Sim: criar em Domain/Entities.
- Nao: ir para a pergunta 2.

## Pergunta 2

A classe representa um valor de negocio sem identidade propria?

- Sim: criar em Domain/ValueObjects.
- Nao: ir para a pergunta 3.

## Pergunta 3

A classe coordena caso de uso e conversa com o dominio?

- Sim: criar em Application/Services.
- Nao: ir para a pergunta 4.

## Pergunta 4

A classe transporta dados entre interface e aplicacao?

- Sim: criar em Application/DTOs.
- Nao: ir para a pergunta 5.

## Pergunta 5

A classe padroniza sucesso e falha de operacoes?

- Sim: criar em Application/Results.

## Regra final

Se ainda houver duvida, nao criar no root. Registrar a duvida e decidir em conjunto antes de criar.
