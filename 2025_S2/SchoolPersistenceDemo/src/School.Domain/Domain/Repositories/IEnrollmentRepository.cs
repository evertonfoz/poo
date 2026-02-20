using System;
using School.Domain.Entities;
using School.Domain.Repositories;

namespace School.Domain.Domain.Repositories;

public interface IEnrollmentRepository : IReadRepository<Enrollment, int>,
       IWriteRepository<Enrollment, int>
    {
        IReadOnlyList<Enrollment> ListByStudent(int studentId);
        IReadOnlyList<Enrollment> ListByCourse(int courseId);
    }