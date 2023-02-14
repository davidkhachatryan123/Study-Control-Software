using System.ComponentModel.DataAnnotations;

namespace StudyControlSoftware_API.Dto.Auth
{
    public class TwoFADto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; } = null!;

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; init; } = null!;
    }
}
