namespace StudyControlSoftware_API.Interfaces.Shared
{
    public interface IEmailRepository
    {
        public bool SendEmail(string to, string subject, string message);
    }
}
