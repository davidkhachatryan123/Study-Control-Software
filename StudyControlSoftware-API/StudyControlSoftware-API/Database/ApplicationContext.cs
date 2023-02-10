using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyControlSoftware_API.Database.Base;
using StudyControlSoftware_API.Database.ModelConfigurations;
using StudyControlSoftware_API.Database.Models;

namespace StudyControlSoftware_API.Database
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<FacultyCourse> FacultysCourses { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityModelConfiguration<Identity>).Assembly);
            modelBuilder.ApplyConfiguration(new ApplicationUserConfigurationModel());

            base.OnModelCreating(modelBuilder);
        }
    }
}
