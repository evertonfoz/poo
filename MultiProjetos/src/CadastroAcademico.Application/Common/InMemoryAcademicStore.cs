using CadastroAcademico.Domain;

namespace CadastroAcademico.Application.Common;

public sealed class InMemoryAcademicStore
{
    private readonly List<Curso> _courses = new();
    private readonly List<Aluno> _students = new();
    private readonly List<Matricula> _enrollments = new();

    public IReadOnlyList<Curso> Courses => _courses;
    public IReadOnlyList<Aluno> Students => _students;
    public IReadOnlyList<Matricula> Enrollments => _enrollments;

    public void AddCourse(Curso course)
    {
        _courses.Add(course);
    }

    public void AddStudent(Aluno student)
    {
        _students.Add(student);
    }

    public void AddEnrollment(Matricula enrollment)
    {
        _enrollments.Add(enrollment);
    }

    public void RemoveCourse(Curso course) => _courses.Remove(course);

    public void RemoveStudent(Aluno student) => _students.Remove(student);

    public void RemoveEnrollment(Matricula enrollment) => _enrollments.Remove(enrollment);
}
