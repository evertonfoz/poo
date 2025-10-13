using Associations.Domain.DepartmentAggregate;
using Associations.Domain.Order;
using Associations.Domain.ValueObjects;

#region Previous Examples - Order and OrderItem
/*
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
*/
#endregion

#region Product Example - Name Property (Guard.AgainstNull)
/*
// Example 1: Creating a valid product
try
{
    var laptop = new Product.Product("Laptop");
    Console.WriteLine($"Product created successfully: {laptop.Name}");
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"Error creating product: {ex.Message}");
}

Console.WriteLine();

// Example 2: Trying to create a product with null name (will throw exception)
try
{
    var invalidProduct = new Product.Product(null!);
    Console.WriteLine($"Product created: {invalidProduct.Name}");
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"Error: Cannot create product with null name");
    Console.WriteLine($"Exception details: {ex.Message}");
    Console.WriteLine($"Parameter name: {ex.ParamName}");
}
*/
#endregion

#region Product Example - Description Property (Guard.TryParseNonEmpty)
/*
// Example 1: Setting a valid description
var laptop = new Product.Product("Laptop");
laptop.SetDescription("High performance laptop for developers");
Console.WriteLine($"Product: {laptop.Name}");
Console.WriteLine($"Description: {laptop.Description ?? "No description"}");
Console.WriteLine();

// Example 2: Setting null description
var mouse = new Product.Product("Mouse");
mouse.SetDescription(null);
Console.WriteLine($"Product: {mouse.Name}");
Console.WriteLine($"Description: {mouse.Description ?? "No description"}");
Console.WriteLine();

// Example 3: Setting empty description
var keyboard = new Product.Product("Keyboard");
keyboard.SetDescription("");
Console.WriteLine($"Product: {keyboard.Name}");
Console.WriteLine($"Description: {keyboard.Description ?? "No description"}");
Console.WriteLine();

// Example 4: Setting description with only whitespace
var monitor = new Product.Product("Monitor");
monitor.SetDescription("   ");
Console.WriteLine($"Product: {monitor.Name}");
Console.WriteLine($"Description: {monitor.Description ?? "No description"}");
Console.WriteLine();

// Example 5: Setting description with spaces at the beginning/end
var headphone = new Product.Product("Headphone");
headphone.SetDescription("  Wireless headphone with noise cancellation  ");
Console.WriteLine($"Product: {headphone.Name}");
Console.WriteLine($"Description: {headphone.Description ?? "No description"}");
*/
#endregion

#region Product Example - Category Property (Lazy Loading with [MemberNotNull])
/*
Console.WriteLine("=== Example 1: Lazy Loading - Category is loaded on first access ===");
var laptop = new Product.Product("Laptop");
Console.WriteLine($"Product created: {laptop.Name}");
Console.WriteLine("Accessing Category for the first time...");
var category = laptop.Category;
Console.WriteLine($"Category loaded: {category.Name} (Id: {category.CategoryId})");
Console.WriteLine();

Console.WriteLine("=== Example 2: Multiple accesses return the same instance ===");
var mouse = new Product.Product("Mouse");
var category1 = mouse.Category;
var category2 = mouse.Category;
Console.WriteLine($"First access:  {category1.Name} (HashCode: {category1.GetHashCode()})");
Console.WriteLine($"Second access: {category2.Name} (HashCode: {category2.GetHashCode()})");
Console.WriteLine($"Same instance? {ReferenceEquals(category1, category2)}");
Console.WriteLine();

Console.WriteLine("=== Example 3: [MemberNotNull] ensures non-null access ===");
var keyboard = new Product.Product("Keyboard");
// No need for null checks thanks to [MemberNotNull]
// The compiler knows Category will never be null after EnsureCategory() is called
Console.WriteLine($"Product: {keyboard.Name}");
Console.WriteLine($"Category Name: {keyboard.Category.Name}");
Console.WriteLine($"Category Id: {keyboard.Category.CategoryId}");
Console.WriteLine($"Category Description: {keyboard.Category.Description ?? "N/A"}");
Console.WriteLine();

Console.WriteLine("=== Example 4: Default Category details ===");
var monitor = new Product.Product("Monitor");
Console.WriteLine($"Product: {monitor.Name}");
Console.WriteLine($"Category: {monitor.Category}");
Console.WriteLine();

Console.WriteLine("=== Example 5: Modifying Category Description ===");
var headphone = new Product.Product("Headphone");
Console.WriteLine($"Product: {headphone.Name}");
Console.WriteLine($"Initial Category: {headphone.Category}");
headphone.Category.SetDescription("Default category for uncategorized products");
Console.WriteLine($"After setting description: {headphone.Category}");
*/
#endregion

#region Product Example - ProductCode ([DisallowNull]) and Notes ([AllowNull])

// Console.WriteLine("=== Example 1: [DisallowNull] - ProductCode never returns null ===");
// var laptop = new Product.Product("Laptop");
// Console.WriteLine($"Product: {laptop.Name}");
// Console.WriteLine($"Initial ProductCode: '{laptop.ProductCode}' (is empty: {laptop.ProductCode == string.Empty})");
// Console.WriteLine($"ProductCode is null? {laptop.ProductCode == null}"); // Always false
// laptop.SetProductCode("LAP-2025-001");
// Console.WriteLine($"After setting: '{laptop.ProductCode}'");
// Console.WriteLine($"ProductCode is null? {laptop.ProductCode == null}"); // Still false
// Console.WriteLine();

// Console.WriteLine("=== Example 2: [AllowNull] - Notes setter accepts null but getter returns empty string ===");
// var mouse = new Product.Product("Mouse");
// Console.WriteLine($"Product: {mouse.Name}");
// Console.WriteLine($"Initial Notes: '{mouse.Notes}' (is empty: {mouse.Notes == string.Empty})");
// Console.WriteLine($"Notes is null? {mouse.Notes == null}"); // Always false thanks to [AllowNull]
// mouse.Notes = "Important: Handle with care";
// Console.WriteLine($"After setting notes: '{mouse.Notes}'");
// mouse.Notes = null; // Setter accepts null
// Console.WriteLine($"After setting null: '{mouse.Notes}' (converted to empty string)");
// Console.WriteLine($"Notes is null? {mouse.Notes == null}"); // Still false
// Console.WriteLine();

// Console.WriteLine("=== Example 3: ProductCode with spaces - automatic trim ===");
// var keyboard = new Product.Product("Keyboard");
// keyboard.SetProductCode("  KBD-123  ");
// Console.WriteLine($"Product: {keyboard.Name}");
// Console.WriteLine($"ProductCode (trimmed): '{keyboard.ProductCode}'");
// Console.WriteLine();

// Console.WriteLine("=== Example 4: Multiple null assignments to Notes ===");
// var monitor = new Product.Product("Monitor");
// Console.WriteLine($"Product: {monitor.Name}");
// monitor.Notes = "First note";
// Console.WriteLine($"First: '{monitor.Notes}'");
// monitor.Notes = null;
// Console.WriteLine($"After null: '{monitor.Notes}' (empty, not null)");
// monitor.Notes = "Second note";
// Console.WriteLine($"Second: '{monitor.Notes}'");
// monitor.Notes = null;
// Console.WriteLine($"After null again: '{monitor.Notes}' (empty, not null)");
// Console.WriteLine();

// Console.WriteLine("=== Example 5: Demonstrating the difference between [DisallowNull] and [AllowNull] ===");
// var headphone = new Product.Product("Headphone");
// Console.WriteLine($"Product: {headphone.Name}");
// Console.WriteLine("\n[DisallowNull] ProductCode:");
// Console.WriteLine($"  - Initial value: '{headphone.ProductCode}' (never null, starts as empty)");
// Console.WriteLine($"  - Can be set via method: SetProductCode()");
// Console.WriteLine($"  - Setter validates and prevents null");

// Console.WriteLine("\n[AllowNull] Notes:");
// Console.WriteLine($"  - Initial value: '{headphone.Notes}' (never null, starts as empty)");
// Console.WriteLine($"  - Can be set directly: Notes = value");
// Console.WriteLine($"  - Setter accepts null but converts to empty string");
// Console.WriteLine($"  - Getter always returns non-null value");

// headphone.SetProductCode("HPH-001");
// headphone.Notes = "Premium audio quality";
// Console.WriteLine($"\nAfter setting both:");
// Console.WriteLine($"  ProductCode: '{headphone.ProductCode}'");
// Console.WriteLine($"  Notes: '{headphone.Notes}'");

#endregion

#region Department and Employee

Department dacom = new("DACOM");
Employee everton = new("Everton");

dacom.AddEmployee(everton);

Console.WriteLine($"Departamento: {dacom.Name} / {dacom.Employees[0].Name}");
Console.WriteLine($"Empregado {everton.Name} / Departamento: {everton.Department.Name}");

Department dahla = new("DAHLA");
dahla.AddEmployee(everton);
// everton.AssignDepartment(dahla);

Console.WriteLine($"Departamento: {dacom.Name} / {dacom.Employees}");
Console.WriteLine($"Empregado {everton.Name} / Departamento: {everton.Department.Name}");

Console.WriteLine($"Departamento: {dahla.Name} / {dahla.Employees[0].Name}");

#endregion