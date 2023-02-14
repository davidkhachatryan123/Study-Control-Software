using System.ComponentModel.DataAnnotations;

namespace StudyControlSoftware_API.Dto.Auth
{
    public class ConfirmEmailDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; } = null!;
    }
}
