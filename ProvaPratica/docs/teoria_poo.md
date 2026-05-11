# Teoria de POO aplicada à Etapa 1

## Estrutura Inicial do Projeto

A primeira etapa de um projeto orientado a objetos (POO) é organizar a estrutura de pastas e namespaces. Isso prepara o terreno para um código limpo, modular e de fácil manutenção.

---

## Princípios de POO envolvidos

### 1. Coesão
- **Definição:** Coesão é o grau em que os elementos de um módulo (classe, pasta, função) estão relacionados e trabalham juntos para um único propósito.
- **Exemplo prático:** Uma pasta chamada `Entidades` deve conter apenas classes que representam objetos do domínio (como Reserva, Usuario, Sala). Isso facilita encontrar e entender o código.

#### Exemplo em C#
```csharp
// Classe coesa: só representa um usuário
namespace ProvaPratica.Dominio.Entidades
{
    public class Usuario
    {
        public string Nome { get; }
        public string Email { get; }
        public Usuario(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
    }
}
```

### 2. Responsabilidade Única (Single Responsibility Principle - SRP)
- **Definição:** Cada módulo (classe, função, pasta) deve ter apenas uma razão para mudar, ou seja, uma única responsabilidade.
- **Exemplo prático:**
  - A pasta `Servicos` conterá apenas classes de serviço (operações de negócio).
  - A pasta `Excecoes` conterá apenas exceções de domínio.
  - A pasta `Repositorios` conterá apenas interfaces de persistência.

#### Exemplo em C#
```csharp
// Serviço só cuida de lógica de reserva
namespace ProvaPratica.Dominio.Servicos
{
    public class ReservaService
    {
        public void FinalizarReserva(Reserva reserva)
        {
            // Lógica para finalizar reserva
        }
    }
}

// Exceção só cuida de erros de domínio
namespace ProvaPratica.Dominio.Excecoes
{
    public class ReservaInvalidaException : Exception
    {
        public ReservaInvalidaException(string mensagem) : base(mensagem) { }
    }
}
```

---

## Estrutura criada nesta etapa

- `Dominio/Entidades`: Para as classes que representam os objetos principais do sistema.
- `Dominio/Servicos`: Para as regras e operações de negócio.
- `Dominio/Excecoes`: Para exceções específicas do domínio.
- `Dominio/Repositorios`: Para interfaces de acesso a dados.

### Namespace
- O namespace `ProvaPratica.Dominio` foi definido para agrupar todas as classes do domínio, evitando conflitos de nomes e organizando logicamente o projeto.

---

## Por que isso é importante?
- **Facilita a manutenção:** Com tudo organizado, é mais fácil encontrar e modificar o código.
- **Facilita o trabalho em equipe:** Outros desenvolvedores entendem rapidamente onde cada coisa está.
- **Permite evolução:** Fica mais simples adicionar novas funcionalidades sem bagunçar o projeto.

---

## Resumo Visual

```
Dominio/
  Entidades/      // Classes do domínio
  Servicos/       // Serviços de negócio
  Excecoes/       // Exceções de domínio
  Repositorios/   // Interfaces de persistência
  NamespaceReferencia.cs // Referência do namespace
```

---

## Dica para estudo
Sempre que iniciar um novo projeto, pense em como organizar as pastas e namespaces. Isso é o primeiro passo para aplicar POO de verdade!

Se quiser exemplos práticos de código, peça para eu mostrar na próxima etapa.

---

# Teoria de POO aplicada à Etapa 2

## Modelagem das Entidades

A modelagem das entidades é um dos pilares da orientação a objetos. Nesta etapa, são criadas as classes que representam os principais conceitos do domínio do sistema, suas propriedades, associações e restrições.

---

## Princípios de POO envolvidos

### 1. Abstração
- **Definição:** Abstração é o processo de identificar os elementos essenciais de um domínio e representá-los como classes, ignorando detalhes irrelevantes.
- **Exemplo prático:** Criar as classes `Reserva`, `Usuario`, `Sala`, `Equipamento`, `EtapaDeConferencia` e `RegistroDeConferencia` para representar os conceitos do sistema de reservas.

#### Exemplo em C#
```csharp
namespace ProvaPratica.Dominio.Entidades
{
        public class Reserva
        {
                public int Id { get; }
                public Usuario Usuario { get; }
                public Sala Sala { get; }
                public Equipamento Equipamento { get; }
                public IReadOnlyCollection<Usuario> Responsaveis { get; }
                public IReadOnlyCollection<EtapaDeConferencia> Etapas { get; }
                public IReadOnlyCollection<RegistroDeConferencia> Registros { get; }
                // ... outros membros e métodos ...
        }
}
```

### 2. Encapsulamento
- **Definição:** Encapsulamento é o princípio de esconder os detalhes internos de implementação e proteger o estado do objeto, expondo apenas o necessário.
- **Exemplo prático:** Utilizar coleções privadas (ex: `List<T>`) e expor apenas coleções de leitura (`IReadOnlyCollection<T>`), impedindo alterações externas indevidas.

#### Exemplo em C#
```csharp
private readonly List<Usuario> _responsaveis = new();
public IReadOnlyCollection<Usuario> Responsaveis => _responsaveis.AsReadOnly();
```

### 3. Associações, Composição e Agregação
- **Definição:**
    - **Associação:** Relacionamento entre objetos, podendo ser 1:1, 1:N, N:N.
    - **Composição:** Relação "parte-todo" forte, onde o ciclo de vida da parte depende do todo (ex: etapas e registros de uma reserva).
    - **Agregação:** Relação "parte-todo" fraca, onde as partes podem existir independentemente do todo (ex: responsáveis por uma reserva).
- **Exemplo prático:**
    - Uma `Reserva` possui uma lista de `Responsaveis` (agregação), uma lista de `Etapas` e de `Registros` (composição).

#### Exemplo em C#
```csharp
public class Reserva
{
        // ...existing code...
        private readonly List<EtapaDeConferencia> _etapas = new();
        public IReadOnlyCollection<EtapaDeConferencia> Etapas => _etapas.AsReadOnly();

        private readonly List<Usuario> _responsaveis = new();
        public IReadOnlyCollection<Usuario> Responsaveis => _responsaveis.AsReadOnly();
        // ...existing code...
}
```

---

## Estrutura criada nesta etapa

- Classes de entidades principais: `Reserva`, `Usuario`, `Sala`, `Equipamento`, `EtapaDeConferencia`, `RegistroDeConferencia`.
- Propriedades e construtores definidos conforme o domínio.
- Associações, composições e agregações implementadas conforme as regras do sistema.
- Coleções protegidas para garantir integridade dos dados.

---

## Por que isso é importante?
- **Representa o domínio real:** O código reflete fielmente as regras e entidades do mundo real.
- **Facilita a evolução:** Mudanças no domínio são facilmente refletidas nas entidades.
- **Garante integridade:** O encapsulamento e as restrições protegem o sistema contra estados inválidos.

---

## Dica para estudo
Ao modelar entidades, pense sempre em quais propriedades e relações são essenciais, e proteja o estado interno das classes. Use abstração e encapsulamento para criar um modelo robusto e flexível.