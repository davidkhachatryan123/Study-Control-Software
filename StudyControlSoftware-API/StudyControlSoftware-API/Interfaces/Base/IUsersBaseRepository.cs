using StudyControlSoftware_API.Database.Base;
using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Users;

namespace StudyControlSoftware_API.Interfaces.Base
{
    public interface IUsersBaseRepository<T>
    {
        Task<UsersTableDto> FindAllAsync(TableOptionsDto options, Func<T, string> foreignKey);
        Task<UserDto?> CreateAsync(UserRegisterDto user);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}
