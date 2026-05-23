# Início de Atividades — Sistema Acadêmico (Console + Blazor)

## Objetivo desta sessão

Preparar o ambiente, validar o contexto técnico e iniciar a implementação da solution seguindo o PRD.

## Checklist de arranque

1. Validar ambiente .NET
- Executar `dotnet --version`.
- Executar `dotnet --list-sdks`.
- Confirmar SDK estável recente disponível.

2. Validar documentos-base
- Ler `Docs/PRD/prd_sistema_academico_console_blazor_poo.md`.
- Ler `Docs/MCP/tutorial_context_7_mcp_vscode_copilot_csharp.md`.
- Ler `Docs/Prompts/01_prompt_agente_planejamento_implementacao_sistema_academico.md`.

3. Confirmar segurança de configuração MCP
- Garantir que `.vscode/mcp.json` não contém chave real de API.
- Usar variável de entrada ou variável de ambiente para `CONTEXT7_API_KEY`.

4. Criar estrutura inicial da solution
- Criar `CadastroAcademico.sln`.
- Criar pasta `src/`.
- Criar projetos:
	- `CadastroAcademico.Domain`
	- `CadastroAcademico.Application`
	- `CadastroAcademico.ConsoleApp`
	- `CadastroAcademico.BlazorApp`

5. Configurar referências entre projetos
- `Application` referencia `Domain`.
- `ConsoleApp` referencia `Application`.
- `BlazorApp` referencia `Application`.

6. Validar compilação inicial
- Executar `dotnet build` na raiz da solution.
- Corrigir quaisquer erros de referência/SDK antes de avançar.

7. Inicializar trilha de acompanhamento em Docs
- Criar/atualizar `Docs/00-acompanhamento/progresso.md`.
- Criar/atualizar `Docs/00-acompanhamento/decisoes-tecnicas.md`.
- Criar/atualizar `Docs/00-acompanhamento/proximas-sessoes.md`.

## Regras de implementação da etapa

- Não implementar banco de dados.
- Não implementar autenticação/autorização.
- Manter persistência em memória.
- Preservar separação de responsabilidades (Domain, Application, Console, Blazor).

## Critério de conclusão desta sessão

- Solution criada e compilando.
- Referências entre projetos corretas.
- Acompanhamento inicial em `Docs/00-acompanhamento/` atualizado.
- Próxima sessão preparada para iniciar entidades de domínio.

## Prompt de continuidade para a próxima sessão

"Com a solution criada e compilando, implemente a Sessão 3: entidades de domínio (`Curso`, `Aluno`, `PerfilAcademico`, `Matricula`) com validações, encapsulamento e associações, atualizando também a documentação didática em `Docs/02-dominio-e-poo/` e o progresso em `Docs/00-acompanhamento/`."