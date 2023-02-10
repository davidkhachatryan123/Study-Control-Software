using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyControlSoftware_API.Database.Base;
using StudyControlSoftware_API.Database.Models;
using System.Reflection.Metadata.Ecma335;

namespace StudyControlSoftware_API.Database.ModelConfigurations
{
    public class CourseModelConfiguraton : IdentityModelConfiguration<Course>
    {
        protected override void AddBuilder(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(128);
            builder.Property(x => x.Description).HasMaxLength(255);
        }

        protected override string TableName()
        {
            return nameof(Course) + 's';
        }
    }
}
