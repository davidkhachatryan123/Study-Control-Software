using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Shared;
using StudyControlSoftware_API.Dto.Users;

namespace StudyControlSoftware_API.Interfaces.Base
{
    public interface IUsersBaseRepository<T>
    {
        Task<TablesDataDto<UserDto>> FindAllAsync(TableOptionsDto options, Func<T, string> foreignKey);
        Task<UserDto?> CreateAsync(UserRegisterDto user);
        Task<UserDto?> UpdateAsync(string id, UserRegisterDto user);
        Task<string?> RemoveAsync(string id);
    }
}
