# Papel da Camada Domain

## Objetivo

A camada Domain concentra as regras de negocio centrais do sistema academico.

## Responsabilidades

1. Modelar entidades do negocio (`Curso`, `Aluno`, `Matricula`).
2. Garantir invariantes (dados obrigatorios, estado valido, transicoes permitidas).
3. Encapsular comportamento que altera estado com regras.

## O que NAO deve ficar no Domain

1. Codigo de interface (Console, Blazor).
2. Detalhes de persistencia de infraestrutura.
3. Regras de apresentacao.

## Princípio-chave

Se uma regra define "o que e valido no negocio", ela deve estar no Domain.

## Classes do projeto relacionadas

1. [src/CadastroAcademico.Domain/Entities/Curso.cs](src/CadastroAcademico.Domain/Entities/Curso.cs)
2. [src/CadastroAcademico.Domain/Entities/Aluno.cs](src/CadastroAcademico.Domain/Entities/Aluno.cs)
3. [src/CadastroAcademico.Domain/Entities/Matricula.cs](src/CadastroAcademico.Domain/Entities/Matricula.cs)
4. [src/CadastroAcademico.Domain/ValueObjects/PerfilAcademico.cs](src/CadastroAcademico.Domain/ValueObjects/PerfilAcademico.cs)
5. [src/CadastroAcademico.Domain/Enums/StatusMatricula.cs](src/CadastroAcademico.Domain/Enums/StatusMatricula.cs)
