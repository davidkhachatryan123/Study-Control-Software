namespace StudyControlSoftware_API.Interfaces
{
    public interface IRepositoryManager
    {
        IUserAuthenticationRepository UserAuthentication { get; }
        Task SaveAsync();
    }
}
