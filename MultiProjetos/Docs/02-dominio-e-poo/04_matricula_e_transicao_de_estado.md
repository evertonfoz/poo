# Matricula e Transicao de Estado

## Ciclo de vida da Matricula

Estados disponiveis:

1. Ativa
2. Cancelada
3. Concluida

## Regras de transicao

1. Apenas matriculas ativas podem ser canceladas.
2. Apenas matriculas ativas podem ser concluidas.
3. Matricula ja finalizada nao pode ser finalizada novamente.

## Onde isso esta implementado

1. [src/CadastroAcademico.Domain/Entities/Matricula.cs](src/CadastroAcademico.Domain/Entities/Matricula.cs)
2. [src/CadastroAcademico.Domain/Enums/StatusMatricula.cs](src/CadastroAcademico.Domain/Enums/StatusMatricula.cs)

## Ligacao com Curso

A criacao da matricula ocorre em `Curso.MatricularAluno(...)`, que tambem aplica a regra de nao permitir duas matriculas ativas do mesmo aluno no mesmo curso.

## Resultado didatico

O aluno enxerga claramente uma maquina de estados simples aplicada ao dominio academico.
