# Setup configs — Rationale e ações

## Introdução

Este documento reúne o histórico do pedido e as ações tomadas. A estrutura a seguir facilita a leitura por novos desenvolvedores: os primeiros capítulos reproduzem o conteúdo original dos arquivos de registro para referência rápida; em seguida há a explicação e motivação técnica e operacional.

## Prompt (resumo do pedido)

Criar arquivos de configuração (`.editorconfig`, `Directory.Build.props`, `global.json`) e `AGENTS.md`; manter arquivos no diretório atual em vez da raiz do repositório; validar uso de MCP/Context7; confirmar top-level do repositório Git.

### Requisitos importantes
- Antes de criar, verificar se já existia algo similar.
- Criar os arquivos apenas se ausentes.
- Não sobrescrever `.gitignore` existente.
- Registrar as ações e decisões em `AGENTS.md` e `ChangeLogs`.

## Tarefas executadas

1. Procurar por `AGENTS.md` e referências a MCP/Context7 no workspace.
2. Criar `AGENTS.md` se ausente e registrar instruções e status.
3. Confirmar top-level do repositório Git e presença de `.git`.
4. Verificar existência dos arquivos de configuração na raiz (`.editorconfig`, `Directory.Build.props`, `global.json`, `.gitignore`).
5. Criar `.editorconfig`, `Directory.Build.props` e `global.json` na pasta de trabalho atual quando ausentes.
6. Não sobrescrever `.gitignore` existente.
7. Criar `ChangeLogs` com registros detalhados do prompt, tarefas, progresso e rationale.

## Progresso e resultado
- `AGENTS.md` criado em `/preparacao/aula_ia/AGENTS.md` e atualizado com confirmação do git root.
- Verificação do top-level do repositório: confirmado como `/Users/evertoncoimbradearaujo/Documents/GitHub/poo` (contém `.git`).
- Arquivos de configuração criados em `/preparacao/aula_ia`: `.editorconfig`, `Directory.Build.props`, `global.json`.
- Arquivos mantidos no diretório atual conforme solicitado.
- `ChangeLogs` criados com este conjunto de arquivos.

## Explicação e motivação

(Resumo) Essas configurações melhoram consistência de estilo, qualidade e reprodutibilidade de builds. Veja o arquivo `rationale_for_new_devs.md` para uma explicação mais detalhada sobre cada item.

## Como usar isso no dia a dia
- Abra o repositório na IDE. As regras do `.editorconfig` serão aplicadas automaticamente.
- Ao gravar código, siga as dicas do editor sobre estilo e corrija warnings.
- Se precisar mudar a versão do SDK, atualize `global.json` e coordene com a equipe.
- Leia o `AGENTS.md` e a pasta `ChangeLogs` quando estiver começando.

## Próximos passos
1. Avaliar se deseja mover essas configurações para o root do repositório.
2. Implementar testes adicionais e fixtures conforme necessidade.
3. Consolidar e remover arquivos temporários de ChangeLogs (se desejar, eu preparo os comandos git).
