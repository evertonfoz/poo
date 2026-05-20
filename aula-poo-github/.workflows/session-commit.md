Objetivo: encerrar a sessao com um commit informativo e um push seguro na branch `main`.

Ao executar este workflow:

1. Identifique apenas o que foi realmente implementado na sessao atual.
2. Rode `git status` e revise o `git diff` antes de preparar o commit.
3. Separe alteracoes da sessao atual de mudancas antigas, acidentais ou sem relacao com o objetivo da conversa.
4. Inclua no commit somente os arquivos que pertencem ao escopo da sessao.
5. Gere uma mensagem de commit objetiva e util, contendo:
	- titulo curto e claro;
	- descricao com o que foi alterado;
	- motivacao ou objetivo da alteracao, quando fizer sentido.
6. Antes do push, confirme que:
	- a branch atual e `main`;
	- o commit contem apenas o escopo pretendido;
	- nao ha arquivos relevantes esquecidos.
7. Execute o commit.
8. Execute o push para `origin main`.
9. Apos o push, sincronize o repositorio local com o remoto:
   - rode `git fetch origin`;
   - verifique se `main` e `origin/main` estao alinhadas;
   - rode `git pull --ff-only origin main` quando for seguro;
   - confirme explicitamente se o repositorio local ficou sincronizado.
10. Registre ao final um resumo curto com:
	- arquivos incluidos no commit;
	- hash curto do commit criado;
	- titulo final do commit;
	- status da sincronizacao local com o remoto.

Regras importantes:

- Nao incluir mudancas sem relacao com a sessao apenas porque estao no working tree.
- Nao criar commit vazio.
- Se nao houver nada para commitar, informar isso explicitamente.
- Se houver risco de misturar trabalho de outra sessao, reduzir o escopo do commit ao minimo seguro.