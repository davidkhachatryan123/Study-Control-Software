using StudyControlSoftware_API.Database.Base;

namespace StudyControlSoftware_API.Database.Models
{
    public class Faculty : Identity
    {
        public Faculty()
        {
            Students = new HashSet<ApplicationUser>();
        }

        public string Name { get; set; } = null!;
        public int StudentId { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }
    }
}
