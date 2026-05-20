# Prova de Programação Orientada a Objetos
## Tema: Associações, Invariantes, Agregação e Composição

## Enunciado inédito

Uma instituição de ensino deseja desenvolver parte de um sistema para organizar **incubação de projetos inovadores desenvolvidos por estudantes**. O sistema deve controlar a proposta incubada, o estudante proponente, o professor mentor, o laboratório de referência, os membros da equipe, os consultores externos, os recursos institucionais utilizados, os marcos de desenvolvimento e as avaliações realizadas ao longo da incubação.

Seu objetivo é implementar, em C#, um modelo orientado a objetos para esse domínio, aplicando corretamente os conceitos de **associação 1:1**, **associação 1:N**, **dependência obrigatória**, **dependência opcional**, **encapsulamento**, **validação**, **proteção de invariantes**, **agregação** e **composição**.

A implementação deve ser feita sem banco de dados, sem interface gráfica e sem frameworks externos. O foco da prova é a qualidade da modelagem orientada a objetos.

## Contexto do domínio

A instituição possui um programa de incubação acadêmica para apoiar projetos inovadores criados por estudantes. Cada projeto incubado possui um estudante proponente, um professor mentor, um laboratório de referência, uma equipe de estudantes colaboradores, uma lista de consultores externos, recursos institucionais utilizados e um conjunto de marcos de desenvolvimento.

A instituição já possui estudantes e professores cadastrados. Um estudante pode participar de diferentes projetos em períodos distintos, e um professor pode atuar como mentor em vários projetos. Portanto, estudantes e professores não nascem dentro de um projeto incubado e não deixam de existir quando a incubação é finalizada.

A instituição também possui recursos institucionais cadastrados, como kits de prototipagem, notebooks, sensores, impressoras 3D, salas de reunião, licenças de software e equipamentos de laboratório. Esses recursos existem independentemente do projeto e podem ser utilizados em diferentes incubações.

Também podem existir consultores externos, como profissionais do mercado, egressos, empreendedores ou especialistas técnicos. Esses consultores existem independentemente de um projeto específico e podem apoiar diferentes projetos.

Por outro lado, os marcos de desenvolvimento são criados especificamente para um projeto incubado. Um marco como “Validação do problema”, “Protótipo inicial”, “Teste com usuários”, “Pitch intermediário” ou “Entrega final” faz sentido apenas dentro do projeto em que foi planejado.

Durante a incubação, membros da equipe podem registrar avaliações ou evidências relacionadas aos marcos. Um projeto somente pode ser finalizado se possuir pelo menos um membro de equipe, pelo menos um marco obrigatório e se todos os marcos obrigatórios tiverem avaliação registrada.

## Classes mínimas esperadas

Você deve criar, no mínimo, as seguintes classes:

- `Estudante`
- `Professor`
- `Laboratorio`
- `ProjetoIncubado`
- `ConsultorExterno`
- `RecursoInstitucional`
- `MarcoDesenvolvimento`
- `AvaliacaoMarco`

Você pode criar outras classes auxiliares, enums ou métodos de apoio, caso julgue necessário.

## Requisitos de modelagem

### 1. Objetos válidos desde a criação

Todas as classes devem proteger seus dados internos.

Campos textuais obrigatórios não podem aceitar valores nulos, vazios ou compostos apenas por espaços.

Exemplos de dados que devem ser validados:

- nome do estudante;
- nome do professor;
- nome do laboratório;
- título do projeto incubado;
- nome do consultor externo;
- descrição do recurso institucional;
- descrição do marco de desenvolvimento;
- texto da avaliação do marco.

Sempre que um valor inválido for informado, o objeto não deve ser criado ou alterado para um estado inconsistente.

Use exceções apropriadas, como `ArgumentException` ou `InvalidOperationException`, quando necessário.

### 2. Associação 1:1 obrigatória

A classe `ProjetoIncubado` deve possuir associação obrigatória com:

- um `Estudante` proponente;
- um `Professor` mentor;
- um `Laboratorio` de referência.

Um projeto incubado não pode existir sem estudante proponente, sem professor mentor ou sem laboratório de referência.

Essas associações devem ser recebidas pelo construtor da classe `ProjetoIncubado` e validadas no momento da criação.

Não é permitido representar essas relações usando apenas `string`, como `NomeProponente`, `NomeMentor` ou `NomeLaboratorio`.

O correto é que o projeto mantenha referências reais para objetos dos tipos `Estudante`, `Professor` e `Laboratorio`.

### 3. Associação 1:1 opcional

O projeto pode possuir, opcionalmente, um professor coorientador.

O coorientador deve ser representado por um objeto do tipo `Professor`.

O projeto deve poder ser criado sem coorientador, mas deve permitir que ele seja definido posteriormente por meio de um método específico, por exemplo:

```csharp
DefinirCoorientador(Professor professor)
```

Esse método deve validar se o professor informado é válido.

O coorientador não pode ser o mesmo professor definido como mentor.

A propriedade do coorientador deve deixar claro que a associação é opcional.

### 4. Associação 1:N com membros da equipe

Um projeto incubado pode possuir vários membros de equipe.

Os membros da equipe são objetos do tipo `Estudante`.

A classe `ProjetoIncubado` deve manter internamente uma coleção privada de membros.

A lista não pode ser exposta diretamente como `List<Estudante>` pública com `set`.

A exposição externa deve permitir apenas leitura, por exemplo com:

```csharp
IReadOnlyCollection<Estudante>
```

A alteração da coleção deve ser controlada por métodos da própria classe `ProjetoIncubado`, como:

```csharp
AdicionarMembro(Estudante estudante)
RemoverMembro(Estudante estudante)
```

### 5. Invariantes dos membros da equipe

A coleção de membros deve obedecer às seguintes regras:

- não pode aceitar estudante nulo;
- não pode permitir estudante duplicado;
- o estudante proponente não pode ser adicionado também como membro comum da equipe;
- o projeto deve ter pelo menos um membro de equipe para ser finalizado;
- membros não podem ser adicionados ou removidos depois que o projeto estiver finalizado;
- a remoção não pode ocorrer se o estudante informado não estiver associado ao projeto;
- a remoção não pode deixar o projeto sem membros caso ele já possua marcos obrigatórios cadastrados.

Caso alguma dessas regras seja violada, uma exceção deve ser lançada.

### 6. Associação 1:N por agregação com consultores externos

O projeto pode possuir vários consultores externos.

A classe `ConsultorExterno` deve representar uma pessoa ou especialista que existe independentemente do projeto. Um consultor pode participar de diferentes projetos ao longo do tempo.

Por isso, a relação entre `ProjetoIncubado` e `ConsultorExterno` deve ser tratada como **agregação**.

Os consultores devem ser criados fora do projeto e passados para ele quando necessário.

Exemplo conceitual:

```csharp
var consultor = new ConsultorExterno("Marina Lopes", "Validação de negócios");
projeto.AdicionarConsultor(consultor);
```

O projeto não deve criar internamente os consultores externos.

A coleção de consultores deve ser privada internamente e exposta apenas como leitura.

### 7. Invariantes dos consultores externos

A coleção de consultores externos deve obedecer às seguintes regras:

- não pode aceitar consultor nulo;
- não pode permitir consultor duplicado no mesmo projeto;
- consultores não podem ser adicionados depois que o projeto estiver finalizado;
- consultores não podem ser removidos depois que o projeto estiver finalizado;
- a remoção não pode ocorrer se o consultor informado não estiver associado ao projeto.

### 8. Associação 1:N por agregação com recursos institucionais

O projeto pode utilizar vários recursos institucionais.

A classe `RecursoInstitucional` deve representar um recurso previamente existente na instituição. Um recurso pode ser usado em diferentes projetos ao longo do tempo.

Por isso, a relação entre `ProjetoIncubado` e `RecursoInstitucional` também deve ser tratada como **agregação**.

Os recursos devem ser criados fora do projeto e passados para ele quando necessário.

Exemplo conceitual:

```csharp
var impressora3D = new RecursoInstitucional("Impressora 3D do laboratório");
projeto.AdicionarRecurso(impressora3D);
```

O projeto não deve criar internamente os recursos institucionais.

A coleção de recursos deve ser privada internamente e exposta apenas como leitura.

### 9. Invariantes dos recursos institucionais

A coleção de recursos institucionais deve obedecer às seguintes regras:

- não pode aceitar recurso nulo;
- não pode permitir recurso duplicado no mesmo projeto;
- recursos não podem ser adicionados depois que o projeto estiver finalizado;
- recursos não podem ser removidos depois que o projeto estiver finalizado;
- a remoção não pode ocorrer se o recurso informado não estiver associado ao projeto.

### 10. Composição com marcos de desenvolvimento

Os marcos de desenvolvimento pertencem ao projeto incubado.

A classe `MarcoDesenvolvimento` deve representar uma etapa específica planejada para a incubação, por exemplo:

- “Validação do problema”;
- “Mapeamento dos usuários”;
- “Protótipo inicial”;
- “Teste com usuários”;
- “Pitch intermediário”;
- “Entrega final”.

Esses marcos devem ser criados pela própria classe `ProjetoIncubado`, e não recebidos prontos de fora.

Portanto, o método de inclusão deve receber os dados necessários para criar o marco internamente, por exemplo:

```csharp
AdicionarMarco(string descricao, DateTime dataPrevista, bool obrigatorio)
```

A própria classe `ProjetoIncubado` deve instanciar o objeto `MarcoDesenvolvimento`.

Essa decisão representa uma composição: o marco pertence ao projeto e não deve existir de forma independente no sistema.

### 11. Invariantes dos marcos de desenvolvimento

A classe `ProjetoIncubado` deve controlar a coleção de marcos de desenvolvimento.

A coleção deve ser privada internamente e exposta apenas para leitura.

Os marcos devem obedecer às seguintes regras:

- a descrição do marco é obrigatória;
- a data prevista não pode ser anterior à data de início do projeto;
- não pode haver dois marcos com a mesma descrição no mesmo projeto;
- o projeto precisa ter pelo menos um marco obrigatório para ser finalizado;
- marcos não podem ser adicionados após o projeto ser finalizado;
- todo marco obrigatório deve receber avaliação antes da finalização do projeto.

### 12. Composição com avaliações dos marcos

A classe `AvaliacaoMarco` deve representar a avaliação registrada por um membro da equipe sobre um marco de desenvolvimento.

Cada avaliação deve estar associada a:

- um estudante membro da equipe;
- um marco de desenvolvimento;
- uma descrição textual da evidência ou resultado;
- uma indicação se o marco foi aprovado ou não.

A avaliação deve ser registrada por meio de um método da classe `ProjetoIncubado`, por exemplo:

```csharp
RegistrarAvaliacao(Estudante membro, MarcoDesenvolvimento marco, string descricao, bool aprovado)
```

A classe `ProjetoIncubado` deve validar:

- se o estudante informado é membro da equipe;
- se o marco pertence ao projeto;
- se a descrição da avaliação é obrigatória;
- se o mesmo membro não registrou duas avaliações para o mesmo marco;
- se não é possível registrar avaliação depois que o projeto foi finalizado.

As avaliações devem ser controladas internamente pelo projeto.

### 13. Finalização do projeto incubado

A classe `ProjetoIncubado` deve possuir um método:

```csharp
Finalizar()
```

Esse método deve verificar todas as invariantes necessárias antes de alterar o estado do projeto para finalizado.

O projeto só pode ser finalizado se:

- possuir estudante proponente válido;
- possuir professor mentor válido;
- possuir laboratório válido;
- possuir pelo menos um membro de equipe;
- possuir pelo menos um marco obrigatório;
- todos os marcos obrigatórios tiverem avaliação registrada;
- não houver membro duplicado;
- não houver consultor duplicado;
- não houver recurso institucional duplicado;
- não houver marco duplicado;
- o proponente não estiver também na lista de membros comuns;
- o coorientador, se existir, for diferente do mentor.

Depois de finalizado, o projeto não pode mais receber novos membros, novos consultores, novos recursos, novos marcos ou novas avaliações.

### 14. Cálculo do percentual de marcos aprovados

A classe `ProjetoIncubado` deve permitir consultar o percentual de marcos aprovados.

O percentual deve considerar os marcos que receberam pelo menos uma avaliação marcada como aprovada.

A consulta do percentual de marcos aprovados só deve ser permitida se o projeto estiver finalizado.

Caso o percentual seja solicitado antes da finalização, uma exceção deve ser lançada.

Exemplo conceitual:

- 5 marcos cadastrados;
- 4 marcos com pelo menos uma avaliação aprovada;
- percentual de marcos aprovados: 80%.

### 15. Demonstração obrigatória

Crie um pequeno trecho de código de demonstração, em `Program.cs`, que mostre:

1. criação de estudante proponente, professor mentor, laboratório, membros da equipe, consultores e recursos institucionais;
2. criação de um projeto incubado válido;
3. definição opcional de coorientador;
4. adição de membros da equipe;
5. adição de consultores externos;
6. adição de recursos institucionais;
7. adição de marcos de desenvolvimento;
8. registro de avaliações;
9. finalização do projeto;
10. exibição do percentual de marcos aprovados.

Também demonstre pelo menos quatro tentativas inválidas, como:

- definir o mentor como coorientador;
- adicionar o proponente como membro comum;
- adicionar membro duplicado;
- adicionar consultor duplicado;
- adicionar recurso duplicado;
- registrar avaliação para estudante que não é membro da equipe;
- registrar avaliação para marco que não pertence ao projeto;
- finalizar projeto sem avaliação em marco obrigatório;
- adicionar marco depois do projeto finalizado;
- criar objeto com texto obrigatório vazio;
- remover consultor que não está associado ao projeto.

As tentativas inválidas devem ser tratadas com `try/catch`, exibindo mensagens adequadas no console.

## Regras técnicas obrigatórias

A solução deve respeitar os seguintes critérios:

- usar propriedades com `private set` ou somente leitura quando adequado;
- evitar atributos públicos modificáveis diretamente;
- não expor listas internas como `List<T>` pública;
- usar `private readonly List<T>` para coleções internas;
- expor coleções como `IReadOnlyCollection<T>`;
- validar objetos nulos antes de associá-los;
- usar objetos reais em associações, e não apenas dados primitivos;
- distinguir corretamente associação obrigatória e opcional;
- aplicar agregação quando os objetos têm ciclo de vida independente;
- aplicar composição quando o objeto parte pertence ao objeto todo;
- manter as regras de negócio dentro das classes responsáveis;
- impedir que código externo corrompa o estado interno dos objetos;
- garantir que as invariantes permaneçam válidas durante toda a vida do objeto.

## Conhecimentos prévios pressupostos

Esta prova pressupõe que o aluno já domina:

- criação de classes;
- atributos e métodos;
- construtores;
- encapsulamento;
- validação de dados;
- uso de exceções;
- listas em C#;
- propriedades;
- tipos nullable;
- métodos com parâmetros;
- instanciação de objetos;
- fundamentos de orientação a objetos.

## Critérios de avaliação

| Critério | Pontuação |
|---|---:|
| Modelagem correta das classes e responsabilidades | 20 |
| Uso adequado de associações 1:1 e 1:N | 15 |
| Diferenciação entre associação obrigatória e opcional | 10 |
| Encapsulamento das coleções com `private readonly List<T>` e `IReadOnlyCollection<T>` | 10 |
| Proteção de invariantes e validações de domínio | 15 |
| Aplicação correta de agregação e composição | 15 |
| Clareza, organização e legibilidade do código | 10 |
| Demonstração funcional no `Program.cs` | 5 |
| **Total** | **100** |

## Observação final

A solução não deve ser apenas um conjunto de classes com atributos. Ela deve representar um modelo orientado a objetos consistente, no qual os objetos colaboram entre si, as associações expressam relações reais do domínio e as regras de negócio impedem estados inválidos ao longo de toda a execução do sistema.

