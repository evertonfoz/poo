using University.Enrollments.Domain;
using Xunit;

namespace University.Enrollments.Tests;

public class ExampleTests
{
    [Fact]
    public void Add_ReturnsSum()
    {
        var result = EnrollmentUtils.Add(2, 3);
        Assert.Equal(5, result);
    }
}
