using Category;

namespace Associations.Domain.Tests;

public class CategorySpecs
{
    #region Constructor Tests

    [Fact]
    public void Ctor_CategoryIdENameValidos_DeveCriarCategoria()
    {
        // Arrange
        var categoryId = 1;
        var name = "Electronics";

        // Act
        var category = new Category.Category(categoryId, name);

        // Assert
        Assert.NotNull(category);
        Assert.Equal(categoryId, category.CategoryId);
        Assert.Equal(name, category.Name);
        Assert.Null(category.Description);
    }

    [Fact]
    public void Ctor_CategoryIdNegativo_DeveLancarArgumentOutOfRangeException()
    {
        // Arrange
        var categoryId = -1;
        var name = "Electronics";

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Category.Category(categoryId, name));
    }

    [Fact]
    public void Ctor_CategoryIdNegativo_DeveLancarExceptionComParametroCorreto()
    {
        // Arrange
        var categoryId = -1;
        var name = "Electronics";

        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Category.Category(categoryId, name));
        Assert.Equal("categoryId", exception.ParamName);
    }

    [Fact]
    public void Ctor_CategoryIdZero_DeveCriarCategoria()
    {
        // Arrange
        var categoryId = 0;
        var name = "Uncategorized";

        // Act
        var category = new Category.Category(categoryId, name);

        // Assert
        Assert.NotNull(category);
        Assert.Equal(categoryId, category.CategoryId);
    }

    [Fact]
    public void Ctor_NameNulo_DeveLancarArgumentNullException()
    {
        // Arrange
        var categoryId = 1;
        string? name = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Category.Category(categoryId, name!));
    }

    [Fact]
    public void Ctor_NameNulo_DeveLancarArgumentNullExceptionComParametroCorreto()
    {
        // Arrange
        var categoryId = 1;
        string? name = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => new Category.Category(categoryId, name!));
        Assert.Equal("name", exception.ParamName);
    }

    #endregion

    #region SetDescription Tests

    [Fact]
    public void SetDescription_DescricaoValida_DeveDefinirDescricao()
    {
        // Arrange
        var category = new Category.Category(1, "Electronics");
        var description = "Electronic products and devices";

        // Act
        category.SetDescription(description);

        // Assert
        Assert.Equal(description, category.Description);
    }

    [Fact]
    public void SetDescription_DescricaoNula_DeveDefinirComoNull()
    {
        // Arrange
        var category = new Category.Category(1, "Electronics");

        // Act
        category.SetDescription(null);

        // Assert
        Assert.Null(category.Description);
    }

    [Fact]
    public void SetDescription_DescricaoVazia_DeveDefinirComoNull()
    {
        // Arrange
        var category = new Category.Category(1, "Electronics");

        // Act
        category.SetDescription("");

        // Assert
        Assert.Null(category.Description);
    }

    [Fact]
    public void SetDescription_DescricaoApenasEspacos_DeveDefinirComoNull()
    {
        // Arrange
        var category = new Category.Category(1, "Electronics");

        // Act
        category.SetDescription("   ");

        // Assert
        Assert.Null(category.Description);
    }

    #endregion

    #region Identity Tests

    [Fact]
    public void Equals_CategoriasComMesmoCategoryId_DeveRetornarTrue()
    {
        // Arrange
        var category1 = new Category.Category(1, "Electronics");
        var category2 = new Category.Category(1, "Different Name");

        // Act
        var result = category1.Equals(category2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_CategoriasComCategoryIdDiferente_DeveRetornarFalse()
    {
        // Arrange
        var category1 = new Category.Category(1, "Electronics");
        var category2 = new Category.Category(2, "Electronics");

        // Act
        var result = category1.Equals(category2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_ComparacaoComNull_DeveRetornarFalse()
    {
        // Arrange
        var category = new Category.Category(1, "Electronics");

        // Act
        var result = category.Equals(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_ComparacaoComObjetoDiferente_DeveRetornarFalse()
    {
        // Arrange
        var category = new Category.Category(1, "Electronics");
        var other = "Not a category";

        // Act
        var result = category.Equals(other);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetHashCode_CategoriasComMesmoCategoryId_DevemTerMesmoHashCode()
    {
        // Arrange
        var category1 = new Category.Category(1, "Electronics");
        var category2 = new Category.Category(1, "Different Name");

        // Act
        var hash1 = category1.GetHashCode();
        var hash2 = category2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_CategoriasComCategoryIdDiferente_DevemTerHashCodeDiferente()
    {
        // Arrange
        var category1 = new Category.Category(1, "Electronics");
        var category2 = new Category.Category(2, "Electronics");

        // Act
        var hash1 = category1.GetHashCode();
        var hash2 = category2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    #endregion

    #region Default Property Tests

    [Fact]
    public void Default_DeveRetornarCategoriaUncategorized()
    {
        // Act
        var defaultCategory = Category.Category.Default;

        // Assert
        Assert.NotNull(defaultCategory);
        Assert.Equal(0, defaultCategory.CategoryId);
        Assert.Equal("Uncategorized", defaultCategory.Name);
        Assert.Null(defaultCategory.Description);
    }

    [Fact]
    public void Default_ChamadasMultiplas_DevemRetornarInstanciasDiferentes()
    {
        // Act
        var default1 = Category.Category.Default;
        var default2 = Category.Category.Default;

        // Assert
        Assert.NotSame(default1, default2); // Diferentes instâncias
        Assert.Equal(default1, default2); // Mas iguais por identidade (CategoryId)
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_SemDescricao_DeveRetornarFormatoCorreto()
    {
        // Arrange
        var category = new Category.Category(1, "Electronics");

        // Act
        var result = category.ToString();

        // Assert
        Assert.Equal("Category [Id: 1, Name: Electronics, Description: N/A]", result);
    }

    [Fact]
    public void ToString_ComDescricao_DeveRetornarFormatoCorreto()
    {
        // Arrange
        var category = new Category.Category(1, "Electronics");
        category.SetDescription("Electronic products");

        // Act
        var result = category.ToString();

        // Assert
        Assert.Equal("Category [Id: 1, Name: Electronics, Description: Electronic products]", result);
    }

    #endregion
}
