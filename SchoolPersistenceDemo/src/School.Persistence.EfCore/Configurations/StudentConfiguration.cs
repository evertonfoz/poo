using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Entities;

namespace School.Persistence.EfCore.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.HasKey(s => s.StudentId);
        builder.Property(s => s.Name)
       .IsRequired()
       .HasMaxLength(200);

        builder.Property(s => s.Email)
           .IsRequired()
           .HasMaxLength(200);

        // Address como tipo agregado (owned)
        builder.OwnsOne(s => s.Address, addressCfg =>
        {
            addressCfg.Property(a => a.Street).HasMaxLength(200);
            addressCfg.Property(a => a.Number).HasMaxLength(50);
            addressCfg.Property(a => a.Neighborhood).HasMaxLength(100);
            addressCfg.Property(a => a.City).HasMaxLength(100);
            addressCfg.Property(a => a.State).HasMaxLength(2);
            addressCfg.Property(a => a.ZipCode).HasMaxLength(20);
        });

        builder.HasMany(s => s.Enrollments)
           .WithOne(e => e.Student!)
           .HasForeignKey(e => e.StudentId);
    }
}