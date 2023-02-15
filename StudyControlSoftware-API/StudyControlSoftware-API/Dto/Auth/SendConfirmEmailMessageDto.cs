using System;
using System.ComponentModel.DataAnnotations;

namespace StudyControlSoftware_API.Dto.Auth
{
    public class SendConfirmEmailMessageDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; } = null!;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; init; } = null!;
    }
}

