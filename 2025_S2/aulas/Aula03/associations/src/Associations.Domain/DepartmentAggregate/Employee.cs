using System;

namespace Associations.Domain.DepartmentAggregate;

public class Employee
{
    public Employee(string name)
    {
        Name = name;
    }

    public Department? Department { get; private set; }
    public string Name { get; set; }

    // Called by Department when adding the employee; internal to allow tests in same assembly
    internal void SetDepartment(Department department)
    {
        Department = department;
    }

    // Called by Department when removing employee
    internal void ClearDepartment()
    {
        Department = null;
    }
}
