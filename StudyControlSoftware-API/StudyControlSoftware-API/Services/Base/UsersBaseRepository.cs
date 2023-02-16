using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Base;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Interfaces.Base;
using System.Reflection;

namespace StudyControlSoftware_API.Services.Base
{
    public class UsersBaseRepository<TEntity> : IUsersBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly IMapper _mapper;

        public UsersBaseRepository(
            ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UsersTableDto> FindAllAsync(TableOptionsDto options, Func<TEntity, string> foreignKey)
        {
            var propertyInfos =
               typeof(UserDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var objectProperty =
                propertyInfos.FirstOrDefault(pi => pi.Name.Equals(options.Sort, StringComparison.InvariantCultureIgnoreCase));

            return new UsersTableDto
            {
                Users = (await _userManager.GetUsersInRoleAsync(typeof(TEntity).Name))
                        .Join(_context.Set<TEntity>(), u => u.Id, foreignKey,
                            (u, e) => _mapper.Map(
                                e,
                                (_mapper.Map<ApplicationUser, UserDto>(u))))

                        .OrderBy(x => options.OrderDirection == "asc" ? objectProperty!.GetValue(x, null) : null)
                        .OrderByDescending(x => options.OrderDirection == "desc" ? objectProperty!.GetValue(x, null) : null)

                        .Skip((options.Page - 1) * options.PageSize)
                        .Take(options.PageSize)
                        .Select(appUser => _mapper.Map<UserDto>(appUser)),

                TotalCount = _userManager.GetUsersInRoleAsync(typeof(TEntity).Name).Result.Count
            };
        }

        public async Task<UserDto?> CreateAsync(UserRegisterDto user)
        {
            ApplicationUser newUser = _mapper.Map<ApplicationUser>(user);

            await _userManager.CreateAsync(newUser, user.Password);
            await _userManager.AddToRoleAsync(newUser, typeof(TEntity).Name);

            TEntity entity = _mapper.Map(
                user,
                _mapper.Map<ApplicationUser, TEntity>(newUser));

            await Task.Run(() => _context.Set<TEntity>().Add(entity));
            await _context.SaveChangesAsync();

            return _mapper.Map(
                entity,
                _mapper.Map<ApplicationUser, UserDto>(newUser));
        }

        public async Task UpdateAsync(TEntity entity)
            => await Task.Run(() => _context.Set<TEntity>().Update(entity));
        public async Task RemoveAsync(TEntity entity)
            => await Task.Run(() => _context.Set<TEntity>().Remove(entity));
    }
}
