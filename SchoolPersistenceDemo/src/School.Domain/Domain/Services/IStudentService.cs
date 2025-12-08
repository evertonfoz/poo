using School.Domain.Entities;

namespace School.Domain.Services;

public interface IStudentService
{
    Student CreateStudent(string name, string email, Address address);
    void UpdateStudent(int id, string? name, string? email, Address?
       newAddress);
    void RemoveStudent(int id);
    IReadOnlyList<Student> ListAll();
}