using AutoMapper;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Users;

namespace StudyControlSoftware_API.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();

            CreateMap<UserRegisterDto, ApplicationUser>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<ApplicationUser, UserRegisterDto>();
        }
    }
}
