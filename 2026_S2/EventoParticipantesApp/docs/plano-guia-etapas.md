# Plano: reescrita do guia "EventoParticipantes — Guia de Implementação" em etapas

Documento de trabalho do professor. Registra o diagnóstico do guia original (PDF feito no Gamma),
a sequência de etapas acordada e o andamento das correções.

- Início: 2026-10-13
- Guia original: `EventoParticipantes-Guia-de-Implementacao-Passo-a-Passo.pdf` (24 páginas, editado no Gamma)
- Forma de trabalho: para cada etapa, o Claude gera prompts que o professor aplica no agente do Gamma.
- O código deste repositório (`2026_S2/EventoParticipantesApp`) é o **gabarito final** (estado após a Etapa 14).

---

## Retomada

Para continuar em outra sessão, leia primeiro [retomada.md](retomada.md).
Código do guia testado até a Etapa 7: [codigo-guia-ate-etapa7/](codigo-guia-ate-etapa7/); até a Etapa 8: [codigo-guia-ate-etapa8/](codigo-guia-ate-etapa8/).

## Princípios da reescrita

1. Toda etapa termina **compilando e rodando algo observável**. Até existir o menu (Etapa 9),
   o `Program.cs` é um roteiro de teste que o aluno reescreve a cada etapa.
2. **Repositório em memória antes do JSON**: a troca para JSON vira uma linha no Program
   (demonstração prática da inversão de dependência).
3. Padrão fixo por etapa: **Objetivo → Conceito → Código → ✅ Checkpoint → 🧪 Experimento → ❓ Pergunta**.
4. Partes repetitivas (Participante, menus) viram **exercícios com gabarito**.
5. **Refatoração no fim** (Etapas 13–14): o aluno primeiro sente a duplicação, depois a elimina.

## Sequência de etapas (aprovada em 2026-10-13)

| Etapa | Tema | Entrega | Checkpoint |
|---|---|---|---|
| 0 | O problema e as regras (sem código) | Classificar responsabilidades nas 3 camadas | Tabela preenchida e discutida |
| 1 | Solução e projetos | sln, 3 projetos, referências, `RootNamespace`, pastas | `dotnet build` OK; 🧪 referência circular Negocio→Console dá erro |
| 2 | Validador + Organizador | `Validador.cs`, `Organizador.cs` | Program cria válido/inválido; 🧪 e-mail "ana", nome "  ", `Alterar()` vazio |
| 3 | Participante (exercício) | `Participante.cs` pelo aluno | Mesmo teste; ❓ "o que você copiou?" (prepara herança) |
| 4 | Evento e coleção protegida | Construtor com `Guid?`, `Alterar(nome?, capacidade?)`, `Inscrever`, `Cancelar` | Experimentos 1–3; 🧪 `ParticipanteIds.Add` não compila |
| 5 | Contrato de persistência | `EstadoSistema`, `IRepositorioSistema`, `RepositorioMemoria` | Salvar/carregar em memória |
| 6 | Gerenciador: núcleo + Organizador | Construtor, `Gravar`, `MesmoEmail`, `Buscar*`, CRUD Organizador | E-mail duplicado (maiúsc./minúsc.) recusado; alterar mantendo o próprio e-mail OK |
| 7 | CRUD Participante (exercício) | Espelho da 6 + `ObterParticipante` | Idem |
| 8 | CRUD Evento + inscrições | Criar, Listar, Obter, `AlterarEvento` (delegando ao Evento), Remover, Inscrever, Cancelar | Linhas duplicidade, lotação, exclusão protegida, cancelamento |
| 9 | Console: menu principal | Laço, `default`, saída com 0/`null`, `Entrada`, menu Organizadores | CRUD interativo de organizadores |
| 10 | Menus Participantes e Eventos (exercício) | Dois menus | Tabela de validação inteira, exceto persistência |
| 11a | JSON: DTOs e Salvar | DTOs, `Salvar` com arquivo temporário; troca de 1 linha no Program | Abrir o `eventos.json` gerado |
| 11b | JSON: Carregar | Reconstrução pelos construtores | Linha "Persistência" da tabela |
| 12 | Integridade e erros de inicialização | `ValidarIntegridade`; `try/catch` na criação do Gerenciador | 🧪 JSON editado à mão: organizador inexistente, e-mail duplicado, JSON quebrado |
| 13 | Refatoração I: herança | `Pessoa` abstrata | Tudo funciona, menos código |
| 14 | Refatoração II: coesão | `partial` no Gerenciador, `MenuCadastro` (Template Method), `MapeadorDados` | Comparar tamanho dos arquivos antes/depois |
| 15 | Consolidação | 4 questões + desafio `Inscricao` | — |

Mapa das etapas para o guia original: 0 = Leia antes + Seção 1; 1 = Seção 2; 2–4 = Seção 4;
5 = Seção 5; 6–8 = Seção 6; 9–10 = Seção 9; 11a/11b = Seções 7–8; 12 = parte da Seção 6 + p. 17;
13–14 = novas; 15 = Seções 10–13 (questões e síntese).

## Diagnóstico do guia original (o que corrigir)

### Erros que impedem compilar
| # | Página | Problema | Etapa que corrige |
|---|---|---|---|
| E1 | p. 6 | `Evento` sem construtor (só "assumido" na p. 15; `Guid?` só na p. 19) | 4 |
| E2 | p. 6/9/11/14/19 | `Evento.Id` × `EventoId` misturados | 4, 8, 11a |
| E3 | p. 12 | `AlterarOrganizador`: parâmetro `organizadorId`, lambda usa `id` | 6 |
| E4 | p. 23 | Program usa `x.Id` para Organizador (é `OrganizadorId`) | 9 |
| E5 | p. 14 | `AlterarEvento` chama `AlterarNome`, criado só na p. 15 | 4, 8 |
| E6 | p. 23 | `dotnet run --project EventoParticipantes.Console/...` (falta `App`) | 9 |
| E7 | p. 19 | "InvalidDataException é uma IOException" — **falso** (herda de `SystemException`, verificado em .NET 10); e o Gerenciador é criado fora do `try` | 12 |

### Inconsistências de conteúdo
| # | Página | Problema | Etapa |
|---|---|---|---|
| I1 | p. 1 | Pré-requisito .NET 8/`net8.0`, comandos usam `net10.0` | 0 |
| I2 | — | Não existe Seção 3; Seções 7–8 e 10–13 fundidas | 0 (roteiro) |
| I3 | p. 4 | Pastas: cita `Servicos`, omite `Validacao`; namespace ≠ nome do projeto sem `RootNamespace` | 1 |
| I4 | p. 12 | `using ...Servicos;` solto no meio do trecho de classe | 6 |
| I5 | p. 2 | Regra 4 mistura capacidade com "a interface avisa após gravação" | 0 |
| I6 | p. 14 | `AlterarEvento` repete no Gerenciador a regra de capacidade que, pela p. 2, é da entidade → mover para `Evento.Alterar(nome?, capacidade?)` | 4, 8 |
| I7 | p. 17 × p. 10 | Carga via `Inscrever()` torna inalcançáveis as verificações de duplicidade/capacidade do `ValidarIntegridade` | 11b, 12 |
| I8 | — | Faltam `ObterParticipante`/`ObterEvento`; menus Participantes/Eventos nunca mostrados; sem `default` nem tratamento de `ReadLine()` nulo | 7, 8, 9, 10 |
| I9 | p. 9 | Gerenciador fragmentado (`RemoverOrganizador` no "trecho principal", antes do CRUD) | 6 |

## Convenções dos exemplos (pedidas pelo professor)

- **Não usar o nome "Ana"** nos exemplos. Usar **"Shiderlene"** (variável `shiderlene`, e-mail `shiderlene@eventos.com`).
  Registrado em 2026-10-13. Afeta os prompts 5 e 6 da Etapa 2, que já foram entregues com "Ana":
  **gerar versão nova quando o professor pedir** (ele vai solicitar; não reenviar por conta própria).
  → Prompt 2-B (troca Ana → Shiderlene) gerado em 2026-10-13 e testado: nome completo "Shiderlene Souza",
  e-mails `shiderlene@eventos.com` / `shiderlene.souza@eventos.com`. Prompt 2-C (ajustes visuais) entregue junto, opcional.

- **Exercícios: código de referência logo após o enunciado, visível** (não em bloco recolhível nem no fim do card),
  para o aluno comparar a implementação dele com a do guia. Registrado em 2026-10-13.
  Vale para a Etapa 3 (Participante) e para os próximos exercícios (Etapas 7 e 10).
  → Prompt 4 da Etapa 3 refeito nesse formato em 2026-10-13 (versão 4-novo para quem ainda não aplicou;
  versão 4-corrigir para quem já aplicou o recolhível).

- **Quebras de linha no código são feitas pelo professor**, de propósito, para legibilidade (2026-10-13).
  Cuidado: quebra **dentro** do texto de uma string `$"..."` não compila (`CS1010`). Por isso, os códigos
  entregues nos prompts já vêm com linhas curtas (≤ ~68 colunas) e quebras em pontos seguros
  (entre argumentos, antes de `+`, antes de `?`/`:`), para o professor não precisar quebrar dentro de strings.

## Decisões

| # | Decisão | Status |
|---|---|---|
| D1 | Carga do JSON: usar `Inscrever()` como o guia **ou** Evento recebe a lista (como está no repositório) | **Adotado na Etapa 4: via `Inscrever()`** — o construtor do Evento não recebe lista de inscritos. O repositório (gabarito) precisa ser ajustado depois — **o professor pode vetar** |
| D2 | `Evento.Alterar(nome?, capacidade?)` validando tudo antes de mudar (corrige I6) | **Adotado na Etapa 4**; substitui `AlterarCapacidade` e `AlterarNome` do guia. Repositório precisa ser ajustado depois |
| D9 | Propriedade de identidade do Evento: `EventoId` (como `OrganizadorId`/`ParticipanteId`), não `Id` (corrige E2) | Adotado na Etapa 4 |
| D10 | `Validador.InteiroPositivo` entra na Etapa 4 (construtor e `Alterar` do Evento) | Adotado na Etapa 4 |
| D11 | Evento associa **objetos** (`Organizador`, `List<Participante>`) em vez de Guids? Pergunta do professor (2026-10-13): os alunos já aprenderam associação. Claude recomendou **sim** (memória = objetos, arquivo = Ids; tradução na Etapa 11b). Impacta Etapas 0 (card/regra), 4, 6, 8, 9–10, 11a/11b, 12 | **Aprovado** (2026-10-13) — aplicado na correção 4-B |
| D13 | `EstadoSistema` fica em `Negocio/Contratos` (namespace `...Negocio.Contratos`), não em `Modelo`: é o pacote trocado com o repositório, não uma entidade | Adotado na Etapa 5 |
| D14 | `RepositorioMemoria` em `Persistencia/Memoria` (namespace `...Persistencia.Memoria`), como no gabarito | Adotado na Etapa 5 |
| D12 | Sobrescrever `Equals`/`GetHashCode` em Organizador e Participante pelo Guid (identidade de entidade). O guia original **não** faz isso. Professor lembrou (2026-10-13) que os alunos já conhecem identidade de objetos. Proposta: introduzir na Etapa 4, junto com D11 (é onde `Contains`/`Remove` passam a comparar objetos); não sobrecarregar `==` e usar isso como discussão. Testado: `a == b` False, `Equals`/`Contains`/`Remove`/`Distinct`/`HashSet` tratam mesmo Guid como o mesmo objeto | **Aprovado** (2026-10-13) — aplicado na correção 4-B |
| D3 | Formato: um documento por etapa ou guia único com etapas numeradas | **Pendente** |
| D4 | Gabarito por etapa no git (`etapa-01`, `etapa-02`…) | **Pendente** |
| D5 | Sequência de etapas | **Aprovada** (2026-10-13) |
| D6 | Correções do guia via prompts para o agente do Gamma | **Aprovada** (2026-10-13) |
| D7 | Nome da pasta/namespace de serviço: `Servico` (singular) | **Substituída pela D15** → `Servicos` |
| D15 | Convenção singular × plural nas pastas (pergunta do professor, 2026-10-13). Proposta do Claude: **plural** para pastas que agrupam vários itens da mesma categoria (`Contratos`, `Servicos`, `Menus`, `Dtos`); **singular** para pastas que nomeiam um conceito, área ou tecnologia (`Modelo` = modelo de domínio, `Validacao`, `Memoria`, `Json`). Consequência: `Servico` → `Servicos` (Etapa 1 + repositório); `Modelo` fica | **Aprovado** (2026-10-13). **Executada no prompt 6.0** (2026-10-13). Original: incluir, no próximo prompt, o ajuste da Etapa 1 (lista de pastas e árvore "Estrutura esperada": `Servico` → `Servicos`; acrescentar o critério singular/plural ao card "Organizar as pastas do Negocio"). A partir da Etapa 6, usar `EventoParticipantes.Negocio.Servicos`. Ajustar o repositório quando chegar na Etapa 6 |
| D16 | Inversão de dependência está **mal explicada** no guia (pergunta do professor, 2026-10-13): só uma frase no card "Por que esse design?" (Etapa 5); o diagrama de 3 círculos (Negócio → Persistência → Console) sugere a direção errada; "injeta" aparece na Etapa 1 sem definição; inversão × injeção não são distinguidas. Proposta: Etapa 5 ganha bloco "Antes × depois" (sem inversão: Gerenciador → RepositorioJson; com inversão: Gerenciador → IRepositorioSistema ← RepositorioJson, a interface **mora no Negocio**) substituindo o diagrama; Etapa 6 nomeia **injeção de dependência pelo construtor** quando o Gerenciador recebe o repositório | **Pendente — aguardando decisão do professor** |
| D17 | Com a D11 (Evento guarda objetos), um `OrganizadorId`/`ParticipanteId` inexistente no arquivo **não chega ao Gerenciador**: é percebido na tradução Id → objeto, no `RepositorioJson.ParaEstado` (Etapa 11b), que lança `InvalidDataException`. O `ValidarIntegridade` da Etapa 12 fica só com o que a carga não detecta: Guids e e-mails repetidos nos cadastros. Duplicidade e lotação de inscrições já são barradas pelo `Inscrever` (resolve I7). Etapa 12 = `ValidarIntegridade` enxuto + `try/catch` na criação do Gerenciador + experimentos editando o JSON | **Aceita** (11b aplicada sem veto, 2026-10-15) |
| D18 | Etapa 13: `Pessoa` abstrata com `Id`; subclasses mantêm `OrganizadorId`/`ParticipanteId` como apelidos (`=> Id`) para não alterar Gerenciador, menus e RepositorioJson; `Equals` na base com `GetType()` | Proposto na 13 (2026-10-15) — **o professor pode preferir renomear tudo para `Id`** |
| D19 | Etapa 14 sem `MapeadorDados`: só `partial` no Gerenciador e `MenuCadastro` (Template Method), como diz o roteiro da p. 2 | Proposto na 14 (2026-10-15) — **o professor pode pedir o MapeadorDados** |
| D8 | `RootNamespace`: `EventoParticipantes.Negocio`, `EventoParticipantes.Persistencia`, `EventoParticipantes.App` (não `.Console`, que esconderia `System.Console`) | Adotada na Etapa 1 |

### Fatos verificados (SDK 10.0.103)
- `dotnet new sln` cria **`.slnx`** (não `.sln`).
- `dotnet sln add <pasta>` e `dotnet add <pasta> reference <pasta>` aceitam o nome da pasta do projeto.
- Referência circular Negocio → Console: o `add` é aceito, mas o `dotnet build` falha com `error MSB4006: There is a circular dependency in the target dependency graph`.
- Para desfazer: `dotnet remove EventoParticipantesApp.Negocio reference EventoParticipantesApp.Console/EventoParticipantesApp.Console.csproj` (só a pasta não funciona).
- `InvalidDataException` herda de `SystemException`, não de `IOException`.
- `UnauthorizedAccessException` herda de `SystemException` (**não** é `IOException`); `DirectoryNotFoundException` e `FileNotFoundException` são `IOException`; `System.Text.Json.JsonException` herda direto de `Exception`.
- `JsonSerializer` padrão grava acentos escapados: "Introdução à POO" → `"Introdução à POO"`.
- `dotnet clean` **não** apaga `bin/Debug/net10.0/dados/eventos.json` (só os arquivos de build); apagar a pasta `bin` apaga. (Corrige a pendência antiga.)
- `RepositorioMemoria` + `EstadoSistema`: listas copiadas, objetos compartilhados. Mudar o interior de um objeto (ex.: `Evento.Inscrever`) aparece no repositório mesmo sem `Gravar()`; adicionar/remover da lista do Gerenciador, não (testado na Etapa 8).
- Função local `void Tentar(Action acao)` no fim de um Program com top-level statements compila sem warnings.
- Etapa 2: atribuir `ana.Nome = "x"` (setter privado) e `ana.OrganizadorId = ...` (sem setter) dão **o mesmo** erro: `CS0200: Property or indexer '...' cannot be assigned to -- it is read only`.
- O Validador do guia ainda não tem `InteiroPositivo`; ele entra na Etapa 4 (Evento).
- Estilo: o guia usa chaves na mesma linha e 2 espaços; o gabarito do repositório usa o padrão .NET (`.editorconfig`). Não afeta compilação.

## Andamento

| Etapa | Status | Observações |
|---|---|---|
| 0 | **Aplicada no Gamma** (2026-10-13) | Corrige I1, I2, I5; roteiro das etapas criado. Desvio: a tabela da atividade saiu **preenchida** (o professor optou por "Confira"); itens 9 e 12 ficaram como "Persistência", diferente do gabarito (ver prompt opcional 0-B na conversa da Etapa 1) |
| 0-B | **Aplicado** | Itens 9 e 12 da tabela corrigidos |
| 1 | **Aplicada** (2026-10-13) | Desvio: o Gamma partiu a grade "Por que essa estrutura?" (2 cards ficaram depois da tabela de RootNamespace) → prompt 1-B. Comandos longos aparecem quebrados no PDF (conferir se é só quebra visual) |
| 1-B | **Aplicado** | Grade de 4 cards reagrupada |
| 2 | **Aplicada** (2026-10-13) | Card dividido; "SEÇÃO 4 · CONTINUAÇÃO — Participante e Evento" criado. Pendências visuais: (a) Checkpoint da Etapa 1 e da Etapa 2 e Experimento da Etapa 1 perderam a caixa colorida (verde/amarela) — estão como texto simples; (b) card "SEÇÃO 4 · CONTINUAÇÃO" ficou com fonte reduzida (o Gamma encolheu para caber); (c) grade do Conceito irregular (1+2+1) e título "private set" sem negrito; (d) bloco da saída do checkpoint com realce de sintaxe colorindo números — usar texto simples. **Ainda com "Ana"** (aguardando pedido do professor para o prompt de troca por Shiderlene) |
| 2-B / 2-C | **Aplicados** | Nome trocado para Shiderlene. Grade do Conceito 2×2 ok. Checkpoint/Experimento das Etapas 0–3 ficaram todos como título em faixa escura (sem caixa colorida) — consistente entre si; saída do checkpoint ainda com cores diferentes na Etapa 2 (realce) e na Etapa 3 (azul negrito) |
| ⚠️ | **Verificar** | No PDF, linhas longas de `Console.WriteLine($"...")` (Etapas 2 e 3) aparecem quebradas **dentro da string interpolada**. Se a quebra for real (não só visual), o código não compila: testado → `CS1010 Newline in constant`. Professor vai conferir copiando do Gamma; se for real, gerar prompt para encurtar as linhas |
| 3 | **Aplicada** (2026-10-13), com 4-novo | Ordem correta: Objetivo → Exercício → Compare (código visível) → Testar → Checkpoint → Experimento → Pergunta. Card do Evento separado, fonte normal |
| 3 (histórico) | Prompts gerados (2026-10-13) | Divide o card "SEÇÃO 4 · CONTINUAÇÃO": Etapa 3 = Participante como exercício (requisitos + gabarito recolhível); Evento fica num card "SEÇÃO 4 · CONTINUAÇÃO" para a Etapa 4. Testado: saída do checkpoint, CS0103 (sem using) e CS0029 (Participante → Organizador) |
| 4 | Prompts gerados (2026-10-13) — aguardando aplicação | Evento completo (construtor com `Guid?`, `EventoId`, `Alterar` tudo-ou-nada, `Inscrever`, `CancelarInscricao`) + `InteiroPositivo` no Validador. Testado: saída do checkpoint, duplicidade, CS1061 (`Add`), `ArgumentException` não tratada → segundo `catch`. Obs.: o card "SEÇÃO 6" ainda tem `AlterarNome`/`AlterarCapacidade` e `x.Id` — corrigir na Etapa 8 |
| 4-B | **Aplicada** (2026-10-13) — conferida no PDF. Obs.: "Próximo desafio" (síntese final) ainda fala em "Guid do participante na coleção de Evento" → ajustar na Etapa 15 | D11 + D12: Equals/GetHashCode em Organizador e Participante; Evento com `Organizador` e `List<Participante>`; novo Program; 4 experimentos (identidade ==×Equals, sem Equals → recusa errada "Evento lotado" e CS0659, CS1061 Add, ArgumentException) — tudo testado. Ajusta regra "Eventos válidos" da Etapa 0 |
| 5 | **Aplicada** (2026-10-13) — conferida no PDF. Card "Por que esse design?" e diagrama de 3 círculos continuam (ver D16) | `EstadoSistema` e `IRepositorioSistema` em `Contratos`; `RepositorioMemoria` em `Persistencia/Memoria` (sai do card "SEÇÕES 7 E 8"); Program com variável do tipo da interface; bloco curto sobre interface (T2, escopo mínimo); responde a pergunta da Etapa 1. Testado: saída, CS0535 (falta Salvar), CS0144 (new interface), CS0234 (Negocio não vê Persistencia) |
| 6 | **Aplicada** (2026-10-13), com 6.0 (`Servicos` na Etapa 1) — conferida no PDF. Obs.: cards "SEÇÃO 6 · CONTINUAÇÃO" e "SEÇÃO 6 · INTEGRIDADE" ficaram com fonte reduzida (Gamma encolheu); saída do checkpoint com realce colorido | Inclui 6.0 = execução da D15 na Etapa 1 (`Servicos`). Divide o card "SEÇÃO 6" em três: Etapa 6 (Gerenciador núcleo + CRUD Organizador), "SEÇÃO 6 · CONTINUAÇÃO" (Participante/Evento → Etapas 7–8) e "SEÇÃO 6 · INTEGRIDADE" (`ValidarIntegridade` → Etapa 12). Construtor **sem** `ValidarIntegridade` por enquanto. Bloco curto de injeção de dependência pelo construtor (parte B da D16). Testado: saída, exceção sem filtro do próprio registro, KeyNotFound, "sem Gravar" → 2 no repositório, CS7036, CS0246 |
| 7 | **Aplicada** (2026-10-14) — conferida no PDF. **Defeito:** no Experimento 2, o bloco de código com o `foreach` ("Inscrito na palestra") sumiu — virou uma nota de rodapé "¹". Experimentos viraram marcadores em vez de números. Saída do checkpoint com realce colorido. → corrigir com prompt 7-B na próxima sessão | Exercício: CRUD de Participante no Gerenciador, com código de referência logo após o enunciado; `RemoverParticipante` usa `Participantes.Contains` (Equals). Card "SEÇÃO 6 · CONTINUAÇÃO" passa a ter só Evento (→ Etapa 8). Testado: saída; exp. 1 "cadastro trocado" (`_organizadores` no lugar de `_participantes`) → exceção não tratada; exp. 2 sem proteção de referência → "Inscrito na palestra: Bruno Lima" órfão |
| 7-B | **Aplicado** (2026-10-14) — conferido no PDF (9): bloco do `foreach` restaurado, experimentos numerados. Obs.: na saída do checkpoint da Etapa 7, as 3 linhas que começam com "-" saíram em vermelho (realce de "diff") → prompt 8-C (opcional) | Restaura o bloco de código do Exp. 2 (`foreach` "Inscrito na palestra") e remove a nota "¹"; experimentos voltam a lista numerada. Retestado: saída termina com "Inscrito na palestra: Bruno Lima" |
| 8 | **Aplicada** (2026-10-14) — conferida no PDF (9); `Tentar` mantido (professor não vetou). **Defeitos** → prompt 8-B: (a) título "Eventos no Gerenciador…" ficou **abaixo** do Objetivo (nas outras etapas vem antes); (b) sobrou o bloco obsoleto `CancelarInscricao(...)` de 3 linhas (`BuscarEvento(eventoId).CancelarInscricao(participanteId)`, não compila) entre os marcadores e "Por que validar antes de alterar?". Saída do checkpoint com realce azul (8-C opcional) | Card "SEÇÃO 6 · CONTINUAÇÃO" vira Etapa 8. Seção `// ---------- Eventos ----------` delegando ao Evento (CriarEvento busca o Organizador; AlterarEvento só chama `evento.Alterar` + `Gravar`; RemoverEvento recusa "Evento possui inscrições."; Inscrever/CancelarInscricao buscam os dois objetos) + `BuscarEvento` no Apoio. Conceito: tabela "quem verifica cada regra". Program 100% via Gerenciador (só Guids), com função local `Tentar(Action)` para não repetir 6 try/catch (**conceito novo: função local + lambda sem parâmetro — o professor pode vetar**). Removidos: código obsoleto, "Ajuste em Evento.cs", "Nota" do construtor, "Por que consultar os eventos?", "Teste sugerido", "Diagnóstico". Mantidos: "Por que validar antes de alterar?" (reescrito) e "Atenção". Testado: saída do checkpoint; exp. 1 Guids trocados → KeyNotFound "Evento não encontrado."; exp. 2 sem proteção no RemoverEvento → linha de recusa some + KeyNotFound "Evento não encontrado." no AlterarEvento; exp. 3 sem `Gravar` no Inscrever → saída idêntica (ver Pergunta). Código testado em [codigo-guia-ate-etapa8/](codigo-guia-ate-etapa8/) |
| 8-B / 8-C | **Aplicados** — conferidos no PDF (10): título antes do Objetivo, bloco obsoleto removido; saídas de checkpoint das Etapas 2–8 em texto simples | |
| 9 (PDF 10) | **Aplicada com defeito** (2026-10-14): o card "ETAPA 9" ficou com fonte minúscula (o Gamma encolheu tudo para uma página) e **cortado** em `Console.WriteLine("Organizador alterado.");` — não aparecem `Remover`/`Exibir`, Program, Checkpoint, Experimento nem Pergunta → prompt 9-B (dividir em "ETAPA 9" + "ETAPA 9 · CONTINUAÇÃO", fonte normal). O que aparece confere com o código testado. **Ordem dos cards:** "SEÇÃO 6 · INTEGRIDADE" (→ Etapa 12) e "SEÇÕES 7 E 8" (→ 11a/11b) estão entre a Etapa 8 e a Etapa 9, com código obsoleto (pré-D9/D11, `x.Id`, `ParticipanteIds`, "InvalidDataException é uma IOException") → prompt 9-C (mover para depois da Etapa 9 + aviso "em revisão") | |
| 9 | Prompts 9.1–9.4 gerados (2026-10-14) | Card "SEÇÃO 9" vira Etapa 9 (corrige E4 `x.Id` e I8 `default`/`ReadLine` nulo). Console ganha `Entrada.cs` (internal static, namespace `EventoParticipantes.App`: `Texto`, `TextoOpcional` — Enter = null = "não alterar" —, `Id`; **sem `Inteiro`**, que entra na Etapa 10 com o menu de Eventos) e `Menus/MenuOrganizadores.cs` (classe com `Gerenciador` no construtor, `Executar` + C/L/B/A/R privados, `default` "Ação inválida."; **sem herança** — `MenuCadastro` só na Etapa 14). Program: ainda `RepositorioMemoria`; menu só "1-Organizadores \| 0-Sair" (o resto entra na Etapa 10); laço sai com "0" ou `null`; um `catch ... when (ex is ArgumentException or InvalidOperationException or KeyNotFoundException)` (**conceito novo: filtro `when` + `is ... or`**; `IOException` entra na Etapa 11/12). Checkpoint = roteiro em tabela + trecho de sessão. Testado com sessão real (cadastro, e-mail duplicado em maiúsculas, alterar com Enter no nome, listar, Id "abc", ação "X", opção "7", remover, lista vazia, sair). Exp. 1 sem `KeyNotFoundException` no filtro → `Unhandled exception. System.Collections.Generic.KeyNotFoundException: Organizador não encontrado.`; exp. 2 sem teste de `null` + Ctrl+D/Ctrl+Z → laço infinito "Opção inválida." (3 milhões de linhas em 2 s); exp. 3 sem `?? ""` no `Texto` → `warning CS8603: Possible null reference return.` Código em [codigo-guia-ate-etapa9/](codigo-guia-ate-etapa9/) |
| 9-B / 9-C | **Aplicados** — conferidos no PDF (11): Etapa 9 em 2 cards, fonte normal, completa; cards "SEÇÕES 7 E 8" e "SEÇÃO 6 · INTEGRIDADE" movidos para depois da Etapa 10, com aviso "Em revisão" | |
| 10 (PDF 11) | **Aplicada com defeitos** (2026-10-14): (a) espaços largos no meio das frases (causa: prosa dos prompts quebrada a 72 colunas com recuo) no Objetivo, Conceito, Exercício e Dica; (b) card "ETAPA 10" encolhido e **cortado** no `Alterar` do MenuParticipantes; (c) card "ETAPA 10 · MENU PRINCIPAL" encolhido e **cortado** no título "Experimento" (sem experimentos nem Pergunta); (d) "MENU DE EVENTOS" completo, fonte um pouco menor → prompt 10-B (reescrever textos em linha única; dividir em 5 cards) | |
| 10-B1 / 10-B2 | **Aplicados** — conferidos no PDF (12): Etapa 10 em 5 cards (ETAPA 10, MENU DE PARTICIPANTES, MENU DE EVENTOS, MENU PRINCIPAL, CHECKPOINT), fonte normal, sem espaços extras, código completo, 15 passos, 3 experimentos e Pergunta. **Etapa 10 concluída.** Resta o 10-C (cosmético): os Experimentos das Etapas 8 e 10 aparecem como marcadores, não numerados; na Etapa 8 isso regrediu (no PDF 9 estavam numerados) e a Pergunta cita "experimento 3" | |
| 10 | Prompts 10.1–10.4 gerados (2026-10-14) | **Três cards novos** desde o início ("ETAPA 10", "ETAPA 10 · MENU DE EVENTOS", "ETAPA 10 · MENU PRINCIPAL"), para o Gamma não encolher/cortar como na Etapa 9. `Entrada` ganha `Inteiro` e `InteiroOpcional` (Enter = null). Exercício: `MenuParticipantes` (espelho) e `MenuEventos` (Cadastrar pede Id do organizador e capacidade; Exibir usa `x.Organizador.Nome` — associação, sem consultar o Gerenciador; Buscar lista os inscritos; Alterar com nome e capacidade opcionais, D2), com código de referência visível. Program: opções 1–5 + `using EventoParticipantes.App;` (para a `Entrada`). Checkpoint = roteiro de validação inteiro, exceto persistência (15 passos), testado em sessão real com 4 Guids. Exp. 1 sem `using EventoParticipantes.App;` → 4× `CS0103: The name 'Entrada' does not exist in the current context`; exp. 2 `Inteiro` no lugar de `InteiroOpcional` (compila) → Enter na capacidade dá "Informe um número inteiro." e o nome novo não é aplicado; exp. 3 `RemoverOrganizador` no `MenuParticipantes.Remover` (compila) → "Organizador não encontrado." e o Bruno continua listado. Código em [codigo-guia-ate-etapa10/](codigo-guia-ate-etapa10/) |
| 11a | Prompts 11a.1–11a.6 gerados (2026-10-14) — aguardando aplicação | Professor aceitou **cards de apoio** para T1/T4/T5: "APOIO · JSON", "APOIO · ARQUIVOS", "APOIO · DTOs" (antes da 11a). Etapa em 3 cards: "ETAPA 11a" (objetivo, conceito, pastas `Persistencia/Json` e `Json/Dtos`, 3 DTOs), "ETAPA 11a · REPOSITÓRIO" (`RepositorioJson` com `Carregar` vazio até a 11b, `Salvar` = `ParaDto` + `GravarArquivo` com `.tmp`/`finally`), "ETAPA 11a · CHECKPOINT" (Program: `using ...Persistencia.Json`, caminho via `AppContext.BaseDirectory`, mostra o caminho, `new RepositorioJson(arquivo)`; filtro do catch ganha `IOException` e `UnauthorizedAccessException`). Card antigo "SEÇÕES 7 E 8" continua (com aviso) até a 11b. Testado: JSON real (acentos saem como `ç`), sem `.tmp` residual; exp. 1 executar de novo → arquivo só com o novo cadastro (Carregar vazio); exp. 2 serializar `estado` direto → organizador e participantes repetidos dentro do evento, chaves `OrganizadorId`/`EventoId`; exp. 3 sem `Directory.CreateDirectory` (pasta `dados` apagada) → "Operação não concluída: Could not find a part of the path '…/dados/eventos.json.tmp'." e o cadastro aparece no L (memória ≠ arquivo, caixa Atenção). Código em [codigo-guia-ate-etapa11a/](codigo-guia-ate-etapa11a/) |
| 10-C / 11a (PDF 13) | **Conferidos** (2026-10-14, sessão 3). **11a entrou completa e na ordem certa** (APOIO · JSON → ARQUIVOS → DTOs → ETAPA 11a → REPOSITÓRIO → CHECKPOINT → cards antigos com aviso); código idêntico ao testado; fonte normal, nada cortado. **Defeitos:** (a) **10-C não fez efeito**: Experimentos das Etapas 8 e 10 continuam com marcadores — e os da 11a também saíram com marcadores, embora a Pergunta 2 da 11a cite "experimento 3" e o card APOIO · DTOs cite "experimento 2 da Etapa 11a"; (b) Objetivo da ETAPA 11a com espaços largos ("grava os ␣␣␣ dados", "nenhuma linha ␣␣␣ do Negocio"); (c) cosmético: espaço antes de "Compare EventoDto…" (11a), dos rótulos "Escrita segura…" e "Onde fica o arquivo" (APOIO · ARQUIVOS) e faixa "Experimento" mais larga (11a · CHECKPOINT); quebras de nomes longos na tabela do APOIO · ARQUIVOS são só visuais → prompts 11a-B1 (Objetivo e espaços) e 11a-B2 (numerar experimentos 8, 10 e 11a, um card por vez) | |
| 11b | Prompts 11b.1–11b.5 gerados (2026-10-14, sessão 3) — aguardando aplicação | 4 cards novos: "ETAPA 11b" (objetivo, conceito 2×2, `using ...Modelo`, `Carregar` + `LerDto`), "ETAPA 11b · DO ARQUIVO À MEMÓRIA" (`ParaEstado`), "ETAPA 11b · CHECKPOINT" (3 execuções + resposta da Pergunta 1 da Etapa 4), "ETAPA 11b · EXPERIMENTOS" (4 experimentos + Pergunta); **apaga o card "SEÇÕES 7 E 8"**. Program e Negocio não mudam. `Carregar` = `File.Exists` → `ReadAllText` → `LerDto` (`Deserialize`, `?? throw` "Arquivo de dados vazio.", `JsonException` → `InvalidDataException($"Arquivo de dados inválido. {ex.Message}", ex)`) → `ParaEstado` (pessoas pelos construtores com o Guid salvo; evento busca o `Organizador` na lista recriada e chama `Inscrever` para cada Id; Id inexistente → `InvalidDataException` "Evento '…': organizador/participante não encontrado." — **D17**). Testado: checkpoint em 3 execuções (lista vazia sem arquivo; dados e inscrição de volta; remover Bruno recusado; alterar o nome da Shiderlene aparece no evento — mesmo objeto — e persiste); exp. 1 sem `Gravar` no `Inscrever` → Carla continua cadastrada, inscrição dela some ao reabrir (2/2 → 1/2; responde a Pergunta da Etapa 8); exp. 2 sem `File.Exists` e só o arquivo apagado → `Unhandled exception. System.IO.FileNotFoundException: Could not find file '…/dados/eventos.json'.` (com a pasta apagada seria `DirectoryNotFoundException`); exp. 3 e-mail "bruno" no arquivo → `Unhandled exception. System.ArgumentException: E-mail inválido.`; exp. 4 Id do Bruno repetido em `ParticipanteIds` → `Unhandled exception. System.InvalidOperationException: Participante já inscrito.` Código em [codigo-guia-ate-etapa11b/](codigo-guia-ate-etapa11b/) |
| 11a-B1/B2 + 11b (PDF 14) | **Conferidos** (2026-10-15). **11a-B1 ok** (Objetivo sem espaços; espaços sobrando removidos). **11a-B2 parcial:** Etapa 8 numerada; Etapas 10 e 11a **continuam com marcadores**; e a Pergunta da 11a ficou numerada, mas em **fonte menor**. **11b entrou completa**: 4 cards na ordem, código igual ao testado, checkpoint, caixa "Resposta da pergunta da Etapa 4", Pergunta numerada; card "SEÇÕES 7 E 8" apagado. **Defeito:** Experimentos da 11b também saíram com marcadores (a Pergunta 1 cita "experimentos 3 e 4"). Diagnóstico: pedir "transforme em lista numerada" não funciona nesses cards → prompt 12-A reescreve cada bloco Experimento com o número **dentro do texto em negrito** ("1. Esquecer o using:"), sem depender do recurso de lista. D17 aceita (o professor aplicou a 11b sem vetar) | |
| 12 | Prompts 12-A (correções) e 12.1–12.5 gerados (2026-10-15) — aguardando aplicação | 4 cards: "ETAPA 12" (objetivo, conceito 2×2 — quem detecta cada problema do arquivo —, `ValidarIntegridade` + `ExigirUnicos` no fim do Apoio do Gerenciador, chamada no fim do construtor), "ETAPA 12 · PROGRAM" (Gerenciador criado dentro de `try`; filtro com `ArgumentException`, `InvalidOperationException`, `InvalidDataException`, `IOException`, `UnauthorizedAccessException`; mensagem "Não foi possível abrir os dados: …" + "Corrija ou apague o arquivo e execute de novo." + `return;`), "ETAPA 12 · CHECKPOINT" (6 edições do arquivo), "ETAPA 12 · EXPERIMENTOS"; **apaga "SEÇÃO 6 · INTEGRIDADE"**. `ValidarIntegridade` só com Guids (3 cadastros) e e-mails (2) repetidos, via `HashSet<string>(StringComparer.OrdinalIgnoreCase)`, `InvalidOperationException("Dados inconsistentes: …")`. Testado: arquivo normal abre; vírgula → "Não foi possível abrir os dados: Arquivo de dados inválido. The JSON object contains a trailing comma … LineNumber: 6 …"; OrganizadorId inexistente → "…Evento 'Introdução à POO': organizador não encontrado."; e-mail BRUNO@ em outro participante → "…Dados inconsistentes: e-mail repetido entre participantes."; e-mail "bruno" → "…E-mail inválido."; Id repetido em ParticipanteIds → "…Participante já inscrito."; Guid de organizador repetido → "…Dados inconsistentes: Guid repetido entre organizadores." Exp. 1 `return new DadosDto();` no catch do LerDto → `warning CS0168: The variable 'ex' is declared but never used`; com o arquivo quebrado o programa abre vazio, e cadastrar a Carla grava um arquivo só com ela (Shiderlene, Bruno e evento perdidos); exp. 2 sem `or InvalidDataException` → `Unhandled exception. System.IO.InvalidDataException: Arquivo de dados inválido. …`; exp. 3 sem a chamada `ValidarIntegridade();` → abre, e 2, L lista "Bruno L. \| BRUNO@eventos.com" e "Bruno Lima \| bruno@eventos.com". Código em [codigo-guia-ate-etapa12/](codigo-guia-ate-etapa12/) |
| 12-A + 12 (PDF 15) | **Conferidos** (2026-10-15). **12-A funcionou**: Experimentos das Etapas 10, 11a e 11b agora são parágrafos com "**1. Título:**" em negrito (o número no texto resolve o problema da lista); Pergunta da 11a em fonte normal. **Etapa 12 entrou completa**: 4 cards na ordem, código igual ao testado, checkpoint com 6 edições, 3 experimentos numerados, Pergunta; "SEÇÃO 6 · INTEGRIDADE" apagado. **Defeitos cosméticos** → prompt 13-A: (a) 1º marcador do card ETAPA 12 com espaços largos ("o ␣Add" e "já está ␣␣␣ lá:"); (b) tabela do ETAPA 12 · CHECKPOINT com linhas em fonte menor que o cabeçalho e "Edição" quebrado em "Ediçã/o" na 2ª página. Linha `ValidarIntegridade();` sem recuo (só visual) | |
| 13 | Prompts 13-A (correções) e 13.1–13.4 gerados (2026-10-15) — aguardando aplicação | **D18**: `Pessoa` abstrata com `Id`, `Nome`, `Email` (`private set`), construtor `protected` (nome, email, Guid? id), `Alterar`, `Equals` com `GetType() == outra.GetType()` + `GetHashCode`. `Organizador`/`Participante` ficam `sealed`, herdam `: Pessoa` e mantêm `OrganizadorId => Id` / `ParticipanteId => Id` (**apelidos para não mudar Gerenciador, menus nem RepositorioJson** — o professor pode preferir renomear tudo para `Id`; custo: 3 projetos). Construtores mantêm os mesmos parâmetros (`organizadorId`, `participanteId`), então nada fora do Modelo muda. 4 cards: "ETAPA 13" (conceito + Pessoa.cs), "ETAPA 13 · ORGANIZADOR E PARTICIPANTE" (2 arquivos de 10 linhas + resposta da Pergunta da Etapa 3), "ETAPA 13 · CHECKPOINT", "ETAPA 13 · EXPERIMENTOS". Linhas: Organizador 30 + Participante 31 → 10 + 10 + Pessoa 33. Testado com o eventos.json da Etapa 12: abre igual; e-mail BRUNO@ recusado; alterar o nome do Bruno persiste; remover Bruno → "Participante inscrito em evento." e remover Shiderlene → "Organizador possui eventos." (Equals herdado). Exp. 1 `new Pessoa(...)` no Program → `CS0144: Cannot create an instance of the abstract type or interface 'Pessoa'`; exp. 2 `Renomear` no Organizador atribuindo `Nome` → `CS0272: The property or indexer 'Pessoa.Nome' cannot be used in this context because the set accessor is inaccessible`; exp. 3 sem `: base(...)` → `CS7036: There is no argument given that corresponds to the required parameter 'nome' of 'Pessoa.Pessoa(string, string, Guid?)'`; exp. 4 sem `GetType()` no Equals → organizadora e participante com o mesmo Guid: `Mesmo cadastro? True` (com a linha: `False`). Código em [codigo-guia-ate-etapa13/](codigo-guia-ate-etapa13/) |
| 13-A + 13 (PDF 16) | **Conferidos** (2026-10-15). 13-A ok (espaços do 1º marcador da ETAPA 12 corrigidos; "Edição" não quebra mais; linhas da tabela ainda um pouco menores que o cabeçalho — aceitável). **Etapa 13 entrou completa**: 4 cards na ordem, código igual ao testado, checkpoint de 7 passos, caixa "Resposta da pergunta da Etapa 3", 4 experimentos numerados no texto, Pergunta. D18 aceita (aplicada sem veto) | |
| 14 | Prompts 14.1–14.6 gerados (2026-10-15) — aguardando aplicação | Escopo: **`partial` no Gerenciador + `MenuCadastro`** (o roteiro da p. 2 diz "dividir Gerenciador e menus"). **D19**: `MapeadorDados` fica **fora** (o RepositorioJson já separa ParaDto/ParaEstado em métodos; extrair classe seria mais um conceito sem ganho visível) — professor pode pedir. Gerenciador: `public sealed partial class` em 4 arquivos — `Gerenciador.cs` (campos, construtor, MesmoEmail, Gravar, ValidarIntegridade, ExigirUnicos; sai o using de Validacao), `Gerenciador.Organizadores.cs` / `.Participantes.cs` (usings Modelo + Validacao; seção + Buscar…), `Gerenciador.Eventos.cs` (using Modelo; seção + BuscarEvento). Só move código. Menus: `MenuCadastro` abstrata com `protected Gerenciador Sistema`, construtor protected, `protected abstract string Titulo`, `Executar` (o roteiro) e 5 `protected abstract void` ações (Template Method); filhos `sealed : MenuCadastro`, construtor `: base(sistema) { }`, `Titulo =>`, `protected override`, `_sistema.` → `Sistema.`. Program não muda. Linhas: Gerenciador 204 → 59+53+54+58; menus 59/59/67 → 35 (base) + 42/42/50. Testado: checkpoint (títulos vindos do MenuCadastro, ação X → "Ação inválida.", cadastrar/inscrever/cancelar/remover a Carla, remover Shiderlene recusado). Exp. 1 sem `partial` em Gerenciador.Eventos.cs → `CS0260: Missing partial modifier on declaration of type 'Gerenciador'; another partial declaration of this type exists`; exp. 2 sem o Remover no MenuEventos → `CS0534: 'MenuEventos' does not implement inherited abstract member 'MenuCadastro.Remover()'`; exp. 3 `private void Remover()` sem override → mesmo CS0534 + `warning CS0114: 'MenuEventos.Remover()' hides inherited member 'MenuCadastro.Remover()'…`; exp. 4 Program chamando `organizadores.Cadastrar()` → `CS0122: 'MenuOrganizadores.Cadastrar()' is inaccessible due to its protection level`. Código em [codigo-guia-ate-etapa14/](codigo-guia-ate-etapa14/) |
| 14 (PDF 17) | **Conferida** (2026-10-15, sessão 4). 6 cards na ordem (ETAPA 14 → GERENCIADOR.CS → MENUCADASTRO → MENUS FILHOS → CHECKPOINT → EXPERIMENTOS), depois "SEÇÕES 10–13". Código igual ao testado (o Gerenciador.cs do card abrevia `ValidarIntegridade`/`ExigirUnicos` com um comentário — aceitável). Fonte normal, nada cortado, 4 passos e 4 experimentos com número em negrito no texto. **Único defeito:** tabela de linhas do CHECKPOINT diz "de 53 a 59 linhas"; os arquivos têm 59 (núcleo), 52 (Organizadores), 53 (Participantes) e 57 (Eventos) → prompt 14-B ("de 52 a 59"). (A contagem 59+53+54+58 registrada antes estava errada.) | |
| 15 | Prompts 15.1–15.4 gerados (2026-10-15, sessão 4) — aguardando aplicação | Sem código novo (base: [codigo-guia-ate-etapa14/](codigo-guia-ate-etapa14/)). 4 cards: "ETAPA 15" (substitui o topo de "SEÇÕES 10–13": objetivo, `dotnet build` + `dotnet run --project EventoParticipantesApp.Console`, apagar a pasta `dados` antes), "ETAPA 15 · ROTEIRO" (Roteiro de validação funcional concreto, 8 linhas em 3 execuções, coluna **Etapas**), "ETAPA 15 · QUESTÕES" (4 questões reescritas apontando as etapas), "ETAPA 15 · SÍNTESE" (Próximo desafio reescrito: `Inscricao` com Id, Participante, Data e Situacao; Evento guarda coleção protegida de `Inscricao`; capacidade conta só inscrições ativas). Testado: comando antigo → `The provided file path does not exist: EventoParticipantes.Console/EventoParticipantes.Console.csproj.`; roteiro em 3 execuções reais sem arquivo (cadastro; alterar Bruno → mesmo Id; já inscrito; lotado; remover Bruno recusado; reabrir → "Palestra de POO \| Shiderlene Souza \| 1/1 inscritos" e Bruno L. Lima + Carla; cancelar, remover evento, "Nenhum evento cadastrado."; reabrir → continua sem eventos); arquivo com vírgula sobrando → "Não foi possível abrir os dados: Arquivo de dados inválido. …"; `public List<…> { get; private set; }` aceita `Add` de fora (2 inscritos iguais) — base da questão 4 |
| (histórico) | | Para a 12, já testado com o código da 11b (todos encerram com `Unhandled exception` antes do menu): vírgula sobrando → `InvalidDataException: Arquivo de dados inválido. The JSON object contains a trailing comma at the end which is not supported in this mode. … LineNumber: 6 …`; arquivo vazio → "…The input does not contain any JSON tokens…"; conteúdo `null` → "Arquivo de dados vazio."; OrganizadorId inexistente → "Evento 'Introdução à POO': organizador não encontrado."; Id de participante inexistente → "…participante não encontrado."; capacidade 0 → `ArgumentException: Capacidade deve ser maior que zero.`; **e-mail repetido (BRUNO@ × bruno@) entre participantes é aceito** → é o que sobra para `ValidarIntegridade` (com Guids repetidos) |

## Tópicos de apoio

Assuntos que os alunos talvez desconheçam, a detalhar depois: ver [topicos-de-apoio.md](topicos-de-apoio.md).
T1 JSON · T2 Interfaces (sem herança) · T3 Arquitetura em camadas, projetos e pastas ·
**T4 Manipulação de arquivos (System.IO)** — pedido do professor em 2026-10-14: fundamentar no guia
(`File`, `Path`, `Directory`, `AppContext.BaseDirectory`, arquivo temporário, `try/finally`, exceções de IO).
**Aguardar o pedido** para gerar os prompts; incluir junto com as Etapas 11a/11b. ·
**T5 DTOs** — verificado em 2026-10-14: o guia só traz a motivação (3 menções curtas), falta fundamentação
(definição, DTO × entidade, mapeamento nos dois sentidos, objetos na memória × Ids no arquivo); o código atual
dos DTOs é anterior à D11. Também **aguardar o pedido**.

## Gabaritos do professor (não vão para o Gamma)

### Etapa 0 — classificação das responsabilidades
| # | Responsabilidade | Camada | Por quê |
|---|---|---|---|
| 1 | Mostrar o menu e ler a opção digitada | Apresentação | Interação com o usuário |
| 2 | Converter o texto digitado em `Guid` | Apresentação | É formato da entrada, não regra do domínio |
| 3 | Verificar se o e-mail tem formato válido | Negócio — entidade | O objeto protege o próprio estado |
| 4 | Impedir dois organizadores com o mesmo e-mail | Negócio — serviço | Uma entidade isolada não conhece os outros cadastros |
| 5 | Recusar inscrição em evento lotado | Negócio — entidade (`Evento`) | O evento conhece capacidade e inscritos |
| 6 | Impedir a remoção de participante inscrito | Negócio — serviço | Exige consultar todos os eventos |
| 7 | Recusar capacidade menor que as inscrições ativas | Negócio — entidade (`Evento`) | Questão investigativa da etapa |
| 8 | Gravar os dados em arquivo JSON | Persistência | Formato de armazenamento |
| 9 | Recriar os objetos ao abrir o programa | Persistência **lê**; entidades **revalidam** | Pegadinha: duas camadas colaboram |
| 10 | Mostrar "Operação não concluída: …" | Apresentação | Exibir resultado |
| 11 | Gerar o `Guid` de um novo cadastro | Negócio — entidade | Identidade nasce com o objeto |
| 12 | Decidir em qual pasta fica o arquivo de dados | Apresentação (composição no `Program`) | Persistência só usa o caminho recebido; bom para discussão |

### Etapa 8 — Pergunta (esquecer `Gravar` no `Inscrever` não muda a saída)
O `EstadoSistema` copia as **listas** (arrays próprios), mas não os **objetos**: o `RepositorioMemoria`
guarda as mesmas instâncias de `Evento` que o Gerenciador usa. Por isso, na Etapa 6, remover um
organizador da lista sem gravar ficou invisível para o repositório (2 organizadores), mas aqui inscrever
altera o **interior** de um `Evento` que o repositório já tem — ele "vê" a mudança sem `Gravar()`.
Com o JSON (Etapa 11), o arquivo só muda quando `Salvar` é chamado: a inscrição se perderia ao reiniciar
(se nenhuma outra operação gravasse depois). Lição: o repositório em memória não prova que `Gravar()` foi chamado.

### Etapa 9 — Pergunta (de onde vem cada mensagem)
"E-mail de organizador já cadastrado." nasce no **Gerenciador** (Negócio – serviço); "Identificador inválido."
nasce na **Entrada** (Apresentação: converter texto em Guid, linha 2 da tabela da Etapa 0); "Ação inválida." e
"Opção inválida." são do menu (Apresentação). O menu só mostra a mensagem que recebeu. Trocando o console por
uma página web, muda só o projeto Console (Program, Entrada, Menus); Negocio e Persistencia ficam iguais.

### Etapa 10 — Pergunta
1. Em `Executar`, só mudam o título ("--- Eventos ---") e o objeto que recebe as chamadas; nas ações,
   repetem-se o roteiro "ler → chamar o Gerenciador → mostrar mensagem" e o `Exibir`. É a duplicação que a
   Etapa 14 elimina com `MenuCadastro` (Template Method).
2. Passo 12 do checkpoint: quem impediu foi a **Entrada** — os argumentos são avaliados da esquerda para a
   direita, antes da chamada; `InteiroOpcional("dois")` lança `ArgumentException` e o `AlterarEvento` **nem é
   chamado**. Nem o Evento nem o Gerenciador chegaram a ver o nome novo.

### Etapa 11b — Pergunta
1. O `Carregar` roda dentro do construtor do `Gerenciador`, criado **antes** do `while` e fora do `try`: a exceção
   não passa pelo `catch` do laço. Quem deve tratar é o `Program` (apresentação), envolvendo a criação do
   Gerenciador — Etapa 12. O repositório não deve "engolir" o erro e devolver vazio: o próximo `Gravar`
   apagaria os dados do arquivo.
2. Com XML, viria uma exceção de outra biblioteca (`XmlException`); com a conversão, o `Program` sempre recebe
   `InvalidDataException`. A tecnologia do arquivo é decisão da Persistência e não deve vazar para as outras camadas.
3. O `Evento` precisa receber o objeto `Organizador` (construtor) e os objetos `Participante` (`Inscrever`):
   eles têm de existir antes. Também precisam ser **os mesmos objetos** das listas, não cópias — por isso o
   nome alterado da organizadora aparece no evento.

### Etapa 12 — Pergunta
1. E-mail repetido é regra do **conjunto** (Negócio – serviço, linha 4 da tabela da Etapa 0): só o Gerenciador
   vê todos os cadastros. Organizador inexistente aparece **antes**, na tradução Id → objeto (D17): o Evento
   precisa do objeto no construtor, então o `ParaEstado` é o primeiro a perceber. Cada verificação fica onde a
   informação está disponível.
2. Abrir vazio parece "funcionar", mas o primeiro `Gravar` substitui o arquivo inteiro (exp. 1): o erro vira
   **perda de dados** silenciosa. Recusar a abertura preserva o arquivo para ser corrigido.

### Etapa 13 — Pergunta
1. Apagar `OrganizadorId`/`ParticipanteId` obrigaria a trocar por `Id` no Gerenciador, nos três menus e no
   `RepositorioJson` (três projetos). Os apelidos mantêm o **contrato público** das classes: quem usava
   continua funcionando, e a refatoração fica restrita ao Modelo. (Renomear de uma vez também é válido.)
2. `abstract` impede criar uma "pessoa genérica": no sistema, toda pessoa é organizador ou participante, e
   o Gerenciador só tem cadastros desses dois tipos. Uma `Pessoa` solta não caberia em nenhuma lista nem
   teria regra de unicidade definida.

### Etapa 14 — Pergunta
1. O total de linhas quase não muda; o que melhora é **onde** cada coisa mora. Uma regra de evento está em
   `Gerenciador.Eventos.cs`; o roteiro dos menus, só no `MenuCadastro`. Arquivos menores, cada um com um
   assunto (coesão), e a mudança de um assunto não obriga a abrir os outros.
2. `Executar` é o roteiro que garante título, leitura da ação e `default`; chamar `Cadastrar` direto pularia
   esse roteiro. `protected` deixa as ações visíveis só para a base e os filhos: o Program conhece apenas
   `Executar`.

## Pendências no código do repositório (para o gabarito bater com o guia)

- [ ] D2: `Evento.Alterar(nome?, capacidade?)` e `Gerenciador.AlterarEvento`; menu de Eventos com "Alterar" completo.
- [ ] D1: se escolhida a carga via `Inscrever()`, ajustar `MapeadorDados`/`Evento` e simplificar `ValidarIntegridade`.
- [x] Dados ficam em `bin/.../dados/eventos.json`. Verificado em 2026-10-14: `dotnet clean` **não** os apaga; apagar a pasta `bin` apaga. Mantido e explicado no card "APOIO · ARQUIVOS".
- [ ] A pasta `2026_S2/EventoParticipantesApp` ainda não está versionada no git.
