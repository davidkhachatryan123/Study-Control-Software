using StudyControlSoftware_API.Interfaces.Shared;

namespace StudyControlSoftware_API.Services.Shared
{
    public class AssetsRepository : IAssetsRepository
    {
        private readonly IConfiguration _configuration;

        public AssetsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Get2FAMessage(string token)
        {
            var content = File.ReadAllText(@"Assets/2FACode.html");
            content = content.Replace("{{token}}", token.Insert(3, " - "));
            return content;
        }

        public string GetEmailConfirmationMessage(string confirmationLink)
        {
            var content = File.ReadAllText(@"Assets/EmailConfirmation.html");
            content = content.Replace("{{link}}", confirmationLink);

            return content;
        }

        public string GetRedirectToLogin()
        {
            var content = File.ReadAllText(@"Assets/RedirectToLogin.html");
            content = content.Replace("{{url}}", _configuration["Client:LoginUrl"]);

            return content;
        }
    }
}
