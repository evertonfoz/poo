using System;

namespace University.Enrollments.Domain
{
    /// <summary>
    /// Simple domain-level exception used to signal rule violations.
    /// Use this to provide domain-specific error messages with optional inner exceptions
    /// so callers can add contextual information while preserving the original cause.
    /// </summary>
    public sealed class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of <see cref="DomainException"/> with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference.</param>
        public DomainException(string message, Exception? innerException) : base(message, innerException) { }
    }
}
