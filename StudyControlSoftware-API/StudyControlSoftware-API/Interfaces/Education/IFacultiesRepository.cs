using StudyControlSoftware_API.Dto.Education;

namespace StudyControlSoftware_API.Interfaces.Education
{
	public interface IFacultiesRepository
    {
        public Task<IEnumerable<CourseDto>> GetAllCoursesAsync(int id);
        public Task AddCoursesAsync(int id, IEnumerable<int> courses);
        public Task<bool> DeleteCourseAsync(int id, int courseId);
    }
}

