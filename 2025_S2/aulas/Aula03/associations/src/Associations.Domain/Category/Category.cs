using Associations.Domain.Guards;

namespace Category;

// Category class with validation using Guard clauses
// Demonstrates identity methods (Equals and GetHashCode) based on CategoryId
public class Category
{
    public int CategoryId { get; }
    public string Name { get; }
    public string? Description { get; private set; }

    // Default category for lazy loading scenarios
    public static Category Default => new(0, "Uncategorized");

    public Category(int categoryId, string name)
    {
        // Validate that CategoryId is not negative
        if (categoryId < 0)
            throw new ArgumentOutOfRangeException(nameof(categoryId), "CategoryId cannot be negative.");

        // Validate that name is not null
        Guard.AgainstNull(ref name, nameof(name));

        CategoryId = categoryId;
        Name = name;
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

    // Identity methods based on CategoryId
    public override bool Equals(object? obj)
    {
        if (obj is not Category other)
            return false;

        return CategoryId == other.CategoryId;
    }

    public override int GetHashCode()
    {
        return CategoryId.GetHashCode();
    }

    public override string ToString()
    {
        return $"Category [Id: {CategoryId}, Name: {Name}, Description: {Description ?? "N/A"}]";
    }
}
