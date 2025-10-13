namespace Associations.Domain.PersonAggregate;

public class Passport
{
    public string Number { get; } 
    public DateOnly Expiration { get; }
    
    public Passport(string number, DateOnly expiration)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Número obrigatório", nameof(number));
        if (expiration <= DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentOutOfRangeException(nameof(expiration), "Expiração deve ser futura");

        Number = number;
        Expiration = expiration;
    }
}
