using AutoMapper;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Education;

namespace StudyControlSoftware_API.Mappings
{
    public class EducationMappingProfile : Profile
    {
        public EducationMappingProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<Faculty, FacultyDto>();

            CreateMap<CourseDto, Course>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<FacultyDto, Faculty>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}

