# Histórico de Implementação

Esta pasta registra a evolução prática e os passos de engenharia de software executados no projeto **SistemaGestaoEventosAcademicos**.

## Registros de Sessões

- [01-setup-da-solucao-e-estrutura.md](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/docs/implementacao/01-setup-da-solucao-e-estrutura.md):
  - Setup inicial da solução no formato `.slnx`.
  - Configuração do `Directory.Build.props` e padronização `.NET 10`.
  - Configuração de dependências unidirecionais entre camadas (`Apresentacao` -> `Persistencia` -> `Negocio`).
  - Criação da topologia de diretórios e validação de compilação/execução.

- [02-nucleo-de-dominio-e-regras-de-negocio.md](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/docs/implementacao/02-nucleo-de-dominio-e-regras-de-negocio.md):
  - Implementação integral das entidades ricas (`Evento`, `Atividade`, `Participante`, `Inscricao`, `Certificado`).
  - Implementação de enums de domínio e exceção `RegraNegocioException`.
  - Cobertura de 100% das 25 regras de negócio (RN01 a RN25).

- [03-planejamento-apresentacao-avalonia-ui.md](file:///Users/evertoncoimbradearaujo/Documents/GitHub/poo/2026_S2/SistemaGestaoEventosAcademicos/docs/implementacao/03-planejamento-apresentacao-avalonia-ui.md):
  - Diagnóstico da solução e formalização do plano para a apresentação em Avalonia UI.
  - Especificação arquitetural do projeto desktop com `SplitView`, MVVM e tema Fluent.
  - Roteiro de continuidade integrando a futura camada de persistência.

