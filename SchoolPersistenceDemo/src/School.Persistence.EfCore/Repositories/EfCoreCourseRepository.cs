using System;
using School.Domain.Domain.Repositories;
using School.Domain.Entities;
using School.Persistence.EfCore.Context;

namespace School.Persistence.EfCore.Repositories;

public class EfCoreCourseRepository(SchoolDbContext dbContext) : EfCoreRepository<Course, int>(dbContext),
   ICourseRepository
{
}