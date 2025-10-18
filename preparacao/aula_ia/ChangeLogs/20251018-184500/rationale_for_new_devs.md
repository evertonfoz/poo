## Introdução

Este documento agora reúne o histórico do pedido e as ações tomadas. A estrutura a seguir facilita a leitura por novos desenvolvedores: os primeiros três capítulos reproduzem o conteúdo original dos arquivos de registro (`prompt.txt`, `tasks.txt`, `progress.txt`) para referência rápida; em seguida vem uma transição e o conteúdo explicativo já existente (razões, benefícios e orientações). Ao final há um encerramento curto.

## Capítulo 1 — Prompt (resumo do pedido)

Criar arquivos de configuração (`.editorconfig`, `Directory.Build.props`, `global.json`) e `AGENTS.md`; manter arquivos no diretório atual em vez da raiz do repositório; validar uso de MCP/Context7; confirmar top-level do repositório Git.

**Requisitos**

- Antes de criar, verificar se já existia algo similar.
- Criar os arquivos apenas se ausentes.
- Não sobrescrever `.gitignore` existente.
- Registrar as ações e decisões em `AGENTS.md` e `ChangeLogs`.

## Capítulo 2 — Tarefas executadas

Tarefas executadas com base no prompt:

1. Procurar por `AGENTS.md` e referências a MCP/Context7 no workspace.
2. Criar `AGENTS.md` se ausente e registrar instruções e status.
3. Confirmar top-level do repositório Git e presença de `.git`.
4. Verificar existência dos arquivos de configuração na raiz (`.editorconfig`, `Directory.Build.props`, `global.json`, `.gitignore`).
5. Criar `.editorconfig`, `Directory.Build.props` e `global.json` na pasta de trabalho atual quando ausentes.
6. Não sobrescrever `.gitignore` existente.
7. Criar `ChangeLogs` com registros detalhados do prompt, tarefas, progresso e rationale.

## Capítulo 3 — Progresso da implementação

- Procuras iniciais por `AGENTS.md` e referências a MCP/Context7: concluídas (nenhuma referência encontrada).
- `AGENTS.md` criado em `/preparacao/aula_ia/AGENTS.md` e atualizado com confirmação do git root.
- Verificação do top-level do repositório: confirmado como `/Users/evertoncoimbradearaujo/Documents/GitHub/poo` (contém `.git`).
- Arquivos de configuração criados em `/preparacao/aula_ia`: `.editorconfig`, `Directory.Build.props`, `global.json`.
- Arquivos mantidos no diretório atual conforme solicitado.
- `ChangeLogs` criados com este conjunto de arquivos.

## Transição para a explicação e motivação

Acima reunimos o "o que foi pedido", "o que foi feito" e o status atual — agora explicamos por que cada peça foi importante, quais benefícios trazem e como você, novo dev, pode usá-las no dia a dia.

## Por que fizemos isso (explicação para novos desenvolvedores)

### O que foi pedido

Fomos instruídos a melhorar a qualidade do projeto adicionando alguns arquivos de configuração importantes (como `.editorconfig`, `Directory.Build.props` e `global.json`) e também a criar um documento `AGENTS.md` que registre orientações para o agente que trabalha no repositório. Foi pedido que estes arquivos fossem criados somente se não existissem e que o `.gitignore` existente não fosse sobrescrito. Também foi pedido que mantivéssemos os arquivos criados no diretório `preparacao/aula_ia` em vez da raiz.

### Por que isso importa

Quando várias pessoas trabalham no mesmo código, diferenças pequenas de formatação (espaços, quebras de linha, posição dos `using`), configurações de compilador e versões do SDK podem gerar inconsistências, erros inesperados ou builds que funcionam no computador de um desenvolvedor mas falham no de outro.

### O que fizemos e por quê

- `.editorconfig`: define regras de estilo (indentação, uso de `var`, ordenação de `using`, etc.). Assim, editores e IDEs aplicam um estilo consistente automaticamente.
- `Directory.Build.props`: centraliza propriedades do MSBuild, como ativar `Nullable` e tratar warnings como erros em projetos importantes (Domain e Tests). Isso melhora a qualidade ao forçar a correção de warnings e habilitar verificações de nulidade que evitam erros em tempo de execução.
- `global.json`: fixa a versão do SDK .NET usada para construir o projeto. Garante reproducibilidade entre as máquinas dos desenvolvedores e nos servidores de CI.
- `AGENTS.md` e `ChangeLogs`: documentam as decisões tomadas e como o agente (ou futuros desenvolvedores) deve operar no repositório.

### Benefícios práticos (não técnico)

- Menos discussões sobre estilo em code reviews. Código mais homogêneo e fácil de ler.
- Menos bugs inesperados relacionados a nulls e advertências não resolvidas porque warnings críticos viram erros de build onde importa.
- Builds mais previsíveis entre máquinas por conta do `global.json`.
- Documentação viva (`AGENTS.md` + `ChangeLogs`) que ajuda qualquer novo membro a entender o que foi feito e por quê.

### Como usar isso no dia a dia

- Abra o repositório na IDE. As regras do `.editorconfig` serão aplicadas automaticamente (ou via plugin).
- Ao gravar código, siga as dicas do editor sobre estilo e corrija os warnings (em projetos marcados, warnings quebram o build).
- Se precisar mudar a versão do SDK, atualize `global.json` e coordene com a equipe.
- Leia o `AGENTS.md` e a pasta `ChangeLogs` quando estiver começando — eles contêm o histórico das decisões importantes.

### Observação final

Essas mudanças são pequenas e de baixo risco, mas de alto impacto para a manutenção do projeto. Se a equipe preferir aplicar as mesmas regras a todo o repositório, basta mover estes arquivos para o root (`/Users/evertoncoimbradearaujo/Documents/GitHub/poo`).

## Encerramento

Se ficou alguma dúvida sobre o que foi alterado ou quiser que a gente aplique essas mesmas regras ao repositório inteiro, me diga e eu movo os arquivos, ajusto as condições em `Directory.Build.props` ou atualizo o `.gitignore` conforme o padrão .NET.
----------------
Essas mudanças são pequenas e de baixo risco, mas de alto impacto para a manutenção do projeto. Se a equipe preferir aplicar as mesmas regras a todo o repositório, basta mover estes arquivos para o root (`/Users/evertoncoimbradearaujo/Documents/GitHub/poo`).

Encerramento

Se ficou alguma dúvida sobre o que foi alterado ou quiser que a gente aplique essas mesmas regras ao repositório inteiro, me diga e eu movo os arquivos, ajusto as condições em `Directory.Build.props` ou atualizo o `.gitignore` conforme o padrão .NET.