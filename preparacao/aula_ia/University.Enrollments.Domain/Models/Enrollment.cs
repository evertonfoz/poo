using System;

namespace University.Enrollments.Domain.Models
{
    /// <summary>
    /// Represents a student's enrollment in a course.
    /// </summary>
    public sealed class Enrollment
    {
        /// <summary>
        /// Identifier of the student.
        /// Invariant: StudentId &gt; 0.
        /// </summary>
        public int StudentId { get; init; }

        /// <summary>
        /// Identifier of the course.
        /// Invariant: CourseId &gt; 0.
        /// </summary>
        public int CourseId { get; init; }

        /// <summary>
        /// Current status of the enrollment.
        /// </summary>
        public EnrollmentStatus Status { get; init; }

        /// <summary>
        /// Date when the enrollment was recorded.
        /// </summary>
        public DateOnly EnrolledOn { get; init; }

        /// <summary>
        /// Semantic equality: two Enrollment instances are considered equal if they refer to the same
        /// (StudentId, CourseId) pair regardless of other properties like Status or EnrolledOn.
        /// (No Equals/GetHashCode implementation here - document expected behavior.)
        /// </summary>
        // TODO: Implement IEquatable&lt;Enrollment&gt; and override Equals/GetHashCode in a later step.

        /// <summary>
        /// Expected state machine (documented expectations):
        /// - Requested -> Enrolled | Cancelled
        /// - Enrolled -> Dropped | Completed
        /// - Dropped -> (terminal)
        /// - Completed -> (terminal)
        /// - Cancelled -> (terminal)
        /// Transitions should validate allowed moves and record timestamps or reasons where applicable.
        /// (No state machine implementation here.)
        /// </summary>
        // TODO: Add unit tests that assert allowed transitions and that invalid transitions are rejected.
    }
}
