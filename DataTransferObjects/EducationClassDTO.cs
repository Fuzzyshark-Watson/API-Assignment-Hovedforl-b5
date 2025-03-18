namespace ToDoApi.DataTransferObjects
{
    public class EducationClassDTO
    {
        public long Id { get; set; }
        public int Hours { get; set; }
        public long DayId { get; set; } // Reference to Day entity
        public DayDTO? Day { get; set; } // Optional detailed day info
        public List<SubjectDTO> Subjects { get; set; } = new();
        public List<TeacherDTO> Teachers { get; set; } = new();
    }
}
