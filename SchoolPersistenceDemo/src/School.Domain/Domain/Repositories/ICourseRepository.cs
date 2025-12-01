using System;
using School.Domain.Entities;
using School.Domain.Repositories;

namespace School.Domain.Domain.Repositories;

public interface ICourseRepository : IReadRepository<Course, int>, 
       IWriteRepository<Course, int>
    {
    }