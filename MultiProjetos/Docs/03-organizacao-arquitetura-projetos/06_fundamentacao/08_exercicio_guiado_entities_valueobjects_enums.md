# Exercicio Guiado: Entity, ValueObject ou Enum

## Objetivo

Treinar a classificacao de conceitos de dominio em:

1. Entity
2. ValueObject
3. Enum

## Como responder

Para cada item, escreva:

1. Classificacao (Entity, ValueObject ou Enum)
2. Justificativa em uma frase
3. Pasta sugerida dentro do projeto

## Casos (8 itens)

1. `Professor` com `Id`, `Nome`, `EmailInstitucional` e historico de disciplinas lecionadas.
2. `PeriodoLetivo` com `Ano` e `Semestre` (ex.: 2026/1).
3. `StatusAluno` com valores `Ativo`, `Trancado`, `Formado`.
4. `Turma` com `Id`, `Codigo`, `Vagas` e lista de alunos matriculados.
5. `CargaHorariaSemanal` com valor inteiro de horas por semana.
6. `TipoOfertaDisciplina` com valores `Presencial`, `Hibrida`, `EAD`.
7. `Boletim` com `Id`, referencia ao aluno e lista de notas por disciplina.
8. `FaixaNota` com `Minimo` e `Maximo` para representar conceito (ex.: 7.0 a 8.9).

---

## Gabarito Comentado

### 1) Professor

1. Classificacao: **Entity**
2. Justificativa: possui identidade (`Id`) e ciclo de vida no dominio.
3. Pasta sugerida: `src/CadastroAcademico.Domain/Entities/Professor.cs`

### 2) PeriodoLetivo

1. Classificacao: **ValueObject**
2. Justificativa: e definido por valores (`Ano` e `Semestre`) e nao por identidade.
3. Pasta sugerida: `src/CadastroAcademico.Domain/ValueObjects/PeriodoLetivo.cs`

### 3) StatusAluno

1. Classificacao: **Enum**
2. Justificativa: representa conjunto fechado de estados permitidos.
3. Pasta sugerida: `src/CadastroAcademico.Domain/Enums/StatusAluno.cs`

### 4) Turma

1. Classificacao: **Entity**
2. Justificativa: possui identidade e comportamento de negocio (controle de vagas, matriculas).
3. Pasta sugerida: `src/CadastroAcademico.Domain/Entities/Turma.cs`

### 5) CargaHorariaSemanal

1. Classificacao: **ValueObject**
2. Justificativa: representa um valor de negocio com regra de validacao (ex.: maior que zero).
3. Pasta sugerida: `src/CadastroAcademico.Domain/ValueObjects/CargaHorariaSemanal.cs`

### 6) TipoOfertaDisciplina

1. Classificacao: **Enum**
2. Justificativa: opcoes fixas de oferta, sem variacao livre.
3. Pasta sugerida: `src/CadastroAcademico.Domain/Enums/TipoOfertaDisciplina.cs`

### 7) Boletim

1. Classificacao: **Entity**
2. Justificativa: possui identidade e agrega informacoes com historico ao longo do tempo.
3. Pasta sugerida: `src/CadastroAcademico.Domain/Entities/Boletim.cs`

### 8) FaixaNota

1. Classificacao: **ValueObject**
2. Justificativa: e definida pelos valores de limite e nao por identidade propria.
3. Pasta sugerida: `src/CadastroAcademico.Domain/ValueObjects/FaixaNota.cs`

---

## Criterio de Correcao (rubrica simples)

Pontuacao total: **10 pontos**

1. Classificacao correta dos 8 itens: 8 pontos (1 por item).
2. Justificativas consistentes: 2 pontos (avaliacao global).

Interpretação:

1. 9-10: dominio bem compreendido.
2. 7-8: bom entendimento, revisar casos de fronteira.
3. Ate 6: revisar material 07 e refazer o exercicio.

## Dica de fronteira

Se houver duvida entre Entity e ValueObject, pergunte:

"Se eu trocar todos os valores internos, mas mantiver o mesmo objeto identificado no sistema, ele continua sendo o mesmo conceito?"

1. Se sim, tende a Entity.
2. Se nao, tende a ValueObject.
