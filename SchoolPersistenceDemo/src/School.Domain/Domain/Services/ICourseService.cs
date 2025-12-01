using School.Domain.Entities;

namespace School.Domain.Domain.Services;

public interface ICourseService
{
    public Course CreateCourse(string? name = null, int? workloadHours = null, bool isActive = true, Course? course = null);
}
