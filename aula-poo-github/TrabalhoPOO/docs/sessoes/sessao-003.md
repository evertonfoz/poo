# Sessao 003

## Data

18 de maio de 2026

## Objetivo

Identificar e documentar os casos de uso do dominio com base nos requisitos funcionais definidos para orientar a implementacao futura.

## Decisoes tomadas

- Consolidar os casos de uso em um unico artefato no dominio, com estrutura padronizada por ator, fluxo principal e excecoes.
- Garantir cobertura completa dos requisitos funcionais por meio de matriz de rastreabilidade RF x UC.
- Manter os casos de uso focados em comportamento observavel de negocio, sem acoplamento a detalhes tecnicos de implementacao.

## Alteracoes realizadas

- Criado o documento de casos de uso em `TrabalhoPOO/docs/dominio_do_sistema/uses_cases/casos_de_uso_incubadora_projetos_alunos.md`.
- Definidos 15 casos de uso cobrindo cadastro, configuracao do projeto, gestao de associacoes, avaliacoes, finalizacao e consulta de percentual aprovado.
- Incluida matriz de rastreabilidade com cobertura de RF01 a RF29.
- Atualizado o indice de sessoes em `TrabalhoPOO/docs/sessoes/README.md` com esta nova sessao.

## Status final

Encerrada e aprovada.

## Proximos passos

- Desdobrar os casos de uso em historias tecnicas e criterios de aceite.
- Implementar as classes de dominio e metodos de comportamento de acordo com os UCs.
- Criar cenarios de teste para fluxos validos e excecoes mapeadas.