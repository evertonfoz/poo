# Registro de Implementação — Passo 2: Núcleo de Domínio e Regras de Negócio

Este documento formaliza a implementação do **Núcleo de Domínio (Domain Core)** no projeto **`SistemaGestaoEventosAcademicos.Negocio`**, em estrita conformidade com o [Diagrama de Classes de Projeto (DCD)](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/docs/projeto/01-diagrama-de-classes-de-projeto.md) e o [Catálogo de Regras de Negócio](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/docs/requisitos/02-regras-de-negocio.md).

---

## 1. Contexto e Objetivo da Sessão

- **Data da Execução:** 19/11/2026
- **Ambiente de Desenvolvimento:** macOS, SDK .NET 10 (`10.0.103`), C# 13
- **Camada Implementada:** `SistemaGestaoEventosAcademicos.Negocio` (núcleo puro e isolado, zero dependências externas)
- **Status da Compilação:** Compilação com êxito (0 erros, 0 avisos).

---

## 2. Componentes Implementados

### 2.1. Enums de Domínio (`Enums/`)
- **[StatusEvento.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Negocio/Enums/StatusEvento.cs):** Estados `Previsto`, `InscricoesAbertas`, `EmAndamento`, `Concluido`, `Cancelado` (**RN13**).
- **[TipoAtividade.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Negocio/Enums/TipoAtividade.cs):** Modalidades `Palestra`, `Minicurso`, `Workshop`, `MesaRedonda`.
- **[StatusInscricao.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Negocio/Enums/StatusInscricao.cs):** Estados `Ativa`, `Cancelada`.

### 2.2. Exceção de Domínio (`Excecoes/`)
- **[RegraNegocioException.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Negocio/Excecoes/RegraNegocioException.cs):** Exceção especializada para sinalização de inconsistências e violações de regras invariantes.

### 2.3. Entidades e Modelos de Domínio Rico (`Entidades/`)
- **[Participante.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Negocio/Entidades/Participante.cs):**
  - Identificador: `ParticipanteId`.
  - Propriedades com padrão enxuto `{ get; private set; }`.
  - Validação de obrigatoriedade (**RN11**).
  - Algoritmo de validação de CPF (11 dígitos, verificação contra sequências iguais e cálculo dos dígitos verificadores) (**RN09**).
  - Validação de formato de e-mail via `MailAddress` e regex (**RN10**).
  - Métodos `AtualizarDados()` e `AtualizarCpf()`.
- **[Atividade.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Negocio/Entidades/Atividade.cs):**
  - Identificadores: `AtividadeId` e referência ao evento pai `EventoId`.
  - Propriedades com padrão enxuto `{ get; private set; }`.
  - Invariantes de datas (`DataHoraFim > DataHoraInicio`) (**RN02**).
  - Vagas maiores que zero e validação de redução de vagas em relação aos inscritos atuais (**RN15**).
  - Métodos `CalcularDuracaoEmHoras()` (**RN25**), `ConflitaHorarioCom()` (**RN06**) e `PossuiSobreposicaoLocal()` (**RN18**).
- **[Evento.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Negocio/Entidades/Evento.cs):**
  - Identificador: `EventoId`.
  - Propriedades com padrão enxuto `{ get; private set; }` e encapsulamento de `_atividades` exposto imutável via `Atividades`.
  - Validação de cronograma e data de início futura no cadastro (**RN01**).
  - Capacidade máxima e carga horária estritamente positivas (**RN12**).
  - Máquina de estados com validação em `AlterarStatus()` (**RN13**) e bloqueio de mutações em eventos concluídos/cancelados via `GarantirModificavel()` (**RN08**).
  - Composição de atividades filhas com validação temporal (**RN02**), limite de vagas (**RN15**) e conflito de sala/local (**RN18**).
  - Remoção segura de atividades bloqueada se houver inscritos (**RN24**).
- **[Inscricao.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Negocio/Entidades/Inscricao.cs):**
  - Identificador: `InscricaoId`.
  - Propriedades com padrão enxuto `{ get; private set; }` e encapsulamento de `_atividadesInscritas` exposto imutável via `AtividadesInscritas`.
  - Validação de período hábil de inscrição no evento (**RN14**).
  - Inscrição em atividades com validação de pertencimento (**RN05**), unicidade (**RN16**), limite de vagas (**RN17**) e choque de horário (**RN06**).
  - Cancelamento em cascata com liberação atômica das vagas de atividades vinculadas (**RN19**) e prazo limite (**RN20**).
  - Registro de presença (check-in) condicionado a evento em andamento (**RN21**), inscrição ativa (**RN23**) e tratamento amigável de idempotência (**RN22**).
  - Emissão de certificado com validação de elegibilidade (**RN07**) e fidelidade da carga horária (**RN25**).
- **[Certificado.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Negocio/Entidades/Certificado.cs):**
  - Modelo imutável contendo os dados do participante, evento, carga horária e código de autenticação único (`InscricaoId`).

---

## 3. Matriz de Cobertura das Regras de Negócio Implementadas

| Regra | Descrição | Onde está garantida no código |
| :--- | :--- | :--- |
| **RN01** | Cronograma do Evento | `Evento(construtor)`, `Evento.AtualizarDados` |
| **RN02** | Atividade contida no Evento | `Atividade(construtor)`, `Evento.AdicionarAtividade` |
| **RN05** | Atividade do mesmo Evento | `Inscricao.InscreverAtividade` |
| **RN06** | Conflito de Horário entre Atividades | `Atividade.ConflitaHorarioCom`, `Inscricao.PossuiConflitoHorario` |
| **RN07** | Elegibilidade para Certificado | `Inscricao.EmitirCertificado` |
| **RN08** | Restrição em Evento Encerrado | `Evento.GarantirModificavel`, `Inscricao.Cancelar` |
| **RN09** | Validação de CPF | `Participante.ValidarCpf`, `Participante.AtualizarCpf` |
| **RN10** | Validação de E-mail | `Participante.ValidarEmail`, `Participante.AtualizarDados` |
| **RN11** | Obrigatoriedade de Dados | `Participante(construtor)`, `Participante.AtualizarDados` |
| **RN12** | Capacidade e Carga Positivas | `Evento(construtor)`, `Evento.AtualizarDados` |
| **RN13** | Máquina de Estados de Evento | `Evento.AlterarStatus` |
| **RN14** | Inscrições Abertas | `Inscricao(construtor)` |
| **RN15** | Capacidade Máxima da Atividade | `Atividade(construtor)`, `Evento.AdicionarAtividade` |
| **RN16** | Unicidade de Inscrição em Atividade | `Inscricao.InscreverAtividade` |
| **RN17** | Limite de Vagas em Atividade | `Inscricao.InscreverAtividade` |
| **RN18** | Conflito de Local entre Atividades | `Atividade.PossuiSobreposicaoLocal`, `Evento.ValidarConflitoLocalAtividades` |
| **RN19** | Cancelamento em Cascata | `Inscricao.Cancelar` |
| **RN20** | Prazo Limite para Cancelamento | `Inscricao.ValidarPrazoCancelamento` |
| **RN21** | Janela de Registro de Presença | `Inscricao.ConfirmarPresenca` |
| **RN22** | Idempotência de Check-in | `Inscricao.ConfirmarPresenca` |
| **RN23** | Check-in exclusivo para Inscrição Ativa | `Inscricao.ConfirmarPresenca` |
| **RN24** | Bloqueio de Exclusão de Atividade com Inscritos | `Evento.RemoverAtividade` |
| **RN25** | Carga Horária Certificada Fiel | `Inscricao.EmitirCertificado`, `Atividade.CalcularDuracaoEmHoras` |

---

## 4. Próxima Etapa: Passo 3 — Camada de Persistência

Com o núcleo de domínio integralmente implementado e compilando sem erros, o próximo passo consiste na implementação da camada **`SistemaGestaoEventosAcademicos.Persistencia`**:
1. `EventoRepositorio`: Gerenciamento e consultas em memória para `Evento`.
2. `ParticipanteRepositorio`: Gerenciamento em memória e validação de unicidade de CPF (**RN09**) e E-mail (**RN10**).
3. `InscricaoRepositorio`: Gerenciamento em memória, controle de capacidade do evento (**RN03**), unicidade de inscrição (**RN04**) e contagens de inscritos por evento e por atividade.
