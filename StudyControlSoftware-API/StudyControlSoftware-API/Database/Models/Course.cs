using System.Text.Json.Serialization;
using StudyControlSoftware_API.Database.Base;

namespace StudyControlSoftware_API.Database.Models
{
    public class Course : Identity
    {
        public Course()
        {
            Faculties = new HashSet<FacultyCourse>();
        }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string? LecturerId { get; set; }
        [JsonIgnore]
        public virtual Lecturer? Lecturer { get; set; }

        [JsonIgnore]
        public virtual ICollection<FacultyCourse> Faculties { get; set; }
    }
}
