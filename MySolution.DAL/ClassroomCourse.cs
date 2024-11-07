namespace MySolution.DAL
{
    public class ClassroomCourse
    {
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
