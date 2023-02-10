using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyControlSoftware_API.Database.Models;

namespace StudyControlSoftware_API.Database.ModelConfigurations
{
    public class ApplicationUserConfigurationModel : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(64);
            builder.Property(x => x.LastName).HasMaxLength(64);
        }
    }
}
