# Checklist de Dominio

## Checklist tecnico

1. [x] Entidades modeladas com identidade.
2. [x] Value Object criado para composicao de aluno.
3. [x] Invariantes essenciais aplicadas.
4. [x] Transicoes de estado protegidas na entidade de matricula.
5. [x] Encapsulamento aplicado (`private set`, colecoes somente leitura).

## Checklist arquitetural

1. [x] Domain sem dependencia de UI.
2. [x] Domain sem dependencia da camada Application.
3. [x] Regras centrais no Domain, nao na interface.

## Critério de saida

A etapa de dominio e considerada madura quando as regras mais importantes estao dentro do modelo e qualquer interface apenas consome esse comportamento.
