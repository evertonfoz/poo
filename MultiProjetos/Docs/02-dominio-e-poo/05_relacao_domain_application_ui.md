# Relacao entre Domain, Application e UI

## Fluxo de chamada

1. UI coleta dados do usuario.
2. Application coordena o caso de uso.
3. Domain executa regra de negocio.
4. Application retorna resultado para UI.

## Exemplo real

1. Blazor chama `EnrollmentService.Enroll(...)`.
2. Service chama `Curso.MatricularAluno(...)`.
3. Domain valida e cria `Matricula`.
4. Service retorna `OperationResult<EnrollmentDto>`.

## Regra de ouro

UI e Application podem mudar com frequencia; Domain deve permanecer estavel e confiavel.

## Arquivos de referencia

1. [src/CadastroAcademico.Application/Services/EnrollmentService.cs](src/CadastroAcademico.Application/Services/EnrollmentService.cs)
2. [src/CadastroAcademico.Domain/Entities/Curso.cs](src/CadastroAcademico.Domain/Entities/Curso.cs)
3. [src/CadastroAcademico.BlazorApp/Features/FluxoAcademico.razor](src/CadastroAcademico.BlazorApp/Features/FluxoAcademico.razor)
