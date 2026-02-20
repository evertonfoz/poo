AGENTS.md

Objetivo
--------
Registrar orientações e instruções para o agente (Copilot) que trabalha neste repositório. Servirá como fonte única para políticas locais, convenções, pontos de contato e como usar integrações (por exemplo, MCP / Context7) neste projeto.

Status atual
------------
- Procurei por `AGENTS.md` e por referências a "mcp", "context7", "Context7" e "MCP" no workspace atual — não foram encontrados resultados.
- Não foi possível ler diretórios acima do workspace (restrição de ambiente), então não pude confirmar diretamente se o repositório Git top-level ou a pasta `.git` está localizada acima da pasta atual. Baseado na estrutura fornecida, parece que o repositório raiz é `.../poo` e que a pasta atual é `.../poo/preparacao/aula_ia`.

Como usar este arquivo
----------------------
- Atualize este arquivo sempre que inserir instruções específicas para o agente (por exemplo: rotinas de atualização, comandos de build, onde encontrar especificações do produto, contatos humanos).
- Documente decisões sobre usar MCP/context7 aqui: quais servidores Context7 usar, como autenticar, e exemplos de chamadas de `mcp_context7_*` se aplicável.

MCP / Context7
---------------
- Resultado da verificação: não há referências a MCP/Context7 nos arquivos do workspace atual.
- Se você quiser usar a integração Context7 (mcp_context7_* tools), recomenda-se:
  1. Definir um ponto de verdade da biblioteca (por exemplo: usar o `mcp_context7_resolve-library-id` antes de `mcp_context7_get-library-docs`).
  2. Registrar no arquivo as credenciais/variáveis de ambiente necessárias (não inclua segredos em texto plano — referencie variáveis de ambiente ou cofre).
  3. Instruções de exemplo para o agente (ex.: "Quando pesquisar docs de biblioteca X, chame: mcp_context7_resolve-library-id 'X' e então mcp_context7_get-library-docs com o ID retornado").

Onde está o repositório Git
---------------------------
- Observação: a ferramenta não permitiu ler pastas acima do workspace. Localmente, confirme executando no terminal na pasta deste projeto:

```zsh
# mostra o caminho para o diretório git root
git rev-parse --show-toplevel
# mostra se existe um .git
ls -la $(git rev-parse --show-toplevel) | grep .git || true
```

- Pelo layout fornecido, provavelmente o repositório raiz é `.../poo` (duas pastas acima de `aula_ia`). Confirme localmente e atualize esta seção com o caminho real.
 - Pelo layout fornecido, o repositório raiz foi confirmado como `/Users/evertoncoimbradearaujo/Documents/GitHub/poo`. Esse diretório contém a pasta `.git`.

Próximos passos recomendados
---------------------------
- Se desejar que eu habilite chamadas para `mcp_context7_*` automatizadas, informe qual biblioteca/documentação devemos priorizar e confirme onde armazenar credenciais (variáveis de ambiente ou segredos do CI).
- Se quiser que eu procure também em níveis superiores (fora do workspace), autorize a leitura desses caminhos ou informe o caminho exato para inspeção.

Registro de alterações
----------------------
- 2025-10-18: Arquivo criado — verificação inicial de MCP/Context7: nenhum uso encontrado no workspace.

Prática de ChangeLogs por chat
-----------------------------
Para manter histórico claro das ações realizadas via chat (agent/assistant), registre um ChangeLog para cada sessão/chat onde alteramos o repositório. Procedimento mínimo (preferência por um único arquivo MD):

1. Criar pasta `ChangeLogs/YYYYMMDD-HHMMSS/` em `preparacao/aula_ia` (ou no root se quiser aplicação global).
2. Incluir um único arquivo principal:
  - `rationale_for_new_devs.md` — deve conter seções claras: "prompt", "tasks", "progress" e "rationale". Evite criar múltiplos `.txt` separados.
3. Se durante a sessão arquivos `.txt` forem criados temporariamente, consolide seu conteúdo em `rationale_for_new_devs.md` e remova os `.txt` antes do commit.
4. Commitar e referenciar esse ChangeLog em um `CHANGELOG.md` no root (ou documento equivalente) para navegação rápida.

Motivação: reduzir dispersão de arquivos e manter um único documento legível e pesquisável por sessão; facilita revisão e onboarding.

Convenção de nomes (recomendada)
--------------------------------
- Nome da pasta: `YYYYMMDD-HHMMSS-<objetivo-breve>` — por exemplo: `20251018-184700-enrollment-tests-empty-specs`.
- Nome do arquivo principal: `<descricao-curta>.md` — por exemplo: `enrollment-tests-rationale.md` (não inclua data/hora no nome do arquivo).

Motivo: incluir o objetivo no nome da pasta ajuda a localizar rapidamente a sessão; manter o arquivo com um nome descritivo sem timestamp facilita buscas e evita duplicação ao consolidar registros.
