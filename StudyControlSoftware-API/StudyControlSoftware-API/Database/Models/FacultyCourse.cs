using StudyControlSoftware_API.Database.Base;

namespace StudyControlSoftware_API.Database.Models
{
    public class FacultyCourse : Identity
    {
        public FacultyCourse()
        {
            Faculties = new HashSet<Faculty>();
            Courses = new HashSet<Course>();
        }

        public int FacultyId { get; set; }
        public int CourseId { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
