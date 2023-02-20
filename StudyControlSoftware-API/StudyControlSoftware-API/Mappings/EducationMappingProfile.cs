using AutoMapper;
using StudyControlSoftware_API.Database.Models;

namespace StudyControlSoftware_API.Mappings
{
    public class EducationMappingProfile : Profile
    {
        public EducationMappingProfile()
        {
            CreateMap<Course, Course>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}

