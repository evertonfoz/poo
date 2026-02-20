using System;
using School.Domain.Entities;
using School.Domain.Repositories;

namespace School.Domain.Domain.Repositories;

public interface IStudentRepository : IReadRepository<Student, int>,
       IWriteRepository<Student, int>
    {
    }