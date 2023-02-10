using StudyControlSoftware_API.Database.Base;

namespace StudyControlSoftware_API.Database.Models
{
    public class FacultyCourse: Identity
    {
        public int FacultyId { get; set; }
        public int CourseId { get; set; }

        public virtual Faculty Faculty { get; set; } = null!;
        public virtual Course Course { get; set; } = null!;
    }
}
