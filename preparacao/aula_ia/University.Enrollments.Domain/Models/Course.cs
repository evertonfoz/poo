using System;
using System.Collections.Generic;
using System.Linq;

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

    // Encapsulated internal storage for enrollments.
    // Navigation is unidirectional: Course -> Enrollments
    private readonly List<Enrollment> _enrollments = new();

    /// <summary>
    /// Read-only view of enrollments for this course.
    /// Kept intentionally as IReadOnlyCollection to preserve encapsulation.
    /// </summary>
    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

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
            if (studentId <= 0) throw new DomainException("Student id must be greater than zero.");

            // Uniqueness: (StudentId, CourseId) must be unique.
            // We consider two enrollments the same when they share StudentId and CourseId.
            var exists = _enrollments.Any(e => e.StudentId == studentId && e.CourseId == Id);
            if (exists)
            {
                throw new DomainException($"Student {studentId} is already enrolled in course {Id}.");
            }

            // Matriculation window check - must be within [MatriculationStart, MatriculationEnd]
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (today < MatriculationStart || today > MatriculationEnd)
            {
                throw new DomainException($"Cannot enroll: today ({today:yyyy-MM-dd}) is outside matriculation window ({MatriculationStart:yyyy-MM-dd} - {MatriculationEnd:yyyy-MM-dd}).");
            }

            // Minimal creation of the enrollment. Other rules (capacity, window) will be
            // added in later steps/tests. For now we create an enrolled entry.
            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = Id,
                Status = EnrollmentStatus.Enrolled,
                EnrolledOn = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            _enrollments.Add(enrollment);
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
