# Tutorial — Usando Context7 e MCP no VS Code Copilot para Trabalhar com C# Atualizado

## Objetivo

Este tutorial orienta o uso de MCP e Context7 no VS Code com GitHub Copilot para apoiar o desenvolvimento em C# usando documentação atualizada.

A proposta é ajudar o aluno a entender por que uma IA pode sugerir código desatualizado, como o MCP resolve parte desse problema e como o Context7 pode trazer documentação recente para dentro do fluxo de trabalho do Copilot.

Este material deve ser usado antes de iniciar a implementação do projeto com múltiplos projetos .NET, Console, Blazor, domínio, serviços e persistência em memória.

## 1. O problema: a IA pode conhecer versões antigas

Ferramentas de IA para programação são úteis para acelerar a escrita de código, explicar conceitos e sugerir soluções. Porém, elas podem errar quando o assunto depende de versões recentes de linguagens, frameworks, bibliotecas ou templates.

No caso deste projeto, isso é especialmente importante porque trabalharemos com:

- C# recente.
- .NET recente.
- Blazor recente.
- Templates atuais de projeto.
- Diretivas como `@page`, `@rendermode`, `@bind`, `@onclick`, `@inject` e componentes `.razor`.
- Estrutura de solution com múltiplos projetos.

Um agente sem documentação atualizada pode sugerir:

- Template antigo de Blazor.
- Organização antiga de pastas.
- Comandos de criação de projeto desatualizados.
- Uso inadequado de `Program.cs`.
- Código que compila em versões antigas, mas não no SDK atual.
- Recursos de C# incompatíveis com o `TargetFramework` configurado.

Por isso, antes de pedir implementação, devemos orientar o Copilot a consultar documentação atualizada.

## 2. C# mais recente: cuidado com “latest” e “preview”

Quando se fala em “C# mais recente”, é importante diferenciar duas ideias:

### Versão estável mais recente

É a versão atual oficialmente lançada e suportada pelo SDK estável do .NET.

Para projetos didáticos, essa deve ser a preferência, pois reduz risco de incompatibilidade nas máquinas dos alunos.

### Versão preview

É uma versão em desenvolvimento, usada para testar recursos futuros da linguagem.

Evite usar `preview` em projetos de aula, salvo quando o objetivo explícito for estudar recursos experimentais.

## 3. Recomendação para este projeto

Para este projeto, o agente deve priorizar:

```xml
<TargetFramework>net10.0</TargetFramework>
<LangVersion>latest</LangVersion>
<Nullable>enable</Nullable>
<ImplicitUsings>enable</ImplicitUsings>
```

Essa configuração indica que o projeto deve usar o .NET atual estável e a versão mais recente estável da linguagem compatível com o compilador instalado.

Evite, neste momento:

```xml
<LangVersion>preview</LangVersion>
```

A opção `preview` só deve ser usada quando houver decisão explícita registrada em `Docs/00-acompanhamento/decisoes-tecnicas.md`.

## 4. O que é MCP

MCP significa Model Context Protocol.

De forma simples, MCP é um padrão que permite conectar uma aplicação de IA, como o GitHub Copilot em modo agente, a ferramentas externas, fontes de documentação, repositórios, sistemas, bancos de dados, APIs ou outros servidores de contexto.

Em vez de a IA depender apenas do conhecimento já aprendido no treinamento, ela pode consultar ferramentas conectadas por MCP.

Pense no MCP como uma ponte padronizada entre o agente de IA e fontes externas de informação.

## 5. O que é um MCP Server

Um MCP Server é um servidor que oferece capacidades ao agente de IA.

Essas capacidades podem incluir:

- Buscar documentação atualizada.
- Consultar exemplos de código.
- Ler informações de repositórios.
- Acessar sistemas externos.
- Executar ferramentas específicas.
- Fornecer recursos e prompts especializados.

No nosso caso, o uso principal será buscar documentação atualizada de C#, .NET e Blazor para reduzir sugestões desatualizadas.

## 6. O que é Context7

Context7 é um servidor MCP voltado a fornecer documentação atualizada e exemplos de código para bibliotecas, frameworks e tecnologias usadas em desenvolvimento.

A utilidade do Context7 neste projeto é orientar o agente a consultar documentação recente antes de sugerir código para:

- C#.
- .NET.
- Blazor.
- ASP.NET Core.
- Templates `dotnet new`.
- Recursos de linguagem.
- APIs e padrões atuais.

Em vez de pedir ao Copilot apenas:

```text
Crie um projeto Blazor com CRUD de alunos.
```

O aluno deve pedir algo mais controlado:

```text
Use Context7 para consultar a documentação atual de .NET, C# e Blazor antes de sugerir a implementação. Depois, gere o plano técnico seguindo o PRD do projeto.
```

## 7. Por que usar Context7 neste projeto

Este projeto envolve muitos pontos sujeitos a mudanças de versão:

- O modelo atual de projeto Blazor.
- A estrutura de `Components`, `Pages`, `Layout` e `Program.cs`.
- O uso de renderização interativa.
- A configuração de serviços no `Program.cs`.
- O uso de `@rendermode InteractiveServer` quando necessário.
- A forma recomendada de criar projetos com `dotnet new`.
- A versão do C# associada ao SDK instalado.

O Context7 ajuda o agente a fundamentar as decisões técnicas em documentação atualizada, em vez de usar apenas memória interna.

## 8. Instalação e configuração no VS Code Copilot

A configuração exata pode variar conforme a versão do VS Code, do GitHub Copilot e das políticas da organização. O fluxo geral é o seguinte:

1. Instalar ou atualizar o VS Code.
2. Instalar ou atualizar a extensão GitHub Copilot.
3. Instalar ou atualizar a extensão GitHub Copilot Chat.
4. Entrar com a conta GitHub no VS Code.
5. Verificar se o modo Agent está disponível no Copilot Chat.
6. Configurar MCP Servers no VS Code.
7. Adicionar o Context7 como servidor MCP.
8. Testar se o Copilot consegue listar e usar as ferramentas do servidor.

## 9. Arquivo de configuração MCP no projeto

Uma forma prática de orientar o uso em equipe é criar um arquivo de configuração no repositório:

```text
.vscode/mcp.json
```

Esse arquivo pode documentar os servidores MCP recomendados para o projeto.

Exemplo conceitual:

```json
{
  "servers": {
    "context7": {
      "type": "http",
      "url": "https://mcp.context7.com/mcp",
      "headers": {
        "CONTEXT7_API_KEY": "${input:context7-api-key}"
      }
    }
  }
}
```

Observação importante: nunca grave chaves reais de API no repositório.

Use variáveis, secrets, inputs do VS Code ou configuração local privada.

## 10. Alternativa: usar MCP Server da Microsoft Learn

Além do Context7, pode ser útil configurar o MCP Server da Microsoft Learn, pois ele fornece acesso a documentação oficial da Microsoft.

Isso é especialmente relevante para:

- C#.
- .NET.
- ASP.NET Core.
- Blazor.
- Comandos do .NET CLI.
- Configuração de projetos.

Exemplo conceitual:

```json
{
  "servers": {
    "microsoft-learn": {
      "type": "http",
      "url": "https://learn.microsoft.com/api/mcp"
    }
  }
}
```

Para o nosso projeto, uma boa estratégia é usar:

- Context7 para documentação prática de bibliotecas e exemplos.
- Microsoft Learn MCP para documentação oficial do ecossistema Microsoft.

## 11. Ativando o uso no Copilot Chat

Depois de configurar os servidores MCP:

1. Abra o GitHub Copilot Chat no VS Code.
2. Selecione o modo `Agent`.
3. Abra a lista de ferramentas disponíveis.
4. Verifique se os servidores MCP aparecem na lista.
5. Habilite as ferramentas relacionadas ao Context7 e, se configurado, Microsoft Learn.
6. Faça uma pergunta de teste.

Prompt de teste:

```text
Use as ferramentas MCP disponíveis para verificar qual é a orientação atual para criar uma aplicação Blazor no .NET mais recente. Depois, explique quais comandos dotnet new devo usar e quais arquivos principais serão gerados.
```

## 12. Como o aluno deve pedir ajuda ao Copilot

Ao usar o Copilot, o aluno não deve pedir apenas código pronto. Deve pedir análise, consulta de documentação e justificativa.

Prompt recomendado:

```text
Antes de responder, use Context7 e os MCP Servers disponíveis para consultar documentação atualizada de C#, .NET e Blazor. Estou implementando uma solution .NET com vários projetos: Domain, Application, ConsoleApp e BlazorApp. Quero usar a versão estável mais recente do C# compatível com o SDK instalado. Gere a resposta explicando o motivo técnico das decisões e evite usar APIs ou templates desatualizados.
```

## 13. Prompt para verificar a versão do SDK e linguagem

Use este prompt no Copilot Agent:

```text
Verifique no workspace qual versão do .NET SDK está sendo usada, quais TargetFrameworks aparecem nos arquivos .csproj e qual versão de C# será assumida pelo compilador. Consulte a documentação atual via MCP antes de sugerir mudanças. Depois, proponha ajustes seguros para usar a versão estável mais recente do C# compatível com o projeto.
```

Comandos que o agente pode sugerir ou executar:

```bash
dotnet --version
dotnet --list-sdks
dotnet --info
```

## 14. Prompt para criar a solution usando documentação atual

```text
Use Context7 e MCP para validar os comandos atuais do .NET CLI. Depois, gere um plano para criar uma solution chamada CadastroAcademico com os projetos CadastroAcademico.Domain, CadastroAcademico.Application, CadastroAcademico.ConsoleApp e CadastroAcademico.BlazorApp. Explique o papel de cada projeto e registre a decisão em Docs/00-acompanhamento/decisoes-tecnicas.md.
```

## 15. Prompt para implementar Blazor com cuidado

```text
Antes de alterar o projeto Blazor, consulte a documentação atual de Blazor via MCP. Verifique o modelo do projeto, a estrutura de Components, Layout, Pages e Program.cs. Depois, implemente a próxima etapa respeitando o PRD: sidebar, páginas, componentes, formulários, @bind, @onclick, @inject e serviços registrados por DI. Explique cada decisão em Docs/05-blazor-app/.
```

## 16. Prompt para evitar código desatualizado

```text
Analise o código gerado e verifique se existe algum padrão desatualizado para a versão atual do .NET e do Blazor. Use MCP/Context7 para comparar com a documentação atual. Aponte o que precisa ser corrigido antes de implementar novas funcionalidades.
```

## 17. Prompt para documentação didática em Docs

```text
Depois de implementar a etapa atual, crie ou atualize os documentos Markdown na pasta Docs explicando o que foi feito. A explicação deve ser didática, em português do Brasil, com foco em alunos de Programação Orientada a Objetos em C#. Explique os conceitos antes do código, principalmente quando envolver solution com vários projetos, Blazor, componentes, serviços, injeção de dependência e persistência em memória.
```

## 18. Boas práticas de segurança ao usar MCP

MCP aumenta a capacidade do agente, mas também exige cuidado.

Adote estas práticas:

- Use apenas MCP Servers confiáveis.
- Prefira documentação oficial quando possível.
- Não exponha chaves de API no repositório.
- Não permita que o agente execute comandos destrutivos sem revisão.
- Revise alterações em arquivos `.csproj`, `Program.cs` e configurações.
- Peça que o agente explique por que precisa de cada ferramenta.
- Confirme comandos como remoção de arquivos, limpeza de diretórios ou alteração de configuração global.

## 19. O que registrar em Docs

Crie um documento específico:

```text
Docs/01-fundamentos-solucao-dotnet/04-uso-de-mcp-context7-e-copilot.md
```

Esse documento deve explicar:

- O problema de documentação desatualizada.
- O que é MCP.
- O que é Context7.
- Como isso ajuda no projeto.
- Como usar no VS Code Copilot.
- Como pedir ao agente para consultar documentação atual.
- Quais cuidados de segurança devem ser adotados.

Também atualize:

```text
Docs/00-acompanhamento/decisoes-tecnicas.md
```

Com uma decisão semelhante:

```markdown
## Uso de MCP e Context7 para apoio à implementação

Decidimos orientar o uso de MCP Servers, especialmente Context7 e Microsoft Learn MCP, para reduzir o risco de geração de código desatualizado em C#, .NET e Blazor. O agente deve consultar documentação atual antes de propor comandos, templates, configurações de projeto e padrões Blazor.

A decisão não substitui a revisão humana. Toda alteração sugerida pelo agente deve ser validada por compilação, leitura crítica e testes manuais.
```

## 20. Checklist para o aluno

Antes de pedir implementação ao Copilot, confira:

- VS Code atualizado.
- GitHub Copilot instalado.
- GitHub Copilot Chat instalado.
- Conta GitHub autenticada no VS Code.
- Modo Agent disponível.
- MCP habilitado no ambiente.
- Context7 configurado.
- Microsoft Learn MCP configurado, se possível.
- `.NET SDK` instalado.
- `dotnet --version` funcionando.
- PRD disponível no workspace.
- Pasta `Docs` criada ou planejada.

## 21. Checkpoint de aprendizagem

Ao final deste tutorial, o aluno deve conseguir responder:

1. Por que uma IA pode sugerir código C# ou Blazor desatualizado?
2. O que é MCP?
3. O que é um MCP Server?
4. Para que serve o Context7?
5. Por que usar documentação atualizada antes de implementar em Blazor?
6. Qual a diferença entre versão estável mais recente e versão preview do C#?
7. Por que não devemos gravar chaves de API no repositório?
8. Como pedir ao Copilot para consultar documentação antes de gerar código?
9. Por que a revisão humana continua necessária?

## 22. Prompt final recomendado para iniciar o projeto

Use este prompt no VS Code Copilot Agent antes da primeira implementação:

```text
Você atuará como agente de implementação e documentação didática deste projeto. Antes de sugerir código, use Context7 e os MCP Servers disponíveis para consultar documentação atualizada de C#, .NET e Blazor.

O projeto deve usar a versão estável mais recente do C# compatível com o SDK instalado, preferencialmente com TargetFramework atual estável, Nullable habilitado e ImplicitUsings habilitado. Não use recursos preview sem registrar decisão técnica.

Analise o PRD do Sistema Acadêmico Console + Blazor. Gere primeiro um plano de implementação em sessões. Depois, implemente etapa por etapa, sempre atualizando a pasta Docs com explicações didáticas para alunos.

A solução deverá ter múltiplos projetos: Domain, Application, ConsoleApp e BlazorApp. Console e Blazor devem consumir o mesmo domínio e os mesmos serviços. A persistência será em memória. Não implemente banco de dados, autenticação ou API HTTP nesta etapa.

Ao final de cada etapa, atualize:
- Docs/00-acompanhamento/progresso.md
- Docs/00-acompanhamento/decisoes-tecnicas.md, quando houver decisão relevante
- Docs/00-acompanhamento/proximas-sessoes.md

Explique sempre os conceitos envolvidos, especialmente solution com vários projetos, Blazor, componentes, sidebar, @bind, @onclick, @inject, DI, serviços e persistência em memória.
```

