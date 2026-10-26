# Tópicos de apoio (a detalhar)

Assuntos que o guia usa e que os alunos talvez ainda não conheçam. Cada tópico será
detalhado depois, sempre no contexto do EventoParticipantes, com o mínimo necessário
para acompanhar as etapas. Plano geral: [plano-guia-etapas.md](plano-guia-etapas.md).

Registrado em 2026-10-13. Status de todos: **a detalhar**.

---

## T1. JSON

- **Onde aparece:** Etapas 11a (salvar), 11b (carregar) e 12 (experimentos editando o arquivo à mão).
- **Por que o aluno precisa:** vai abrir, ler e editar o `eventos.json`; precisa reconhecer a estrutura
  para entender o que os DTOs representam e por que um arquivo "quebrado" é recusado.
- **Escopo previsto:**
  - O que é: texto para guardar e trocar dados; objeto `{ }`, lista `[ ]`, par `"chave": valor`.
  - Tipos de valor: texto, número, `true`/`false`, `null`.
  - Ler o `eventos.json` real do projeto e ligar cada parte a uma classe (`DadosDto`, `PessoaDto`, `EventoDto`).
  - Serializar (objeto → texto) e desserializar (texto → objeto) com `System.Text.Json`, só no nível de uso.
  - Por que a entidade não é gravada direto (setters privados) → motivação dos DTOs.
  - Erros típicos ao editar à mão: vírgula sobrando, aspas faltando, `null` onde não pode.
- **Fora do escopo:** JSON Schema, atributos de serialização, conversores customizados.

## T2. Interfaces (só o necessário para o material)

- **Onde aparece:** Etapa 5 (`IRepositorioSistema`), Etapas 6 e 11a (troca de `RepositorioMemoria` por `RepositorioJson`).
- **Por que o aluno precisa:** o `Gerenciador` depende de um contrato, não de uma classe concreta;
  é isso que permite trocar memória por JSON mudando uma linha no `Program`.
- **Escopo previsto:**
  - Interface como **contrato**: declara *o que* fazer (`Carregar`, `Salvar`), não *como*.
  - Uma classe **implementa** a interface (`: IRepositorioSistema`) e é obrigada a cumprir o contrato
    (mostrar o erro de compilação quando falta um método).
  - Variável/parâmetro do tipo da interface recebendo objetos de classes diferentes.
  - Não é possível fazer `new IRepositorioSistema()`.
  - Convenção de nome com `I`.
  - Quem define o contrato (Negocio) × quem implementa (Persistencia) → ligação com T3 e com a direção das referências.
- **Fora do escopo:** herança de classes, classes abstratas, polimorfismo em profundidade,
  implementação padrão em interface, múltiplas interfaces, genéricos. (Herança entra só na Etapa 13.)

## T3. Arquitetura em camadas, em projetos e em pastas

- **Onde aparece:** Etapa 0 (camadas como ideia), Etapa 1 (projetos e referências), todas as etapas
  (pastas e namespaces), Etapa 14 (reorganização).
- **Por que o aluno precisa:** são três níveis diferentes de organização que costumam ser confundidos.
- **Escopo previsto:**
  - **Camadas (conceito):** Apresentação, Negócio, Persistência — *responsabilidades*, independem de como os arquivos estão organizados.
  - **Projetos (físico/compilação):** cada camada vira um `.csproj`; a Solution agrupa; as **referências**
    impõem a direção das dependências (Console → Negocio, Console → Persistencia, Persistencia → Negocio;
    Negocio não referencia ninguém). Experimento: referência circular dá erro.
  - **Pastas (organização interna):** `Modelo/`, `Contratos/`, `Servico/`, `Validacao/`, `Json/`, `Memoria/`, `Menus/`;
    pasta ↔ namespace (`RootNamespace`); `internal` × `public` entre projetos.
  - Quadro comparativo dos três níveis: o que cada um garante e o que não garante
    (ex.: pastas não impedem dependência errada; projetos impedem).
- **Fora do escopo:** Clean/Hexagonal/Onion, microsserviços, injeção de dependência com contêiner.

## T4. Manipulação de arquivos (System.IO)

Pedido do professor em 2026-10-14: **fundamentar no próprio guia** as classes e os métodos de arquivo usados
no `RepositorioJson` e no `Program`. **Aguardar o pedido de geração de prompts**: entra junto com as Etapas
11a/11b, e a Etapa 12 cobre a parte de erros.

- **Onde aparece:** Etapa 11a (`Salvar` com arquivo temporário; caminho montado no `Program`), Etapa 11b
  (`Carregar`), Etapa 12 (erros de inicialização: arquivo inválido ou ilegível).
- **Por que o aluno precisa:** o código do `RepositorioJson` usa vários membros de `System.IO` sem explicação.
  O aluno precisa entender o que cada um faz e por que a escrita é "segura".
- **Itens que o guia já usa (PDF, cards "SEÇÕES 7 E 8" e "SEÇÃO 9"):**
  - `File.Exists`, `File.ReadAllText`, `File.WriteAllText`, `File.Move(origem, destino, overwrite: true)`, `File.Delete`.
  - `Path.Combine`, `Path.GetFullPath`, `Path.GetDirectoryName`.
  - `Directory.CreateDirectory` (não falha se a pasta já existe).
  - `AppContext.BaseDirectory`: o arquivo fica na pasta de saída (`bin/...`), não na pasta do terminal. Ele é apagado no `dotnet clean` (ver pendência no plano).
  - `try/finally` para apagar o `.tmp`.
  - Estratégia de escrita segura: gravar em `eventos.json.tmp` e depois substituir o original com `Move`. Se a gravação falhar no meio, o arquivo bom continua intacto.
  - Exceções: `IOException` (e derivadas, como `FileNotFoundException`, `DirectoryNotFoundException`), `UnauthorizedAccessException` (**não** é `IOException` — verificar), `JsonException`, `InvalidDataException` (**não** é `IOException`; fato verificado, corrige E7).
- **Escopo previsto:**
  - Caminho absoluto × relativo; separador de pastas (`Path.Combine` em vez de concatenar `"/"`).
  - Ler e gravar o arquivo inteiro como texto (`ReadAllText`/`WriteAllText`) — suficiente para o projeto.
  - Por que o arquivo temporário (falha no meio da escrita) e o papel do `finally`.
  - Quais exceções cada operação pode lançar, e quem as captura (repositório × `Program`).
  - Experimentos possíveis (testar no .NET 10 antes): apagar a pasta `dados` e ver que ela é recriada; deixar o arquivo somente leitura; abrir o `.json` gerado; `dotnet clean` apaga os dados.
- **Fora do escopo:** streams (`FileStream`, `StreamReader`/`StreamWriter`), leitura assíncrona, `FileSystemWatcher`, travas entre processos.
- **Formato:** decidir com o professor se vira um bloco "Conceito" dentro da Etapa 11a, ou um card de apoio próprio antes dela, ligado ao T1 (JSON).

## T5. DTOs (objetos de transferência de dados)

Pedido do professor em 2026-10-14: verificar se o guia fundamenta os DTOs. **Aguardar o pedido de geração de
prompts**; entra junto com as Etapas 11a/11b (e com o T1 e o T4).

- **O que o guia tem hoje** (PDF (10), cards "SEÇÕES 7 E 8"): só três menções curtas:
  1. a introdução ("converte os dados para representações de armazenamento (DTOs), evitando abrir setters públicos");
  2. o card "02 Deserializar para DTOs" (tipos intermediários mantêm os setters privados das entidades);
  3. a caixa "Por que DTOs?" (setters públicos porque o `System.Text.Json` precisa; `internal`, não vazam do projeto).
  → Há a **motivação**, mas não a **fundamentação**.
- **O que falta:**
  - o significado da sigla (*Data Transfer Object*) e a definição: objeto só com dados, sem regras nem validação, cujo formato acompanha o arquivo;
  - DTO × entidade, lado a lado (`Organizador` × `PessoaDto`: construtor que valida × `{ get; set; }`; `private set` × `set` público);
  - **mapeamento nos dois sentidos**: entidade → DTO no `Salvar`; DTO → entidade no `Carregar`, **pelos construtores**, que revalidam (linha 9 da tabela da Etapa 0);
  - **objetos na memória, Ids no arquivo** (D11): `Evento.Organizador` vira `OrganizadorId`, e `Evento.Participantes` vira `ParticipanteIds`. Na carga, os Ids voltam a ser objetos (buscar o `Organizador`; `Inscrever` cada `Participante`). Explicar por que o arquivo guarda Ids e não cópias (a mesma pessoa repetida em vários eventos; alterar o nome em um lugar só);
  - por que os DTOs ficam na **Persistencia** e não no Negocio (o formato do arquivo é decisão da persistência — ligação com o T3);
  - `DadosDto` como raiz, espelhando a estrutura do `eventos.json` (ligação com o T1);
  - `PessoaDto` serve para Organizador e Participante (os dados são os mesmos) — antecipa a pergunta da Etapa 13 (`Pessoa`);
  - detalhes de sintaxe: `= ""` e `= new()` nos DTOs (evitar `null` quando o JSON não traz o campo).
- **Código obsoleto nos cards atuais** (anterior às D9/D11, não compila com o Evento do guia): `x.Id` e `x.OrganizadorId` no Evento, `x.ParticipanteIds`, `new Evento(dto.Nome, dto.OrganizadorId, ...)` e `evento.Inscrever(participanteId)` com Guid. A Etapa 11a/11b reescreve.
- **Experimentos possíveis (testar no .NET 10 antes):**
  - serializar uma entidade direto (`JsonSerializer.Serialize(organizador)`) funciona;
  - desserializar a entidade direto falha ou ignora campos (construtor com parâmetros/`private set`) — confirmar a mensagem real;
  - JSON com e-mail inválido → o construtor recusa na carga;
  - serializar um `Evento` direto repete o organizador e os participantes inteiros dentro de cada evento.
- **Fora do escopo:** AutoMapper, atributos de serialização (`[JsonPropertyName]`), `record`, conversores.
- **Ligações:** T1 (JSON), T4 (arquivos), Etapa 14 (`MapeadorDados` extrai o mapeamento do `RepositorioJson`).

---

## Próximos tópicos

(O professor vai acrescentar outros.)
