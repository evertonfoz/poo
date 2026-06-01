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
4. Atualizar curso
5. Excluir curso

A validacao principal do curso continua no Domain (classe Curso).

Além disso, o service agora também protege a entrada contra sigla duplicada ja no cadastro, nao apenas na atualizacao.

## StudentService

Responsavel por:

1. Criar aluno
2. Buscar aluno por id
3. Listar alunos
4. Atualizar aluno
5. Excluir aluno

A construcao de PerfilAcademico e a criacao de Aluno usam as classes do Domain.

O service tambem valida na criacao conflitos de unicidade para:

1. e-mail;
2. registro academico.

## EnrollmentService

Responsavel por:

1. Matricular aluno em curso
2. Cancelar matricula
3. Concluir matricula
4. Listar matriculas
5. Excluir matricula

Ponto importante: a matricula e criada pelo metodo de dominio Curso.MatricularAluno(...), preservando regra de negocio no Domain.

Nesta etapa, a exclusao continua simples por escolha didatica: ela remove o item do store em memoria e nao tenta remodelar toda a consistencia historica entre os objetos agregados.

## Padrao de retorno

Os servicos retornam OperationResult e OperationResult<T> para padronizar sucesso/falha de forma clara para a interface.
