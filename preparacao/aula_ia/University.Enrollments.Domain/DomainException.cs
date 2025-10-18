using System;

namespace University.Enrollments.Domain
{
    /// <summary>
    /// Simple domain-level exception used to signal rule violations.
    /// </summary>
    public sealed class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
