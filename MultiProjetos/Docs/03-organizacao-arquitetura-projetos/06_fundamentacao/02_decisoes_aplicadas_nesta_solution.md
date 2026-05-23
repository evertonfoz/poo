# Decisoes Aplicadas Nesta Solution

## Domain

- Entities: classes com identidade e ciclo de vida.
- ValueObjects: classes sem identidade propria, com foco em valor.
- Enums: tipos enumerados do dominio.

Motivo: separar semantica de negocio por natureza do objeto.

## Application

- Common: tipos compartilhados pela camada.
- Services: casos de uso e orquestracao.
- DTOs: contratos de entrada e saida.
- Results: padrao de retorno para operacoes.

Motivo: evitar acumulacao de logica em um unico arquivo e preparar a sessao de servicos.

## Console

- Menus: fluxo de navegacao textual.
- IO: interacao de entrada e saida.

Motivo: impedir que Program cresca sem controle.

## Blazor

- Features: organizacao por funcionalidade da interface.

Motivo: escalar telas sem espalhar arquivos por toda a camada de apresentacao.
