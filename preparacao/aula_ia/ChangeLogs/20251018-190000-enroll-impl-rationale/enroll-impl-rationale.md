# Rationale: minimal Enroll implementation

Prompt: Implement minimal functionality so the test `Enroll_cria_vinculo_e_respeita_unicidade_do_par` passes.

Tasks performed:
- Implemented encapsulated storage of enrollments in `Course` as a private List<Enrollment> and exposed a read-only view via `IReadOnlyCollection<Enrollment> Enrollments`.
- Implemented `Enroll(int studentId)` on `Course` with the minimal checks:
  - Validate studentId > 0.
  - Enforce uniqueness of (StudentId, CourseId) pair; if duplicate, throw `DomainException` with a clear message.
  - Create an `Enrollment` with `EnrollmentStatus.Enrolled` and current UTC date.
- Added `DomainException` type to `University.Enrollments.Domain` to represent domain rule violations.

Design notes / assumptions:
- Navigation remains unidirectional: Course -> Enrollments. No back-references to Student are added.
- Capacity and matriculation window checks are intentionally omitted in this minimal change as the user requested the minimum to make the specific test pass.
- Equality semantics for `Enrollment` were not changed; uniqueness is enforced by checking the pair on insertion.

Next steps (optional):
- Implement capacity and matriculation window checks in `Enroll` and add corresponding tests.
- Implement `Unenroll` behavior and tests.
- Provide IEquatable implementation for `Enrollment` and unit tests for equality.

Update 2025-10-18 19:00: Matriculation window check
- Implemented matriculation window validation in `Course.Enroll`.
- Behavior: when current UTC date is outside `MatriculationStart`..`MatriculationEnd`, `Enroll` throws `DomainException` with message indicating today's date and the allowed window.

Update 2025-10-18 19:05: Capacity check
- Implemented capacity validation in `Course.Enroll`.
- Behavior: counts existing enrollments with `Status == EnrollmentStatus.Enrolled`. If `enrolledCount >= Capacity` (and Capacity >= 0) a `DomainException` is thrown with a clear message including the course id and capacity.

Update 2025-10-18 19:12: Unenroll removal
- Implemented `Course.Unenroll(int studentId)` to remove all matching enrollments for the given student and course.
- Behavior: if no matching enrollment was found, throws `DomainException` with a message indicating the student is not enrolled; otherwise removes the enrollment(s) from the internal list which implicitly frees seats.

Update 2025-10-18 19:20: Conclude transition
- Implemented minimal transition to `Completed`:
  - made `Enrollment.Status` mutable and added `Enrollment.Complete()` which only allows completing when status == Enrolled.
  - added `Course.Conclude(int studentId)` which finds the enrollment and calls `Complete()`. If no enrollment is found or transition invalid, a `DomainException` is thrown with contextual message.

Update 2025-10-18 19:30: State machine for Enrollment
- Implemented state transition helpers on `Enrollment`:
  - `Confirm()` transitions Requested -> Enrolled
  - `Cancel()` transitions Requested -> Cancelled
  - `Drop()` transitions Enrolled -> Dropped
  - `Complete()` transitions Enrolled -> Completed
- Each helper throws `DomainException` when called from an invalid current state, with clear messages indicating the invalid transition and current status.

Update 2025-10-18 19:40: Equality semantics
- Implemented `IEquatable<Enrollment>` and overrides for `Equals`/`GetHashCode`.
- Equality is based only on the pair `(StudentId, CourseId)` as requested; other properties like `Status` and `EnrolledOn` are ignored for equality purposes.

Update 2025-10-18 19:45: Unidirectional navigation helpers
- Added helper APIs on `Course` to support common queries while keeping navigation unidirectional:
  - `EnrolledCount` — number of students with `Status == Enrolled`.
  - `IsStudentEnrolled(int studentId)` — boolean check if a student is currently enrolled.
  - `GetEnrollment(int studentId)` — returns the `Enrollment?` if present.
- Rationale: these helpers allow application code to perform checks and read-only queries without adding back-references from Enrollment to Student or other bidirectional links.

