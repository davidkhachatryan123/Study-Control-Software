using StudyControlSoftware_API.Dto.Users;

namespace StudyControlSoftware_API.Interfaces.Users
{
    public interface ILecturersRepository
    {
        public Task<UserDto?> CreateLecturer(UserRegisterDto user);
    }
}
