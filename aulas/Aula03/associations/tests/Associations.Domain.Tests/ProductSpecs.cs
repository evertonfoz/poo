using Product;

namespace Associations.Domain.Tests;

public class ProductSpecs
{
    [Fact]
    public void Ctor_NameValido_DeveCriarProduto()
    {
        // Arrange
        var name = "Laptop";

        // Act
        var product = new Product.Product(name);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(name, product.Name);
    }

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

    [Fact]
    public void SetDescription_DescricaoValida_DeveDefinirDescricao()
    {
        // Arrange
        var product = new Product.Product("Laptop");
        var description = "High performance laptop";

        // Act
        product.SetDescription(description);

        // Assert
        Assert.Equal(description, product.Description);
    }

    [Fact]
    public void SetDescription_DescricaoNula_DeveDefinirComoNull()
    {
        // Arrange
        var product = new Product.Product("Laptop");

        // Act
        product.SetDescription(null);

        // Assert
        Assert.Null(product.Description);
    }

    [Fact]
    public void SetDescription_DescricaoVazia_DeveDefinirComoNull()
    {
        // Arrange
        var product = new Product.Product("Laptop");

        // Act
        product.SetDescription("");

        // Assert
        Assert.Null(product.Description);
    }

    [Fact]
    public void SetDescription_DescricaoApenasEspacos_DeveDefinirComoNull()
    {
        // Arrange
        var product = new Product.Product("Laptop");

        // Act
        product.SetDescription("   ");

        // Assert
        Assert.Null(product.Description);
    }

    [Fact]
    public void SetDescription_DescricaoComEspacosNoInicio_DeveDefinirDescricao()
    {
        // Arrange
        var product = new Product.Product("Laptop");
        var description = "  Valid description  ";

        // Act
        product.SetDescription(description);

        // Assert
        Assert.Equal(description, product.Description);
    }

    #region Category Tests - Lazy Loading with [MemberNotNull]

    [Fact]
    public void Category_PrimeiroAcesso_DeveCarregarCategoriaDefault()
    {
        // Arrange
        var product = new Product.Product("Laptop");

        // Act
        var category = product.Category;

        // Assert
        Assert.NotNull(category);
        Assert.Equal(0, category.CategoryId);
        Assert.Equal("Uncategorized", category.Name);
    }

    [Fact]
    public void Category_MultiploAcessos_DeveRetornarMesmaInstancia()
    {
        // Arrange
        var product = new Product.Product("Laptop");

        // Act
        var category1 = product.Category;
        var category2 = product.Category;

        // Assert
        Assert.Same(category1, category2); // Mesma instância (lazy loading)
    }

    [Fact]
    public void Category_DeveSerCategoriaDefaultAposInstanciacaoProduto()
    {
        // Arrange & Act
        var product = new Product.Product("Mouse");
        
        // Assert
        Assert.Equal("Uncategorized", product.Category.Name);
        Assert.Equal(0, product.Category.CategoryId);
        Assert.Null(product.Category.Description);
    }

    [Fact]
    public void Category_NuncaNulo_GraciasAoMemberNotNull()
    {
        // Arrange
        var product = new Product.Product("Keyboard");

        // Act - Multiple accesses without null checks
        var categoryName = product.Category.Name;
        var categoryId = product.Category.CategoryId;
        var categoryDesc = product.Category.Description;

        // Assert - No NullReferenceException should occur
        Assert.NotNull(categoryName);
        Assert.Equal(0, categoryId);
        Assert.Null(categoryDesc);
    }

    #endregion

    #region ProductCode Tests - [DisallowNull]

    [Fact]
    public void ProductCode_ValorInicial_DeveSerStringVazia()
    {
        // Arrange & Act
        var product = new Product.Product("Laptop");

        // Assert
        Assert.NotNull(product.ProductCode); // Never returns null
        Assert.Equal(string.Empty, product.ProductCode);
    }

    [Fact]
    public void SetProductCode_CodigoValido_DeveDefinirCodigo()
    {
        // Arrange
        var product = new Product.Product("Laptop");
        var code = "LAP-001";

        // Act
        product.SetProductCode(code);

        // Assert
        Assert.Equal(code, product.ProductCode);
        Assert.NotNull(product.ProductCode); // DisallowNull ensures never null
    }

    [Fact]
    public void SetProductCode_CodigoComEspacos_DeveFazerTrim()
    {
        // Arrange
        var product = new Product.Product("Laptop");
        var code = "  LAP-001  ";

        // Act
        product.SetProductCode(code);

        // Assert
        Assert.Equal("LAP-001", product.ProductCode);
    }

    [Fact]
    public void SetProductCode_CodigoNulo_DeveLancarArgumentNullException()
    {
        // Arrange
        var product = new Product.Product("Laptop");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => product.SetProductCode(null!));
    }

    [Fact]
    public void ProductCode_NuncaRetornaNulo_DisallowNull()
    {
        // Arrange
        var product = new Product.Product("Laptop");

        // Act - Multiple accesses
        var code1 = product.ProductCode;
        product.SetProductCode("ABC123");
        var code2 = product.ProductCode;

        // Assert - Never null thanks to [DisallowNull]
        Assert.NotNull(code1);
        Assert.NotNull(code2);
    }

    #endregion

    #region Notes Tests - [AllowNull]

    [Fact]
    public void Notes_ValorInicial_DeveSerStringVazia()
    {
        // Arrange & Act
        var product = new Product.Product("Laptop");

        // Assert
        Assert.NotNull(product.Notes); // Getter never returns null
        Assert.Equal(string.Empty, product.Notes);
    }

    [Fact]
    public void Notes_DefinirValorValido_DeveArmazenarValor()
    {
        // Arrange
        var product = new Product.Product("Laptop");
        var notes = "High priority product";

        // Act
        product.Notes = notes;

        // Assert
        Assert.Equal(notes, product.Notes);
        Assert.NotNull(product.Notes);
    }

    [Fact]
    public void Notes_DefinirNull_DeveRetornarStringVazia()
    {
        // Arrange
        var product = new Product.Product("Laptop");
        product.Notes = "Some notes";

        // Act - Setter accepts null (AllowNull)
        product.Notes = null;

        // Assert - Getter never returns null
        Assert.NotNull(product.Notes);
        Assert.Equal(string.Empty, product.Notes);
    }

    [Fact]
    public void Notes_SetterAceitaNull_AllowNull()
    {
        // Arrange
        var product = new Product.Product("Laptop");

        // Act - No exception should be thrown
        product.Notes = null; // AllowNull on setter
        product.Notes = "Valid notes";
        product.Notes = null; // Can set to null again

        // Assert
        Assert.Equal(string.Empty, product.Notes); // But getter returns empty string
    }

    [Fact]
    public void Notes_SempreRetornaStringNaoNula_MesmoAposSetNull()
    {
        // Arrange
        var product = new Product.Product("Laptop");

        // Act
        product.Notes = "Initial notes";
        var notes1 = product.Notes;
        
        product.Notes = null; // Set to null
        var notes2 = product.Notes;
        
        product.Notes = "New notes";
        var notes3 = product.Notes;

        // Assert - All return non-null values
        Assert.NotNull(notes1);
        Assert.NotNull(notes2); // Even after setting null
        Assert.NotNull(notes3);
        Assert.Equal(string.Empty, notes2);
    }

    #endregion
}
