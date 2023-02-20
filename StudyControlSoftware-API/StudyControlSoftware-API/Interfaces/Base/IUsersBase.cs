using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Shared;
using StudyControlSoftware_API.Dto.Users;

namespace StudyControlSoftware_API.Interfaces.Base
{
    public interface IUsersBase
    {
        Task<TablesDataDto<UserDto>> GetAllAsync(TableOptionsDto options);
        Task<UserDto?> CreateAsync(UserRegisterDto user);
        Task<UserDto?> UpdateAsync(string id, UserRegisterDto user);
        Task<string?> RemoveAsync(string id);
    }
}

