# Plano de Implementação: Sistema de Reservas Acadêmicas (C#)

## Etapa 1: Estrutura Inicial do Projeto
- Criar pastas: Dominio/Entidades, Dominio/Servicos, Dominio/Excecoes, Dominio/Repositorios.
- Definir o namespace padrão para o domínio.

## Etapa 2: Modelagem das Entidades
- Implementar as classes principais:
  - Reserva
  - Usuario
  - Sala
  - Equipamento
  - EtapaDeConferencia
  - RegistroDeConferencia
- Definir propriedades, construtores e associações:
  - Associação 1:1 (Reserva-Usuario, Reserva-Sala, Reserva-Equipamento, Reserva-Supervisor)
  - Associação 1:N (Reserva-Responsáveis, Reserva-Etapas, Reserva-Registros)
  - Composição (Reserva-Etapas, Reserva-Registros)
  - Agregação (Reserva-Responsáveis)
- Proteger coleções internas (List<T> privadas, IReadOnlyCollection<T> públicas).

## Etapa 3: Validações e Invariantes
- Implementar validações nos construtores das entidades.
- Garantir invariantes de domínio (ex: não permitir reserva sem obrigatórios, sem responsáveis, sem etapas obrigatórias, etc).
- Impedir alterações após finalização da reserva.

## Etapa 4: Exceções de Domínio
- Criar exceções customizadas para violações de regras de negócio.

## Etapa 5: Serviços de Domínio
- Implementar serviços para operações complexas (ex: finalização de reserva, criação de etapas, registros de conferência).

## Etapa 6: Repositórios (Interfaces)
- Definir interfaces para persistência das entidades principais.

## Etapa 7: Testes e Ajustes
- Validar regras de negócio com testes manuais ou unitários.
- Ajustar o modelo conforme necessário.

---


### Registro de Progresso
- [x] Etapa 1: Estrutura Inicial do Projeto (concluída em 11/05/2026)
- [x] Etapa 2: Modelagem das Entidades (concluída em 11/05/2026)
- [ ] Etapa 3: Validações e Invariantes
- [ ] Etapa 4: Exceções de Domínio
- [ ] Etapa 5: Serviços de Domínio
- [ ] Etapa 6: Repositórios (Interfaces)
- [ ] Etapa 7: Testes e Ajustes

Cada etapa concluída será registrada neste arquivo para acompanhamento do progresso.