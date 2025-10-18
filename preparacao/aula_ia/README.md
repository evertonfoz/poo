# University.Enrollments

Exemplo mínimo de solution C# para demonstração de estrutura:

Projetos:
- University.Enrollments.App (Console)
- University.Enrollments.Domain (Class Library)
- University.Enrollments.Tests (xUnit)

Requisitos:
- .NET SDK 9.x instalado (o repositório foi atualizado para `net9.0`).

Como construir e testar:

```bash
cd aula_ia
dotnet restore University.Enrollments.sln
dotnet build University.Enrollments.sln -c Debug
dotnet test University.Enrollments.sln
```

Notas:
- Os projetos estão configurados com `ImplicitUsings` e `Nullable` habilitados.
- Se seu ambiente CI usar outro SDK, atualize o pipeline para utilizar .NET 9.
