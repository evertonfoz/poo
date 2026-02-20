using System;
using University.Enrollments.Domain.Models;
using University.Enrollments.Domain;

// Simple demo program that uses domain APIs only (no domain logic duplicated here).
var today = DateOnly.FromDateTime(DateTime.UtcNow);

var course = new Course
{
	Id = 1,
	Title = "Intro to Programming",
	Capacity = 2,
	MatriculationStart = today.AddDays(-1),
	MatriculationEnd = today.AddDays(7)
};

var students = new[]
{
	new Student { Id = 1, Name = "Alice" },
	new Student { Id = 2, Name = "Bob" },
	new Student { Id = 3, Name = "Carol" }
};

Console.WriteLine($"Course: {course.Title} (Id={course.Id}), Capacity={course.Capacity}");
Console.WriteLine($"Matriculation window: {course.MatriculationStart:yyyy-MM-dd} - {course.MatriculationEnd:yyyy-MM-dd}");
Console.WriteLine();

for (int i = 0; i < students.Length; i++)
{
	var s = students[i];
	try
	{
		Console.WriteLine($"Enrolling student {s.Id} - {s.Name}...");
		course.Enroll(s.Id);
		Console.WriteLine($"  -> Success: student {s.Id} enrolled. Enrolled count: {course.EnrolledCount}");
	}
	catch (DomainException ex)
	{
		// Friendly error message for the console; preserve domain exception message for details.
		Console.WriteLine($"  -> Failed to enroll student {s.Id} ({s.Name}): {ex.Message}");
	}
	catch (Exception ex)
	{
		Console.WriteLine($"  -> Unexpected error enrolling student {s.Id}: {ex.Message}");
	}

	Console.WriteLine();
}

Console.WriteLine("Final enrollments:");
foreach (var e in course.Enrollments)
{
	Console.WriteLine($" - Student {e.StudentId}: Status={e.Status}, EnrolledOn={e.EnrolledOn:yyyy-MM-dd}");
}

Console.WriteLine($"Total enrolled: {course.EnrolledCount} / {course.Capacity}");
