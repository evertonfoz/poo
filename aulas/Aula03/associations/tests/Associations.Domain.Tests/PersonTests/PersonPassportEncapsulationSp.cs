using Associations.Domain.PersonAggregate;
using System.Reflection;

namespace Associations.Domain.Tests.PersonTests;

public class PersonPassportEncapsulationSp
{
    [Fact]
    public void Propriedade_Passport_deve_ter_get_publico_e_set_nao_publico()
    {
        // Arrange
        var prop = typeof(Person).GetProperty(nameof(Person.Passport),
                     BindingFlags.Instance | BindingFlags.Public);

        // Assert — expõe a leitura...
        Assert.NotNull(prop);

        Assert.True(prop!.CanRead);
        Assert.True(prop.GetMethod!.IsPublic);

        // ...mas o setter não pode ser público (pode ser 
        //    private/protected/interno ou inexistente)
        var set = prop.SetMethod;
        Assert.True(set is null || !set.IsPublic);
    }

    [Fact]
    public void IssuePassport_deve_estabelecer_o_vinculo_quando_nao_existe()
    {
        // Arrange
        var person = new Person("Maria");
        var pass = new Passport("AB123456", DateOnly.FromDateTime(DateTime.Today.AddYears(5)));

        // Act
        person.IssuePassport(pass);

        // Assert
        // Assert.Same(pass, person.Passport);

        Assert.NotNull(person.Passport);
        Assert.Equal(pass.Number, person.Passport!.Number);
        Assert.Equal(pass.Expiration, person.Passport!.Expiration);
    }

    // 1) Passport: número inválido
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Passport_ctor_deve_falhar_quando_numero_invalido(string? numero)
    {
        var exp = DateOnly.FromDateTime(DateTime.Today.AddYears(1));
        Assert.Throws<ArgumentException>(() => new Passport(numero!, exp));
    }

    // 2) Passport: expiração não futura (ontem e hoje)
    [Theory]
    [InlineData(-1)] // ontem
    [InlineData(0)]  // hoje
    public void Passport_ctor_deve_falhar_quando_expiracao_nao_futura(int diasApartirDeHoje)
    {
        var data = DateOnly.FromDateTime(DateTime.Today.AddDays(diasApartirDeHoje));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Passport("AB123456", data));
    }

    // 3) Person: passaporte nulo
    [Fact]
    public void IssuePassport_deve_falhar_quando_passaporte_nulo()
    {
        var person = new Person("Maria");
        Assert.Throws<ArgumentNullException>(() => person.IssuePassport(null!));
    }

    // 4) Person: já possui passaporte
    [Fact]
    public void IssuePassport_deve_falhar_quando_ja_possui()
    {
        var person = new Person("João");
        person.IssuePassport(new Passport("XY999999", DateOnly.FromDateTime(DateTime.Today.AddYears(5))));

        Assert.Throws<InvalidOperationException>(() =>
            person.IssuePassport(new Passport("ZZ000000", DateOnly.FromDateTime(DateTime.Today.AddYears(5)))));
    }
}

