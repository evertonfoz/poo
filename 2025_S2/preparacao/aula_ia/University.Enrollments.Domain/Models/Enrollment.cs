using System;

namespace University.Enrollments.Domain.Models
{
    /// <summary>
    /// Represents a student's enrollment in a course.
    /// </summary>
    public sealed class Enrollment : IEquatable<Enrollment>
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
    public EnrollmentStatus Status { get; set; }

        /// <summary>
        /// Date when the enrollment was recorded.
        /// </summary>
        public DateOnly EnrolledOn { get; init; }

        /// <summary>
        /// Semantic equality: two Enrollment instances are considered equal if they refer to the same
        /// (StudentId, CourseId) pair regardless of other properties like Status or EnrolledOn.
        /// (No Equals/GetHashCode implementation here - document expected behavior.)
        /// </summary>
        // Equality based only on the (StudentId, CourseId) pair.
        public bool Equals(Enrollment? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return StudentId == other.StudentId && CourseId == other.CourseId;
        }

        public override bool Equals(object? obj)
            => obj is Enrollment other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(StudentId, CourseId);

        /// <summary>
        /// Expected state machine (documented expectations):
        /// - Requested -> Enrolled | Cancelled
        /// - Enrolled -> Dropped | Completed
        /// - Dropped -> (terminal)
        /// - Completed -> (terminal)
        /// - Cancelled -> (terminal)
        /// Transitions should validate allowed moves and record timestamps or reasons where applicable.
        /// These helpers enforce valid transitions and throw <see cref="University.Enrollments.Domain.DomainException"/>
        /// when an invalid transition is attempted. Messages include the current status and the
        /// identifying (StudentId, CourseId) pair to aid diagnostics.
        /// </summary>
        // Minimal state transition helpers
        public void Complete()
        {
            if (Status != EnrollmentStatus.Enrolled)
            {
                throw new University.Enrollments.Domain.DomainException($"Cannot complete enrollment for student {StudentId} in course {CourseId}: current status is {Status}.");
            }

            Status = EnrollmentStatus.Completed;
        }

        public void Cancel()
        {
            if (Status != EnrollmentStatus.Requested)
            {
                throw new University.Enrollments.Domain.DomainException($"Cannot cancel enrollment for student {StudentId} in course {CourseId}: current status is {Status}.");
            }

            Status = EnrollmentStatus.Cancelled;
        }

        public void Drop()
        {
            if (Status != EnrollmentStatus.Enrolled)
            {
                throw new University.Enrollments.Domain.DomainException($"Cannot drop enrollment for student {StudentId} in course {CourseId}: current status is {Status}.");
            }

            Status = EnrollmentStatus.Dropped;
        }

        public void Confirm()
        {
            if (Status != EnrollmentStatus.Requested)
            {
                throw new University.Enrollments.Domain.DomainException($"Cannot confirm enrollment for student {StudentId} in course {CourseId}: current status is {Status}.");
            }

            Status = EnrollmentStatus.Enrolled;
        }

        // TODO: Add unit tests that assert allowed transitions and that invalid transitions are rejected.
    }
}
