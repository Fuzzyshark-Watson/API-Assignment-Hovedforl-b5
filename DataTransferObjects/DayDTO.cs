namespace ToDoApi.DataTransferObjects
{
    public class DayDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
