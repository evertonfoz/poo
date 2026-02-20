using School.Domain.Entities;

namespace School.Domain.Domain.Services;

public interface ICourseService
{
    public Course CreateCourse(string? name = null, int? workloadHours = null, bool isActive = true, Course? course = null);
    void UpdateCourse(int id, string? name, int? workloadHours, bool?
       isActive);
    void RemoveCourse(int id);
    IReadOnlyList<Course> ListAll();
}
