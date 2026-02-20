using System.Reflection;
using System.Runtime.InteropServices;
using Associations.Domain.PersonAggregate;

namespace Associations.Domain.Tests.PersonAggregateTests;

public class PersonPassportEncapsulationSpecs
{
    [Fact]
    public void Propriedade_Passport_Deve_Ter_Get_Public_E_Set_Nao_Publico()
    {
        // Arrange
        var propriedade = typeof(Person).GetProperty(nameof(Person.Passport),
            BindingFlags.Instance | BindingFlags.Public);

        // Assert
        Assert.NotNull(propriedade);
        Assert.True(propriedade!.CanRead);

        var set = propriedade.SetMethod;
        Assert.True(set is null || !set.IsPublic);
    }

    [Fact]
    public void IssuePassport_Deve_Estabelecer_O_Vinculo_Quando_Nao_Existe()
    {
        // Arange
        var person = new Person("Everton");
        var passport = new Passport("123456", DateOnly.FromDateTime(DateTime.Today.AddYears(5)));

        person.IssuePassport(passport);

        Assert.Same(passport, person.Passport);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Passport_Ctor_Deve_Falhar_Quando_Numero_Invalido(string? numero)
    {
        // Arrange
        var expiration = DateOnly.FromDateTime(DateTime.Today.AddYears(5));
        Assert.Throws<ArgumentException>(() => new Passport(numero, expiration));
    }
}
