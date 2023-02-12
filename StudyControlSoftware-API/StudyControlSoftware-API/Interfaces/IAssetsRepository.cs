namespace StudyControlSoftware_API.Interfaces
{
    public interface IAssetsRepository
    {
        public string Get2FAMessage(string code);
        public string GetEmailConfirmationMessage(string confirmationLink);
        public string GetRedirectToLogin();
    }
}
