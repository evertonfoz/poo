using System;
using Associations.Domain.Order.Helpers;
using Associations.Domain.ValueObjects;

namespace Associations.Domain.Order;

public class Order
{
    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly<OrderItem>();
    public Money Total => _items.Aggregate(new Money(0),
        (acc, it) => new Money(acc.Value + it.SubTotal.Value));

    public void AddItem(OrderItem orderItem)
    {
        var orderItemJustExists = _items.FirstOrDefault(i => i.Equals(orderItem));

        if (orderItemJustExists is null)
        {
            _items.Add(orderItem);
        }
        else
        {
            orderItemJustExists.Increase(orderItem.Quantity);
        }
        // _items.Add(orderItem);
    }

    public void AddItem(string sku, double quantity, Money unitPrice)
    {
        // if (string.IsNullOrWhiteSpace(sku))
        // {
        //     throw new ArgumentException("SKU vazio", nameof(sku));
        // }
        // if (quantity <= 0)
        // {
        //     throw new ArgumentOutOfRangeException(nameof(quantity), "Quantidade deve ser MAIOR QUE ZERO");
        // }

        // sku = OrderHelper.NormalizeSku(sku);
        AddItem(new OrderItem(sku, quantity, unitPrice));
        // var newOrderItem = new OrderItem(sku, quantity, unitPrice);

        // var orderItemJustExists = _items.FirstOrDefault(i => i.Equals(newOrderItem));

        // if (orderItemJustExists is null)
        // {
        //     _items.Add(new OrderItem(sku, quantity, unitPrice));
        // }
        // else
        // {
        //     orderItemJustExists.Increase(quantity);
        // }
    }

    public void RemoveItem(string sku, double quantity)
    {
        sku = OrderHelper.NormalizeSku(sku);
        var orderItemJustExists = _items.FirstOrDefault(i => i.Sku == sku) ?? throw new InvalidOperationException($"{sku} não está registrado nesse pedido");
        if (orderItemJustExists.Quantity < quantity)
        {
            throw new InvalidOperationException($"{sku} não possui quantidade suficiente para remover");
        }
        orderItemJustExists.Decrease(quantity);
        if (orderItemJustExists.Quantity == 0)
        {
            _items.Remove(orderItemJustExists);
        }
    }

    public override string ToString()
    {
        return $"Quantidade de Itens: {_items.Count}";
    }
}
