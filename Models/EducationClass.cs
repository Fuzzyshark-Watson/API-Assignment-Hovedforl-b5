namespace ToDoApi.Models
{
    public class EducationClass
    {
        public long Id { get; set; }
        public int Hours { get; set; }
        public long DayId { get; set; }
        public Day? Day { get; set; } 
        public List<EducationClassSubject> EducationClassSubjects { get; set; } = new();
        public List<EducationClassTeacher> EducationClassTeachers { get; set; } = new();
        public List<EducationClassDay> EducationClassDays { get; set; } = new(); 
    }
}
