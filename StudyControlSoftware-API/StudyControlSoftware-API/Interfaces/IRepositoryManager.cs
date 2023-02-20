using StudyControlSoftware_API.Database.Models;
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

        IEducationBase<Course> Courses { get; }
        IEducationBase<Faculty> Faculties { get; }

        IEmailRepository Email { get; }
        IAssetsRepository Assets { get; }

        Task SaveAsync();
    }
}
