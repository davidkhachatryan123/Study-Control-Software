using System.ComponentModel.DataAnnotations;

namespace StudyControlSoftware_API.Dto.Users
{
    public class UserRegisterDto
    {
        public string? Id { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set;}


        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "PasswordConfirm is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; } = null!;


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }
    }
}
