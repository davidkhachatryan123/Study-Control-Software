using Microsoft.AspNetCore.Identity;
using StudyControlSoftware_API.Interfaces;

namespace StudyControlSoftware_API.HostedServices
{
    public class SetupDefaultUserHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SetupDefaultUserHostedService> _logger;
        private readonly IConfiguration _configuration;

        public SetupDefaultUserHostedService(
            IServiceProvider serviceProvider,
            ILogger<SetupDefaultUserHostedService> logger,
            IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IRepositoryManager? repositoryManager =
                    scope.ServiceProvider.GetService<IRepositoryManager>();

                IdentityResult result = new IdentityResult();

                if (repositoryManager != null)
                    result = await repositoryManager.UserAuthentication.Setup();

                if (result.Succeeded)
                    _logger.LogInformation(
                        "Default User are created!" +
                        "\nUsername:......{0}" +
                        "\nPassword:......{1}" +
                        "\nEmail:.........{2}",
                        _configuration["DefaultUser:UserName"],
                        _configuration["DefaultUser:Password"],
                        _configuration["DefaultUser:Email"]);
            }
        }
    }
}
