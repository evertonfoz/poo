using School.Domain.Domain.Repositories;
using School.Domain.Domain.Services;
using School.Domain.Entities;

public sealed class CourseService(ICourseRepository courseRepository)
    : ICourseService
{
    private readonly ICourseRepository _courseRepository = courseRepository ??
               throw new ArgumentNullException(nameof(courseRepository));

    public Course CreateCourse(string? name = null, int? workloadHours = null, bool isActive = true, Course? course = null)
    {
        if (course != null)
        {
            return ValidateAndAddCourse(course);
        }
        else
        {
            return CreateAndAddCourse(name, workloadHours, isActive);
        }
    }

    private Course ValidateAndAddCourse(Course course)
    {
        if (string.IsNullOrWhiteSpace(course.Name))
        {
            throw new ArgumentException("Course name cannot be null or empty.", nameof(course.Name));
        }

        if (course.WorkloadHours <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(course.WorkloadHours), "Workload hours must be greater than zero.");
        }

        return _courseRepository.Add(course);
    }

    private Course CreateAndAddCourse(string? name, int? workloadHours, bool isActive)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Course name cannot be null or empty.", nameof(name));
        }

        if (!workloadHours.HasValue || workloadHours.Value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(workloadHours), "Workload hours must be greater than zero.");
        }

        var newCourse = new Course
        {
            Name = name,
            WorkloadHours = workloadHours.Value,
            IsActive = isActive
        };

        return _courseRepository.Add(newCourse);
    }
}