using System.Text.Json.Serialization; //circular reference

namespace ToDoApi.Models
{
    public class EducationClassTeacher
    {
        public long EducationClassId { get; set; }

        [JsonIgnore] //circular reference
        public EducationClass EducationClass { get; set; }

        public long TeacherId { get; set; }

        [JsonIgnore] //circular reference
        public Teacher Teacher { get; set; }
    }
}
