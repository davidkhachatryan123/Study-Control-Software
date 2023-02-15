using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Users;

namespace StudyControlSoftware_API.Interfaces.Users
{
    public interface IAdminsRepository
    {
        Task<UsersTableDto> GetAllAsync(TableOptionsDto options);
        Task<UserDto?> CreateAsync(UserRegisterDto user);
        Task<UserDto?> UpdateAsync(string id, UserRegisterDto user);
        Task<string?> DeleteAsync(string id);
    }
}
