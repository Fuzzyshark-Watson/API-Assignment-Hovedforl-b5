using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Models
{
    public class TeacherContext : DbContext
    {
        public TeacherContext(DbContextOptions<TeacherContext> options) : base(options)
        {

        }

        public DbSet<EducationClass> Teachers { get; set; } = null!;
    }
}
