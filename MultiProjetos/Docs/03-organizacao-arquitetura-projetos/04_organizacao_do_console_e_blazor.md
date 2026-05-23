# Organizacao do Console e Blazor

## ConsoleApp

- `IO/ConsoleIO.cs`: helper estatico para entrada e saida formatada (cabecalho, sucesso, erro, linha, pausa, leitura de texto/inteiro e confirmacao).
- `Menus/`: menu textual completo com submenu por contexto funcional e dispatcher principal.
  - `MenuPrincipal.cs`: menu raiz — instancia os submenus e despacha para cada um. Opcoes: Cursos, Alunos, Matriculas, Relatorios.
  - `MenuCursos.cs`: CRUD completo de cursos (cadastrar, listar, consultar por Id, atualizar, excluir com confirmacao).
  - `MenuAlunos.cs`: CRUD completo de alunos (cadastrar com secoes "Dados do Aluno" e "Perfil Academico", listar, consultar, atualizar, excluir com confirmacao).
  - `MenuMatriculas.cs`: gerenciamento de matriculas (realizar, listar, consultar, cancelar, concluir, excluir com confirmacao).
  - `MenuRelatorios.cs`: relatorios e consultas (alunos por curso, cursos de um aluno, total de alunos por curso, matriculas por status).
- `Program.cs`: ponto de entrada — instancia store, services e `MenuPrincipal`; nao contem logica de negocio.

## BlazorApp

- `Features/`: paginas da aplicacao, uma por contexto funcional.
  - `_Imports.razor`: `@using` compartilhados entre todas as paginas da pasta.
  - `Cursos.razor` → rota `/cursos`: CRUD completo — cadastrar, listar, editar inline (card amarelo com pré-preenchimento), excluir com confirmacao na linha.
  - `Alunos.razor` → rota `/alunos`: CRUD completo — cadastrar (secoes "Dados do Aluno" e "Perfil Academico"), listar, editar inline, excluir com confirmacao na linha.
  - `Matriculas.razor` → rota `/matriculas`: busca filtravel de aluno (autocomplete), dropdown de curso, matricular; tabela com cancelar/concluir (apenas status Ativa) e excluir com confirmacao na linha.
  - `FluxoAcademico.razor` → rota `/academico`: tela didatica com os tres fluxos lado a lado para fins de estudo.
- `Components/`: componentes de infraestrutura (layout, roteamento, paginas de erro).
  - `Layout/`: `MainLayout.razor` (layout principal, sem top-row de About), `NavMenu.razor` (sidebar com titulo "Sistema Academico").
  - `Pages/`:
    - `Home.razor` → rota `/`: pagina inicial do projeto com cards de acesso rapido e descricao da arquitetura.
    - `Error.razor`, `NotFound.razor`: paginas de erro do framework (mantidas do template).
    - `Counter.razor`, `Weather.razor`: paginas do template mantidas no projeto mas fora da navegacao da aplicacao.

## Diretriz

Interfaces nao devem carregar regra de negocio. Elas apenas coletam entrada, exibem saida e chamam a camada Application.
