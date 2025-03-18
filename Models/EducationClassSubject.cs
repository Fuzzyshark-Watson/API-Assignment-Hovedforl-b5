using System.Text.Json.Serialization;

namespace ToDoApi.Models
{
    public class EducationClassSubject
    {
        public long EducationClassId { get; set; }

        [JsonIgnore] // This will prevent the circular reference
        public EducationClass EducationClass { get; set; }

        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
