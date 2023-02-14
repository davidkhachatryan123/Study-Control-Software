﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Interfaces.Users;
using System.Diagnostics.CodeAnalysis;

namespace StudyControlSoftware_API.Services.Users
{
    public class AdminsRepository : IAdminsRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AdminsRepository(
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        private Func<ApplicationUser, Object?> orderByFunc = null!;

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
                    orderByFunc = x => x.PhoneNumber;
                    break;
            }

            return new UsersTableDto
            {
                Users = (options.OrderDirection == "asc"
                    ? (await _userManager.GetUsersInRoleAsync(nameof(UserRoles.Admin))).OrderBy(orderByFunc)
                    : (await _userManager.GetUsersInRoleAsync(nameof(UserRoles.Admin))).OrderByDescending(orderByFunc))
                        .Skip((options.Page - 1) * options.PageSize)
                        .Take(options.PageSize)
                        .Select(appUser => _mapper.Map<UserDto>(appUser)),

                TotalCount = _userManager.GetUsersInRoleAsync(nameof(UserRoles.Admin)).Result.Count
            };
        }

        public async Task<UserDto?> RegisterNewAdmin(UserRegisterDto user)
        {
            ApplicationUser newUser = _mapper.Map<ApplicationUser>(user);

            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded) return null;
            
            result = await _userManager.AddToRoleAsync(newUser, nameof(UserRoles.Admin));

            return result.Succeeded ? _mapper.Map<UserDto>(newUser) : null;
        }
    }
}