# University.Enrollments.App (Demo)

Este repositório contém um pequeno aplicativo de console que demonstra o uso da API pública do domínio em `University.Enrollments.Domain`.

## Objetivo do demo

- Demonstrar o fluxo de inscrição em um curso usando apenas a API de domínio (sem duplicar regras no App).
- Mostrar captura e apresentação de erros de domínio (`DomainException`) pelo aplicativo.

## O que o programa faz (resumo)

- Constrói programaticamente um `Course` e alguns `Student`.
- Tenta inscrever vários estudantes chamando as operações expostas pelo domínio (ex.: `Course.Enroll(studentId)`).
- Captura exceções de domínio e imprime mensagens amigáveis no console.
- Mostra o estado final das inscrições (enrolled count, lista de enrolados).

## Como rodar

Execute estes comandos a partir do diretório raiz do repositório (assumindo shell zsh):

```bash
cd /Users/evertoncoimbradearaujo/Documents/GitHub/poo/preparacao/aula_ia

# Executa a suíte de testes (xUnit)
dotnet test

# Executa o demo de console
dotnet run --project University.Enrollments.App
```

## Entidades e regras de negócio (resumo)

- Course
  - Propriedades relevantes: Id, Capacity (vagas), MatriculationWindow (data inicial/final), coleção de `Enrollment`.
  - Regras: não aceitar inscrições fora da janela, respeitar a capacidade, contar inscritos corretamente.

- Student
  - Propriedades mínimas: StudentId, Nome (no demo podem ser gerados programaticamente).

- Enrollment (classe de associação)
  - Representa o vínculo entre `Student` e `Course`.
  - Unicidade: um par (StudentId, CourseId) só pode existir uma vez.
  - Estados possíveis (ex.: Pending, Enrolled, Completed, Cancelled) e transições controladas por regras de domínio.
  - Igualdade/identity: baseada em `StudentId` + `CourseId`.

Notes and edge cases
--------------------
- The App does not reimplement domain validations. All rules (window, capacity, uniqueness) are enforced by `Course` and exceptions are captured.
- Dates use `DateOnly.FromDateTime(DateTime.UtcNow)` in the domain, so tests or environments with different timezones should take UTC into account.
- The demo is intentionally minimal. For production apps you would normally add structured logging, configuration-driven inputs (e.g., course data), and better error classification.

## Limitações e próximos passos

- Adicionar um teste de integração automatizado que execute o mesmo cenário e verifique os comportamentos esperados.
- Expor parâmetros do demo (capacity, matriculation window, lista de estudantes) via argumentos de CLI ou arquivo JSON.
- Melhorar formatação do console ou adicionar flags --quiet/--verbose.

Licença / Atribuição
---------------------
Este demo faz parte da solução de exemplo University.Enrollments. Consulte o repositório raiz para notas de licença e atribuição.

````
