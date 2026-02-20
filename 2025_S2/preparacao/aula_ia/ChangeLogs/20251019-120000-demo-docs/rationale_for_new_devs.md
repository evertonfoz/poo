# Demo app documentation — rationale and run instructions

prompt
------
Criar documentação do demo `University.Enrollments.App` dentro de `ChangeLogs/` seguindo a convenção do repositório (cada sessão tem sua pasta timestamped e um `rationale_for_new_devs.md`). A documentação deve descrever o propósito do demo, o contrato, como executar e a saída esperada.

tasks
-----
- Descrever propósito do demo
- Listar contrato (inputs, outputs, comportamento)
- Incluir passo-a-passo para executar localmente
- Incluir saída esperada e notas sobre edge cases

progress
--------
- Criei `Program.cs` no projeto `University.Enrollments.App` que usa as APIs do domínio para demonstrar o cenário (capacidade 2 e tentativa de 3 matrículas).
- Compilei a solução com `dotnet build` e passou.
- Removi o README que havia sido criado no projeto App e estou adicionando esta documentação centralizada em `ChangeLogs`.

rationale
---------
Motivação: o repositório segue uma convenção de registrar mudanças e decisões por sessão em `ChangeLogs/YYYYMMDD-HHMMSS-<objetivo>/rationale_for_new_devs.md`. Isso facilita revisão por outros desenvolvedores e mantém o root e os projetos limpos de arquivos temporários ou documentações de sessão.

how to run (local)
-------------------
1. A partir do diretório raiz do workspace:

```bash
cd /Users/evertoncoimbradearaujo/Documents/GitHub/poo/preparacao/aula_ia
dotnet run --project University.Enrollments.App/University.Enrollments.App.csproj
```

Expected output (example)
-------------------------
```
Course: Intro to Programming (Id=1), Capacity=2
Matriculation window: 2025-10-18 - 2025-10-26

Enrolling student 1 - Alice...
  -> Success: student 1 enrolled. Enrolled count: 1

Enrolling student 2 - Bob...
  -> Success: student 2 enrolled. Enrolled count: 2

Enrolling student 3 - Carol...
  -> Failed to enroll student 3 (Carol): Cannot enroll: course 1 has no available seats (capacity 2).

Final enrollments:
 - Student 1: Status=Enrolled, EnrolledOn=2025-10-19
 - Student 2: Status=Enrolled, EnrolledOn=2025-10-19
Total enrolled: 2 / 2
```

notes and follow-ups
--------------------
- For CI or automated tests, prefer adding an integration test under `University.Enrollments.Tests` that asserts this scenario rather than relying on the console output.
- If desejado, posso também commitar este ChangeLog e rodar `dotnet test` para adicionar um teste que valida o comportamento.
