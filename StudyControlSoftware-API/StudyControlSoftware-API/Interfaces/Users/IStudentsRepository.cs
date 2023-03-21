using StudyControlSoftware_API.Dto.Education;
using StudyControlSoftware_API.Dto.Users;

namespace StudyControlSoftware_API.Interfaces.Users
{
    public interface IStudentsRepository
    {
        public Task<FacultyDto?> GetFacultyAsync(string id);
        public Task<FacultyDto?> SetFacultyAsync(string id, int facultyId);
        public Task<FacultyDto?> DeleteFacultyAsync(string id);
    }
}
