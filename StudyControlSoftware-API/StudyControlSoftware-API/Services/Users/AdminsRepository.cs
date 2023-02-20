using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Shared;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Interfaces.Base;
using System.Reflection;

namespace StudyControlSoftware_API.Services.Users
{
    public class AdminsRepository : IUsersBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IMapper _mapper;

        public AdminsRepository(
            UserManager<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IMapper mapper)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<TablesDataDto<UserDto>> GetAllAsync(TableOptionsDto options)
        {
            var propertyInfos = 
                typeof(ApplicationUser).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var objectProperty = 
                propertyInfos.FirstOrDefault(pi => pi.Name.Equals(options.Sort, StringComparison.InvariantCultureIgnoreCase));

            return new TablesDataDto<UserDto>
            {
                Entities = (await _userManager.GetUsersInRoleAsync(nameof(UserRoles.Admin)))

                        .OrderBy(x => options.OrderDirection == "asc" ? objectProperty!.GetValue(x, null) : null)
                        .OrderByDescending(x => options.OrderDirection == "desc" ? objectProperty!.GetValue(x, null) : null)

                        .Skip((options.Page - 1) * options.PageSize)
                        .Take(options.PageSize)
                        .Select(appUser => _mapper.Map<UserDto>(appUser)),

                TotalCount = _userManager.GetUsersInRoleAsync(nameof(UserRoles.Admin)).Result.Count
            };
        }

        public async Task<UserDto?> CreateAsync(UserRegisterDto user)
        {
            ApplicationUser newUser = _mapper.Map<ApplicationUser>(user);

            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded) return null;
            
            result = await _userManager.AddToRoleAsync(newUser, nameof(UserRoles.Admin));

            return result.Succeeded ? _mapper.Map<UserDto>(newUser) : null;
        }

        public async Task<UserDto?> UpdateAsync(string id, UserRegisterDto user)
        {
            var updateUser = await _userManager.FindByIdAsync(id);
            if (updateUser == null) return null;

            updateUser.UserName = user.UserName;
            updateUser.Email = user.Email;
            updateUser.EmailConfirmed = false;
            updateUser.PasswordHash =
                _passwordHasher.HashPassword(updateUser, user.Password);
            updateUser.PhoneNumber = user.PhoneNumber;

            var result = await _userManager.UpdateAsync(updateUser);

            return !result.Succeeded
                ? null
                : _mapper.Map<UserDto>(updateUser);
        }

        public async Task<string?> RemoveAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            var result = await _userManager.DeleteAsync(user);

            return !result.Succeeded
                ? null
                : id;
        }
    }
}
