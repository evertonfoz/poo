using System;
using School.Domain.Domain.Services;
using School.Domain.Entities;

namespace School.ConsoleApp.Menus;

public class CourseMenu(ICourseService courseService)
{
    private readonly ICourseService _courseService = courseService;

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Course Menu");
            Console.WriteLine("1. List Courses");
            Console.WriteLine("2. Add Course");
            Console.WriteLine("3. Remove Course");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Select an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    // ListCourses();
                    break;
                case "2":
                    CreateCourse();
                    break;
                case "3":
                    // RemoveCourse();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void CreateCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Cadastro de Curso ===");
            Console.Write("Nome: ");
            var name = Console.ReadLine() ?? string.Empty;

            Console.Write("Carga horária (horas): ");
            var workloadText = Console.ReadLine();
            int.TryParse(workloadText, out var workloadHours);

            Console.Write("Curso ativo? (s/n): ");
            var activeInput = Console.ReadLine();
            var isActive = string.Equals(activeInput, "s", 
                StringComparison.OrdinalIgnoreCase);

            _courseService.CreateCourse(name: name, workloadHours: workloadHours, isActive: isActive);
            Console.WriteLine("Course created successfully. Press any key to continue.");
            Console.ReadKey();
        }
}
