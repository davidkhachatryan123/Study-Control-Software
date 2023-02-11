using AutoMapper;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;

namespace StudyControlSoftware_API.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserLoginDto, ApplicationUser>();
        }
    }
}
