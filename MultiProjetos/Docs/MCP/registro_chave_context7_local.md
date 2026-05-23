# Registro Local e Protegido da Chave Context7

## Objetivo

Guardar a chave da Context7 localmente, sem expor segredo no repositório.

## Como está configurado

O arquivo `.vscode/mcp.json` usa variável de ambiente:

- `CONTEXT7_API_KEY` via `${env:CONTEXT7_API_KEY}`

Isso evita gravar chave em texto puro no Git.

## Passo a passo (macOS / zsh)

1. Criar arquivo local a partir do exemplo:

```bash
cp .env.local.example .env.local
```

2. Editar `.env.local` e preencher a chave:

```bash
CONTEXT7_API_KEY=ctx7sk-...
```

3. Carregar variável no shell atual:

```bash
set -a
source .env.local
set +a
```

4. Abrir o VS Code a partir desse shell:

```bash
code .
```

## Validação

No VS Code, abrir o chat/agent e testar uma consulta usando Context7.

Se a chave estiver correta, o servidor Context7 responde sem erro de autenticação.

## Segurança

- `.env.local` está no `.gitignore`.
- Nunca commitar chaves reais.
- Se a chave já vazou anteriormente, rotacionar no provedor.
