using StudyControlSoftware_API.Interfaces.Auth;
using StudyControlSoftware_API.Interfaces.Shared;
using StudyControlSoftware_API.Interfaces.Users;

namespace StudyControlSoftware_API.Interfaces
{
    public interface IRepositoryManager
    {
        IUserAuthenticationRepository UserAuthentication { get; }
        IAdminsRepository Admins { get; }

        IEmailRepository Email { get; }
        IAssetsRepository Assets { get; }

        Task SaveAsync();
    }
}
