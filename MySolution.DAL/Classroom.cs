namespace MySolution.DAL
{
    public class Classroom
    {
        public int ClassroomId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        
        public ICollection<ClassroomCourse> ClassroomCourses { get; set; }
    }
}
