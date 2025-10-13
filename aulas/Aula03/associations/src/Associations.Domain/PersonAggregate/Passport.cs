using System;

namespace Associations.Domain.PersonAggregate;

public class Passport
{

    public string Number { get; private set; }
    public Passport(string number, DateOnly expiration)
    {
        if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
        {
            throw new ArgumentException($"Number inválido: {nameof(Number)}");
        }
        Number = number;
    }
}
