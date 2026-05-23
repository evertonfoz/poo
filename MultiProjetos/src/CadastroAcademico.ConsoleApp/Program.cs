using CadastroAcademico.Application.Common;
using CadastroAcademico.Application.Services;
using CadastroAcademico.ConsoleApp.Menus;

var store = new InMemoryAcademicStore();

var courseService = new CourseService(store);
var studentService = new StudentService(store);
var enrollmentService = new EnrollmentService(store);

var menu = new MenuPrincipal(courseService, studentService, enrollmentService);
menu.Executar();
