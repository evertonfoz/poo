using System;

namespace University.Enrollments.Domain.Models
{
    /// <summary>
    /// Minimal representation of a student.
    /// </summary>
    public sealed class Student
    {
        /// <summary>
        /// Unique identifier for the student.
        /// Invariant: Id &gt; 0.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Full name of the student.
        /// Invariant: not null or empty.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        // TODO: Add simple factory or validations in future iterations.
        // TODO: Add tests to ensure invariants (Id > 0, Name not empty).
    }
}
