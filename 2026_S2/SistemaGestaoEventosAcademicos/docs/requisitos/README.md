# Requisitos do Sistema de Gestão de Eventos Acadêmicos

Esta pasta contém as especificações de requisitos e regras de negócio para a implementação da solução do **Sistema de Gestão de Eventos Acadêmicos**.

## Documentos

- [01-especificacao-de-requisitos.md](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/docs/requisitos/01-especificacao-de-requisitos.md): Especificação completa de Requisitos de Software (ERS), contendo:
  - **Requisitos Funcionais (RF)** divididos por módulos (Eventos, Atividades, Participantes, Inscrições, Presença/Certificados e Relatórios).
  - **Quadro de Regras de Negócio (RN)** consolidado (RN01 a RN25).
  - **Requisitos Não Funcionais (RNF)** abrangendo arquitetura em 3 camadas, boas práticas de POO, interface de console e persistência.
  - **Diagrama de Classes / Modelo Conceitual** com relações de Composição e Associação.
  - **Mapeamento para as Camadas C#** da solução (`Apresentacao`, `Negocio`, `Persistencia`).
  - **Critérios de Aceitação do MVP**.

- [02-regras-de-negocio.md](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/docs/requisitos/02-regras-de-negocio.md): Catálogo detalhado e exaustivo das Regras de Negócio (RN), contendo:
  - **Detalhamento operacional de cada regra (RN01 a RN25)** com objetivo, entidades envolvidas, gatilho, validações e mensagens de erro padronizadas.
  - **Diagrama de estados do Evento** (máquina de estados finitos do ciclo de vida).
  - **Matriz de Rastreabilidade** preliminar correlacionando cada Requisito Funcional (RF) às suas respectivas Regras de Negócio (RN).
  - **Diretrizes de implementação em C# / POO** na camada `SistemaGestaoEventosAcademicos.Negocio` (exceções de domínio e métodos ricos).

- [03-matriz-de-rastreabilidade.md](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/docs/requisitos/03-matriz-de-rastreabilidade.md): Matriz de Rastreabilidade de Requisitos (RTM) completa e bidirecional, contendo:
  - **Matriz Principal (RF × UC × RN × POO × Camadas × MVP)** relacionando cada requisito aos Casos de Uso, regras de negócio e módulos de Apresentação, Negócio e Persistência.
  - **Matriz Reversa (RN × Entidades × RFs Impactados)** indicando o tratamento e o efeito de violações.
  - **Matriz de Rastreabilidade de RNF** conectando os requisitos não funcionais às decisões arquiteturais.
  - **Cenários de Teste e Validação** para verificação do MVP.

- [04-especificacao-de-casos-de-uso.md](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/docs/requisitos/04-especificacao-de-casos-de-uso.md): Especificação formal de Casos de Uso (ECU), contendo:
  - **Diagrama Geral de Casos de Uso** (Mermaid) agrupado por atores e módulos do sistema.
  - **Matriz Resumo (UC × Ator × RF × RN × Prioridade)**.
  - **Detalhamento de UC01 a UC18**: Atores, Pré-condições, Pós-condições, Fluxo Principal, Fluxos Alternativos e Fluxos de Exceção baseados nas Regras de Negócio.

