namespace ToDoApi.Models
{

    //0: Bobby Dillarn
    //1: Tina Turné
    //2: Bom Tombadill 
    //3: Big Dick Mcdwarf
    //4: Turbo Tove
    //5: Pepperoni Peter
    public class Teacher
    {
        public long Id { get; set; }
        public required string Name { get; set; }

        public ICollection<SubjectTeacher> SubjectTeachers { get; set; } = new List<SubjectTeacher>();

        public ICollection<EducationClassTeacher> EducationClassTeachers { get; set; } = new List<EducationClassTeacher>();

    }
}
