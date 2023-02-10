using StudyControlSoftware_API.Database.Base;

namespace StudyControlSoftware_API.Database.Models
{
    public class Course : Identity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int LecturerId { get; set; }

        public virtual ApplicationUser Lecturer { get; set; } = null!;
    }
}
