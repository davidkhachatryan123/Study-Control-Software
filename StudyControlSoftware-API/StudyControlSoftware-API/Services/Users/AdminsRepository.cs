using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Interfaces.Users;
using System;

namespace StudyControlSoftware_API.Services.Users
{
    public class AdminsRepository : IAdminsRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminsRepository(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        private Func<ApplicationUser, Object> orderByFunc = null!;

        public async Task<UsersTableDto> GetAdminsAsync(TableOptionsDto options)
        {
            switch ((UsersSort)Enum.Parse(typeof(UsersSort), options.Sort, true))
            {
                case UsersSort.Id:
                    orderByFunc = x => x.Id;
                    break;
                case UsersSort.Username:
                    orderByFunc = x => x.UserName!;
                    break;
                case UsersSort.Email:
                    orderByFunc = x => x.Email!;
                    break;
                case UsersSort.EmailConfirmed:
                    orderByFunc = x => x.EmailConfirmed;
                    break;
                case UsersSort.Phone:
                    orderByFunc = x => x.PhoneNumber!;
                    break;
            }

            return new UsersTableDto
            {
                Users = (options.OrderDirection == "asc"
                    ? (await _userManager.GetUsersInRoleAsync(nameof(UserRoles.Admin))).OrderBy(orderByFunc)
                    : (await _userManager.GetUsersInRoleAsync(nameof(UserRoles.Admin))).OrderByDescending(orderByFunc))
                        .Skip((options.Page - 1) * options.PageSize)
                        .Take(options.PageSize)
                        .Select(appUser =>
                        new UserDto
                        {
                            Id = appUser.Id,
                            UserName = appUser.UserName!,
                            Email = appUser.Email!,
                            EmailConfirmed = appUser.EmailConfirmed,
                            PhoneNumber = appUser.PhoneNumber
                        }),

                TotalCount = _userManager.GetUsersInRoleAsync(nameof(UserRoles.Admin)).Result.Count,
            };
        }
    }
}
