using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Education;
using StudyControlSoftware_API.Interfaces.Auth;
using StudyControlSoftware_API.Interfaces.Base;
using StudyControlSoftware_API.Interfaces.Education;
using StudyControlSoftware_API.Interfaces.Shared;
using StudyControlSoftware_API.Services.Auth;
using StudyControlSoftware_API.Services.Education;
using StudyControlSoftware_API.Services.Users;

namespace StudyControlSoftware_API.Interfaces
{
    public interface IRepositoryManager
    {
        IUserAuthenticationRepository UserAuthentication { get; }

        IUsersBase Admins { get; }
        IUsersBase Lecturers { get; }
        StudentsRepository Students { get; }

        FacultiesRepository<Faculty, FacultyDto> Faculties { get; }
        CoursesRepository<Course, CourseDto> Courses { get; }

        IEmailRepository Email { get; }
        IAssetsRepository Assets { get; }

        Task SaveAsync();
    }
}
