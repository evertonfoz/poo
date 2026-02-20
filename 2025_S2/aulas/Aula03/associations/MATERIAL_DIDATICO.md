# Material Didático: Guard Clauses e Atributos de Nullability em C#

## 📚 Índice

1. [Introdução](#introdução)
2. [Conceitos Fundamentais](#conceitos-fundamentais)
3. [Parte 1: Guard Clauses](#parte-1-guard-clauses)
4. [Parte 2: TryParseNonEmpty](#parte-2-tryparsenonempty)
5. [Parte 3: MemberNotNull](#parte-3-membernotnull)
6. [Parte 4: DisallowNull e AllowNull](#parte-4-disallownull-e-allownull)
7. [Estrutura Completa do Projeto](#estrutura-completa-do-projeto)
8. [Exercícios Práticos](#exercícios-práticos)
9. [Referências](#referências)

---

## Introdução

Este material apresenta técnicas avançadas de programação defensiva em C#, focando em validação de entrada de dados e controle de nullability. Você aprenderá a criar código mais seguro, legível e manutenível.

### Objetivos de Aprendizagem

Ao final deste material, você será capaz de:
- ✅ Implementar Guard Clauses para validação de parâmetros
- ✅ Utilizar atributos de nullability do C# moderno
- ✅ Aplicar padrões de lazy loading com segurança
- ✅ Criar testes unitários para validações
- ✅ Compreender a diferença entre os atributos `[DisallowNull]`, `[AllowNull]` e `[MemberNotNull]`

### Pré-requisitos

- C# básico e intermediário
- Conceitos de POO (Programação Orientada a Objetos)
- .NET 9.0 ou superior
- xUnit para testes unitários

---

## Conceitos Fundamentais

### O que é Null Safety?

Null safety é um conjunto de práticas e recursos da linguagem que ajudam a prevenir o famoso `NullReferenceException`, um dos erros mais comuns em aplicações C#.

### Contexto de Nullable Reference Types (C# 8.0+)

A partir do C# 8.0, podemos habilitar o contexto de nullable reference types, que torna todas as referências não-anuláveis por padrão:

```csharp
#nullable enable

string nomeObrigatorio;     // Não pode ser null
string? nomeOpcional;       // Pode ser null
```

### Programação Defensiva

É uma prática de programação que antecipa possíveis erros e condições inválidas, validando dados antes de usá-los.

---

## Parte 1: Guard Clauses

### 1.1 O que são Guard Clauses?

Guard Clauses (cláusulas de guarda) são validações colocadas no início de métodos para verificar se os parâmetros atendem aos requisitos necessários. Se não atenderem, o método "falha rápido" (fail-fast), lançando uma exceção imediatamente.

### 1.2 Por que usar Guard Clauses?

**Sem Guard Clause:**
```csharp
public class Pedido
{
    private Cliente _cliente;
    
    public Pedido(Cliente cliente)
    {
        _cliente = cliente;
        // Se cliente for null, só descobriremos depois, 
        // quando tentarmos usar _cliente.Nome ou similar
    }
    
    public void ProcessarPedido()
    {
        // NullReferenceException aqui! 💥
        Console.WriteLine(_cliente.Nome);
    }
}
```

**Com Guard Clause:**
```csharp
public class Pedido
{
    private Cliente _cliente;
    
    public Pedido(Cliente cliente)
    {
        // Falha imediatamente se cliente for null
        if (cliente is null)
            throw new ArgumentNullException(nameof(cliente));
            
        _cliente = cliente;
    }
    
    public void ProcessarPedido()
    {
        // Seguro! _cliente nunca será null
        Console.WriteLine(_cliente.Nome);
    }
}
```

### 1.3 Criando a Classe Guard

**Objetivo:** Centralizar validações comuns em uma classe estática reutilizável.

**Localização:** `src/Associations.Domain/Guards/Guard.cs`

```csharp
using System.Diagnostics.CodeAnalysis;

namespace Associations.Domain.Guards;

public static class Guard
{
    public static void AgainstNull<[DynamicallyAccessedMembers(0)] T>(
        [NotNull] ref T? value, string paramName)
    {
        if (value is null)
            throw new ArgumentNullException(paramName);
    }
}
```

**Explicação dos componentes:**

1. **`static class Guard`**: Classe estática pois não precisa ser instanciada
2. **`<T>`**: Método genérico que funciona com qualquer tipo
3. **`[DynamicallyAccessedMembers(0)]`**: Informa ao compilador que não acessamos membros via reflexão (otimização para AOT)
4. **`[NotNull]`**: Atributo que informa ao analisador que após o método retornar, `value` não será null
5. **`ref T? value`**: Parâmetro por referência, permite ao compilador atualizar o estado de nullability
6. **`nameof(paramName)`**: Obtém o nome do parâmetro em tempo de compilação

### 1.4 Usando Guard.AgainstNull

**Exemplo: Classe Product**

```csharp
using Associations.Domain.Guards;

namespace Product;

public class Product
{
    public string Name { get; }
    
    public Product(string name)
    {
        // Valida antes de usar
        Guard.AgainstNull(ref name, nameof(name));
        Name = name;
    }
}
```

**Uso:**
```csharp
// ✅ Funciona
var produto = new Product("Laptop");

// ❌ Lança ArgumentNullException
var produtoInvalido = new Product(null);
```

### 1.5 Testes para Guard.AgainstNull

**Localização:** `tests/Associations.Domain.Tests/ProductSpecs.cs`

```csharp
[Fact]
public void Ctor_NameNulo_DeveLancarArgumentNullException()
{
    // Arrange
    string? name = null;

    // Act & Assert
    Assert.Throws<ArgumentNullException>(() => new Product.Product(name!));
}

[Fact]
public void Ctor_NameNulo_DeveLancarArgumentNullExceptionComParametroCorreto()
{
    // Arrange
    string? name = null;

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => new Product.Product(name!));
    Assert.Equal("name", exception.ParamName);
}
```

### 1.6 Exercício Prático 1

**Tarefa:** Crie a classe `Guard` e a classe `Product` com validação no construtor.

**Passos:**
1. Criar o diretório `src/Associations.Domain/Guards/`
2. Criar o arquivo `Guard.cs` com o método `AgainstNull`
3. Criar o diretório `src/Associations.Domain/Product/`
4. Criar o arquivo `Product.cs` com validação no construtor
5. Criar testes em `tests/Associations.Domain.Tests/ProductSpecs.cs`
6. Executar os testes: `dotnet test`

---

## Parte 2: TryParseNonEmpty

### 2.1 O padrão Try-Parse

O padrão Try-Parse é comum em C# (exemplo: `int.TryParse`). Ao invés de lançar exceção, retorna `bool` indicando sucesso ou falha.

**Vantagens:**
- ✅ Sem exceções para casos esperados de falha
- ✅ Melhor performance
- ✅ Código mais limpo

### 2.2 Implementando TryParseNonEmpty

Adicione este método à classe `Guard`:

```csharp
public static bool TryParseNonEmpty(string? s, 
    [NotNullWhen(true)] out string? result)
{
    if (!string.IsNullOrWhiteSpace(s)) 
    { 
        result = s; 
        return true; 
    }
    result = null; 
    return false;
}
```

**Explicação:**

1. **`[NotNullWhen(true)]`**: Quando o método retorna `true`, `result` não será null
2. **`out string? result`**: Parâmetro de saída
3. **`string.IsNullOrWhiteSpace`**: Valida se a string tem conteúdo real

### 2.3 Usando TryParseNonEmpty

Adicione à classe `Product`:

```csharp
public class Product
{
    public string Name { get; }
    public string? Description { get; private set; }
    
    // ... construtor ...
    
    public void SetDescription(string? description)
    {
        if (Guard.TryParseNonEmpty(description, out string? validDescription))
        {
            Description = validDescription;
        }
        else
        {
            Description = null;
        }
    }
}
```

### 2.4 Testes para TryParseNonEmpty

```csharp
[Fact]
public void SetDescription_DescricaoValida_DeveDefinirDescricao()
{
    var product = new Product.Product("Laptop");
    var description = "High performance laptop";
    
    product.SetDescription(description);
    
    Assert.Equal(description, product.Description);
}

[Fact]
public void SetDescription_DescricaoNula_DeveDefinirComoNull()
{
    var product = new Product.Product("Laptop");
    
    product.SetDescription(null);
    
    Assert.Null(product.Description);
}

[Fact]
public void SetDescription_DescricaoVazia_DeveDefinirComoNull()
{
    var product = new Product.Product("Laptop");
    
    product.SetDescription("");
    
    Assert.Null(product.Description);
}

[Fact]
public void SetDescription_DescricaoApenasEspacos_DeveDefinirComoNull()
{
    var product = new Product.Product("Laptop");
    
    product.SetDescription("   ");
    
    Assert.Null(product.Description);
}
```

### 2.5 Exercício Prático 2

**Tarefa:** Adicione o método `TryParseNonEmpty` e a propriedade `Description`.

**Passos:**
1. Adicionar `TryParseNonEmpty` em `Guard.cs`
2. Adicionar propriedade `Description` em `Product.cs`
3. Criar método `SetDescription` com validação
4. Criar 4 testes para diferentes cenários
5. Executar os testes

---

## Parte 3: MemberNotNull

### 3.1 O problema do Lazy Loading

```csharp
public class Product
{
    private Category? _category;
    
    public Category Category => _category; // Warning: pode retornar null!
}
```

### 3.2 Solução com [MemberNotNull]

O atributo `[MemberNotNull]` informa ao compilador que após a execução do método, um membro específico não será null.

### 3.3 Criando a Classe Category

**Localização:** `src/Associations.Domain/Category/Category.cs`

```csharp
using Associations.Domain.Guards;

namespace Category;

public class Category
{
    public int CategoryId { get; }
    public string Name { get; }
    public string? Description { get; private set; }

    public static Category Default => new(0, "Uncategorized");

    public Category(int categoryId, string name)
    {
        if (categoryId < 0)
            throw new ArgumentOutOfRangeException(nameof(categoryId), 
                "CategoryId cannot be negative.");

        Guard.AgainstNull(ref name, nameof(name));

        CategoryId = categoryId;
        Name = name;
    }

    public void SetDescription(string? description)
    {
        if (Guard.TryParseNonEmpty(description, out string? validDescription))
        {
            Description = validDescription;
        }
        else
        {
            Description = null;
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Category other)
            return false;

        return CategoryId == other.CategoryId;
    }

    public override int GetHashCode()
    {
        return CategoryId.GetHashCode();
    }

    public override string ToString()
    {
        return $"Category [Id: {CategoryId}, Name: {Name}, Description: {Description ?? "N/A"}]";
    }
}
```

**Conceitos importantes:**

1. **Propriedade estática `Default`**: Fornece uma categoria padrão
2. **Métodos de identidade**: `Equals` e `GetHashCode` baseados em `CategoryId`
3. **Validações**: CategoryId não pode ser negativo, Name não pode ser null

### 3.4 Usando [MemberNotNull] em Product

```csharp
using System.Diagnostics.CodeAnalysis;
using CategoryNs = Category;

public class Product
{
    private CategoryNs.Category? _category;
    
    // Propriedade que garante categoria carregada
    public CategoryNs.Category Category
    {
        get
        {
            EnsureCategory();
            return _category; // Compilador sabe que não é null
        }
    }
    
    // Método que garante _category não-null
    [MemberNotNull(nameof(_category))]
    private void EnsureCategory()
    {
        _category ??= CategoryNs.Category.Default;
    }
}
```

**Explicação:**

1. **`_category ??= ...`**: Operador de atribuição null-coalescing (só atribui se for null)
2. **`[MemberNotNull(nameof(_category))]`**: Garante ao compilador que após executar, `_category` não será null
3. **Lazy Loading**: Categoria só é carregada no primeiro acesso

### 3.5 Testes para Category e MemberNotNull

```csharp
[Fact]
public void Category_PrimeiroAcesso_DeveCarregarCategoriaDefault()
{
    var product = new Product.Product("Laptop");
    
    var category = product.Category;
    
    Assert.NotNull(category);
    Assert.Equal(0, category.CategoryId);
    Assert.Equal("Uncategorized", category.Name);
}

[Fact]
public void Category_MultiploAcessos_DeveRetornarMesmaInstancia()
{
    var product = new Product.Product("Laptop");
    
    var category1 = product.Category;
    var category2 = product.Category;
    
    Assert.Same(category1, category2); // Mesma instância
}
```

### 3.6 Exercício Prático 3

**Tarefa:** Implemente a classe `Category` e integre com `Product` usando `[MemberNotNull]`.

**Passos:**
1. Criar `src/Associations.Domain/Category/Category.cs`
2. Implementar validações e métodos de identidade
3. Criar testes para `Category` em `CategorySpecs.cs`
4. Adicionar propriedade `Category` em `Product` com lazy loading
5. Criar testes de integração Product-Category
6. Executar todos os testes

---

## Parte 4: DisallowNull e AllowNull

### 4.1 Entendendo os Atributos

| Atributo | Setter | Getter | Uso |
|----------|--------|--------|-----|
| `[DisallowNull]` | Não aceita null | Nunca retorna null | Valores obrigatórios que podem iniciar vazios |
| `[AllowNull]` | Aceita null | Nunca retorna null | Valores opcionais que convertem null em default |

### 4.2 Implementando [DisallowNull]

```csharp
public class Product
{
    [DisallowNull]
    public string ProductCode { get; private set; } = string.Empty;
    
    public void SetProductCode(string code)
    {
        Guard.AgainstNull(ref code, nameof(code));
        ProductCode = code.Trim();
    }
}
```

**Características:**
- ✅ Nunca retorna null
- ✅ Pode ser string vazia inicialmente
- ✅ Setter valida e previne null

### 4.3 Implementando [AllowNull]

```csharp
public class Product
{
    private string _notes = string.Empty;
    
    [AllowNull]
    public string Notes
    {
        get => _notes;
        set => _notes = value ?? string.Empty;
    }
}
```

**Características:**
- ✅ Setter aceita null (semântica de "ausente")
- ✅ Getter sempre retorna não-null
- ✅ Converte null em string vazia automaticamente

### 4.4 Comparação Prática

```csharp
var product = new Product("Laptop");

// [DisallowNull] ProductCode
Console.WriteLine(product.ProductCode); // "" (vazio, nunca null)
product.SetProductCode("LAP-001");      // ✅ OK
// product.SetProductCode(null);        // ❌ Lança exceção

// [AllowNull] Notes
Console.WriteLine(product.Notes);       // "" (vazio, nunca null)
product.Notes = "Important notes";      // ✅ OK
product.Notes = null;                   // ✅ OK, converte para ""
Console.WriteLine(product.Notes);       // "" (não null!)
```

### 4.5 Testes Completos

```csharp
// Testes para [DisallowNull] ProductCode
[Fact]
public void ProductCode_ValorInicial_DeveSerStringVazia()
{
    var product = new Product.Product("Laptop");
    
    Assert.NotNull(product.ProductCode);
    Assert.Equal(string.Empty, product.ProductCode);
}

[Fact]
public void SetProductCode_CodigoNulo_DeveLancarArgumentNullException()
{
    var product = new Product.Product("Laptop");
    
    Assert.Throws<ArgumentNullException>(() => product.SetProductCode(null!));
}

// Testes para [AllowNull] Notes
[Fact]
public void Notes_DefinirNull_DeveRetornarStringVazia()
{
    var product = new Product.Product("Laptop");
    product.Notes = "Some notes";
    
    product.Notes = null; // Setter aceita null
    
    Assert.NotNull(product.Notes);
    Assert.Equal(string.Empty, product.Notes);
}

[Fact]
public void Notes_SempreRetornaStringNaoNula_MesmoAposSetNull()
{
    var product = new Product.Product("Laptop");
    
    product.Notes = "First";
    var notes1 = product.Notes;
    
    product.Notes = null;
    var notes2 = product.Notes;
    
    Assert.NotNull(notes1);
    Assert.NotNull(notes2);
    Assert.Equal(string.Empty, notes2);
}
```

### 4.6 Quando usar cada atributo?

**Use `[DisallowNull]`:**
- Identificadores que não podem ser null
- Códigos, SKUs, chaves
- Propriedades que têm valor obrigatório mas podem iniciar vazias

**Use `[AllowNull]`:**
- Propriedades opcionais que preferem default ao invés de null
- Campos de texto onde null significa "sem valor"
- Situações onde você quer converter null em valor padrão

### 4.7 Exercício Prático 4

**Tarefa:** Implemente `ProductCode` e `Notes` com seus respectivos atributos.

**Passos:**
1. Adicionar propriedade `ProductCode` com `[DisallowNull]`
2. Criar método `SetProductCode` com validação
3. Adicionar propriedade `Notes` com `[AllowNull]`
4. Criar 5 testes para cada propriedade
5. Executar testes
6. Criar exemplos no `Program.cs`

---

## Estrutura Completa do Projeto

### Organização de Pastas

```
Associations/
├── src/
│   └── Associations.Domain/
│       ├── Guards/
│       │   └── Guard.cs
│       ├── Product/
│       │   └── Product.cs
│       └── Category/
│           └── Category.cs
├── tests/
│   └── Associations.Domain.Tests/
│       ├── ProductSpecs.cs
│       └── CategorySpecs.cs
└── src/Associations.UI.Console/
    └── Program.cs
```

### Arquivo Guard.cs Completo

```csharp
using System.Diagnostics.CodeAnalysis;

namespace Associations.Domain.Guards;

public static class Guard
{
    public static void AgainstNull<[DynamicallyAccessedMembers(0)] T>(
        [NotNull] ref T? value, string paramName)
    {
        if (value is null)
            throw new ArgumentNullException(paramName);
    }

    public static bool TryParseNonEmpty(string? s, 
        [NotNullWhen(true)] out string? result)
    {
        if (!string.IsNullOrWhiteSpace(s)) 
        { 
            result = s; 
            return true; 
        }
        result = null; 
        return false;
    }
}
```

### Arquivo Product.cs Completo

```csharp
using Associations.Domain.Guards;
using System.Diagnostics.CodeAnalysis;
using CategoryNs = Category;

namespace Product;

public class Product
{
    private CategoryNs.Category? _category;
    private string _notes = string.Empty;

    public string Name { get; }
    public string? Description { get; private set; }
    
    [DisallowNull]
    public string ProductCode { get; private set; } = string.Empty;
    
    [AllowNull]
    public string Notes
    {
        get => _notes;
        set => _notes = value ?? string.Empty;
    }
    
    public CategoryNs.Category Category
    {
        get
        {
            EnsureCategory();
            return _category;
        }
    }
    
    public Product(string name)
    {
        Guard.AgainstNull(ref name, nameof(name));
        Name = name;
    }

    public void SetProductCode(string code)
    {
        Guard.AgainstNull(ref code, nameof(code));
        ProductCode = code.Trim();
    }

    public void SetDescription(string? description)
    {
        if (Guard.TryParseNonEmpty(description, out string? validDescription))
        {
            Description = validDescription;
        }
        else
        {
            Description = null;
        }
    }

    [MemberNotNull(nameof(_category))]
    private void EnsureCategory()
    {
        _category ??= CategoryNs.Category.Default;
    }
}
```

---

## Exercícios Práticos

### Exercício 1: Guard Básico
**Nível:** Iniciante

Crie uma classe `Cliente` com validação no construtor:
- Propriedade `Nome` (obrigatório)
- Propriedade `Email` (obrigatório)
- Use `Guard.AgainstNull` para ambos
- Crie 4 testes unitários

### Exercício 2: TryParse Customizado
**Nível:** Intermediário

Adicione à classe `Guard`:
```csharp
public static bool TryParsePositive(int? value, out int result)
{
    // Retorna true se value > 0
    // Caso contrário, result = 0 e retorna false
}
```

Use em uma propriedade `Quantity` da classe `Product`.

### Exercício 3: Lazy Loading Avançado
**Nível:** Intermediário

Crie uma classe `ProductRepository` que:
- Tem campo privado `List<Product>? _products`
- Método `[MemberNotNull(nameof(_products))] EnsureLoaded()`
- Propriedade pública `Products` que usa `EnsureLoaded`

### Exercício 4: Combinando Atributos
**Nível:** Avançado

Crie uma classe `Order` com:
- `[DisallowNull] string OrderNumber`
- `[AllowNull] string ShippingAddress`
- `[MemberNotNull] Customer Customer` (lazy loading)

Implemente todos os testes necessários.

### Exercício 5: Projeto Completo
**Nível:** Avançado

Crie um sistema de biblioteca com:
- Classe `Book` com Guard validations
- Classe `Author` com atributos de nullability
- Classe `Library` com lazy loading de coleções
- Testes completos para todas as classes
- Programa console demonstrando uso

---

## Referências

### Documentação Oficial Microsoft

- [Nullable Reference Types](https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references)
- [Attributes for null-state static analysis](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/attributes/nullable-analysis)
- [NotNull Attribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.notnullattribute)
- [MemberNotNull Attribute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.membernotnullattribute)

### Padrões e Práticas

- **Guard Clauses Pattern**: Técnica de programação defensiva
- **Fail-Fast Principle**: Detectar erros o mais cedo possível
- **Lazy Loading Pattern**: Carregar recursos apenas quando necessário
- **Try-Parse Pattern**: Validação sem exceções

### Atributos de Nullability

| Atributo | Descrição |
|----------|-----------|
| `[NotNull]` | Parâmetro/retorno não será null após execução |
| `[NotNullWhen(bool)]` | Não-null quando método retorna valor específico |
| `[MemberNotNull]` | Membro não será null após método executar |
| `[DisallowNull]` | Propriedade nunca retorna null |
| `[AllowNull]` | Setter aceita null, mas getter não retorna |

### Comandos Úteis

```bash
# Criar solução e projetos
dotnet new sln -n Associations
dotnet new classlib -n Associations.Domain -o src/Associations.Domain
dotnet new xunit -n Associations.Domain.Tests -o tests/Associations.Domain.Tests
dotnet new console -n Associations.UI.Console -o src/Associations.UI.Console

# Adicionar projetos à solução
dotnet sln add src/Associations.Domain/Associations.Domain.csproj
dotnet sln add tests/Associations.Domain.Tests/Associations.Domain.Tests.csproj
dotnet sln add src/Associations.UI.Console/Associations.UI.Console.csproj

# Adicionar referências
dotnet add tests/Associations.Domain.Tests reference src/Associations.Domain
dotnet add src/Associations.UI.Console reference src/Associations.Domain

# Compilar e testar
dotnet build
dotnet test
dotnet run --project src/Associations.UI.Console
```

---

## Resumo Final

### Conceitos Aprendidos

1. **Guard Clauses**: Validação no início dos métodos
2. **Try-Parse Pattern**: Validação sem exceções
3. **Lazy Loading**: Carregamento sob demanda
4. **Null Safety**: Controle de nullability com atributos

### Benefícios Práticos

- ✅ Código mais seguro e robusto
- ✅ Menos `NullReferenceException`
- ✅ Melhor experiência do desenvolvedor (IntelliSense)
- ✅ Detecção de erros em tempo de compilação
- ✅ Código auto-documentado

### Próximos Passos

1. Pratique implementando os exercícios propostos
2. Aplique estes conceitos em projetos reais
3. Explore outros atributos de nullability
4. Estude padrões de validação avançados
5. Contribua com código de qualidade

---

## 📞 Suporte

Para dúvidas e discussões, consulte:
- Documentação do projeto
- Issues no repositório
- Professor/instrutor do curso

---

**Bons estudos! 🚀**

*Material criado para fins educacionais - 2025*
