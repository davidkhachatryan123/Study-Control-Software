using Microsoft.AspNetCore.Identity;

namespace StudyControlSoftware_API.Database.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
