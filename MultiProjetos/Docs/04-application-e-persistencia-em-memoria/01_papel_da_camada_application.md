# Papel da Camada Application

## Objetivo

A camada Application coordena casos de uso do sistema sem carregar regra de negocio central da entidade.

## Responsabilidades

1. Receber dados de entrada da interface (Console, Blazor etc.).
2. Chamar metodos do dominio para executar o comportamento.
3. Organizar retorno para a interface (DTOs e resultados).
4. Controlar fluxo de aplicacao (orquestracao), nao regra de negocio profunda.

## O que NAO deve ficar na Application

1. Invariantes de entidade (ex.: validacoes internas de Curso, Aluno, Matricula).
2. Regras de mudanca de estado que pertencem ao dominio.
3. Dependencia da UI.

## Dependencias

Fluxo esperado:

Application -> Domain

A Application nao deve depender de ConsoleApp nem BlazorApp.

## Resultado didatico no projeto

Nesta etapa, a Application passou a expor servicos de caso de uso:

1. CourseService
2. StudentService
3. EnrollmentService

Todos eles chamam classes do Domain para manter o centro da regra no lugar correto.
