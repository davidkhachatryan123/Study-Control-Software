namespace StudyControlSoftware_API.Interfaces
{
    public interface IRepositoryManager
    {
        IUserAuthenticationRepository UserAuthentication { get; }
        IEmailRepository Email { get; }
        IAssetsRepository Assets { get; }

        Task SaveAsync();
    }
}
