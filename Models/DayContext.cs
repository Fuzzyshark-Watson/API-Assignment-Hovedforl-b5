using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Models
{
    public class DayContext : DbContext
    {
        public DayContext(DbContextOptions<DayContext> options) : base(options)
        {
        }

        public DbSet<Day> Days { get; set; } = null!;
    }
}
