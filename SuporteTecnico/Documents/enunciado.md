# Atividade Avaliativa: Sistema de Controle de Chamados de Suporte Técnico

## Contexto do problema

Uma empresa de tecnologia deseja desenvolver um pequeno sistema em C# para organizar os chamados recebidos pelo setor de suporte técnico. Cada chamado representa uma solicitação feita por um usuário e possui informações básicas, como número de protocolo, nome do solicitante, descrição do problema, nível de prioridade, status e data de abertura.

Com o crescimento da empresa, percebeu-se que nem todos os chamados são iguais. Alguns chamados são relacionados a problemas de infraestrutura, outros a sistemas internos e outros a solicitações de segurança da informação. Apesar dessas diferenças, todos continuam sendo chamados de suporte e compartilham dados e comportamentos comuns.

O objetivo desta atividade é implementar uma estrutura orientada a objetos que evite repetição de código, utilize herança de forma correta e aplique especialização de comportamentos quando necessário.

A implementação deverá aplicar os conceitos de herança, relação “é um”, classe base, classes derivadas, construtores com `base(...)`, métodos herdados, métodos específicos em classes derivadas, métodos `virtual`, sobrescrita com `override`, uso de `base.Metodo()`, encapsulamento e modificadores de acesso.

---

## Enunciado para os alunos

Você deverá implementar, em C#, um sistema simples de controle de chamados de suporte técnico. O sistema deverá modelar diferentes tipos de chamados, aplicando corretamente os conceitos de herança, especialização de classes, construtores em cadeia, sobrescrita de métodos e modificadores de acesso.

A implementação deve iniciar pela identificação dos dados e comportamentos comuns a todos os chamados. Em seguida, devem ser criadas classes especializadas para representar diferentes tipos de chamados, evitando repetição de código e mantendo as regras de negócio centralizadas sempre que possível.

---

## Requisitos de modelagem

Crie uma classe base chamada `ChamadoSuporte`.

Essa classe deverá representar aquilo que todo chamado possui em comum:

* número de protocolo;
* nome do solicitante;
* descrição do problema;
* prioridade;
* status do chamado;
* data de abertura.

A classe base deverá conter validações no construtor.

O número de protocolo, o nome do solicitante e a descrição do problema não podem ser nulos, vazios ou compostos apenas por espaços.

A prioridade deverá ser um valor válido definido pelo sistema.

A data de abertura não poderá ser uma data futura.

O status inicial de todo chamado deverá ser `Aberto`.

---

## Tipos especializados de chamado

Crie pelo menos três classes derivadas de `ChamadoSuporte`.

### 1. `ChamadoInfraestrutura`

Representa chamados relacionados a equipamentos, rede, impressoras, servidores ou recursos físicos.

Deve possuir, além dos dados herdados:

* local do problema;
* equipamento afetado;
* indicador se o equipamento é crítico.

Exemplo de situação: computador do laboratório sem acesso à rede.

---

### 2. `ChamadoSistemaInterno`

Representa chamados relacionados a sistemas utilizados pela empresa.

Deve possuir, além dos dados herdados:

* nome do sistema;
* módulo afetado;
* indicador se o sistema é essencial.

Exemplo de situação: erro ao gerar relatório no sistema financeiro.

---

### 3. `ChamadoSeguranca`

Representa chamados relacionados à segurança da informação.

Deve possuir, além dos dados herdados:

* tipo de incidente;
* indicador se o chamado contém dados sensíveis.

Exemplo de situação: tentativa de acesso indevido a uma conta de usuário.

---

## Regras obrigatórias de herança

As classes `ChamadoInfraestrutura`, `ChamadoSistemaInterno` e `ChamadoSeguranca` deverão herdar de `ChamadoSuporte`.

Antes de implementar, o aluno deverá justificar no código ou em um pequeno comentário no arquivo principal por que a relação de herança faz sentido.

A justificativa deve seguir a lógica:

Um chamado de infraestrutura é um chamado de suporte.

Um chamado de sistema interno é um chamado de suporte.

Um chamado de segurança é um chamado de suporte.

Não use herança para representar relações do tipo “tem um”. Caso seja necessário representar algum dado associado, utilize composição ou associação, e não herança.

---

## Construtores

Cada classe derivada deverá possuir um construtor próprio.

O construtor da classe derivada deverá receber os dados comuns e os dados específicos, chamando obrigatoriamente o construtor da classe base com `base(...)`.

As validações dos dados comuns deverão ficar na classe base.

As validações dos dados específicos deverão ficar nas classes derivadas.

Exemplo de divisão esperada:

* validação do protocolo: classe base;
* validação do nome do solicitante: classe base;
* validação da descrição do problema: classe base;
* validação da data de abertura: classe base;
* validação do local do problema: `ChamadoInfraestrutura`;
* validação do equipamento afetado: `ChamadoInfraestrutura`;
* validação do nome do sistema: `ChamadoSistemaInterno`;
* validação do módulo afetado: `ChamadoSistemaInterno`;
* validação do tipo de incidente: `ChamadoSeguranca`.

---

## Comportamentos obrigatórios da classe base

A classe base `ChamadoSuporte` deverá possuir os seguintes métodos:

```csharp
public virtual string GerarResumo()
```

Esse método deverá retornar uma descrição geral do chamado, contendo protocolo, solicitante, prioridade, status e descrição do problema.

```csharp
public virtual decimal CalcularPrazoAtendimentoHoras()
```

Esse método deverá calcular um prazo base de atendimento conforme a prioridade.

Sugestão de regra:

* prioridade baixa: 72 horas;
* prioridade média: 48 horas;
* prioridade alta: 24 horas;
* prioridade crítica: 8 horas.

```csharp
public void Encerrar()
```

Esse método deverá alterar o status do chamado para `Encerrado`.

O status não deve ser alterado diretamente fora da classe. A classe deve proteger seu estado interno.

---

## Sobrescrita de comportamento

Cada classe derivada deverá sobrescrever pelo menos um dos métodos virtuais.

### `ChamadoInfraestrutura`

Deve sobrescrever `GerarResumo()` para incluir o local do problema e o equipamento afetado.

Também deverá sobrescrever `CalcularPrazoAtendimentoHoras()` para reduzir o prazo quando o equipamento for crítico.

Sugestão de regra: se o equipamento for crítico, o prazo calculado pela classe base deve ser reduzido pela metade.

---

### `ChamadoSistemaInterno`

Deve sobrescrever `GerarResumo()` para incluir o nome do sistema e o módulo afetado.

Também deverá sobrescrever `CalcularPrazoAtendimentoHoras()` para reduzir o prazo quando o sistema afetado for essencial.

Sugestão de regra: se o sistema for essencial, o prazo calculado pela classe base deve ser reduzido em 25%.

---

### `ChamadoSeguranca`

Deve sobrescrever `GerarResumo()` para destacar o tipo de incidente e informar se há dados sensíveis envolvidos.

Também deverá sobrescrever `CalcularPrazoAtendimentoHoras()` para que chamados com dados sensíveis tenham prazo reduzido.

Sugestão de regra: se houver dados sensíveis envolvidos, o prazo calculado pela classe base deve ser reduzido pela metade.

Sempre que fizer sentido, utilize `base.GerarResumo()` ou `base.CalcularPrazoAtendimentoHoras()` dentro da sobrescrita, reaproveitando parte do comportamento original antes de adicionar a lógica específica.

---

## Modificadores de acesso

A implementação deverá aplicar corretamente os modificadores de acesso.

Regras mínimas esperadas:

* classes principais podem ser `public`;
* propriedades que fazem parte do contrato do objeto podem ser `public` com `get`;
* propriedades que não devem ser alteradas diretamente devem ter `private set` ou serem somente leitura;
* campos internos, caso existam, devem ser `private`;
* métodos auxiliares que só fazem sentido dentro da classe devem ser `private`;
* comportamentos que podem ser usados pelas classes derivadas, mas não pelo restante do sistema, podem ser `protected`;
* evite tornar tudo `public`.

O aluno deverá demonstrar cuidado com encapsulamento e menor exposição possível da API das classes.

---

## Enumerações sugeridas

Para organizar melhor o domínio, crie enumerações para representar prioridade e status do chamado.

Exemplo:

```csharp
public enum PrioridadeChamado
{
    Baixa,
    Media,
    Alta,
    Critica
}
```

```csharp
public enum StatusChamado
{
    Aberto,
    EmAtendimento,
    Encerrado
}
```

O uso de `enum` é recomendado para evitar uso excessivo de strings soltas no código.

---

## Execução no programa principal

No `Program.cs`, crie pelo menos um objeto de cada tipo de chamado:

* um `ChamadoInfraestrutura`;
* um `ChamadoSistemaInterno`;
* um `ChamadoSeguranca`.

Depois, para cada objeto, exiba:

* o resumo gerado;
* o prazo de atendimento calculado;
* o status antes do encerramento;
* o status após chamar o método `Encerrar()`.

O programa deve demonstrar que cada tipo de chamado responde de forma diferente aos métodos sobrescritos.

---

## Exemplo de fluxo esperado no programa

O programa principal pode criar objetos semelhantes aos seguintes cenários:

1. Um chamado de infraestrutura para um servidor sem conexão de rede.
2. Um chamado de sistema interno para falha em um módulo financeiro essencial.
3. Um chamado de segurança para tentativa de acesso indevido com dados sensíveis envolvidos.

Para cada chamado, o programa deverá apresentar as informações na tela e demonstrar o cálculo do prazo conforme o tipo específico.

---

## Critérios de avaliação

A atividade será avaliada considerando:

1. uso correto de herança, respeitando a relação “é um”;
2. existência de uma classe base com dados e regras comuns;
3. uso correto de `base(...)` nos construtores das classes derivadas;
4. validações bem distribuídas entre classe base e classes derivadas;
5. ausência de repetição desnecessária de código;
6. uso adequado de `virtual` e `override`;
7. uso adequado de `base.Metodo()` quando houver reaproveitamento do comportamento da classe base;
8. aplicação correta de modificadores de acesso;
9. proteção do estado interno dos objetos;
10. clareza, organização e legibilidade do código;
11. demonstração funcional no `Program.cs`;
12. coerência entre o problema proposto e a modelagem criada.

---

## Entrega esperada

O aluno deverá entregar um projeto C# funcional contendo:

* classe base `ChamadoSuporte`;
* classes derivadas `ChamadoInfraestrutura`, `ChamadoSistemaInterno` e `ChamadoSeguranca`;
* enumerações, se utilizadas;
* `Program.cs` demonstrando a execução;
* comentários breves justificando a relação de herança utilizada;
* código organizado, validado e sem duplicações desnecessárias.

---

## Observação final para os alunos

O foco desta atividade não é apenas fazer o código funcionar. O objetivo principal é demonstrar que você compreendeu quando a herança faz sentido, como uma classe derivada reaproveita a classe base, como comportamentos podem ser especializados e como o acesso aos membros deve ser controlado para manter o sistema seguro, organizado e fácil de evoluir.
