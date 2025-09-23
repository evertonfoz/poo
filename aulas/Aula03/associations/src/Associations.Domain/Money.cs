using System;

namespace Associations.Domain;

public class Money
{
    public decimal Value { get; }

    public Money(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value),
                "Valor Monetário não pode ser negativo");
        }
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Money)
        {
            return false;
        }
        return Value == ((Money)obj).Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
