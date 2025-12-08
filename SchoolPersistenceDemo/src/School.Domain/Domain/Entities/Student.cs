using System.Collections.Generic;

namespace School.Domain.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Address Address { get; set; } = new Address();
        public List<Enrollment> Enrollments { get; } = new();
    }
}