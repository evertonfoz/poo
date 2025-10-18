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
