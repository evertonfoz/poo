# 🎯 Desafio Prático: Sistema de Gerenciamento de Eventos

## 📝 Descrição do Problema

Você foi contratado para desenvolver um **Sistema de Gerenciamento de Eventos** para uma empresa de organização de conferências, workshops e seminários. O sistema deve permitir o cadastro de eventos, palestrantes e participantes, aplicando todas as técnicas de programação defensiva e null safety aprendidas.

---

## 🎓 Objetivo do Desafio

Aplicar na prática todos os conceitos estudados:
- ✅ Guard Clauses para validação de parâmetros
- ✅ TryParseNonEmpty para validação de strings
- ✅ [MemberNotNull] para lazy loading
- ✅ [DisallowNull] e [AllowNull] para controle de nullability
- ✅ Testes unitários completos
- ✅ Documentação clara

---

## 📋 Requisitos Funcionais

### 1. Classe `Speaker` (Palestrante)

**Propriedades obrigatórias:**
- `SpeakerId` (int) - Identificador único (não pode ser negativo)
- `FullName` (string) - Nome completo (não pode ser null ou vazio)
- `Email` (string) - Email de contato (não pode ser null ou vazio)

**Propriedades opcionais:**
- `Biography` (string?) - Biografia do palestrante
- `Company` (string) - Empresa onde trabalha (use [AllowNull])
- `LinkedInProfile` (string) - URL do LinkedIn (use [AllowNull])

**Regras de negócio:**
- SpeakerId deve ser maior ou igual a 1
- Email deve conter '@' (validação básica)
- FullName não pode ter apenas espaços em branco
- Biography aceita null ou vazio (usar TryParseNonEmpty)
- Company e LinkedInProfile aceitam null mas nunca retornam null (convertem para string vazia)

**Métodos:**
- Construtor validando SpeakerId, FullName e Email
- `SetBiography(string? biography)` - com validação TryParseNonEmpty
- `Equals` e `GetHashCode` baseados em `SpeakerId`
- `ToString()` retornando representação formatada

### 2. Classe `Venue` (Local do Evento)

**Propriedades obrigatórias:**
- `VenueId` (int) - Identificador único (não pode ser negativo)
- `Name` (string) - Nome do local (não pode ser null)
- `Address` (string) - Endereço completo (não pode ser null)
- `Capacity` (int) - Capacidade máxima (deve ser positivo)

**Propriedades opcionais:**
- `Description` (string?) - Descrição do local
- `ParkingInfo` (string) - Informações sobre estacionamento (use [AllowNull])

**Regras de negócio:**
- VenueId >= 1
- Capacity deve ser > 0
- Address não pode ser vazio ou só espaços
- Propriedade estática `Default` retornando local virtual "Online Event"

**Métodos:**
- Construtor com validações apropriadas
- `SetDescription(string? description)` usando TryParseNonEmpty
- `Equals` e `GetHashCode` baseados em `VenueId`

### 3. Classe `Event` (Evento)

**Propriedades obrigatórias:**
- `EventId` (int) - Identificador único
- `Title` (string) - Título do evento (não pode ser null)
- `EventCode` (string) - Código único do evento (use [DisallowNull])
- `EventDate` (DateTime) - Data do evento
- `Duration` (TimeSpan) - Duração do evento

**Propriedades com lazy loading:**
- `Venue` (Venue) - Local do evento (usar [MemberNotNull] e lazy loading para `Venue.Default`)
- `MainSpeaker` (Speaker?) - Palestrante principal (opcional)

**Propriedades opcionais:**
- `Description` (string?) - Descrição do evento
- `Requirements` (string) - Requisitos para participação (use [AllowNull])
- `Notes` (string) - Observações gerais (use [AllowNull])

**Regras de negócio:**
- EventId >= 1
- EventCode inicializa como string vazia, nunca retorna null
- EventDate não pode ser no passado (validar no construtor)
- Duration deve ser >= 30 minutos
- Venue usa lazy loading, carrega Default se não definido
- MainSpeaker é opcional (pode ser null)

**Métodos:**
- Construtor validando EventId, Title e EventDate
- `SetEventCode(string code)` - validação com Guard.AgainstNull e trim
- `SetDescription(string? description)` - validação com TryParseNonEmpty
- `AssignMainSpeaker(Speaker speaker)` - atribui palestrante principal
- `ToString()` retornando informações formatadas

### 4. Classe Guard (estendida)

Além dos métodos já conhecidos, adicione:

```csharp
public static void AgainstNegativeOrZero(int value, string paramName)
{
    if (value <= 0)
        throw new ArgumentOutOfRangeException(paramName, 
            $"{paramName} must be greater than zero.");
}

public static void AgainstPastDate(DateTime date, string paramName)
{
    if (date < DateTime.Now)
        throw new ArgumentException(
            $"{paramName} cannot be in the past.", paramName);
}

public static bool IsValidEmail(string? email)
{
    return !string.IsNullOrWhiteSpace(email) && email.Contains('@');
}
```

---

## 🏗️ Estrutura do Projeto

### Organização de Pastas

```
EventManagement/
├── README.md (sua documentação)
├── EventManagement.sln
├── src/
│   ├── EventManagement.Domain/
│   │   ├── EventManagement.Domain.csproj
│   │   ├── Guards/
│   │   │   └── Guard.cs
│   │   ├── Entities/
│   │   │   ├── Speaker.cs
│   │   │   ├── Venue.cs
│   │   │   └── Event.cs
│   └── EventManagement.Console/
│       ├── EventManagement.Console.csproj
│       └── Program.cs
├── tests/
│   └── EventManagement.Domain.Tests/
│       ├── EventManagement.Domain.Tests.csproj
│       ├── SpeakerSpecs.cs
│       ├── VenueSpecs.cs
│       └── EventSpecs.cs
```

---

## ✅ Requisitos de Testes

### Para cada classe, crie testes para:

#### Speaker (mínimo 15 testes)
- ✅ Construtor com dados válidos
- ✅ SpeakerId negativo ou zero (deve lançar exceção)
- ✅ FullName nulo (deve lançar exceção)
- ✅ FullName vazio ou só espaços (deve lançar exceção)
- ✅ Email nulo ou inválido (deve lançar exceção)
- ✅ SetBiography com texto válido
- ✅ SetBiography com null, vazio, espaços
- ✅ Company aceita null mas retorna string vazia
- ✅ LinkedInProfile aceita null mas retorna string vazia
- ✅ Equals com mesmo SpeakerId
- ✅ Equals com SpeakerId diferente
- ✅ GetHashCode consistente
- ✅ ToString formata corretamente

#### Venue (mínimo 12 testes)
- ✅ Construtor com dados válidos
- ✅ VenueId inválido (negativo ou zero)
- ✅ Name nulo ou vazio
- ✅ Address nulo, vazio ou só espaços
- ✅ Capacity zero ou negativo
- ✅ SetDescription válida e inválida
- ✅ ParkingInfo com null
- ✅ Propriedade Default funciona
- ✅ Equals e GetHashCode

#### Event (mínimo 18 testes)
- ✅ Construtor com dados válidos
- ✅ EventId inválido
- ✅ Title nulo ou vazio
- ✅ EventDate no passado (deve lançar exceção)
- ✅ Duration menor que 30 minutos (deve lançar exceção)
- ✅ EventCode inicializa vazio
- ✅ SetEventCode válido e com trim
- ✅ SetEventCode nulo (exceção)
- ✅ EventCode nunca retorna null ([DisallowNull])
- ✅ SetDescription válida e inválida
- ✅ Requirements aceita null mas retorna vazio
- ✅ Notes aceita null mas retorna vazio
- ✅ Venue lazy loading carrega Default
- ✅ Venue múltiplos acessos retornam mesma instância
- ✅ AssignMainSpeaker com speaker válido
- ✅ MainSpeaker pode ser null

---

## 🎨 Requisitos do Program.cs

Crie exemplos práticos demonstrando:

### Região 1: Speaker Examples
- Criar palestrantes válidos
- Tentar criar com dados inválidos (capturar exceções)
- Demonstrar SetBiography com diferentes valores
- Mostrar Company e LinkedInProfile com null

### Região 2: Venue Examples
- Criar locais válidos
- Demonstrar Venue.Default
- Mostrar SetDescription
- Demonstrar ParkingInfo com null

### Região 3: Event Examples
- Criar eventos válidos
- Mostrar lazy loading de Venue
- Demonstrar SetEventCode e [DisallowNull]
- Atribuir palestrante principal
- Mostrar Requirements e Notes com [AllowNull]

### Região 4: Complete Scenario
Criar um cenário completo:
```csharp
// Criar palestrante
var speaker = new Speaker(1, "João Silva", "joao@email.com");
speaker.SetBiography("Especialista em C# com 10 anos de experiência");
speaker.Company = "Tech Corp";

// Criar local
var venue = new Venue(1, "Centro de Convenções", "Av. Principal, 100", 500);
venue.SetDescription("Moderno centro com infraestrutura completa");

// Criar evento
var evento = new Event(1, ".NET Conference 2025", new DateTime(2025, 12, 15), TimeSpan.FromHours(8));
evento.SetEventCode("NETCONF2025");
evento.SetDescription("Conferência anual sobre tecnologias .NET");
evento.AssignMainSpeaker(speaker);

// Exibir informações
Console.WriteLine(evento);
Console.WriteLine($"Local: {evento.Venue}");
Console.WriteLine($"Palestrante: {evento.MainSpeaker?.FullName ?? "A definir"}");
```

---

## 📦 Entregáveis

### 1. Repositório Git

Estrutura esperada:
```
seu-usuario/event-management-system
├── README.md
├── .gitignore
├── EventManagement.sln
├── src/
├── tests/
└── docs/
    └── EXPLICACAO.md
```

### 2. README.md do Projeto

Deve conter:

```markdown
# Sistema de Gerenciamento de Eventos

## 📋 Descrição
[Breve descrição do projeto]

## 🚀 Tecnologias Utilizadas
- .NET 9.0
- C# 13
- xUnit
- [outras]

## 🏗️ Arquitetura
[Explicar organização do projeto]

## ⚙️ Como Executar

### Pré-requisitos
- .NET SDK 9.0+

### Passos
\`\`\`bash
# Clonar repositório
git clone [url]

# Restaurar dependências
dotnet restore

# Executar testes
dotnet test

# Executar aplicação
dotnet run --project src/EventManagement.Console
\`\`\`

## 🧪 Testes
[Informações sobre cobertura de testes]

## 📚 Conceitos Aplicados
- Guard Clauses
- TryParseNonEmpty
- [MemberNotNull]
- [DisallowNull]
- [AllowNull]
- Lazy Loading
- Métodos de Identidade

## 👤 Autor
[Seu nome]
```

### 3. EXPLICACAO.md (documentação técnica)

Deve seguir o modelo do MATERIAL_DIDATICO.md, incluindo:

#### Seção 1: Introdução
- Problema que o sistema resolve
- Decisões de design

#### Seção 2: Guard Clauses Implementados
- Explicar cada Guard usado
- Exemplos de código
- Por que foram necessários

#### Seção 3: TryParseNonEmpty
- Onde foi usado
- Motivo da escolha
- Comparação com alternativas

#### Seção 4: [MemberNotNull] - Lazy Loading
- Implementação do Venue em Event
- Benefícios da abordagem
- Testes que comprovam funcionamento

#### Seção 5: [DisallowNull] vs [AllowNull]
- EventCode com [DisallowNull]
- Requirements/Notes com [AllowNull]
- Diferenças e quando usar cada um

#### Seção 6: Métodos de Identidade
- Equals e GetHashCode em Speaker e Venue
- Importância para comparações
- Testes que validam

#### Seção 7: Validações Customizadas
- AgainstNegativeOrZero
- AgainstPastDate
- IsValidEmail
- Casos de uso

#### Seção 8: Testes Unitários
- Estratégia de testes
- Cobertura alcançada
- Testes mais importantes

#### Seção 9: Desafios Encontrados
- Problemas durante desenvolvimento
- Soluções adotadas
- Aprendizados

#### Seção 10: Conclusão
- Resumo do que foi implementado
- Conceitos consolidados
- Próximos passos

---

## 🎯 Critérios de Avaliação

### Implementação (40 pontos)
- [ ] Todas as classes implementadas corretamente (15 pts)
- [ ] Guard Clauses aplicados adequadamente (10 pts)
- [ ] Atributos de nullability corretos (10 pts)
- [ ] Lazy loading funcionando (5 pts)

### Testes (30 pontos)
- [ ] Mínimo de 45 testes (15 pts)
- [ ] Cobertura de cenários válidos e inválidos (10 pts)
- [ ] Nomenclatura clara dos testes (5 pts)

### Documentação (20 pontos)
- [ ] README.md completo e claro (10 pts)
- [ ] EXPLICACAO.md detalhado (10 pts)

### Código (10 pontos)
- [ ] Organização e legibilidade (5 pts)
- [ ] Boas práticas C# (5 pts)

### Bônus (até 10 pontos extras)
- [ ] Validação de email mais robusta (3 pts)
- [ ] Implementação de mais Guards customizados (3 pts)
- [ ] Interface de linha de comando interativa (4 pts)

**Total: 100 pontos (110 com bônus)**

---

## 💡 Dicas para o Sucesso

### 1. Planejamento
- ✅ Leia todo o enunciado antes de começar
- ✅ Faça um esboço da estrutura do projeto
- ✅ Liste as validações necessárias

### 2. Implementação
- ✅ Comece pela classe Guard
- ✅ Implemente uma classe por vez
- ✅ Teste cada classe antes de prosseguir

### 3. Testes
- ✅ Escreva testes enquanto desenvolve
- ✅ Teste casos válidos E inválidos
- ✅ Verifique a cobertura com `dotnet test --collect:"XPlat Code Coverage"`

### 4. Documentação
- ✅ Documente enquanto desenvolve
- ✅ Use exemplos práticos
- ✅ Explique o "por quê" das decisões

### 5. Revisão
- ✅ Execute todos os testes
- ✅ Compile o projeto sem warnings
- ✅ Revise a documentação
- ✅ Teste o clone do repositório em outra pasta

---

## 📅 Cronograma Sugerido

### Semana 1: Estrutura e Guard
- Dia 1-2: Criar estrutura do projeto e classe Guard
- Dia 3-4: Implementar e testar classe Speaker
- Dia 5: Documentar Speaker

### Semana 2: Venue e Event
- Dia 1-2: Implementar e testar classe Venue
- Dia 3-5: Implementar e testar classe Event (mais complexa)

### Semana 3: Integração e Documentação
- Dia 1-2: Program.cs com exemplos práticos
- Dia 3-4: Documentação completa (README + EXPLICACAO.md)
- Dia 5: Revisão final e ajustes

---

## 🔗 Recursos Úteis

### Documentação
- [Material Didático do Curso](./MATERIAL_DIDATICO.md)
- [Microsoft Docs - Nullable Reference Types](https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references)
- [xUnit Documentation](https://xunit.net/docs/getting-started/netcore/cmdline)

### Ferramentas
- [.NET CLI Reference](https://learn.microsoft.com/en-us/dotnet/core/tools/)
- [Git Basics](https://git-scm.com/book/en/v2/Getting-Started-Git-Basics)
- [Markdown Guide](https://www.markdownguide.org/)

---

## ❓ FAQ - Perguntas Frequentes

### 1. Posso usar outras bibliotecas?
Sim, mas apenas para testes (como FluentAssertions). O domínio deve usar apenas .NET padrão.

### 2. Preciso implementar persistência (banco de dados)?
Não. Foque nas validações e null safety. Persistência está fora do escopo.

### 3. Posso adicionar mais propriedades?
Sim, desde que implemente as obrigatórias e mantenha o foco nos conceitos aprendidos.

### 4. Como faço para validar email de forma mais robusta?
Para o bônus, você pode usar Regex ou a classe MailAddress do .NET.

### 5. Devo seguir exatamente a estrutura proposta?
Sim, para facilitar a correção. Variações devem ser justificadas na documentação.

---

## 🏆 Exemplos de Excelência

Um projeto excelente deve:
- ✅ Compilar sem erros ou warnings
- ✅ Ter 100% dos testes passando
- ✅ Aplicar todos os conceitos aprendidos corretamente
- ✅ Ter documentação clara e completa
- ✅ Seguir boas práticas de código C#
- ✅ Ter commits organizados com mensagens descritivas

---

## 📬 Entrega

### Formato
- Repositório público no GitHub
- Link do repositório enviado até [DATA]
- README.md visível na página principal

### Checklist Final
- [ ] Projeto compila sem erros
- [ ] Todos os testes passam
- [ ] README.md completo
- [ ] EXPLICACAO.md detalhado
- [ ] Código bem organizado
- [ ] .gitignore configurado
- [ ] Commits com mensagens claras

---

## 🎓 Objetivos de Aprendizagem Alcançados

Ao completar este desafio, você terá demonstrado capacidade de:

1. ✅ Aplicar Guard Clauses para validação robusta
2. ✅ Utilizar TryParseNonEmpty para validação sem exceções
3. ✅ Implementar lazy loading com [MemberNotNull]
4. ✅ Diferenciar e usar [DisallowNull] e [AllowNull]
5. ✅ Criar métodos de identidade (Equals/GetHashCode)
6. ✅ Escrever testes unitários abrangentes
7. ✅ Documentar código e decisões técnicas
8. ✅ Organizar projeto seguindo boas práticas

---

**Boa sorte e bom código! 🚀**

*Desafio criado para consolidação de conhecimentos em Programação Defensiva e Null Safety - 2025*
