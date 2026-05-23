# Organizacao do Domain

## Estrutura adotada

- `Entities/`: entidades com identidade e ciclo de vida.
- `ValueObjects/`: objetos de valor sem identidade propria.
- `Enums/`: tipos enumerados do dominio.

## Aplicacao no projeto

1. `Curso`, `Aluno` e `Matricula` ficaram em `Entities`.
2. `PerfilAcademico` ficou em `ValueObjects`.
3. `StatusMatricula` ficou em `Enums`.

## Resultado

O dominio deixa de ser uma pasta "flat" e passa a comunicar intencao arquitetural pelo proprio caminho dos arquivos.
