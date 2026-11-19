# Registro de Implementação — Passo 3: Planejamento da Apresentação com Avalonia UI e Diagnóstico do Sistema

Este documento consolida o diagnóstico do estado atual do sistema, formaliza o feedback técnico da solução e define a arquitetura para a nova camada de apresentação rica baseada em **Avalonia UI**, além de preparar o caminho para a posterior implementação da camada de persistência.

---

## 1. Diagnóstico do Estado Atual da Solução

### 1.1. Camada de Domínio (`SistemaGestaoEventosAcademicos.Negocio`) — Concluída
A camada de domínio puro e regras de negócio está plenamente implementada, compilada com zero erros/avisos e com 100% de cobertura das regras descritas no [Catálogo de Regras de Negócio](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/docs/requisitos/02-regras-de-negocio.md):
- **Entidades com Domínio Rico:**
  - `Participante`: Encapsulamento de dados, validação de formato de e-mail e validação algorítmica de CPF (11 dígitos, sequências e cálculo dos dígitos verificadores) (**RN09**, **RN10**, **RN11**).
  - `Atividade`: Invariantes de datas, limite de vagas, cálculo de duração e métodos de detecção de choque de horário (`ConflitaHorarioCom`) e choque de local (`PossuiSobreposicaoLocal`) (**RN02**, **RN06**, **RN15**, **RN18**, **RN25**).
  - `Evento`: Composição forte com atividades filhas, máquina de estados com transições válidas (`AlterarStatus`), validações temporais e bloqueio de alterações em eventos encerrados (`GarantirModificavel`) (**RN01**, **RN08**, **RN12**, **RN13**, **RN24**).
  - `Inscricao`: Validação de período aberto para inscrição, inscrição em atividades do evento sem conflito de horário e sem exceder vagas, cancelamento em cascata liberando vagas atomicamente, credenciamento/check-in idempotente e emissão de certificado (**RN05**, **RN06**, **RN07**, **RN14**, **RN16**, **RN17**, **RN19**, **RN20**, **RN21**, **RN22**, **RN23**, **RN25**).
  - `Certificado`: Modelo de valor imutável com autenticação por chave única.
- **Comunicação de Falhas e Invariantes:**
  - `RegraNegocioException` é lançada de forma consistente em todas as falhas de regra, permitindo captura elegante e feedback visual para o usuário sem quebras na aplicação.

### 1.2. Camada de Persistência (`SistemaGestaoEventosAcademicos.Persistencia`) — Próxima Etapa
A estrutura de projeto está configurada, contendo a pasta `Repositorios/` com `.gitkeep`.
A ser implementado na próxima etapa para suportar a UI gráfica:
- `EventoRepositorio`: Gerenciamento e consultas em memória para `Evento`.
- `ParticipanteRepositorio`: Gerenciamento em memória e validações de unicidade de CPF (**RN09**) e E-mail (**RN10**).
- `InscricaoRepositorio`: Gerenciamento em memória, controle de capacidade do evento (**RN03**), unicidade de inscrição (**RN04**) e contagens de inscritos por atividade.
- `DadosIniciaisSemente`: Carga de dados iniciais para facilitar testes visuais e demonstrações.

### 1.3. Camada de Apresentação Console (`SistemaGestaoEventosAcademicos.Apresentacao`)
Estrutura inicial de terminal criada no setup da solução.

---

## 2. Decisão Arquitetural: Nova Camada de Apresentação Desktop com Avalonia UI

Para disponibilizar uma experiência de usuário rica, intuitiva e moderna (UI/UX) mantendo os princípios de separação de responsabilidades da arquitetura em camadas, foi definida a criação do projeto:
**`SistemaGestaoEventosAcademicos.Apresentacao.Desktop`**

### 2.1. Diretrizes Técnicas (Consultadas via Context7)
- **Framework:** Avalonia UI v12 (`Avalonia`, `Avalonia.Desktop`, `Avalonia.Themes.Fluent`, `Avalonia.Fonts.Inter`).
- **Padrão Arquitetural:** MVVM (Model-View-ViewModel) com `CommunityToolkit.Mvvm`.
- **Target Framework:** `.NET 10` (`net10.0`), alinhado a toda a solução.
- **Tema Visual:** `FluentTheme` (aparência moderna compatível nativamente com macOS, Windows e Linux, com suporte a modo claro e escuro).

### 2.2. Diretrizes de UI/UX
- **Janela Principal (`MainWindow`):**
  - Layout mestre baseado em `SplitView` lateral colapsável/expansível.
  - Ícone de menu hambúrguer no topo para alternar entre modo compacto (apenas ícones) e expandido (ícones + rótulos).
  - Cabeçalho superior moderno com título dinâmico da página atual e badges de status.
  - Painel de conteúdo central dinâmico que alterna views de forma desacoplada via bindings de ViewModel.
- **Navegação Modular:**
  1. 🏠 **Início / Dashboard:** Resumo visual do sistema (indicadores rápidos de eventos, participantes e ocupação).
  2. 📅 **Eventos e Atividades:** Listagem, filtro, cadastro e gerenciamento de oficinas/palestras.
  3. 👥 **Participantes:** Cadastro com validação de CPF e e-mail e consulta rápida.
  4. 📝 **Inscrições:** Inscrição em eventos, seleção de atividades com prevenção visual de choque de horários e cancelamento.
  5. 🎟️ **Credenciamento:** Registro de presença (check-in) rápido e idempotente com feedback visual instantâneo.
  6. 📊 **Relatórios e Certificados:** Consulta de lotação e emissão de certificados acadêmicos.

---

## 3. Scaffold Implementado com Êxito

### 3.1. Estrutura de Arquivos Criada
- **Projeto:** [SistemaGestaoEventosAcademicos.Apresentacao.Desktop.csproj](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/SistemaGestaoEventosAcademicos.Apresentacao.Desktop.csproj)
  - Configurado com Avalonia UI v12 (`Avalonia`, `Avalonia.Desktop`, `Avalonia.Themes.Fluent`, `Avalonia.Fonts.Inter`) e `CommunityToolkit.Mvvm`.
- **Ponto de Entrada e Configuração:**
  - [Program.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/Program.cs): Inicialização desktop com `.UsePlatformDetect()`, `.WithInterFont()` e `.LogToTrace()`.
  - [App.axaml](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/App.axaml) e [App.axaml.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/App.axaml.cs): Configuração do tema `FluentTheme` e injeção do `ViewLocator`.
  - [ViewLocator.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/ViewLocator.cs): Resolução automática desacoplada entre ViewModels e Views.
- **Modelos de Apresentação:**
  - [MenuItemModel.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/Models/MenuItemModel.cs): Registro dos itens de navegação (ícone, título, subtítulo e tipo do ViewModel).
- **ViewModels (`ViewModels/`):**
  - [ViewModelBase.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/ViewModels/ViewModelBase.cs): Base com `ObservableObject`.
  - [MainWindowViewModel.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/ViewModels/MainWindowViewModel.cs): Gestão do estado do `SplitView` (aberto/fechado), lista de menu e seleção da página ativa.
  - [DashboardViewModel.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/ViewModels/DashboardViewModel.cs): Métricas e visão geral da aplicação.
  - [EventosViewModel.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/ViewModels/EventosViewModel.cs): Gestão de eventos e cronograma.
  - [ParticipantesViewModel.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/ViewModels/ParticipantesViewModel.cs): Cadastro e busca de participantes.
  - [InscricoesViewModel.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/ViewModels/InscricoesViewModel.cs): Gerenciamento de inscrições e histórico.
  - [CredenciamentoViewModel.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/ViewModels/CredenciamentoViewModel.cs): Check-in ágil com feedback visual.
  - [RelatoriosViewModel.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/ViewModels/RelatoriosViewModel.cs): Relatórios de ocupação e emissão de certificados.
- **Views (`Views/`):**
  - [MainWindow.axaml](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/Views/MainWindow.axaml) e [MainWindow.axaml.cs](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/Views/MainWindow.axaml.cs): Layout mestre com `SplitView` moderno (`DisplayMode="CompactInline"`), cabeçalho dinâmico e botão de menu hambúrguer.
  - [DashboardView.axaml](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/Views/DashboardView.axaml)
  - [EventosView.axaml](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/Views/EventosView.axaml)
  - [ParticipantesView.axaml](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/Views/ParticipantesView.axaml)
  - [InscricoesView.axaml](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/Views/InscricoesView.axaml)
  - [CredenciamentoView.axaml](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/Views/CredenciamentoView.axaml)
  - [RelatoriosView.axaml](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/SistemaGestaoEventosAcademicos.Apresentacao.Desktop/Views/RelatoriosView.axaml)

### 3.2. Status da Compilação
- Compilação realizada com sucesso: **0 Erros, 0 Avisos**.

---

## 4. Próxima Etapa: Implementação da Camada de Persistência

Com a estrutura visual e os ViewModels scaffoldados, a próxima etapa consistirá em:
1. Implementar os repositórios em memória em `SistemaGestaoEventosAcademicos.Persistencia`:
   - `EventoRepositorio`
   - `ParticipanteRepositorio`
   - `InscricaoRepositorio`
2. Criar a classe de carga inicial de semente (`CargaDadosIniciais`) para popular o sistema na inicialização.
3. Conectar a injeção dos repositórios nos ViewModels para permitir cadastros, consultas e operações reais de negócio via interface gráfica.
