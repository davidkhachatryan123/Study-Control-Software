using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Interfaces.Base;
using StudyControlSoftware_API.Services.Base;

namespace StudyControlSoftware_API.Services.Users
{
    public class LecturersRepository : UsersBaseRepository<Lecturer>, IUsersBase
    {
        public LecturersRepository(
            ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IMapper mapper) : base(context, userManager, passwordHasher, mapper) { }

        public async Task<UsersTableDto> GetAllAsync(TableOptionsDto options)
            => await base.FindAllAsync(options, x => x.LecturerId);
    }
}
