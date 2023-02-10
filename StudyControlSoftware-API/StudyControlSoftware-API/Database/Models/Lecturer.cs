using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyControlSoftware_API.Database.Models
{
    public class Lecturer
    {
        public Lecturer()
        {
            Courses = new HashSet<Course>();
        }

        [Key]
        [ForeignKey("ApplicationUser")]
        public string LecturerId { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
        public virtual ICollection<Course> Courses { get; set; }
    }
}
