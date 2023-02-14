using StudyControlSoftware_API.Dto;
using StudyControlSoftware_API.Dto.Users;

namespace StudyControlSoftware_API.Interfaces.Users
{
    public interface IAdminsRepository
    {
        Task<UsersTableDto> GetAdminsAsync(TableOptionsDto options);
    }
}
