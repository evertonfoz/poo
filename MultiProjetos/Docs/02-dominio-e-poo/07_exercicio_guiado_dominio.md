# Exercicio Guiado de Dominio

## Objetivo

Reforcar leitura de codigo de dominio e identificacao de regras centrais.

## Questao 1

Em qual classe esta a regra que impede matricula ativa duplicada no mesmo curso?

Resposta esperada:

`Curso`, no metodo `MatricularAluno(...)`.

## Questao 2

Qual classe controla as transicoes de status de matricula?

Resposta esperada:

`Matricula`, nos metodos `Cancelar()` e `Concluir()`.

## Questao 3

Por que `PerfilAcademico` e um Value Object neste contexto?

Resposta esperada:

Porque representa um conjunto de valores sem identidade propria no negocio.

## Questao 4

Citar dois sinais de encapsulamento no dominio atual.

Resposta esperada:

1. Propriedades com `private set`.
2. Exposicao de colecoes como somente leitura.

## Rubrica rapida

1. Acertou 4/4: dominio compreendido.
2. Acertou 2-3/4: revisar arquivos 01 a 05.
3. Abaixo de 2/4: refazer leitura com foco em regras e responsabilidades.
