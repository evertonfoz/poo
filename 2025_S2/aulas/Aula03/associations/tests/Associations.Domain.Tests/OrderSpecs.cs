using Associations.Domain.Order;
using Associations.Domain.ValueObjects;
using OrderEntity = Associations.Domain.Order.Order;

namespace Associations.Domain.Tests;

public class OrderSpecs
{
    [Fact]
    public void AddItem_SkuInexistente_DeveAdicionarNovoItem()
    {
        // Arrange
        var order = new OrderEntity();
        var sku = "PROD-001";
        var quantity = 2.0;
        var unitPrice = new Money(50.00m);

        // Act
        order.AddItem(sku, quantity, unitPrice);

        // Assert
        Assert.Single(order.Items);
        Assert.Contains(order.Items, item => item.Sku == "PROD-001" && item.Quantity == 2.0);
    }

    [Fact]
    public void Decrease_QuantidadeMaiorQueAtual_LancaExcecao()
    {
        // Arrange
        var sku = "PROD-002";
        var quantidadeInicial = 5.0;
        var unitPrice = new Money(100.00m);
        var orderItem = new OrderItem(sku, quantidadeInicial, unitPrice);
        var quantidadeParaDecrementar = 10.0; // Maior que a quantidade atual

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            orderItem.Decrease(quantidadeParaDecrementar));

        Assert.Equal("10 é superior ao que existe em estoque", exception.Message);
    }

    [Fact]
    public void Total_AposRemocao_CalculaValorCorreto()
    {
        // Arrange
        var order = new OrderEntity();
        order.AddItem("PROD-001", 3.0, new Money(50.00m));  // 3 x 50 = 150
        order.AddItem("PROD-002", 2.0, new Money(100.00m)); // 2 x 100 = 200
        // Total inicial esperado: 350

        // Act
        order.RemoveItem("PROD-001", 1.0); // Remove 1 unidade do PROD-001
        // Novo total esperado: (3-1) x 50 + 2 x 100 = 100 + 200 = 300

        // Assert
        Assert.Equal(300.00m, order.Total.Value);
    }

}
