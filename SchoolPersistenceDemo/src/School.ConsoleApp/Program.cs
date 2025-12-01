using School.ConsoleApp.Menus;
using School.Persistence.AdoNet.Connections;
using School.Persistence.AdoNet.Repositories;

var connectionString = "Data Source=school.db";
var connectionFactory =
   new SqliteConnectionFactory(connectionString);

var courseRepository = new SqliteCourseRepository(connectionFactory);
var courseService = new CourseService(courseRepository);
var courseMenu = new CourseMenu(courseService);

courseMenu.Show();


