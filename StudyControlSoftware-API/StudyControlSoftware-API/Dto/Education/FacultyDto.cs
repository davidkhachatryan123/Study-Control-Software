using System.ComponentModel.DataAnnotations;

namespace StudyControlSoftware_API.Dto.Education
{
    public class FacultyDto
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Name { get; set; } = null!;
    }
}
