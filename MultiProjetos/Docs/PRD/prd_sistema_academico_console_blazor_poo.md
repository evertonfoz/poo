# PRD — Sistema Acadêmico em C# com Console e Blazor

## 1. Visão Geral

Este documento descreve os requisitos para construção de uma solução em C#/.NET voltada à prática de Programação Orientada a Objetos, com duas aplicações consumidoras do mesmo modelo de negócio: uma aplicação Console e uma aplicação Blazor.

A solução deverá demonstrar, de forma progressiva e profissional, que o console e o Blazor são camadas de apresentação distintas, enquanto as regras, entidades, validações, associações e operações principais devem permanecer no domínio e nos serviços de aplicação.

O sistema terá como foco um mini sistema acadêmico, permitindo o cadastro e manutenção de cursos, alunos, perfis acadêmicos e matrículas. A persistência será feita em memória nesta etapa, mantendo os dados acessíveis durante toda a execução da aplicação.

## 2. Objetivo do Produto

Criar uma solução .NET organizada em múltiplos projetos, capaz de permitir o gerenciamento acadêmico básico por duas interfaces diferentes:

- Aplicação Console, com menu textual e operações CRUD.
- Aplicação Blazor, com sidebar de navegação, formulários, tabelas, feedback visual e boa experiência de uso.

Ambas as aplicações devem utilizar o mesmo modelo de negócio e a mesma camada de serviços, evitando duplicação de regras e reforçando a separação entre domínio, aplicação e interface.

## 3. Público-Alvo

O produto será usado em contexto didático por estudantes de Programação Orientada a Objetos em C#, que já tiveram contato com console, classes, objetos, encapsulamento, construtores, validações, listas, associações e primeiros conceitos de Blazor.

Também servirá como base para demonstrar a transição de um sistema de console para uma interface visual, preservando o domínio e substituindo apenas a camada de interação.

## 4. Problema a Resolver

Em exercícios iniciais de C#, é comum misturar entrada de dados, saída de dados, regras de negócio e armazenamento em listas diretamente no `Program.cs` ou em componentes `.razor`.

Essa abordagem dificulta a evolução do sistema, pois:

- O console passa a parecer a própria aplicação.
- O componente Blazor pode concentrar regras que deveriam estar no domínio.
- Listas ficam expostas ou manipuladas diretamente pela interface.
- Regras de validação ficam duplicadas ou espalhadas.
- O mesmo caso de uso precisa ser refeito para console e web.

A solução proposta resolve isso por meio de uma arquitetura simples, porém bem separada, com domínio, serviços de aplicação, repositório em memória e duas interfaces consumidoras.

## 5. Escopo da Solução

A solução deverá conter três projetos principais:

```text
CadastroAcademico/
  CadastroAcademico.sln

  src/
    CadastroAcademico.Domain/
      Entidades, regras de negócio, associações e contratos principais

    CadastroAcademico.ConsoleApp/
      Aplicação console com menu textual

    CadastroAcademico.BlazorApp/
      Aplicação Blazor com sidebar e interface visual
```

Opcionalmente, se for necessário manter melhor separação, poderá ser criado um quarto projeto:

```text
CadastroAcademico.Application/
  Serviços de aplicação, DTOs de entrada/saída e repositórios em memória
```

Caso o projeto adicional não seja criado, os serviços de aplicação e repositórios em memória poderão ficar temporariamente dentro da biblioteca de domínio, desde que separados em pastas específicas. No entanto, a recomendação é separar `Domain` e `Application` para uma estrutura mais profissional.

## 6. Escopo Funcional

O sistema deverá permitir CRUD completo para:

1. Cursos.
2. Alunos.
3. Perfis acadêmicos vinculados aos alunos.
4. Matrículas de alunos em cursos.

A matrícula será tratada como uma operação de associação entre um aluno já cadastrado e um curso já cadastrado.

## 7. Entidades do Domínio

### 7.1 Curso

Representa um curso acadêmico.

Campos mínimos:

- `Id`: inteiro gerado automaticamente em memória.
- `Nome`: obrigatório.
- `Sigla`: obrigatória e única.
- `CargaHoraria`: obrigatória e maior que zero.
- Coleção de matrículas ou alunos matriculados.

Regras:

- Não permitir curso sem nome.
- Não permitir curso sem sigla.
- Não permitir carga horária menor ou igual a zero.
- Não permitir sigla duplicada.
- Não permitir matrícula duplicada do mesmo aluno no mesmo curso.
- A coleção interna de alunos/matrículas deve ser protegida.

Padrão esperado:

```csharp
private readonly List<Matricula> _matriculas = new();
public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();
```

### 7.2 Aluno

Representa um aluno do sistema.

Campos mínimos:

- `Id`: inteiro gerado automaticamente em memória.
- `Nome`: obrigatório.
- `Email`: obrigatório e único.
- `PerfilAcademico`: associação 1:1 obrigatória.

Regras:

- Não permitir aluno sem nome.
- Não permitir aluno sem e-mail.
- Não permitir e-mail duplicado.
- Não permitir aluno sem perfil acadêmico.
- O perfil acadêmico deve ser validado como objeto do domínio, não como campos soltos.

### 7.3 PerfilAcademico

Representa os dados acadêmicos associados ao aluno.

Campos mínimos:

- `RegistroAcademico`: obrigatório e único.
- `Periodo`: obrigatório.

Regras:

- Não permitir registro acadêmico vazio.
- Não permitir período vazio.
- Não permitir registro acadêmico duplicado entre alunos.

### 7.4 Matricula

Representa o vínculo entre aluno e curso.

Campos mínimos:

- `Id`: inteiro gerado automaticamente em memória.
- `Aluno`: obrigatório.
- `Curso`: obrigatório.
- `DataMatricula`: data gerada no momento da matrícula.
- `Status`: ativa, cancelada ou concluída.

Regras:

- Não permitir matrícula sem aluno.
- Não permitir matrícula sem curso.
- Não permitir o mesmo aluno matriculado duas vezes no mesmo curso com matrícula ativa.
- Permitir cancelamento da matrícula.
- Permitir consulta das matrículas por curso e por aluno.

## 8. Arquitetura Esperada

## 8.1 Camada de Domínio

A biblioteca de domínio deverá conter as entidades e regras principais.

Responsabilidades:

- Representar conceitos centrais do sistema.
- Proteger invariantes.
- Validar construtores e métodos de domínio.
- Encapsular coleções internas.
- Expor coleções como `IReadOnlyCollection<T>`.
- Evitar dependência de console, Blazor, banco de dados ou qualquer interface.

A camada de domínio não deve conter:

- `Console.ReadLine()`.
- `Console.WriteLine()`.
- Componentes `.razor`.
- Código HTML.
- Mensagens específicas de tela quando não forem mensagens de regra.
- Dependência direta de banco de dados.

## 8.2 Camada de Aplicação

A camada de aplicação deverá coordenar os casos de uso.

Responsabilidades:

- Receber dados de entrada vindos da interface.
- Chamar entidades e métodos de domínio.
- Manipular repositórios em memória.
- Retornar resultados estruturados.
- Evitar que Console e Blazor reimplementem regras.

Serviços sugeridos:

- `CourseService`
- `StudentService`
- `EnrollmentService`

Objetos auxiliares sugeridos:

- `OperationResult`
- `CreateCourseInput`
- `UpdateCourseInput`
- `CreateStudentInput`
- `UpdateStudentInput`
- `CreateEnrollmentInput`

## 8.3 Persistência em Memória

A persistência será feita em memória.

Requisitos:

- Os dados devem permanecer acessíveis durante toda a execução da aplicação.
- Não é necessário banco de dados nesta etapa.
- O armazenamento deve ser compartilhado pelos serviços dentro da mesma aplicação.
- No Blazor, os serviços de memória deverão ser registrados com ciclo de vida adequado para preservar os dados enquanto a aplicação estiver em execução.

Registro recomendado no Blazor:

```csharp
builder.Services.AddSingleton<InMemoryAcademicStore>();
builder.Services.AddSingleton<CourseService>();
builder.Services.AddSingleton<StudentService>();
builder.Services.AddSingleton<EnrollmentService>();
```

No Console, os mesmos serviços devem ser instanciados uma vez no início da execução e reutilizados durante todo o menu.

## 9. Requisitos da Aplicação Console

## 9.1 Objetivo

Fornecer uma interface textual para executar todos os cadastros e operações do sistema.

## 9.2 Menu Principal

A aplicação Console deverá apresentar um menu principal com as seguintes opções:

```text
Sistema Acadêmico

1. Gerenciar cursos
2. Gerenciar alunos
3. Gerenciar matrículas
4. Relatórios e consultas
0. Sair
```

## 9.3 Menu de Cursos

Operações obrigatórias:

```text
Cursos

1. Cadastrar curso
2. Listar cursos
3. Consultar curso por Id
4. Atualizar curso
5. Excluir curso
0. Voltar
```

## 9.4 Menu de Alunos

Operações obrigatórias:

```text
Alunos

1. Cadastrar aluno
2. Listar alunos
3. Consultar aluno por Id
4. Atualizar aluno
5. Excluir aluno
0. Voltar
```

O cadastro de aluno deverá solicitar também os dados do perfil acadêmico.

## 9.5 Menu de Matrículas

Operações obrigatórias:

```text
Matrículas

1. Realizar matrícula
2. Listar matrículas
3. Consultar matrícula por Id
4. Cancelar matrícula
5. Excluir matrícula
0. Voltar
```

Na matrícula, o console deverá listar os cursos e alunos disponíveis antes de solicitar os Ids.

## 9.6 Relatórios e Consultas

Relatórios mínimos:

- Listar alunos por curso.
- Listar cursos de um aluno.
- Exibir total de alunos por curso.
- Exibir matrículas ativas.
- Exibir matrículas canceladas.

## 9.7 Tratamento de Erros no Console

A aplicação deverá:

- Validar opções inválidas do menu.
- Tratar entradas numéricas inválidas.
- Exibir mensagens claras de erro.
- Não encerrar a aplicação por exceções de domínio.
- Retornar ao menu após cada operação.

## 10. Requisitos da Aplicação Blazor

## 10.1 Objetivo

Fornecer uma interface visual agradável, organizada e funcional para executar os mesmos CRUDs disponíveis no console.

## 10.2 Estrutura Visual

A aplicação Blazor deverá possuir:

- Layout com sidebar lateral.
- Menu de navegação para cursos, alunos, matrículas e relatórios.
- Área principal de conteúdo.
- Cabeçalho com título da página atual.
- Feedback visual de sucesso e erro.
- Tabelas ou cards para listagem.
- Botões de ação claros.
- Formulários organizados em seções.

## 10.3 Sidebar

A sidebar deverá conter os itens:

```text
Sistema Acadêmico

Dashboard
Cursos
Alunos
Matrículas
Relatórios
```

Cada item deve navegar para sua respectiva página.

## 10.4 Dashboard

O dashboard deverá exibir, no mínimo:

- Total de cursos cadastrados.
- Total de alunos cadastrados.
- Total de matrículas ativas.
- Total de matrículas canceladas.
- Lista resumida dos últimos registros criados.

## 10.5 Página de Cursos

Funcionalidades obrigatórias:

- Criar curso.
- Listar cursos.
- Editar curso.
- Excluir curso.
- Visualizar detalhes do curso.
- Exibir total de alunos matriculados no curso.

Campos do formulário:

- Nome.
- Sigla.
- Carga horária.

Requisitos de UI/UX:

- Formulário em card.
- Tabela responsiva.
- Botões “Editar” e “Excluir” por linha.
- Confirmação antes de excluir.
- Mensagem de erro em caso de sigla duplicada ou dados inválidos.

## 10.6 Página de Alunos

Funcionalidades obrigatórias:

- Criar aluno com perfil acadêmico.
- Listar alunos.
- Editar aluno e perfil acadêmico.
- Excluir aluno.
- Visualizar detalhes do aluno.

Campos do formulário:

Dados do aluno:

- Nome.
- E-mail.

Dados do perfil acadêmico:

- Registro acadêmico.
- Período.

Requisitos de UI/UX:

- Separar visualmente “Dados do Aluno” e “Perfil Acadêmico”.
- Exibir feedback de validação.
- Destacar e-mail e registro acadêmico como dados únicos.
- Exibir lista em tabela com colunas: Nome, E-mail, Registro Acadêmico, Período e Ações.

## 10.7 Página de Matrículas

Funcionalidades obrigatórias:

- Criar matrícula.
- Listar matrículas.
- Consultar matrícula.
- Cancelar matrícula.
- Excluir matrícula.

Campos da matrícula:

- Curso.
- Aluno.

Requisitos específicos:

- O curso deverá ser selecionado por dropdown/combobox.
- O aluno deverá ser selecionado por um campo pesquisável.
- À medida que o usuário digitar o nome do aluno, a lista de alunos deve ser filtrada.
- Ao selecionar o aluno, o sistema deve manter o vínculo com o objeto real, não apenas com o texto digitado.
- O sistema deve impedir matrícula duplicada do mesmo aluno no mesmo curso.

Comportamento esperado do campo de aluno:

1. Usuário começa a digitar parte do nome.
2. A interface filtra os alunos compatíveis.
3. Usuário seleciona um aluno da lista.
4. O formulário guarda o `Id` do aluno selecionado.
5. Ao confirmar, o serviço realiza a matrícula.

## 10.8 Página de Relatórios

Relatórios mínimos:

- Alunos por curso.
- Cursos por aluno.
- Matrículas ativas.
- Matrículas canceladas.
- Quantidade de alunos por curso.

Requisitos de UI/UX:

- Usar cards, tabelas ou seções expansíveis.
- Permitir seleção de curso para visualizar seus alunos.
- Permitir seleção de aluno para visualizar suas matrículas.

## 11. Requisitos de CRUD

## 11.1 CRUD de Curso

Criar:

- Validar nome obrigatório.
- Validar sigla obrigatória.
- Validar carga horária maior que zero.
- Validar sigla única.

Listar:

- Exibir todos os cursos cadastrados.
- Exibir quantidade de alunos matriculados.

Consultar:

- Buscar por Id.

Atualizar:

- Permitir alteração de nome, sigla e carga horária.
- Validar as mesmas regras da criação.
- Não permitir sigla duplicada.

Excluir:

- Não permitir exclusão de curso com matrículas ativas, salvo se a regra escolhida for remover/cancelar previamente as matrículas.
- Exibir mensagem clara em caso de bloqueio.

## 11.2 CRUD de Aluno

Criar:

- Validar nome obrigatório.
- Validar e-mail obrigatório.
- Validar e-mail único.
- Validar registro acadêmico obrigatório.
- Validar registro acadêmico único.
- Validar período obrigatório.

Listar:

- Exibir todos os alunos cadastrados.

Consultar:

- Buscar por Id.

Atualizar:

- Permitir alteração de nome, e-mail, registro acadêmico e período.
- Validar duplicidade de e-mail e registro acadêmico.

Excluir:

- Não permitir exclusão de aluno com matrícula ativa, salvo se a regra escolhida for cancelar/remover matrículas previamente.
- Exibir mensagem clara em caso de bloqueio.

## 11.3 CRUD de Matrícula

Criar:

- Selecionar curso existente.
- Selecionar aluno existente.
- Validar duplicidade.
- Gerar data da matrícula.
- Definir status inicial como ativa.

Listar:

- Exibir curso, aluno, data e status.

Consultar:

- Buscar matrícula por Id.

Atualizar:

- Permitir alteração controlada de status.
- Não permitir troca livre de aluno/curso sem regra clara.

Excluir:

- Remover matrícula da memória.
- Solicitar confirmação na interface Blazor.

Cancelar:

- Alterar status para cancelada.
- Manter histórico em memória durante a execução.

## 12. Requisitos Não Funcionais

## 12.1 Organização do Código

O projeto deve seguir separação clara de responsabilidades:

- Domínio: entidades e regras.
- Aplicação: casos de uso, serviços, DTOs e resultados.
- Console: menus e interação textual.
- Blazor: componentes, páginas e interação visual.

## 12.2 Encapsulamento

As entidades devem evitar propriedades públicas com `set` livre.

Preferir:

```csharp
public string Nome { get; private set; }
```

Evitar:

```csharp
public string Nome { get; set; }
```

Coleções internas devem ser privadas e expostas como somente leitura.

## 12.3 Reutilização

Console e Blazor devem consumir os mesmos serviços de aplicação e entidades de domínio.

Nenhuma regra de negócio deve existir apenas no console ou apenas no Blazor.

## 12.4 Usabilidade no Blazor

A interface Blazor deverá:

- Ser limpa e legível.
- Usar espaçamento adequado.
- Exibir feedback visual após operações.
- Ter botões com rótulos claros.
- Evitar telas sobrecarregadas.
- Usar tabelas organizadas.
- Ter navegação por sidebar.
- Facilitar a seleção de curso e aluno na matrícula.

## 12.5 Manutenibilidade

A aplicação deve permitir evolução futura para banco de dados sem reescrever o domínio.

A persistência em memória deve estar isolada em um store ou repositório para facilitar troca futura por ADO.NET, Entity Framework ou outro mecanismo.

## 13. Componentes Blazor Sugeridos

Estrutura sugerida:

```text
CadastroAcademico.BlazorApp/
  Components/
    Layout/
      MainLayout.razor
      NavMenu.razor

    Pages/
      Dashboard.razor
      CoursesPage.razor
      StudentsPage.razor
      EnrollmentsPage.razor
      ReportsPage.razor

    Academic/
      CourseForm.razor
      CourseTable.razor
      StudentForm.razor
      StudentTable.razor
      EnrollmentForm.razor
      EnrollmentTable.razor
      SearchableStudentSelect.razor
      FeedbackMessage.razor
      StatCard.razor
```

## 14. Serviços Sugeridos

```text
CadastroAcademico.Application/
  Services/
    CourseService.cs
    StudentService.cs
    EnrollmentService.cs
    AcademicReportService.cs

  Stores/
    InMemoryAcademicStore.cs

  Inputs/
    CreateCourseInput.cs
    UpdateCourseInput.cs
    CreateStudentInput.cs
    UpdateStudentInput.cs
    CreateEnrollmentInput.cs

  Results/
    OperationResult.cs
    OperationResultOfT.cs
```

## 15. Fluxo de Matrícula no Blazor

Fluxo esperado:

1. Usuário acessa “Matrículas” pela sidebar.
2. Sistema carrega cursos no dropdown.
3. Sistema exibe campo pesquisável de aluno.
4. Usuário seleciona um curso.
5. Usuário digita parte do nome do aluno.
6. Sistema filtra alunos por nome.
7. Usuário seleciona o aluno correto.
8. Usuário clica em “Matricular”.
9. Blazor envia os Ids para o `EnrollmentService`.
10. O serviço busca curso e aluno na memória.
11. O domínio valida duplicidade e consistência.
12. A matrícula é criada.
13. A interface exibe mensagem de sucesso.
14. A tabela de matrículas é atualizada.

## 16. Critérios de Aceitação

## 16.1 Solução e Projetos

- A solução `.sln` deve ser criada corretamente.
- A biblioteca de domínio deve existir.
- A aplicação Console deve existir.
- A aplicação Blazor deve existir.
- Console e Blazor devem referenciar a biblioteca de domínio e/ou aplicação.
- O projeto deve compilar sem erros.

## 16.2 Domínio

- Entidades devem possuir validações no construtor e/ou métodos de domínio.
- Coleções devem ser protegidas.
- Associações devem ser modeladas corretamente.
- Regras principais não devem estar apenas na interface.

## 16.3 Persistência em Memória

- Dados cadastrados devem permanecer disponíveis enquanto a aplicação estiver rodando.
- Não deve haver uso de banco de dados nesta etapa.
- Serviços devem compartilhar o mesmo armazenamento em memória dentro da execução.

## 16.4 Console

- Deve possuir menu principal.
- Deve possuir menus específicos de cursos, alunos e matrículas.
- Deve implementar CRUD completo.
- Deve tratar entradas inválidas.
- Deve exibir mensagens de erro e sucesso.

## 16.5 Blazor

- Deve possuir sidebar funcional.
- Deve possuir páginas para cursos, alunos, matrículas e relatórios.
- Deve implementar CRUD completo.
- Deve ter interface visual organizada.
- Deve usar dropdown para cursos na matrícula.
- Deve usar campo pesquisável/filtrável para alunos na matrícula.
- Deve exibir feedback visual de sucesso e erro.

## 17. Fora do Escopo Nesta Etapa

Não fazem parte desta entrega:

- Banco de dados.
- Login/autenticação.
- Controle de permissões.
- API HTTP.
- Entity Framework.
- ADO.NET.
- Testes automatizados obrigatórios.
- Deploy em servidor.

Esses pontos poderão ser tratados em uma etapa posterior.

## 18. Riscos e Cuidados

## 18.1 Risco: Regras ficarem no Blazor

Mitigação:

- Manter validações essenciais nas entidades e serviços.
- Usar Blazor apenas para coleta e exibição.

## 18.2 Risco: Console e Blazor duplicarem lógica

Mitigação:

- Ambos devem consumir os mesmos serviços.
- Evitar implementar regras diretamente nos menus ou componentes.

## 18.3 Risco: Perder dados durante a navegação Blazor

Mitigação:

- Registrar store e serviços como `Singleton` nesta etapa.
- Evitar guardar listas apenas dentro de componentes de página.

## 18.4 Risco: Campo de aluno pesquisável guardar apenas texto

Mitigação:

- O componente deve armazenar o `Id` do aluno selecionado.
- O texto digitado deve ser usado apenas para filtro.

## 19. Ordem Recomendada de Implementação

1. Criar solução `.sln`.
2. Criar projeto de biblioteca de domínio.
3. Criar entidades: `Curso`, `Aluno`, `PerfilAcademico`, `Matricula`.
4. Criar projeto de aplicação, se adotado.
5. Criar store em memória.
6. Criar serviços: curso, aluno, matrícula e relatórios.
7. Criar aplicação Console.
8. Implementar menus e CRUD no Console.
9. Criar aplicação Blazor.
10. Configurar sidebar e layout.
11. Implementar página de cursos.
12. Implementar página de alunos.
13. Implementar página de matrículas com dropdown e busca de aluno.
14. Implementar relatórios.
15. Testar cenários válidos e inválidos nas duas interfaces.
16. Ajustar README com instruções de execução.

## 20. Comandos Base Esperados

Exemplo de criação da solução:

```bash
dotnet new sln -n CadastroAcademico
mkdir src
cd src

dotnet new classlib -n CadastroAcademico.Domain
dotnet new console -n CadastroAcademico.ConsoleApp
dotnet new blazor -n CadastroAcademico.BlazorApp

cd ..
dotnet sln add src/CadastroAcademico.Domain/CadastroAcademico.Domain.csproj
dotnet sln add src/CadastroAcademico.ConsoleApp/CadastroAcademico.ConsoleApp.csproj
dotnet sln add src/CadastroAcademico.BlazorApp/CadastroAcademico.BlazorApp.csproj

dotnet add src/CadastroAcademico.ConsoleApp/CadastroAcademico.ConsoleApp.csproj reference src/CadastroAcademico.Domain/CadastroAcademico.Domain.csproj
dotnet add src/CadastroAcademico.BlazorApp/CadastroAcademico.BlazorApp.csproj reference src/CadastroAcademico.Domain/CadastroAcademico.Domain.csproj
```

Se houver projeto `Application`:

```bash
dotnet new classlib -n CadastroAcademico.Application

dotnet sln add src/CadastroAcademico.Application/CadastroAcademico.Application.csproj

dotnet add src/CadastroAcademico.Application/CadastroAcademico.Application.csproj reference src/CadastroAcademico.Domain/CadastroAcademico.Domain.csproj

dotnet add src/CadastroAcademico.ConsoleApp/CadastroAcademico.ConsoleApp.csproj reference src/CadastroAcademico.Application/CadastroAcademico.Application.csproj

dotnet add src/CadastroAcademico.BlazorApp/CadastroAcademico.BlazorApp.csproj reference src/CadastroAcademico.Application/CadastroAcademico.Application.csproj
```

## 21. Definição de Pronto

A entrega será considerada pronta quando:

- A solução compilar.
- O Console executar e permitir CRUD completo.
- O Blazor executar e permitir CRUD completo.
- Os dados permanecerem em memória durante a execução.
- A matrícula no Blazor usar dropdown para curso.
- A matrícula no Blazor usar busca filtrável para aluno.
- O domínio estiver separado das interfaces.
- As coleções estiverem protegidas.
- As regras principais estiverem nas entidades e serviços.
- O README explicar como executar a solução e cada projeto.

## 22. Resumo Executivo

A solução deverá demonstrar que uma mesma base orientada a objetos pode sustentar diferentes interfaces. O console será mantido como interface textual com menus, enquanto o Blazor fornecerá uma experiência visual mais próxima de aplicações reais. A persistência em memória permitirá foco nos conceitos de domínio, serviços, CRUD, associações e organização arquitetural sem antecipar banco de dados.

O ponto central da entrega é preservar a modelagem: interface coleta e exibe dados; serviços coordenam operações; domínio protege regras e associações.

