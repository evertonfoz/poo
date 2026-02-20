using System;
using Associations.Domain.Order.Helpers;
using Associations.Domain.ValueObjects;

namespace Associations.Domain.Order;

public class OrderItem
{
    public string Sku { get; }
    public double Quantity { get; private set; }
    public Money UnitPrice { get; }
    public Money SubTotal => new(UnitPrice.Value * (decimal)Quantity);

    // private static string NormalizeSku(string raw)
    // {
    //     ArgumentNullException.ThrowIfNull(raw);
    //     var noWs = new string([.. raw.Where(c => !char.IsWhiteSpace(c))]);
    //     return noWs.ToUpperInvariant();
    // }

    public OrderItem(string sku, double quantity, Money? unitPrice)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new ArgumentException("SKU vazio", nameof(sku));
        }
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantidade deve ser MAIOR QUE ZERO");
        }

        Sku = OrderHelper.NormalizeSku(sku);
        Quantity = quantity;
        UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice), "O preço é obrigatório");
    }

    public void Increase(double quantityToIncrease)
    {
        if (quantityToIncrease <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantityToIncrease), "Quantidade para acrescentar no item, deve ser maior que zero");
        }
        Quantity += quantityToIncrease;
    }

    public void Decrease(double quantityToDecrease)
    {
        if (quantityToDecrease <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantityToDecrease), "Quantidade para retirar do item deve ser maior que zero");
        }
        if (quantityToDecrease > Quantity)
        {
            throw new InvalidOperationException($"{quantityToDecrease} é superior ao que existe em estoque");
        }
        Quantity -= quantityToDecrease;
    }

    public override string ToString()
    {
        return $"Sku: {Sku} tem {Quantity} de quantidade, custando {UnitPrice.Value} cada um, totalizando {SubTotal.Value}";
    }

    public override bool Equals(object? obj)
    {
        return obj is OrderItem other && other.Sku.Equals(Sku);
    }
}
