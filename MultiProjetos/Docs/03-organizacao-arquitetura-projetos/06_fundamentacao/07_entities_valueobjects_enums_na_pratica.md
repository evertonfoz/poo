# Entities, ValueObjects e Enums na Pratica

## Objetivo

Explicar, com exemplos do projeto, o que sao `Entity`, `ValueObject` e `Enum`, e por que cada um foi colocado em uma pasta diferente no Domain.

## 1. Entity

### Definicao

Entity e um objeto com identidade propria. Mesmo que seus dados mudem, ele continua sendo o mesmo objeto dentro do dominio.

### Como reconhecer

1. Possui identificador (`Id`).
2. Tem ciclo de vida (e criado, alterado, consultado).
3. Costuma ter comportamento de negocio.

### Exemplo no projeto

- `Aluno`, `Curso` e `Matricula` em `Domain/Entities`.
- Todos possuem `Id` e regras de comportamento.

### Leitura pedagogica

Se dois objetos tem o mesmo nome, mas IDs diferentes, eles nao sao a mesma entidade.

## 2. ValueObject

### Definicao

ValueObject e um objeto definido pelos valores que carrega, nao por identidade.

### Como reconhecer

1. Nao precisa de `Id` para existir no dominio.
2. Representa um conceito descritivo (ex.: endereco, periodo, faixa de nota).
3. Seu valor importa mais que "quem ele e".

### Exemplo no projeto

- `PerfilAcademico` em `Domain/ValueObjects`.
- Ele representa os dados academicos que caracterizam o aluno no contexto atual.

### Leitura pedagogica

Dois ValueObjects com mesmos valores sao equivalentes para a regra de negocio.

## 3. Enum

### Definicao

Enum representa um conjunto fechado de estados/valores permitidos.

### Como reconhecer

1. Existe uma lista pequena e controlada de opcoes.
2. O dominio nao aceita valores fora dessa lista.

### Exemplo no projeto

- `StatusMatricula` em `Domain/Enums`.
- Valores: `Ativa`, `Cancelada`, `Concluida`.

### Leitura pedagogica

Enum reduz erros de string solta e deixa a regra mais explicita no codigo.

## 4. Por que separar em pastas

Separar por tipo arquitetural ajuda o aluno a entender a intencao do modelo:

1. `Entities`: onde estao os principais objetos de negocio.
2. `ValueObjects`: onde estao os conceitos descritivos.
3. `Enums`: onde estao os estados controlados.

Resultado: menos ambiguidade ao criar novas classes e melhor organizacao para evolucao do sistema.

## 5. Regra pratica para o aluno

Antes de criar uma classe nova no Domain, responda:

1. Precisa de identidade? Vai para `Entities`.
2. E definida apenas por valores? Vai para `ValueObjects`.
3. E um conjunto fechado de opcoes? Vai para `Enums`.
