# Organizacao do Application

## Estrutura proposta

- `Common/`: tipos compartilhados da camada.
- `Services/`: casos de uso e orquestracao.
- `DTOs/`: contratos de entrada e saida.
- `Results/`: resultados padronizados de operacoes.

## Aplicacao no projeto

1. Classe padrao foi substituida por `ApplicationAssemblyMarker` em `Common`.
2. Pastas `Services`, `DTOs` e `Results` foram criadas para guiar a proxima sessao.

## Proxima evolucao

Na Sessao 4, os servicos de aplicacao devem nascer ja dentro dessas pastas.
