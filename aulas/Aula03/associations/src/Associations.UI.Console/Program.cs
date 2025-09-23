using Associations.Domain;

try
{
    Money money = new(-1);
}
catch (ArgumentOutOfRangeException exception)
{
    Console.WriteLine(exception.Message);
}
