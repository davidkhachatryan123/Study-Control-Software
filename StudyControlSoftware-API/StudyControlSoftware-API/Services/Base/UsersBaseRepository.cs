using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Base;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Users;
using StudyControlSoftware_API.Enums;
using StudyControlSoftware_API.Interfaces.Base;

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

        public async Task<UsersTableDto> FindAllAsync(TableOptionsDto options)
        {
            
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
