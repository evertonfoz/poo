# DTOs e Resultados de Operacao

## DTOs

DTO (Data Transfer Object) e um objeto simples para transportar dados entre camadas.

## DTOs criados na etapa

1. Courses
   - CreateCourseInput
   - CourseDto
2. Students
   - CreateStudentInput
   - StudentDto
3. Enrollments
   - CreateEnrollmentInput
   - EnrollmentDto

## Por que usar DTO

1. Evita expor entidade de dominio diretamente para UI.
2. Melhora clareza de entrada e saida por caso de uso.
3. Facilita evolucao da API da aplicacao.

## Resultados de operacao

Foram criados:

1. OperationResult
2. OperationResult<T>

## Beneficio didatico

Com esses objetos, o aluno aprende um fluxo comum em sistemas reais:

1. Entrada via Input DTO
2. Processamento no Service
3. Saida via Result + DTO de retorno
