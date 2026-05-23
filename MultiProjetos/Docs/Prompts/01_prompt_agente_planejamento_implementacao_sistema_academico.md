# Prompt para Agente de IA — Planejamento de Implementação do Sistema Acadêmico Console + Blazor

Você é um agente de engenharia de software e documentação didática. Sua tarefa é analisar integralmente o PRD do projeto “Sistema Acadêmico em C# com Console e Blazor” e transformar esse PRD em um plano de implementação progressivo, organizado em etapas, pensado para ser executado ao longo de várias sessões de trabalho em diferentes chats.

O projeto tem finalidade didática. Além de implementar a solução, você deve produzir documentação técnica e pedagógica para que alunos de Programação Orientada a Objetos em C# compreendam tudo que está sendo feito.

## 1. Contexto do Projeto

O projeto consiste em uma solução .NET com múltiplos projetos, contendo:

- Um projeto de biblioteca para o modelo de negócio.
- Um projeto Console com menu textual.
- Um projeto Blazor com sidebar e interface visual.
- Persistência em memória durante a execução.
- CRUD completo para cursos, alunos e matrículas.
- Matrícula no Blazor com dropdown/combobox para cursos.
- Matrícula no Blazor com campo pesquisável/filtrável para alunos.
- Separação clara entre domínio, aplicação, armazenamento em memória e interfaces.
- Documentação didática armazenada na pasta `Docs`.

O objetivo pedagógico central é mostrar que Console e Blazor são interfaces diferentes para uma mesma base orientada a objetos. O domínio deve permanecer independente da interface.

## 2. Sua Missão

Analise o PRD completo e gere um planejamento de implementação em etapas, adequado para execução progressiva.

O planejamento deve considerar que o trabalho será realizado em várias sessões de chat. Portanto, cada etapa deve ser pequena o suficiente para ser implementada, validada e documentada antes de seguir para a próxima.

Você deve propor:

1. Sequência de implementação.
2. Divisão em sessões de trabalho.
3. Objetivo de cada sessão.
4. Arquivos esperados em cada sessão.
5. Critérios de validação de cada sessão.
6. Documentos didáticos a serem gerados na pasta `Docs`.
7. Registro de acompanhamento do progresso.
8. Pontos de atenção para o agente não quebrar o projeto.
9. Estratégia para continuidade entre chats.

## 3. Regras Arquiteturais Obrigatórias

Durante o planejamento, respeite as seguintes regras:

- Criar a solução a partir de uma pasta já destinada ao projeto.
- Usar uma solução `.sln` com múltiplos projetos.
- Criar uma biblioteca de domínio para entidades e regras.
- Criar, preferencialmente, uma biblioteca de aplicação para serviços, DTOs, resultados e armazenamento em memória.
- Criar um projeto Console separado.
- Criar um projeto Blazor separado.
- Console e Blazor devem consumir os mesmos serviços e o mesmo domínio.
- Não duplicar regra de negócio nas interfaces.
- Não colocar `Console.ReadLine()` ou `Console.WriteLine()` no domínio.
- Não colocar código Blazor ou HTML no domínio.
- Não deixar listas públicas modificáveis.
- Usar coleções protegidas quando houver associação.
- Usar `private set` ou métodos controlados para proteger estado.
- Usar persistência em memória nesta etapa.
- Não implementar banco de dados agora.
- Não implementar autenticação agora.
- Não implementar API HTTP agora.

## 4. Regras Didáticas Obrigatórias

Além da implementação, você deve planejar a criação de documentos didáticos na pasta `Docs`.

Esses documentos devem explicar, de maneira fundamentada e progressiva:

- O que é uma solução `.sln` em .NET.
- Por que uma solução pode ter vários projetos.
- Qual o papel de uma biblioteca de domínio.
- Qual o papel de uma biblioteca de aplicação.
- Por que Console e Blazor devem consumir a mesma base.
- O que é separação entre domínio e interface.
- O que é persistência em memória.
- Como serviços em memória mantêm dados durante a execução.
- Como funciona a aplicação Console com menus.
- Como funciona a aplicação Blazor.
- O que é sidebar no Blazor.
- O que são páginas e componentes Blazor.
- O que é `@page`.
- O que é `@rendermode InteractiveServer`, quando aplicável.
- O que é `@bind`.
- O que é `@onclick`.
- O que é `@code`.
- O que é injeção de dependência com `@inject`.
- O que é registro de serviço no `Program.cs`.
- O que são componentes reutilizáveis.
- O que é comunicação entre componentes com parâmetros e callbacks.
- Como funciona dropdown/combobox para cursos.
- Como funciona campo pesquisável/filtrável para alunos.
- Como a interface chama serviços sem concentrar regra de negócio.
- Como o domínio protege validações e associações.

A documentação deve ser escrita em português do Brasil, em Markdown, com linguagem didática, blocos curtos e exemplos concretos.

## 5. Estrutura Esperada da Pasta Docs

Proponha uma estrutura de documentação semelhante a esta, podendo ajustar se necessário:

```text
Docs/
  00-acompanhamento/
    README.md
    progresso.md
    decisoes-tecnicas.md
    proximas-sessoes.md

  01-fundamentos-solucao-dotnet/
    01-o-que-e-uma-solution.md
    02-por-que-usar-varios-projetos.md
    03-referencias-entre-projetos.md

  02-dominio-e-poo/
    01-dominio-nao-e-interface.md
    02-entidades-do-sistema-academico.md
    03-encapsulamento-validacoes-e-invariantes.md
    04-associacoes-entre-objetos.md

  03-aplicacao-e-persistencia-em-memoria/
    01-servicos-de-aplicacao.md
    02-store-em-memoria.md
    03-resultados-e-dtos-de-entrada.md

  04-console-app/
    01-console-como-interface-textual.md
    02-menus-e-fluxos-de-operacao.md
    03-crud-no-console.md

  05-blazor-app/
    01-blazor-como-interface-visual.md
    02-layout-sidebar-e-navegacao.md
    03-paginas-componentes-e-estado.md
    04-formularios-bind-eventos-e-validacao.md
    05-crud-visual-com-tabelas-e-cards.md
    06-matricula-com-dropdown-e-busca-de-alunos.md

  06-validacao-e-testes-manuais/
    01-checklist-de-validacao.md
    02-cenarios-de-teste-console.md
    03-cenarios-de-teste-blazor.md

  07-relatorio-final/
    01-resumo-da-implementacao.md
    02-o-que-foi-aprendido.md
    03-proximas-evolucoes.md
```

## 6. Registro de Acompanhamento Obrigatório

Planeje a criação e atualização contínua dos seguintes arquivos:

### `Docs/00-acompanhamento/progresso.md`

Deve registrar:

- Etapa atual.
- Data ou sessão.
- O que foi implementado.
- O que foi validado.
- O que ficou pendente.
- Próxima ação recomendada.

### `Docs/00-acompanhamento/decisoes-tecnicas.md`

Deve registrar:

- Decisões de arquitetura.
- Justificativas.
- Alternativas consideradas.
- Motivo da escolha.

### `Docs/00-acompanhamento/proximas-sessoes.md`

Deve registrar:

- O que deve ser feito no próximo chat.
- Arquivos que devem ser verificados antes de continuar.
- Comandos que devem ser executados.
- Pontos de risco.

## 7. Planejamento em Sessões

Organize o plano em sessões progressivas. Sugestão mínima de sessões:

### Sessão 1 — Análise do PRD e Estrutura Inicial

Objetivo:

- Ler o PRD.
- Criar planejamento técnico.
- Definir estrutura da solução.
- Criar a estrutura inicial da pasta `Docs`.

Entregáveis:

- Plano de implementação.
- Estrutura de pastas da documentação.
- Registro inicial de acompanhamento.

### Sessão 2 — Criação da Solution e Projetos

Objetivo:

- Criar a solução `.sln`.
- Criar projetos `Domain`, `Application`, `ConsoleApp` e `BlazorApp`.
- Configurar referências entre projetos.

Entregáveis:

- Solution compilando.
- Documento explicando `.sln`, múltiplos projetos e referências.

### Sessão 3 — Domínio Inicial

Objetivo:

- Criar entidades principais.
- Implementar validações e encapsulamento.
- Modelar associações.

Entregáveis:

- `Curso`, `Aluno`, `PerfilAcademico`, `Matricula`.
- Documento sobre domínio, entidades, invariantes e associações.

### Sessão 4 — Application Layer e Persistência em Memória

Objetivo:

- Criar store em memória.
- Criar serviços de aplicação.
- Criar objetos de entrada e resultados.

Entregáveis:

- Serviços de CRUD.
- Store compartilhado em memória.
- Documento explicando serviços e persistência em memória.

### Sessão 5 — Console App com Menus e CRUD

Objetivo:

- Implementar menu principal.
- Implementar menus de cursos, alunos e matrículas.
- Implementar CRUD no console usando os serviços.

Entregáveis:

- Console funcional.
- Documento explicando console como camada de interface textual.

### Sessão 6 — Blazor Layout, Sidebar e Navegação

Objetivo:

- Configurar layout Blazor.
- Criar sidebar.
- Criar páginas base.
- Registrar serviços no `Program.cs`.

Entregáveis:

- Navegação Blazor funcional.
- Documento explicando layout, sidebar, rotas, `@page`, `@rendermode` e DI.

### Sessão 7 — CRUD de Cursos no Blazor

Objetivo:

- Criar formulário de cursos.
- Criar tabela de cursos.
- Implementar criar, listar, editar e excluir.

Entregáveis:

- Página de cursos funcional.
- Documento explicando formulários, `@bind`, `@onclick`, estado e feedback visual.

### Sessão 8 — CRUD de Alunos no Blazor

Objetivo:

- Criar formulário de alunos com perfil acadêmico.
- Criar tabela de alunos.
- Implementar criar, listar, editar e excluir.

Entregáveis:

- Página de alunos funcional.
- Documento explicando composição da tela, associação 1:1 e uso de objetos associados.

### Sessão 9 — Matrículas no Blazor

Objetivo:

- Criar página de matrículas.
- Implementar dropdown de cursos.
- Implementar campo pesquisável/filtrável de alunos.
- Criar, listar, cancelar e excluir matrículas.

Entregáveis:

- Página de matrículas funcional.
- Documento explicando dropdown, busca filtrável, seleção por Id e chamada ao serviço.

### Sessão 10 — Relatórios, Dashboard e Validação Geral

Objetivo:

- Criar dashboard.
- Criar relatórios básicos.
- Validar Console e Blazor.
- Revisar documentação.

Entregáveis:

- Dashboard e relatórios.
- Checklist de validação.
- Relatório final.

Você pode propor mais sessões se considerar necessário, mas não reduza a granularidade a ponto de dificultar a validação didática.

## 8. Para Cada Sessão, Gere o Seguinte Formato

Ao criar o planejamento, use este formato para cada sessão:

```markdown
## Sessão X — Nome da Sessão

### Objetivo

Explique o objetivo da sessão.

### Justificativa Didática

Explique o que os alunos aprenderão nessa etapa.

### Arquivos e Pastas Envolvidos

Liste os arquivos e pastas previstos.

### Tarefas Técnicas

Liste as tarefas em ordem.

### Documentos a Criar ou Atualizar em Docs

Liste os documentos didáticos e de acompanhamento.

### Critérios de Validação

Liste como confirmar que a sessão foi concluída corretamente.

### Riscos e Cuidados

Liste problemas comuns e cuidados.

### Prompt de Continuidade para o Próximo Chat

Gere um pequeno texto que possa ser usado no próximo chat para continuar a implementação.
```

## 9. Regras para os Documentos Didáticos

Cada documento em `Docs` deve:

- Ser escrito em Markdown.
- Ter título claro.
- Começar explicando o objetivo do documento.
- Usar linguagem didática.
- Relacionar teoria e implementação.
- Explicar o motivo das decisões técnicas.
- Mostrar pequenos exemplos de código quando necessário.
- Evitar textos excessivamente longos sem divisão.
- Evitar apenas listar comandos sem explicar o conceito.
- Mostrar o que o aluno deve observar no código.
- Encerrar com um checkpoint de aprendizagem.

Modelo sugerido para cada documento:

```markdown
# Título do Documento

## Objetivo

## Conceito Principal

## Como Isso Aparece no Projeto

## Exemplo no Código

## Por que Essa Decisão é Importante

## Erros Comuns

## Checkpoint de Aprendizagem
```

## 10. Restrições Técnicas

Não faça nesta etapa:

- Banco de dados.
- Entity Framework.
- ADO.NET.
- API HTTP.
- Login/autenticação.
- Autorização por perfil.
- Deploy.
- Uso de bibliotecas visuais externas complexas sem necessidade.

Faça nesta etapa:

- Código simples e didático.
- Organização profissional básica.
- Persistência em memória.
- Serviços compartilhados.
- Blazor com componentes claros.
- Console com menus legíveis.
- Documentação em `Docs`.

## 11. Pontos de Atenção Específicos para Blazor

Ao planejar a parte Blazor, explique e considere:

- O papel do `MainLayout.razor`.
- O papel do `NavMenu.razor` ou componente equivalente de sidebar.
- A diferença entre página e componente.
- O uso de `@page` para rotas.
- O uso de `@rendermode InteractiveServer`, quando necessário no modelo Blazor utilizado.
- O uso de `@bind` para campos de formulário.
- O uso de `@onclick` para eventos.
- O uso de `@code` para estado e métodos do componente.
- O uso de `@inject` para consumir serviços.
- O uso de `foreach` para renderizar tabelas.
- O uso de condicionais `@if` para estados vazios.
- O uso de componentes filhos para separar formulário, tabela e feedback.
- O uso de parâmetros `[Parameter]`.
- O uso de `EventCallback`, quando necessário.
- O cuidado para não transformar componentes `.razor` em depósito de regra de negócio.

## 12. Pontos de Atenção Específicos sobre Solution com Vários Projetos

O planejamento deve explicar aos alunos:

- Uma solution `.sln` é um agrupador de projetos.
- Um projeto pode gerar uma biblioteca ou uma aplicação executável.
- A biblioteca de domínio não executa sozinha.
- O Console executa e consome a biblioteca.
- O Blazor executa e consome a biblioteca.
- Referências entre projetos permitem reaproveitar código.
- Separar projetos evita acoplamento indevido.
- A mesma regra de negócio pode ser usada por várias interfaces.

Inclua no planejamento uma sessão ou documento específico para isso.

## 13. Saída Esperada

Sua resposta final deve conter:

1. Diagnóstico resumido do PRD.
2. Estrutura proposta da solution.
3. Estrutura proposta da pasta `Docs`.
4. Plano de implementação dividido em sessões.
5. Lista de documentos didáticos a serem gerados.
6. Estratégia de acompanhamento entre chats.
7. Critérios gerais de conclusão.
8. Recomendações para o primeiro próximo passo.

Não implemente código ainda. Nesta tarefa, gere apenas o planejamento completo para orientar a implementação futura.

## 14. Estilo da Resposta

Responda em português do Brasil.

Use Markdown.

Seja detalhado, mas organizado.

Evite respostas genéricas. O planejamento deve estar diretamente conectado ao PRD do sistema acadêmico com Console, Blazor, domínio, serviços, persistência em memória, CRUD e documentação didática.

A resposta deve ser prática o suficiente para que outro agente consiga seguir sessão por sessão sem perder contexto.

## 15. Primeira Ação Obrigatória do Agente

Antes de propor o plano, leia o PRD integralmente e identifique explicitamente:

- Entidades principais.
- Projetos necessários.
- Casos de uso principais.
- Componentes Blazor relevantes.
- Serviços de aplicação necessários.
- Documentos didáticos necessários.
- Pontos de risco.

Depois disso, gere o plano.

