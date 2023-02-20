using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Shared;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Interfaces.Base;
using StudyControlSoftware_API.Services.Base;

namespace StudyControlSoftware_API.Services.Users
{
    public class StudentsRepository : UsersBaseRepository<Student>, IUsersBase
    {
        public StudentsRepository(
            ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IMapper mapper) : base(context, userManager, passwordHasher, mapper) { }

        public async Task<TablesDataDto<UserDto>> GetAllAsync(TableOptionsDto options)
            => await base.FindAllAsync(options, x => x.StudentId);
    }
}

