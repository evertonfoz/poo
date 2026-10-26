# Retomada — reescrita do guia EventoParticipantes

Atualizado em 2026-10-14. Leia este arquivo inteiro antes de continuar.

## Onde estamos

Situação em 2026-10-15 (sessão 4, PDF 17):
- **Etapas 0 a 14 aplicadas no Gamma e conferidas.** Detalhes: tabela Andamento do plano. D17 e D18 aceitas.
- **Formato que funciona para Experimentos e Perguntas:** parágrafos (sem lista automática) começando com "**1. Título:**" em negrito. Usar sempre assim.
- Pendente de decisão do professor: **D19** (Etapa 14 sem `MapeadorDados`) e **D16** (diagrama da Etapa 5) — perguntado na sessão 4.
- **Entregues, ainda não conferidos:** **14-B** (tabela de linhas: "de 52 a 59") e **15.1–15.4** — cards "ETAPA 15", "ETAPA 15 · ROTEIRO", "ETAPA 15 · QUESTÕES", "ETAPA 15 · SÍNTESE" (substituem "SEÇÕES 10–13"). Sem código novo; [codigo-guia-ate-etapa14/](codigo-guia-ate-etapa14/) é o código final do guia.
- **Próximo passo:** conferir o PDF novo (última etapa). Depois: decidir D16/D19; pendências do gabarito fora de `docs/` (seção "Pendências no código do repositório" do plano).
- Ordem esperada dos cards no fim: … ETAPA 13 (4 cards) → ETAPA 14 (6 cards) → ETAPA 15 (4 cards). Não deve sobrar card "SEÇÕES 10–13".
- Para testar sessões interativas: `scratchpad/sessao.py` foi recriado na sessão 3 (subprocess + select; `{G1}`… = Ids de "Id: …"; zsh: não usar `echo =====` em comandos, vira expansão `=cmd`).
- Decisões tomadas nesta sessão: `Tentar(Action)` no Program da Etapa 8 (aceito); filtro `catch ... when` na Etapa 9; menus como classes sem herança até a Etapa 14; T1/T4/T5 como cards de apoio.
- Pendente de decisão do professor: **D16** (trocar o diagrama de 3 círculos da Etapa 5 por um "antes × depois" da inversão de dependência). Não aplicar sem ele pedir.

Arquivos de apoio (nesta pasta `docs/`):
- [plano-guia-etapas.md](plano-guia-etapas.md) — sequência de etapas, diagnóstico do PDF original, decisões D1–D16, andamento, gabaritos, fatos verificados.
- [topicos-de-apoio.md](topicos-de-apoio.md) — tópicos de apoio (T1 JSON, T2 Interfaces, T3 Camadas, T4 Arquivos, T5 DTOs); T1/T4/T5 já viraram cards na 11a.
- `codigo-guia-ate-etapa7/` … `codigo-guia-ate-etapa11a/` — **código do guia exatamente como ensinado até cada etapa**, compilando (.NET 10.0.103). Use o mais recente como base: copie para o scratchpad, acrescente o código da etapa nova, rode `dotnet build`/`dotnet run`. Sessões interativas foram testadas com um script Python que digita as entradas e captura os Guids (recriar se necessário: subprocess + select, substituindo {G1}, {G2}… pelos Ids que aparecem em "Id: …").
- O código fora de `docs/` (`EventoParticipantesApp.*`) é o **gabarito final antigo** e está **desatualizado** em relação às decisões D1, D2, D9, D11, D12, D13, D15 — ajustar só quando chegar nas etapas correspondentes.

## Como trabalhamos (regras combinadas com o professor)

1. Para cada etapa, o Claude entrega **prompts em português, prontos para colar no agente do Gamma**, numerados (ex.: 8.1, 8.2…), cada um dizendo exatamente qual card/bloco mexer e "não altere o resto".
2. **Antes de entregar qualquer prompt, testar todo o código no .NET 10**: build sem warnings, saída do checkpoint real, e mensagens/códigos de erro reais de cada experimento (CSxxxx, "Unhandled exception…"). Nunca inventar saída.
3. O professor aplica, manda o **PDF atualizado**; o Claude revisa (o que entrou certo, defeitos visuais/de conteúdo), registra no plano e prepara a próxima etapa.
4. Quando o professor diz "anote / aguarde eu pedir", **só registrar** no plano; gerar o prompt apenas quando ele pedir.
5. Atualizar `plano-guia-etapas.md` (Andamento + Decisões) a cada etapa.

### Convenções dos prompts e do código
- Padrão de cada etapa no guia: **Objetivo → Conceito → Código → Checkpoint → Experimento → Pergunta**.
- Nome de exemplo: **Shiderlene Souza** (`shiderlene@eventos.com`); **nunca "Ana"**. Outros: Bruno Lima, Carla Dias.
- **Exercícios:** código de referência **visível logo após o enunciado** ("Compare com a sua implementação"), nunca recolhível.
- **Linhas de código curtas (≤ ~72 colunas)**, quebradas só em pontos seguros (entre argumentos, antes de `+`, `?`, `:`, `&&`). O professor quebra linhas por legibilidade; quebra **dentro** de `$"..."` não compila (CS1010).
- Estilo do guia: chave na mesma linha, 2 espaços.
- Saída do checkpoint: pedir "texto simples, sem realce de sintaxe".
- Pedir ao Gamma "mesmo tamanho de fonte dos demais cards" ao dividir cards (ele costuma encolher).
- **Texto corrido dos prompts sem quebras de linha manuais** (cada parágrafo/item numa linha só): prosa quebrada a ~72 colunas virou espaços largos no meio das frases (Etapa 10, PDF 11). Só blocos de código mantêm as quebras.
- **Cards curtos:** card longo é encolhido e **cortado** pelo Gamma (Etapas 9 e 10). No máximo um arquivo de código grande por card; dividir já na criação.
- Pastas: plural para gaveta de itens (`Contratos`, `Servicos`, `Menus`, `Dtos`); singular para conceito/área/tecnologia (`Modelo`, `Validacao`, `Memoria`, `Json`).

## Estado do código (o que o guia já ensinou)

- `Negocio/Validacao/Validador.cs`: `TextoObrigatorio`, `Email`, `InteiroPositivo`.
- `Negocio/Modelo/Organizador.cs` e `Participante.cs`: Guid estável, `private set`, `Alterar`, **`Equals`/`GetHashCode` pelo Guid** (D12). Desde a Etapa 13 (D18), tudo isso fica em `Pessoa.cs` (abstrata); as duas classes têm 10 linhas, com `OrganizadorId => Id` / `ParticipanteId => Id`.
- `Negocio/Modelo/Evento.cs`: `EventoId`, **`Organizador Organizador`** e **`IReadOnlyCollection<Participante> Participantes`** (associação de objetos, D11), construtor `(nome, organizador, capacidade, Guid? eventoId)`, `Alterar(nome?, capacidade?)` tudo-ou-nada (D2), `Inscrever(Participante)`, `CancelarInscricao(Participante)`.
- `Negocio/Contratos/EstadoSistema.cs` e `IRepositorioSistema.cs` (D13).
- `Persistencia/Memoria/RepositorioMemoria.cs` (D14).
- `Negocio/Servicos/Gerenciador.cs` (namespace `EventoParticipantes.Negocio.Servicos`, D15): construtor com injeção de `IRepositorioSistema` (com `ValidarIntegridade` + `ExigirUnicos` desde a Etapa 12: Guids e e-mails repetidos); seções Organizadores e Participantes completas; Apoio com `BuscarOrganizador`, `BuscarParticipante`, `MesmoEmail`, `Gravar`.
- Console: `Entrada.cs` (`Texto`, `TextoOpcional`, `Id`, `Inteiro`, `InteiroOpcional`), `Menus/` (3 menus sem herança), Program com `RepositorioJson` e filtro `catch ... when` (+ `IOException`, `UnauthorizedAccessException`); desde a Etapa 12, o Gerenciador é criado dentro de um `try` próprio (erro de abertura → mensagem e `return;`).
- `Persistencia/Json/`: DTOs (`PessoaDto`, `EventoDto`, `DadosDto`, internal) e `RepositorioJson` com `Carregar` (`LerDto` + `ParaEstado`, via construtores e `Inscrever()` — D1) e `Salvar` (`ParaDto` + `GravarArquivo` com `.tmp`) — Etapa 11b.

## Próximos passos detalhados

### Prompt 7-B (correção da Etapa 7)
No card "ETAPA 7", Experimento 2 ("Sem proteção de referência"): o bloco de código sumiu e virou nota de rodapé "¹". Restaurar, logo após "No final do Program.cs, acrescente:", o bloco de código (como código, não rodapé):
```
foreach (var p in palestra.Participantes)
  Console.WriteLine($"Inscrito na palestra: {p.Nome}");
```
e remover a nota de rodapé. Opcional: voltar os experimentos para lista numerada.

### Etapa 8 — CRUD de Evento e inscrições (card "SEÇÃO 6 · CONTINUAÇÃO")
O card atual tem código **obsoleto** (usa `organizadorId` no construtor do Evento, `ParticipanteIds`, `AlterarNome`/`AlterarCapacidade`, regra de capacidade repetida no Gerenciador) e blocos "Ajuste em Evento.cs" (AlterarNome) e "Por que consultar os eventos?" (fala em Guid). Substituir por uma seção `// ---------- Eventos ----------` no Gerenciador que **delega ao Evento**:
- `CriarEvento(nome, organizadorId, capacidade)` → `BuscarOrganizador` → `new Evento(nome, organizador, capacidade)`.
- `ListarEventos()`, `ObterEvento(eventoId)`, `BuscarEvento` (Apoio, KeyNotFound "Evento não encontrado.").
- `AlterarEvento(eventoId, nome?, capacidade?)` → `BuscarEvento(...).Alterar(nome, capacidade)` + `Gravar()` (a regra fica na entidade).
- `RemoverEvento(eventoId)` → recusa "Evento possui inscrições." se `Participantes.Count > 0`.
- `Inscrever(eventoId, participanteId)` e `CancelarInscricao(eventoId, participanteId)` → buscar os dois objetos e chamar o Evento; `Gravar()`.
- Remover/reescrever os blocos obsoletos; manter "Por que validar antes de alterar?" (agora apontando para `Evento.Alterar`) e a caixa "Atenção".
- Program de teste com RepositorioMemoria cobrindo as linhas da tabela de validação: lotação, duplicidade, exclusão protegida (participante e organizador com evento), cancelamento e remoção do evento.
- Experimentos sugeridos: evento com organizador inexistente (KeyNotFound), remover evento com inscritos, esquecer `Gravar` no `Inscrever`.
- Depois da Etapa 8: Etapas 9 (menu), 10 (menus — exercício), 11a/11b (JSON), 12 (integridade, card "SEÇÃO 6 · INTEGRIDADE"), 13 (Pessoa), 14 (partial/MenuCadastro/Mapeador), 15 (consolidação; corrigir "Próximo desafio", que ainda fala em "Guid do participante na coleção de Evento").

### Fatos que já foram verificados (não precisa retestar)
Ver seção "Fatos verificados" no plano. Destaques: `InvalidDataException` **não** é `IOException`; `dotnet new sln` cria `.slnx`; `dotnet remove ... reference` exige o caminho do `.csproj`.
