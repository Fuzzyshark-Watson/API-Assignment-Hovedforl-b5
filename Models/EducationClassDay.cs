using System.Text.Json.Serialization;

namespace ToDoApi.Models
{
    public class EducationClassDay
    {
        public long EducationClassId { get; set; }

        [JsonIgnore] //circular reference
        public EducationClass? EducationClass { get; set; }

        public long DayId { get; set; }

        [JsonIgnore] //circular reference
        public Day? Day { get; set; }
    }
}
