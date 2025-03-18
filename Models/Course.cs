namespace ToDoApi.Models
{
    //0: H5PD010125
    //1: H5PD011125
    public class Course
    {
        public long Id { get; set; }
        public required string Name { get; set; }

        public ICollection<CourseSubject> CourseSubjects { get; set; } = new List<CourseSubject>();

    }
}
