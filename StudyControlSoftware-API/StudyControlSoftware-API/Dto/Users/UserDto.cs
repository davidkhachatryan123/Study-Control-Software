﻿using System.ComponentModel.DataAnnotations;

namespace StudyControlSoftware_API.Dto.Users
{
    public class UserDto
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = null!;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        [Required(ErrorMessage = "UserName is required")]
        public string Username { get; set; } = null!;


        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;

        public bool EmailConfirmed { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
