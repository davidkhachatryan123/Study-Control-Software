using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudyControlSoftware_API.Database.Models
{
    public class Student
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string StudentId { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public int? FacultyId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
        public virtual Faculty? Faculty { get; set; }
    }
}
