namespace University.Enrollments.Domain.Models
{
    /// <summary>
    /// Represents the lifecycle status of an enrollment.
    /// </summary>
    public enum EnrollmentStatus
    {
        /// <summary>
        /// The student requested enrollment but it's not yet confirmed.
        /// </summary>
        Requested,

        /// <summary>
        /// The student is currently enrolled in the course.
        /// </summary>
        Enrolled,

        /// <summary>
        /// The student dropped the course after enrollment.
        /// </summary>
        Dropped,

        /// <summary>
        /// The student completed the course.
        /// </summary>
        Completed,

        /// <summary>
        /// The enrollment was cancelled before becoming active.
        /// </summary>
        Cancelled
    }
}
