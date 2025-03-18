namespace ToDoApi.DataTransferObjects
{
    public class EducationClassCreateDTO
    {
        public long Id { get; set; } // Add this line
        public int Hours { get; set; }
        public long DayId { get; set; }
        public List<long> SubjectIds { get; set; } = new();
        public List<long> TeacherIds { get; set; } = new();
    }
}
