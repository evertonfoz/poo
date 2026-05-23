# Services de Aplicacao

## Visao geral

Foram implementados tres servicos de aplicacao:

1. CourseService
2. StudentService
3. EnrollmentService

## CourseService

Responsavel por:

1. Criar curso
2. Buscar curso por id
3. Listar cursos

A validacao principal do curso continua no Domain (classe Curso).

## StudentService

Responsavel por:

1. Criar aluno
2. Buscar aluno por id
3. Listar alunos

A construcao de PerfilAcademico e a criacao de Aluno usam as classes do Domain.

## EnrollmentService

Responsavel por:

1. Matricular aluno em curso
2. Cancelar matricula
3. Concluir matricula
4. Listar matriculas

Ponto importante: a matricula e criada pelo metodo de dominio Curso.MatricularAluno(...), preservando regra de negocio no Domain.

## Padrao de retorno

Os servicos retornam OperationResult e OperationResult<T> para padronizar sucesso/falha de forma clara para a interface.
