namespace ToDoApi.Models
{
    public class Day
    {
        public long Id { get; set; }
        public required DateTime Date { get; set; }
        public ICollection<EducationClassDay> EducationClassDays { get; set; } = new List<EducationClassDay>();
        private long? MaxHours { get; set; }
        private long SpentHours { get; set; }
       
        public Day() {
            MaxHours = 6;
            SpentHours = 0;
        }
        public long GetSpentHours()
        {
            long hours = 0;
            // Iterate through the related EducationClassDays
            foreach (var educationClassDay in EducationClassDays)
            {
                // Access the related EducationClass and add its Hours
                if (educationClassDay.EducationClass != null)
                {
                    hours += educationClassDay.EducationClass.Hours;
                }
            }
            return hours;
        }


        public long? GetMaxHours()
        {
            return MaxHours;
        }
    }
}
