using System.Collections.ObjectModel;

namespace Associations.Domain.DepartmentAggregate;

public class Department
{
    private readonly List<Employee> _employees = new();

    public Department(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public ReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();

    public void AddEmployee(Employee employee)
    {
        if (employee is null)
            throw new ArgumentNullException(nameof(employee));

        if (_employees.Contains(employee))
            return;

        // If employee already belongs to another department, remove from there first
        if (employee.Department != null && employee.Department != this)
        {
            employee.Department.RemoveEmployee(employee);
        }

        _employees.Add(employee);
        // Set employee side without causing another call back to Department
        employee.SetDepartment(this);
    }

    public void RemoveEmployee(Employee employee)
    {
        if (employee is null)
            throw new ArgumentNullException(nameof(employee));

        if (!_employees.Contains(employee))
            return;

        _employees.Remove(employee);
        // Clear the employee's department reference without calling back here
        employee.ClearDepartment();
    }
}
