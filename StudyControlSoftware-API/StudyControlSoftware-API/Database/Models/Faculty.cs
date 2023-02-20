using System.Text.Json.Serialization;
using StudyControlSoftware_API.Database.Base;

namespace StudyControlSoftware_API.Database.Models
{
    public class Faculty : Identity
    {
        public Faculty()
        {
            Students = new HashSet<Student>();
            Courses = new HashSet<FacultyCourse>();
        }

        public string Name { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; }
        [JsonIgnore]
        public virtual ICollection<FacultyCourse> Courses { get; set; }
    }
}
