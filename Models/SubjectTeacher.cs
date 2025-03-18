using System.Text.Json.Serialization;

namespace ToDoApi.Models
{
    public class SubjectTeacher
    {
        public long SubjectId { get; set; }

        [JsonIgnore] //circular reference
        public Subject? Subject { get; set; }

        public long TeacherId { get; set; }

        [JsonIgnore] //circular reference
        public Teacher? Teacher { get; set; }
    }
}
