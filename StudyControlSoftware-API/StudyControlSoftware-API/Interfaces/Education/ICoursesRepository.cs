using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Users;

namespace StudyControlSoftware_API.Interfaces.Education
{
	public interface ICoursesRepository
	{
        public Task<LecturerDto?> GetLecturerAsync(int id);
        public Task<LecturerDto?> SetLecturerAsync(int id, string lecturerId);
        public Task<LecturerDto?> DeleteLecturerAsync(int id);
    }
}

