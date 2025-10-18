# ChangeLog — 2025-10-18 18:50:00 (setup-domain-esbocos)

## Prompt
Criar esboços sem lógica para domínio de matrículas: `EnrollmentStatus`, `Student`, `Course`, `Enrollment`. Documentar invariantes, operações esperadas e colocar // TODOs para testes, sem implementar lógica.

## Tasks
- Criar enum `EnrollmentStatus`.
- Criar entidades mínimas em `University.Enrollments.Domain/Models/`:
  - `Student.cs` (Id, Name) — invariantes documentadas.
  - `Course.cs` (Id, Title, Capacity, MatriculationStart, MatriculationEnd) — documentar operações `Enroll`/`Unenroll` sem implementar.
  - `Enrollment.cs` (StudentId, CourseId, Status, EnrolledOn) — documentar igualdade semântica e máquina de estados esperada.
- Incluir comentários XML e // TODOs para cobertura por testes futuros.

## Progress
- Arquivos criados:
  - `University.Enrollments.Domain/Models/EnrollmentStatus.cs` — enum com comentários XML.
  - `University.Enrollments.Domain/Models/Student.cs` — esboço com comentários e // TODOs.
  - `University.Enrollments.Domain/Models/Course.cs` — esboço com propriedades, comentários XML, métodos `Enroll`/`Unenroll` que lançam `NotImplementedException` e // TODOs.
  - `University.Enrollments.Domain/Models/Enrollment.cs` — esboço com comentários sobre igualdade semântica e a máquina de estados (sem implementação).

## Rationale
Segui a seção "Prática de ChangeLogs por chat" de `AGENTS.md`:
- Criar pasta `ChangeLogs/YYYYMMDD-HHMMSS-<objetivo>` e um arquivo principal com rationale/summary.
- Consolidar alterações e indicar próximos passos e testes.

## Next steps
- Implementar validações (factories/constructors) e a máquina de estados em `Enrollment`.
- Adicionar testes unitários em `University.Enrollments.Tests/` cobrindo invariantes, janela de matrícula, capacidade e transições inválidas.

## Notes
- Este arquivo foi criado automaticamente pelo agente conforme orientações de `AGENTS.md`.
- Remova/ajuste o timestamp no nome da pasta se preferir outra convenção.
