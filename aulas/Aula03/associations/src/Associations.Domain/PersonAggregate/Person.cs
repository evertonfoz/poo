using System;

namespace Associations.Domain.PersonAggregate;

public class Person
{
    public Passport Passport { get; private set; }

    public Person(string name)
    {
        
    }

    public void IssuePassport(Passport passport)
    {
        Passport = passport;
    }
}
