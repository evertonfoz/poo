Leia todos os arquivos no workspace e me indique o que você identifica que deve fazer por favor.

---

Vou mapear todos os arquivos do workspace, ler cada um e em seguida te devolver um diagnóstico objetivo do que precisa ser feito.

Vou primeiro coletar a lista completa de arquivos para garantir que nada fique de fora; em seguida leio um por um e consolido as ações recomendadas.

Agora vou ler o conteúdo integral dos 5 arquivos encontrados para te retornar um plano de ação claro e priorizado.

Li todos os arquivos do workspace e o diagnóstico é este:

1. O repositório está em fase de planejamento/documentação, sem implementação da solução .NET ainda.
2. O PRD está muito bem detalhado e já define claramente escopo, arquitetura, fluxo por sessões e critérios de aceite.
3. O maior ponto crítico imediato é segurança de configuração MCP: existe chave de API em texto puro em mcp.json.
4. Existe um prompt de planejamento completo pronto para execução em 01_prompt_agente_planejamento_implementacao_sistema_academico.md.
5. O arquivo 02_inicio_de_atividades.md está vazio e deveria virar o ponto de partida operacional da próxima sessão.

O que você deve fazer agora, em ordem:

1. Corrigir segurança do MCP
- Remover a chave real de mcp.json e usar variável de ambiente/input local.
- Isso evita vazamento de credencial no Git.

2. Executar a Sessão 1 do plano
- Usar o prompt de 01_prompt_agente_planejamento_implementacao_sistema_academico.md para gerar o plano por sessões e criar a base de acompanhamento em Docs.

3. Preencher o início operacional
- Escrever em 02_inicio_de_atividades.md um checklist curto de arranque:
- validar SDK .NET
- confirmar estrutura de pastas
- criar solution e projetos
- registrar progresso e decisões

4. Iniciar implementação técnica da solution
- Seguir o PRD em prd_sistema_academico_console_blazor_poo.md:
- criar projetos Domain, Application, ConsoleApp, BlazorApp
- referenciar corretamente
- persistência em memória
- CRUD progressivo
- Blazor com sidebar, dropdown de curso e busca de aluno

5. Usar MCP/Context7 como padrão durante implementação
- Seguir orientações de tutorial_context_7_mcp_vscode_copilot_csharp.md para evitar código desatualizado de .NET/Blazor/C#.

Se quiser, já começo agora pelo passo 1 e 2: sanitizo o mcp.json e crio o arquivo inicial de atividades com checklist pronto para execução.

---

Sim, por favor, pode seguir