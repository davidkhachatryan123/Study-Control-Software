using System.ComponentModel.DataAnnotations;

namespace StudyControlSoftware_API.Dto.Auth
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; } = null!;
    }
}
