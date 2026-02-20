using Associations.Domain.ValueObjects;

namespace Associations.Domain.Tests;

public class MoneySpecs
{
    [Fact]
    public void Ctor_ValorNegativo_DeveLancarArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Money(-1));
    }
}
