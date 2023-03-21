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
            CreateMap<ApplicationUser, UserRegisterDto>();

            CreateMap<UserRegisterDto, ApplicationUser>()
                .ForMember(x => x.Id, opt => opt.Ignore());


            CreateMap<ApplicationUser, Lecturer>()
                .ForMember(l => l.LecturerId, opt => opt.MapFrom(u => u.Id));
            CreateMap<UserRegisterDto, Lecturer>();

            CreateMap<Lecturer, UserDto>();


            CreateMap<ApplicationUser, Student>()
                .ForMember(l => l.StudentId, opt => opt.MapFrom(u => u.Id));
            CreateMap<UserRegisterDto, Student>();

            CreateMap<Student, UserDto>();


            CreateMap<Lecturer, LecturerDto>()
                .ForMember(l => l.Id, opt => opt.MapFrom(u => u.LecturerId));
        }
    }
}
