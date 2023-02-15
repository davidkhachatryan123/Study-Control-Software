using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Interfaces.Users;
using StudyControlSoftware_API.Services.Base;

namespace StudyControlSoftware_API.Services.Users
{
    public class LecturersRepository : UsersBaseRepository<Lecturer>, ILecturersRepository
    {
        public LecturersRepository(
            ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            IMapper mapper) : base(context, userManager, mapper) { }

        public async Task<UserDto?> CreateLecturer(UserRegisterDto user)
            => await CreateAsync(user);
    }
}
