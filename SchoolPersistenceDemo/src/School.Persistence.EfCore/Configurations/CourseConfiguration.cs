using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Entities;

namespace School.Persistence.EfCore.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        builder.HasKey(c => c.CourseId);

        builder.Property(c => c.Name)
           .IsRequired()
           .HasMaxLength(200);

        builder.Property(c => c.WorkloadHours)
           .IsRequired();

        builder.Property(c => c.IsActive)
           .IsRequired();

        builder.Property(c => c.Year);

        // Relacionamento 1-N com Enrollment
        builder.HasMany(c => c.Enrollments)
           .WithOne(e => e.Course!)
           .HasForeignKey(e => e.CourseId);
    }
}