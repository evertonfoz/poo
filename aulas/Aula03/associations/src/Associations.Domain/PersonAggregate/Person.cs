namespace Associations.Domain.PersonAggregate;

public class Person
{
    public string Name { get; }
    public Passport? Passport { get; private set; }

    public Person(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome obrigatório", nameof(name));
        Name = name;
    }

    public void IssuePassport(Passport passport)
    {
        ArgumentNullException.ThrowIfNull(passport);

        if (Passport is not null)
            throw new InvalidOperationException("Pessoa já possui passaporte");

        // Cópia defensiva (nova instância com o mesmo estado)
        Passport = new Passport(passport.Number, passport.Expiration);
        // Passport = passport; 
    }
}
