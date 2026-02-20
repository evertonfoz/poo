using Associations.Domain.Guards;
using System.Diagnostics.CodeAnalysis;
using CategoryNs = Category;

namespace Product;

// Example: Validation in constructor
// This demonstrates how to use Guard.AgainstNull to validate that a parameter 
// is not null before assigning it to a property. This ensures the object is 
// always in a valid state after construction.
public class Product
{
    private CategoryNs.Category? _category;

    public string Name { get; }
    public string? Description { get; private set; }
    
    // [DisallowNull] - Property never returns null, but can be set to empty
    // Setter is private to control initialization
    [DisallowNull]
    public string ProductCode { get; private set; } = string.Empty;
    
    // [AllowNull] - Setter accepts null (meaning "absent"), but getter never returns null
    // Returns empty string when value is absent (null)
    [AllowNull]
    public string Notes
    {
        get => _notes;
        set => _notes = value ?? string.Empty;
    }
    private string _notes = string.Empty;
    
    // Property that ensures category is loaded before accessing
    public CategoryNs.Category Category
    {
        get
        {
            EnsureCategory();
            return _category;
        }
    }
    
    public Product(string name)
    {
        // Validates before assigning - will throw ArgumentNullException if name is null
        Guard.AgainstNull(ref name, nameof(name));
        Name = name;
    }

    public void SetProductCode(string code)
    {
        // Validates that code is not null and sets it
        // If null or whitespace, sets to empty string (never null)
        Guard.AgainstNull(ref code, nameof(code));
        ProductCode = code.Trim();
    }

    public void SetDescription(string? description)
    {
        // Uses TryParseNonEmpty to validate if description has valid content
        // Only sets the description if it's not null, empty, or whitespace
        if (Guard.TryParseNonEmpty(description, out string? validDescription))
        {
            Description = validDescription;
        }
        else
        {
            Description = null;
        }
    }

    // Ensures category is loaded using lazy loading pattern
    // The [MemberNotNull] attribute tells the compiler that after this method executes,
    // the _category field will not be null, improving null safety analysis
    [MemberNotNull(nameof(_category))]
    private void EnsureCategory()
    {
        _category ??= CategoryNs.Category.Default;
    }
}
