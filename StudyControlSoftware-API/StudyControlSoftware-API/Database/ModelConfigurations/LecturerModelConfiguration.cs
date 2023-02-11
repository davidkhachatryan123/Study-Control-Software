using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyControlSoftware_API.Database.Base;
using StudyControlSoftware_API.Database.Models;

namespace StudyControlSoftware_API.Database.ModelConfigurations
{
    public class LecturerModelConfiguration : IEntityTypeConfiguration<Lecturer>
    {
        public void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(256);
            builder.Property(x => x.LastName).HasMaxLength(256);
        }
    }
}
