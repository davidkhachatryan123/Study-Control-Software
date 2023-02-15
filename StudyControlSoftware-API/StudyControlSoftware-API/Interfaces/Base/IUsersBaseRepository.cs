using StudyControlSoftware_API.Dto.Users;

namespace StudyControlSoftware_API.Interfaces.Base
{
    public interface IUsersBaseRepository<T>
    {
        Task<IQueryable<T>> FindAllAsync();
        Task<UserDto?> CreateAsync(UserRegisterDto user);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}
