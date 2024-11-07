using MySolution.DAL;
using Microsoft.Extensions.Logging;

namespace MySolution.Services
{
    public class SamplerService
    {
        private readonly LabDbContext _context;
        private readonly ILogger<SamplerService> _logger;

        public SamplerService(LabDbContext context, ILogger<SamplerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Sample()
        {
            try
            {
                if (_context.Students.Any() || _context.Courses.Any())
                {
                    _logger.LogInformation("Database already populated.");
                    return;
                }

                var course1 = new Course { Title = "Math 101", Credits = 3 };
                var course2 = new Course { Title = "English 101", Credits = 2 };
                var course3 = new Course { Title = "History 101", Credits = 3 };

                _context.Courses.AddRange(course1, course2, course3);

                var student1 = new Student
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(2005, 5, 1),
                    PlaceOfBirth = "Kyiv"
                };

                var student2 = new Student
                {
                    FirstName = "Anna",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(2004, 6, 15),
                    PlaceOfBirth = "Lviv"
                };

                var student3 = new Student
                {
                    FirstName = "David",
                    LastName = "Brown",
                    DateOfBirth = new DateTime(2005, 7, 20),
                    PlaceOfBirth = "Odesa"
                };

                var student4 = new Student
                {
                    FirstName = "Olga",
                    LastName = "Petrova",
                    DateOfBirth = new DateTime(2004, 3, 5),
                    PlaceOfBirth = "Kharkiv"
                };

                var student5 = new Student
                {
                    FirstName = "Serhiy",
                    LastName = "Kovalenko",
                    DateOfBirth = new DateTime(2005, 12, 10),
                    PlaceOfBirth = "Dnipro"
                };

                _context.Students.AddRange(student1, student2, student3, student4, student5);
                _context.SaveChanges();

                var teacher1 = new Teacher { FirstName = "Leonhard", LastName = "Euler" };
                var teacher2 = new Teacher { FirstName = "Mary", LastName = "Williams" };
                var teacher3 = new Teacher { FirstName = "Winston", LastName = "Churchill" };
                
                _context.Teachers.AddRange(teacher1, teacher2, teacher3);
                _context.SaveChanges();

                var classroom1 = new Classroom
                {
                    Name = "Room 123",
                    Location = "Building 1, 2nd Floor"
                };

                var classroom2 = new Classroom
                {
                    Name = "Room 402",
                    Location = "Building 2, 3rd Floor"
                };

                var classroom3 = new Classroom
                {
                    Name = "Lectorium",
                    Location = "Building 1, Ground Floor"
                };
                
                _context.Classrooms.AddRange(classroom1, classroom2, classroom3);
                _context.SaveChanges();

                var studentCourse1 = new StudentCourse { StudentId = student1.StudentId, CourseId = course1.CourseId };
                var studentCourse2 = new StudentCourse { StudentId = student1.StudentId, CourseId = course2.CourseId };
                var studentCourse3 = new StudentCourse { StudentId = student2.StudentId, CourseId = course3.CourseId };

                _context.StudentCourses.AddRange(studentCourse1, studentCourse2, studentCourse3);
                _context.SaveChanges();

                var teacherCourse1 = new TeacherCourse { TeacherId = teacher1.TeacherId, CourseId = course1.CourseId };
                var teacherCourse2 = new TeacherCourse { TeacherId = teacher2.TeacherId, CourseId = course2.CourseId };
                var teacherCourse3 = new TeacherCourse { TeacherId = teacher3.TeacherId, CourseId = course3.CourseId };

                _context.TeacherCourses.AddRange(teacherCourse1, teacherCourse2, teacherCourse3);
                _context.SaveChanges();

                var classroomCourse1 = new ClassroomCourse { ClassroomId = classroom1.ClassroomId, CourseId = course3.CourseId };
                var classroomCourse2 = new ClassroomCourse { ClassroomId = classroom2.ClassroomId, CourseId = course2.CourseId };
                var classroomCourse3 = new ClassroomCourse { ClassroomId = classroom3.ClassroomId, CourseId = course1.CourseId };

                _context.ClassroomCourses.AddRange(classroomCourse1, classroomCourse2, classroomCourse3);
                _context.SaveChanges();

                _logger.LogInformation("Database populated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while seeding the database: " + ex.Message);
            }
        }
    }
}
