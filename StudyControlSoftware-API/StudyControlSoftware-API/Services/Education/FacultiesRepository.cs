using AutoMapper;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Interfaces.Education;
using StudyControlSoftware_API.Services.Base;

namespace StudyControlSoftware_API.Services.Education
{
	public class FacultiesRepository : EducationBaseRepository<Faculty>, ICoursesRepository
    {
        public FacultiesRepository(
            ApplicationContext context,
            IMapper mapper) : base(context, mapper) { }
    }
}

