using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyControlSoftware_API.Database.Base;
using StudyControlSoftware_API.Database.Models;

namespace StudyControlSoftware_API.Database.ModelConfigurations
{
    public class FacultyModelConfiguration : IdentityModelConfiguration<Faculty>
    {
        protected override void AddBuilder(EntityTypeBuilder<Faculty> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(128);
        }

        protected override string TableName()
        {
            return nameof(Faculty) + 's';
        }
    }
}
