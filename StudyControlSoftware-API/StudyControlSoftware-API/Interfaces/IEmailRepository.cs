namespace StudyControlSoftware_API.Interfaces
{
    public interface IEmailRepository
    {
        public bool SendEmail(string to, string subject, string message);
    }
}
