using System.ComponentModel.DataAnnotations;

namespace StudyControlSoftware_API.Dto.Users
{
    public class LecturerDto
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; } = null!;
    }
}
