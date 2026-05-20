# Sessao 001

## Data

18 de maio de 2026

## Objetivo

Definir e criar a estrutura inicial de pastas seguindo o padrao MVC para o projeto.

## Decisoes tomadas

- Adotar organizacao MVC dentro de `TrabalhoPOO`.
- Separar `Models` por papel de dominio (entidades base, agregacoes, composicoes e projeto).
- Preparar `Views` para uso em console.

## Alteracoes realizadas

- Criada pasta `Controllers`.
- Criada estrutura em `Models`:
  - `EntidadesBase`
  - `Projetos`
  - `Agregacoes`
  - `Composicoes`
- Criada estrutura em `Views`:
  - `ConsoleViews`
  - `ViewModels`

## Status final

Encerrada e aprovada.

## Proximos passos

- Criar os arquivos `.cs` de cada classe do dominio.
- Implementar regras de negocio e invariantes no modelo.
- Adicionar demonstracao completa em `Program.cs`.
