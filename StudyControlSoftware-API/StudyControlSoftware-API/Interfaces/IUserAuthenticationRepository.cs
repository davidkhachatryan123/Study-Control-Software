using Microsoft.AspNetCore.Identity;

namespace StudyControlSoftware_API.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        Task<IdentityResult> Setup();
    }
}
