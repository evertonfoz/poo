using System;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;

namespace School.Persistence.EfCore.Context;

public class SchoolDbContext(DbContextOptions<SchoolDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplicar configurações específicas por entidade
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolDbContext)
           .Assembly);
        base.OnModelCreating(modelBuilder);
    }
}