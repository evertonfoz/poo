using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using School.ConsoleApp.Menus;
using School.Domain.Domain.Repositories;
using School.Domain.Domain.Services;
using School.Domain.Services;
using School.Persistence.EfCore.Context;
using School.Persistence.EfCore.Repositories;

var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false,
               reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

var connectionString = configuration
   .GetConnectionString("Default");

// 2) Configurar EF Core (DbContextOptions)
var options = new DbContextOptionsBuilder<SchoolDbContext>()
    .UseSqlite(connectionString)
    .Options;

using var dbContext = new SchoolDbContext(options);

// 3) Instanciar repositórios EF Core
ICourseRepository courseRepository = new
   EfCoreCourseRepository(dbContext);
IStudentRepository studentRepository = new
   EfCoreStudentRepository(dbContext);
IEnrollmentRepository enrollmentRepository = new
   EfCoreEnrollmentRepository(dbContext);

// 4) Instanciar serviços
ICourseService courseService = new CourseService(courseRepository);
// IStudentService studentService = new StudentService(studentRepository);
// IEnrollmentService enrollmentService = new EnrollmentService(
//     enrollmentRepository, studentRepository, courseRepository);

// 5) Instanciar menus (agora consumindo services)
var courseMenu = new CourseMenu(courseService);
// var studentMenu = new StudentMenu(studentService);
// var enrollmentMenu = new EnrollmentMenu(enrollmentService,
//    studentService, courseService);


// using School.ConsoleApp.Menus;
// using School.Persistence.AdoNet.Connections;
// using School.Persistence.AdoNet.Repositories;

// var connectionString = "Data Source=school.db";
// var connectionFactory =
//    new SqliteConnectionFactory(connectionString);

// var courseRepository = new SqliteCourseRepository(connectionFactory);
// var courseService = new CourseService(courseRepository);
// var courseMenu = new CourseMenu(courseService);

courseMenu.Show();


