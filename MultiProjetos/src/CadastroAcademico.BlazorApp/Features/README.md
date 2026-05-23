# Features

Contem as paginas da interface Blazor, cada uma com sua rota e responsabilidade:

| Arquivo | Rota | Responsabilidade |
|---|---|---|
| `Cursos.razor` | `/cursos` | Formulario de cadastro e tabela de cursos |
| `Alunos.razor` | `/alunos` | Formulario de cadastro (com perfil academico) e tabela de alunos |
| `Matriculas.razor` | `/matriculas` | Campo de busca filtravel de aluno, dropdown de curso e tabela de matriculas |
| `FluxoAcademico.razor` | `/academico` | Tela didatica com os tres fluxos lado a lado para fins de estudo |

## Campo de busca de aluno (Matriculas.razor)

O campo de aluno na pagina de matriculas funciona como um autocomplete:

1. O usuario digita parte do nome.
2. A lista de alunos e filtrada em tempo real.
3. O usuario clica no nome desejado na lista.
4. O componente armazena o `Id` do aluno selecionado (nao apenas o texto).
5. O formulario fica pronto para enviar ao `EnrollmentService`.
