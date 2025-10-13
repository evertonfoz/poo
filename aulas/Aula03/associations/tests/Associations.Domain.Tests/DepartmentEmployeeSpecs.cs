using Associations.Domain.DepartmentAggregate;
using Xunit;

namespace Associations.Domain.Tests;

public class DepartmentEmployeeSpecs
{
    [Fact]
    public void Employee_CanBeCreated_WithoutDepartment()
    {
        var e = new Employee("Alice");

        Assert.NotNull(e);
        Assert.Equal("Alice", e.Name);
        Assert.Null(e.Department);
    }

    [Fact]
    public void AddingEmployee_ToDepartment_SetsBidirectionalRelationship()
    {
        var dept = new Department("HR");
        var emp = new Employee("Bob");

        dept.AddEmployee(emp);

        Assert.Contains(emp, dept.Employees);
        Assert.Equal(dept, emp.Department);
    }

    [Fact]
    public void ReassigningEmployee_RemovesFromPreviousDepartment()
    {
        var d1 = new Department("Sales");
        var d2 = new Department("Marketing");
        var emp = new Employee("Carol");

        d1.AddEmployee(emp);
        Assert.Equal(d1, emp.Department);
        Assert.Contains(emp, d1.Employees);

        d2.AddEmployee(emp);

        // now employee should belong to d2 and not be in d1
        Assert.Equal(d2, emp.Department);
        Assert.Contains(emp, d2.Employees);
        Assert.DoesNotContain(emp, d1.Employees);
    }

    [Fact]
    public void RemovingEmployee_FromDepartment_ClearsEmployeeDepartment()
    {
        var dept = new Department("IT");
        var emp = new Employee("Dave");

        dept.AddEmployee(emp);
        Assert.Equal(dept, emp.Department);

        dept.RemoveEmployee(emp);

        Assert.Null(emp.Department);
        Assert.DoesNotContain(emp, dept.Employees);
    }
}
