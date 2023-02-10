using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyControlSoftware_API.Database.Base;
using StudyControlSoftware_API.Database.ModelConfigurations;
using StudyControlSoftware_API.Database.Models;

namespace StudyControlSoftware_API.Database
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<FacultyCourse> FacultysCourses { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Student> Students { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityModelConfiguration<Identity>).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
