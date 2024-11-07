namespace MySolution.DAL;
public class Teacher
{
    public int TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<TeacherCourse> TeacherCourses { get; set; }
}