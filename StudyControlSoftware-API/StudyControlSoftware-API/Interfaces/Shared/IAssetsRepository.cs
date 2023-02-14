namespace StudyControlSoftware_API.Interfaces.Shared
{
    public interface IAssetsRepository
    {
        public string Get2FAMessage(string code);
        public string GetEmailConfirmationMessage(string confirmationLink);
        public string GetRedirectToLogin();
    }
}
