# Diagrama de Classes — Sistema de Reservas Acadêmicas

```mermaid
classDiagram
    class Usuario {
        +string Nome
        +string Email
        +Usuario(nome, email)
    }

    class Sala {
        +string Identificacao
        +Sala(identificacao)
    }

    class Equipamento {
        +string Nome
        +Equipamento(nome)
    }

    class EtapaDeConferencia {
        +string Descricao
        +bool Obrigatoria
        ~EtapaDeConferencia(descricao, obrigatoria)
    }

    class RegistroDeConferencia {
        +Usuario Responsavel
        +EtapaDeConferencia Etapa
        +string Observacao
        +bool Aprovada
        ~RegistroDeConferencia(responsavel, etapa, observacao, aprovada)
    }

    class Reserva {
        +Usuario Solicitante
        +Sala Sala
        +Equipamento Equipamento
        +Usuario? Supervisor
        +bool Finalizada
        -List~Usuario~ _responsaveis
        -List~EtapaDeConferencia~ _etapas
        -List~RegistroDeConferencia~ _registros
        +IReadOnlyCollection~Usuario~ Responsaveis
        +IReadOnlyCollection~EtapaDeConferencia~ Etapas
        +IReadOnlyCollection~RegistroDeConferencia~ Registros
        +Reserva(solicitante, sala, equipamento, supervisor?)
        +AdicionarResponsavel(responsavel)
        +AdicionarEtapa(descricao, obrigatoria) EtapaDeConferencia
        +AdicionarRegistro(responsavel, etapa, observacao, aprovada) RegistroDeConferencia
        +Finalizar()
    }

    %% Associação 1:1 — Reserva -> Solicitante
    Reserva "1" --> "1" Usuario : solicitante

    %% Associação 1:1 — Reserva -> Supervisor (opcional)
    Reserva "1" --> "0..1" Usuario : supervisor

    %% Associação 1:1 — Reserva -> Sala
    Reserva "1" --> "1" Sala

    %% Associação 1:1 — Reserva -> Equipamento
    Reserva "1" --> "1" Equipamento

    %% Agregação 1:N — Reserva <>-- Responsaveis (Usuario existe independentemente)
    Reserva "1" o-- "0..*" Usuario : responsaveis

    %% Composição 1:N — Reserva *-- EtapaDeConferencia (ciclo de vida dependente)
    Reserva "1" *-- "0..*" EtapaDeConferencia : etapas

    %% Composição 1:N — Reserva *-- RegistroDeConferencia (ciclo de vida dependente)
    Reserva "1" *-- "0..*" RegistroDeConferencia : registros

    %% RegistroDeConferencia referencia Etapa e Responsavel
    RegistroDeConferencia --> EtapaDeConferencia : etapa
    RegistroDeConferencia --> Usuario : responsavel
```

## Legenda

| Notação | Significado |
|---|---|
| `-->` | Associação |
| `o--` | Agregação (parte pode existir sem o todo) |
| `*--` | Composição (parte pertence exclusivamente ao todo) |
| `+` | Público |
| `-` | Privado |
| `~` | Internal |
| `?` | Opcional (nullable) |
