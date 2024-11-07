namespace MySolution.DAL;
public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }
    public string PlaceOfBirth { get; set; }
    // Навiгацiйна властивiсть для вiдношення "багато до багатьох"
    public ICollection<StudentCourse> StudentCourses { get; set; }
}