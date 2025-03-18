using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Models
{
    public class CourseContext : DbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
        }
        public DbSet<ToDoApi.Models.Course> Course { get; set; } = default!;
    }
}
