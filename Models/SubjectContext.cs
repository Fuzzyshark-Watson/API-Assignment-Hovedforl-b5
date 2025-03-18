using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Models
{
    public class SubjectContext : DbContext
    {
        public SubjectContext(DbContextOptions<SubjectContext> options) : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; } = null!;
    }
}
