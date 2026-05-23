# Integracao com Console e Blazor

## ConsoleApp

A ConsoleApp foi integrada para demonstrar um fluxo ponta a ponta:

1. Criar curso
2. Criar aluno
3. Matricular aluno
4. Listar dados

Objetivo didatico: mostrar consumo direto de servicos da Application sem colocar regra de negocio na interface.

## BlazorApp

A BlazorApp foi integrada com:

1. Registro de servicos no DI (`InMemoryAcademicStore`, `CourseService`, `StudentService`, `EnrollmentService` como Singleton).
2. Paginas dedicadas por contexto funcional:
   - `/cursos`: formulario de cadastro e tabela de cursos.
   - `/alunos`: formulario separado em dados do aluno e perfil academico, com tabela.
   - `/matriculas`: campo de busca filtravel de aluno (autocomplete), dropdown de curso, tabela com badges de status e acoes de cancelar/concluir.
3. Tela didatica em `/academico` com os tres fluxos lado a lado para fins de estudo.
4. Menu lateral com navegacao entre todas as paginas academicas.

## Aprendizado arquitetural

1. A mesma camada Application atende duas interfaces diferentes.
2. Interface muda, dominio e casos de uso permanecem.
3. Reuso aumenta quando responsabilidade esta bem separada.
