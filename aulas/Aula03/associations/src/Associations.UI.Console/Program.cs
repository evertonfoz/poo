using Associations.Domain.Order;
using Associations.Domain.ValueObjects;

// try
// {
//     Money money = new(-1);
// }
// catch (ArgumentOutOfRangeException exception)
// {
//     Console.WriteLine(exception.Message);
// }

try
{
    OrderItem monitor = new(" 1Ax Yz ", 1, new Money(2));
    OrderItem impressora = new("ABC123", 1, new Money(3));

    Order order = new();
    order.AddItem(monitor);
    order.AddItem(impressora);
    order.AddItem("ABC123", 1, new Money(3));

    // monitor.Increase(1);
    // monitor.Decrease(1);

    Console.WriteLine(monitor);
    Console.WriteLine(impressora);

    Console.WriteLine(order.Total.Value);
    Console.WriteLine(order);

    order.RemoveItem("ABC123", 2);
    Console.WriteLine(order.Total.Value);
    Console.WriteLine(order);
}
catch (SystemException se) when (se is ArgumentException || se is InvalidOperationException)
{
    Console.WriteLine($"ERRO:{se.Message} ");
}
// catch (ArgumentException ae)
// {
//     Console.WriteLine($"ERRO:{ae.Message} ");
// }
// catch (InvalidOperationException ioe)
// {
//     Console.WriteLine($"ERRO: {ioe.Message}");
// }