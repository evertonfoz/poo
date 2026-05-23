# Erros Comuns e Como Evitar

## Erro 1: jogar tudo no Program

Sintoma: Program cresce e mistura fluxo, validacao e regra de negocio.

Prevencao: mover responsabilidades para Menus, IO e Services.

## Erro 2: criar classe sem local definido

Sintoma: arquivos novos aparecem no root sem criterio.

Prevencao: usar o guia pratico de classificacao antes de criar qualquer classe.

## Erro 3: confundir Domain com Application

Sintoma: regra de negocio fica em service e entidade vira apenas dado.

Prevencao: manter invariantes e comportamento no Domain; Application so orquestra.

## Erro 4: duplicar modelos para cada interface

Sintoma: Console e Blazor com regras diferentes para o mesmo caso.

Prevencao: centralizar casos de uso na Application e usar DTOs de forma consistente.
