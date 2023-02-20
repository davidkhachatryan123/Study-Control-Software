using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Education;

namespace StudyControlSoftware_API.Interfaces.Education
{
	public interface ICoursesRepository
	{
		public Task<CoursesTableDto> GetAllAsync(TableOptionsDto options);
		public Task<Course> CreateAsync(Course course);
		public Task<Course?> UpdateAsync(int id, Course course);
		public Task<int?> DeleteAsync(int id);
	}
}

