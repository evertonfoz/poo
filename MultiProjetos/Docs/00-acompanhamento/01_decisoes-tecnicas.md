# Decisões Técnicas

## 2026-05-23 — Formato da solution

Decisão: usar `CadastroAcademico.slnx`.

Justificativa:
1. Em .NET 10, `dotnet new sln` passa a gerar `slnx` por padrão.
2. Formato simplificado e suportado pelo tooling atual.
3. Mantém alinhamento com comportamento padrão do SDK instalado no ambiente.

Referência oficial consultada:
- Microsoft Learn: `dotnet new sln` defaults to SLNX file format.

## 2026-05-23 — Segredo da Context7

Decisão: usar variável de ambiente no `.vscode/mcp.json`.

Implementação:
1. `CONTEXT7_API_KEY` configurada como `${env:CONTEXT7_API_KEY}`.
2. Chave registrada localmente em `.env`.
3. `.env` adicionado ao `.gitignore` para não versionar segredo.

Justificativa:
1. Evitar chave em texto puro no repositório.
2. Permitir execução local segura e simples.
