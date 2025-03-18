using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Models
{
    public class EducationClassContext : DbContext
    {
        public EducationClassContext(DbContextOptions<EducationClassContext> options) : base(options)
        {
        }

        public DbSet<Day> Days { get; set; }
        public DbSet<EducationClass> EducationClasses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(c => c.CourseSubjects)
                .WithOne(cs => cs.Course)
                .HasForeignKey(cs => cs.CourseId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for Course

            // EDUCATION CLASS DAY BINDER
            modelBuilder.Entity<EducationClassDay>()
                .HasKey(ecd => new { ecd.EducationClassId, ecd.DayId });  // Composite primary key

            modelBuilder.Entity<EducationClassDay>()
                .HasOne(ecd => ecd.EducationClass)
                .WithMany(ec => ec.EducationClassDays)
                .HasForeignKey(ecd => ecd.EducationClassId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict delete for EducationClass to avoid cycles

            modelBuilder.Entity<EducationClassDay>()
                .HasOne(ecd => ecd.Day)
                .WithMany(d => d.EducationClassDays)
                .HasForeignKey(ecd => ecd.DayId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for Day

            // COURSE SUBJECT BINDER
            modelBuilder.Entity<CourseSubject>()
                .HasKey(css => new { css.CourseId, css.SubjectId });

            modelBuilder.Entity<CourseSubject>()
                .HasOne(css => css.Course)
                .WithMany(cs => cs.CourseSubjects)
                .HasForeignKey(css => css.CourseId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for Course

            modelBuilder.Entity<CourseSubject>()
                .HasOne(css => css.Subject)
                .WithMany(ss => ss.CourseSubjects)
                .HasForeignKey(css => css.SubjectId)
                .OnDelete(DeleteBehavior.NoAction); // No action on Subject

            // SUBJECT TEACHER BINDER
            modelBuilder.Entity<SubjectTeacher>()
                .HasKey(sts => new { sts.SubjectId, sts.TeacherId });

            modelBuilder.Entity<SubjectTeacher>()
                .HasOne(sts => sts.Subject)
                .WithMany(ss => ss.SubjectTeachers)
                .HasForeignKey(sts => sts.SubjectId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for Subject

            modelBuilder.Entity<SubjectTeacher>()
                .HasOne(sts => sts.Teacher)
                .WithMany(ts => ts.SubjectTeachers)
                .HasForeignKey(sts => sts.TeacherId)
                .OnDelete(DeleteBehavior.NoAction); // No action on Teacher

            // EDUCATION CLASS TEACHER BINDER
            modelBuilder.Entity<EducationClassTeacher>()
                .HasKey(sts => new { sts.EducationClassId, sts.TeacherId });

            modelBuilder.Entity<EducationClassTeacher>()
                .HasOne(sts => sts.EducationClass)
                .WithMany(ss => ss.EducationClassTeachers)
                .HasForeignKey(sts => sts.EducationClassId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict delete for EducationClass to avoid cycles

            modelBuilder.Entity<EducationClassTeacher>()
                .HasOne(sts => sts.Teacher)
                .WithMany(ts => ts.EducationClassTeachers)
                .HasForeignKey(sts => sts.TeacherId)
                .OnDelete(DeleteBehavior.NoAction); // No action on Teacher

            // EDUCATION CLASS SUBJECT BINDER
            modelBuilder.Entity<EducationClassSubject>()
                .HasKey(ecs => new { ecs.EducationClassId, ecs.SubjectId });

            modelBuilder.Entity<EducationClassSubject>()
                .HasOne(ecs => ecs.EducationClass)
                .WithMany(ec => ec.EducationClassSubjects)
                .HasForeignKey(ecs => ecs.EducationClassId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict delete for EducationClass to avoid cycles

            modelBuilder.Entity<EducationClassSubject>()
                .HasOne(ecs => ecs.Subject)
                .WithMany(ts => ts.EducationClassSubjects)
                .HasForeignKey(ecs => ecs.SubjectId)
                .OnDelete(DeleteBehavior.NoAction); // No action on Subject

            base.OnModelCreating(modelBuilder);
        }




    }
}
