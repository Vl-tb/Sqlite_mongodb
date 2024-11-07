namespace MySolution.DAL;
public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
    // Навiгацiйна властивiсть для вiдношення "багато до багатьох"
    public ICollection<StudentCourse> StudentCourses { get; set; }
    public ICollection<ClassroomCourse> ClassroomCourses { get; set; }
    public ICollection<TeacherCourse> TeacherCourses { get; set; }
}