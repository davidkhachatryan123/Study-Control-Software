using AutoMapper;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Education;
using StudyControlSoftware_API.Interfaces.Education;
using StudyControlSoftware_API.Services.Base;

namespace StudyControlSoftware_API.Services.Education
{
    public class CoursesRepository : EducationBaseRepository<Course, CourseDto>, ICoursesRepository
    {
        public CoursesRepository(
            ApplicationContext context,
            IMapper mapper) : base(context, mapper) { }
    }
}

