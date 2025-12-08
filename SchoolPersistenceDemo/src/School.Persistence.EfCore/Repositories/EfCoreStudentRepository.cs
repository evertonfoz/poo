using School.Domain.Domain.Repositories;
using School.Domain.Entities;
using School.Domain.Repositories;
using School.Persistence.EfCore.Context;

namespace School.Persistence.EfCore.Repositories;

public class EfCoreStudentRepository(SchoolDbContext dbContext) : EfCoreRepository<Student, int>(dbContext),
   IStudentRepository
{
}