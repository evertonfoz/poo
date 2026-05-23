# Próximas Sessões

## Status atual

1. Etapa de fundamentos da solution .NET concluída (arquivos 01 a 05 em `Docs/01-fundamentos-solucao-dotnet/`).
2. Etapa de Domain documentada em trilha dedicada (`Docs/02-dominio-e-poo/`).
3. Etapa Application e memoria concluida com integracao em ConsoleApp e BlazorApp (`Docs/03-application-e-persistencia-em-memoria/`).

## Sessão 3 — Domínio inicial

Objetivo:
1. Criar entidades `Curso`, `Aluno`, `PerfilAcademico` e `Matricula`.
2. Aplicar validações de invariantes no domínio.
3. Proteger estado com encapsulamento (`private set`, coleções somente leitura).

Arquivos esperados:
1. Novos arquivos em `src/CadastroAcademico.Domain/` para entidades e enums.
2. Remoção/substituição do `Class1.cs` padrão.

Checklist de validação:
1. Build da solution continua verde.
2. Entidades não dependem de Console/Blazor.
3. Regras de negócio centrais ficam no domínio.

## Sessão 4 — Application e memória

Objetivo:
1. Criar store em memória compartilhado.
2. Criar serviços de aplicação (`CourseService`, `StudentService`, `EnrollmentService`).
3. Criar objetos de resultado/entrada mínimos para operações.

Checklist de validação:
1. Build verde.
2. Dependência respeita fluxo Application -> Domain.
3. Sem banco de dados nesta etapa.

## Sessão 5 — Consolidação e qualidade

Objetivo:
1. Consolidar validacoes manuais da UI com roteiro reproduzivel.
2. Planejar introducao de testes automatizados para Application.
3. Revisar cobertura didatica das trilhas 02 e 03 para evitar lacunas.

Checklist de validação:
1. Build verde.
2. Fluxos principais validados com roteiro claro.
3. Plano de testes automatizados definido sem quebrar arquitetura atual.
