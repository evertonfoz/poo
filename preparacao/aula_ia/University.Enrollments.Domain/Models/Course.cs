using System;

namespace University.Enrollments.Domain.Models
{
    /// <summary>
    /// Minimal representation of a course.
    /// </summary>
    public sealed class Course
    {
        /// <summary>
        /// Unique identifier for the course.
        /// Invariant: Id &gt; 0.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Title of the course.
        /// Invariant: not null or empty.
        /// </summary>
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// Maximum number of students allowed in the course.
        /// Invariant: Capacity &gt;= 0.
        /// </summary>
        public int Capacity { get; init; }

        /// <summary>
        /// Inclusive start date for matriculation window.
        /// </summary>
        public DateOnly MatriculationStart { get; init; }

        /// <summary>
        /// Inclusive end date for matriculation window.
        /// </summary>
        public DateOnly MatriculationEnd { get; init; }

        /// <summary>
        /// Expected operation: Enroll a student by id.
        /// Rules to document (no implementation here):
        /// - Must verify matriculation window (MatriculationStart <= today <= MatriculationEnd).
        /// - Must verify capacity has not been reached.
        /// - Should create an Enrollment in Requested or Enrolled state depending on policy.
        /// </summary>
        /// <param name="studentId">Identifier of the student to enroll.</param>
        public void Enroll(int studentId)
        {
            // TODO: Implement enrollment logic.
            // TODO: Add tests for matriculation window, capacity checks, and edge cases.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Expected operation: Unenroll a student by id.
        /// Rules to document (no implementation here):
        /// - Should update or remove the corresponding Enrollment.
        /// - Behavior when outside matriculation window should be defined (e.g., allow drop but not new enrollments).
        /// - Should free capacity for other students.
        /// </summary>
        /// <param name="studentId">Identifier of the student to unenroll.</param>
        public void Unenroll(int studentId)
        {
            // TODO: Implement unenrollment logic.
            // TODO: Add tests for unenroll behavior, capacity adjustments and window rules.
            throw new NotImplementedException();
        }
    }
}
