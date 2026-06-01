# Progresso — Sistema Acadêmico

## Sessão atual

Data: 2026-05-23
Status: Sessão 6 concluída — Revisão técnica completa e MCPs configurados

## O que foi implementado

1. Configuração de segredo para MCP ajustada para variável de ambiente.
2. Estrutura de solution criada no formato `slnx`:
   - `CadastroAcademico.slnx`
   - `src/CadastroAcademico.Domain`
   - `src/CadastroAcademico.Application`
   - `src/CadastroAcademico.ConsoleApp`
   - `src/CadastroAcademico.BlazorApp`
3. Referências entre projetos configuradas:
   - Application -> Domain
   - ConsoleApp -> Application
   - BlazorApp -> Application
4. Build inicial da solution executado com sucesso.
5. Documento de fundamentos atualizado com a anatomia real do arquivo `.slnx`, incluindo explicacao linha a linha para alunos.
6. Criado o arquivo de fundamentos sobre arquitetura em multiplos projetos na solution, com mapa de dependencias e diagrama didatico.
7. Criado o arquivo de fundamentos sobre referencias entre projetos na pratica, com comandos dotnet add reference e impacto no build.
8. Criado o arquivo de fundamentos sobre ordem de compilacao e restore na solution, consolidando o fluxo de build do .NET no contexto do projeto.
9. Criado o arquivo de fundamentos sobre diferenca entre build de projeto isolado e build da solution completa, com cenarios praticos de erro e diagnostico.
10. Bloco de fundamentos da solution .NET (arquivos 01 a 05) concluido.
11. Removido arquivo padrão de domínio (`Class1.cs`).
12. Implementadas entidades de domínio:
   - `Curso`
   - `Aluno`
   - `PerfilAcademico`
   - `Matricula`
13. Implementado enum `StatusMatricula` para controlar ciclo da matrícula.
14. Aplicadas validações de invariantes no domínio (obrigatoriedade, carga horária > 0, proteção contra matrícula ativa duplicada no mesmo curso).
15. Encapsulamento aplicado com `private set` e coleções expostas como `IReadOnlyCollection<T>`.
16. Reorganização arquitetural de pastas aplicada em todos os projetos da solution:
   - Domain: `Entities`, `ValueObjects`, `Enums`.
   - Application: `Common`, `Services`, `DTOs`, `Results`.
   - ConsoleApp: `Menus`, `IO`.
   - BlazorApp: `Features`.
17. Classes do Domain movidas do root para pastas sem alteração de comportamento.
18. Classe padrão da Application substituída por `ApplicationAssemblyMarker` em `Common`.
19. Criado novo bloco de fundamentos em `Docs/02-organizacao-arquitetura-projetos` com arquivos 01 a 05.
20. Criada trilha aprofundada em `Docs/02-organizacao-arquitetura-projetos/06_fundamentacao` para explicar motivacoes e criterios de organizacao.
21. Checklist de organizacao atualizado com referencia para estudo guiado da fundamentacao.
22. Adicionado material conceitual especifico sobre `Entities`, `ValueObjects` e `Enums` com exemplos do proprio projeto.
23. Criado mini exercicio guiado com 8 casos, gabarito comentado e rubrica de correcao para fixacao dos conceitos.
24. Criado store em memoria compartilhado em `CadastroAcademico.Application/Common/InMemoryAcademicStore.cs`.
25. Criados objetos de resultado de operacao em `CadastroAcademico.Application/Results`:
   - `OperationResult`
   - `OperationResult<T>`
26. Criados DTOs minimos de entrada e saida para cursos, alunos e matriculas em `CadastroAcademico.Application/DTOs`.
27. Implementados servicos de aplicacao em `CadastroAcademico.Application/Services`:
   - `CourseService`
   - `StudentService`
   - `EnrollmentService`
28. Servicos implementados consumindo regras do dominio (ex.: `Curso.MatricularAluno`, `Matricula.Cancelar`, `Matricula.Concluir`) sem mover regra de negocio para a interface da Application.
29. Persistencia desta etapa mantida integralmente em memoria (sem banco de dados), conforme plano da Sessao 4.
30. ConsoleApp integrada com os servicos da Application em fluxo didatico completo no `Program.cs`.
31. Fluxo validado em runtime na ConsoleApp com criacao de curso, criacao de aluno, matricula e listagens.
32. BlazorApp integrada com a camada Application via DI no `Program.cs` (`InMemoryAcademicStore`, `CourseService`, `StudentService`, `EnrollmentService`).
33. Nova tela didatica criada em `Features/FluxoAcademico.razor` com formularios para criar curso/aluno, efetuar matricula e listar dados em memoria.
34. Menu de navegacao da BlazorApp atualizado para incluir acesso ao fluxo academico (`/academico`).
35. Criado novo bloco didatico da etapa Application em `Docs/03-application-e-persistencia-em-memoria` com 6 arquivos numerados, no mesmo padrao progressivo das etapas anteriores.
36. Evoluida a tela `Features/FluxoAcademico.razor` com operacoes de cancelar e concluir matricula, com exibicao condicional para status ativo.
37. Criado documento complementar da evolucao em `Docs/03-application-e-persistencia-em-memoria/07_evolucao_status_matricula_na_blazor.md`.
38. Adicionada secao de cenarios de teste manual no documento `07_evolucao_status_matricula_na_blazor.md` para apoiar validacao em aula/laboratorio.
39. Criado bloco didatico de dominio em `Docs/02-dominio-e-poo` com 7 arquivos progressivos para consolidar fundamentos de modelagem e regras de negocio.
40. Criadas paginas dedicadas na BlazorApp para cada contexto do sistema academico:
   - `Features/Cursos.razor` (rota `/cursos`): formulario inline e tabela de cursos.
   - `Features/Alunos.razor` (rota `/alunos`): formulario com secoes separadas "Dados do Aluno" e "Perfil Academico" e tabela de alunos.
   - `Features/Matriculas.razor` (rota `/matriculas`): campo de busca filtravel de aluno (autocomplete), dropdown de curso, tabela de matriculas com badges de status e acoes de cancelar/concluir.
41. Criado `Features/_Imports.razor` para centralizar `@using` compartilhados entre todas as paginas da pasta Features, eliminando repeticao e mantendo o padrao da pasta Components.
42. Menu de navegacao da BlazorApp atualizado com tres novos itens (Cursos, Alunos, Matriculas) e icones SVG embutidos em CSS adicionados para todos os itens do menu academico.
43. Documentacao atualizada em `README.md`, `Features/README.md`, `Docs/04-application-e-persistencia-em-memoria/` e `Docs/03-organizacao-arquitetura-projetos/` para refletir novas rotas e estrutura de paginas.
44. Menu lateral limpo: itens de template (Counter, Weather) removidos da navegacao; titulo da sidebar alterado para "Sistema Academico" para caber na area visivel.
45. Link "About" removido do `MainLayout.razor` (top-row eliminada).
46. `Home.razor` reescrita como pagina inicial do projeto: cards de acesso rapido para Cursos, Alunos e Matriculas, descricao da arquitetura em camadas e referencia ao Fluxo Academico didatico.
47. Criado `IO/ConsoleIO.cs` com helper estatico de entrada e saida formatada para a ConsoleApp (cabecalho, sucesso/erro com cor, linha separadora, pausa, leitura tipada e confirmacao).
48. Criado `Menus/MenuCursos.cs` com CRUD completo de cursos: cadastrar, listar (tabela formatada), consultar por Id, atualizar (valor atual exibido, branco mantem) e excluir (confirmacao obrigatoria).
49. Criado `Menus/MenuAlunos.cs` com CRUD completo de alunos: cadastrar com secoes "Dados do Aluno" e "Perfil Academico", listar, consultar, atualizar e excluir com confirmacao.
50. Criado `Menus/MenuMatriculas.cs` com gerenciamento de matriculas: realizar (lista cursos e alunos disponiveis), listar (tabela com data e status), consultar por Id, cancelar/concluir (exibe apenas ativas), excluir com confirmacao.
51. Criado `Menus/MenuRelatorios.cs` com relatorios: alunos por curso (seleciona curso e lista matriculas), cursos de um aluno (seleciona aluno e lista matriculas), total de alunos por curso (tabela resumida), matriculas por status (ativas, canceladas, concluidas).
52. Criado `Menus/MenuPrincipal.cs` como dispatcher raiz: instancia os quatro submenus no construtor e oferece as opcoes Cursos, Alunos, Matriculas, Relatorios e Sair.
53. `Program.cs` da ConsoleApp reescrito para apenas instanciar store, services e `MenuPrincipal`, eliminando o fluxo didatico hardcoded da versao anterior.
54. `Cursos.razor` evoluida com editar inline (card amarelo pre-preenchido, substituindo o card de cadastro) e excluir com confirmacao na linha da tabela (Sim/Nao).
55. `Alunos.razor` evoluida com editar inline (mesma estrutura de duas colunas do cadastro) e excluir com confirmacao na linha da tabela.
56. `Matriculas.razor` evoluida com excluir matricula com confirmacao na linha; cancelar e concluir continuam disponiveis apenas para status Ativa.
57. Configurados MCPs do projeto em `.claude/settings.json`: `context7` e `microsoft-learn`.
58. Revisão técnica completa executada sobre todo o código-fonte — resultado em `Docs/00-acompanhamento/05_revisao-tecnica-2026-05-23.md`.
59. README atualizado com seção de status da última validação.
60. Ajustado o lifetime do `InMemoryAcademicStore` na BlazorApp para `scoped`, alinhando o estado em memória ao circuito do usuário em Blazor Interactive Server.
61. Adicionadas validações de unicidade também na criação:
   - `CourseService.CreateCourse(...)` bloqueia sigla duplicada.
   - `StudentService.CreateStudent(...)` bloqueia e-mail duplicado.
   - `StudentService.CreateStudent(...)` bloqueia registro acadêmico duplicado.
62. Documentada explicitamente a decisão didática de manter as exclusões simples em memória nesta etapa, mesmo sem sincronização histórica total nas coleções internas das entidades.

## O que foi validado

1. SDK .NET disponível: 10.0.103.
2. Solução compila sem erros.
3. Chave Context7 pode ser carregada via `.env` sem exposição no repositório.
4. Build completo da solution executado com sucesso após alterações do domínio.
5. Projeto Domain validado sem referências a Application, ConsoleApp ou BlazorApp.
6. Build da solution permanece verde após reorganização física dos arquivos.
7. Build completo da solution executado com sucesso apos implementacao da Session 4.
8. Projeto Application validado com referencia apenas para Domain (`dotnet list ... reference`).
9. Build da solution continua verde apos integracao da Application na ConsoleApp.
10. Execucao da ConsoleApp validada com sucesso no fluxo didatico ponta a ponta.
11. Build completo da solution validado com sucesso apos integracao da BlazorApp com a camada Application.
12. Build completo da solution validado com sucesso apos evolucao das operacoes de status de matricula na BlazorApp.
13. Checklist didatico da etapa 03 atualizado com validacao explicita dos cenarios de teste manual.
14. Pendencia de documentacao de dominio resolvida com trilha dedicada em `Docs/02-dominio-e-poo`.
15. Build completo da solution validado com sucesso apos criacao das paginas Cursos, Alunos e Matriculas na BlazorApp (0 erros, 0 warnings).
16. Build completo da solution validado com sucesso apos criacao da estrutura de menus da ConsoleApp (0 erros, 0 warnings).
17. Build completo da solution validado com sucesso apos adicionar editar e excluir nas paginas Blazor (0 erros, 0 warnings).
18. Revisão técnica completa em 2026-05-23: 0 erros, 0 avisos, 3 observações de baixa severidade documentadas em `05_revisao-tecnica-2026-05-23.md`.
19. Build completo da solution validado com sucesso apos ajuste do lifetime do store e validacoes de unicidade na criacao (0 erros, 0 warnings).

## Pendências

1. Revisar e consolidar pendencias do bloco `03-application-e-persistencia-em-memoria` com exemplos adicionais se necessario.
2. Avaliar necessidade de testes automatizados para os fluxos de Application e UI.
3. Planejar inicio do bloco de testes automatizados sem quebrar foco didatico atual.
4. Corrigir `lang="en"` para `lang="pt-BR"` no `App.razor` (baixa prioridade).
5. Evoluir `PerfilAcademico` para Value Object imutável em sessão futura (conceito avançado).
6. Revisitar a estratégia de exclusão em memória caso a próxima etapa exija consistência histórica mais forte entre store e agregados.

## Próxima ação recomendada

Iniciar proxima etapa consolidando testes e qualidade sobre os fluxos ja implementados.
