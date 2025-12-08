using Microsoft.EntityFrameworkCore;
using School.Domain.Domain.Repositories;
using School.Domain.Entities;
using School.Domain.Repositories;
using School.Persistence.EfCore.Context;

namespace School.Persistence.EfCore.Repositories;

public class EfCoreEnrollmentRepository(SchoolDbContext dbContext) : 
   EfCoreRepository<Enrollment, int>(dbContext), IEnrollmentRepository
{
    public IReadOnlyList<Enrollment> ListByStudent(int studentId)
    {
        return [.. _dbSet.AsNoTracking().Where(e => e.StudentId == studentId)];
    }

    public IReadOnlyList<Enrollment> ListByCourse(int courseId)
    {
        return [.. _dbSet.AsNoTracking().Where(e => e.CourseId == courseId)];
    }
}