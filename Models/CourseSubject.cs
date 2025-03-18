using System.Text.Json.Serialization;

namespace ToDoApi.Models
{
    public class CourseSubject
    {
        public long CourseId { get; set; }

        [JsonIgnore] //circular reference
        public Course? Course { get; set; }

        public long SubjectId { get; set; }

        [JsonIgnore] //circular reference
        public Subject? Subject { get; set; }
    }
}
