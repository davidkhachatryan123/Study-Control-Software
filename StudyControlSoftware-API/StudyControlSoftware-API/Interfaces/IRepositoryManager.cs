using StudyControlSoftware_API.Interfaces.Auth;
using StudyControlSoftware_API.Interfaces.Base;
using StudyControlSoftware_API.Interfaces.Shared;

namespace StudyControlSoftware_API.Interfaces
{
    public interface IRepositoryManager
    {
        IUserAuthenticationRepository UserAuthentication { get; }
        IUsersBase Admins { get; }
        IUsersBase Lecturers { get; }
        IUsersBase Students { get; }

        IEmailRepository Email { get; }
        IAssetsRepository Assets { get; }

        Task SaveAsync();
    }
}
