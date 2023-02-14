using StudyControlSoftware_API.Interfaces.Shared;
using System.Net.Mail;

namespace StudyControlSoftware_API.Services.Shared
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public EmailRepository(
           ILogger logger,
           IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public bool SendEmail(string to, string subject, string message)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration["MailServer:From"]!);
            mailMessage.To.Add(new MailAddress(to));

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;

            SmtpClient client = new SmtpClient(_configuration["MailServer:SmtpClient"]!, 587);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("xdavit7@gmail.com", _configuration["MailServer:Secret"]!);

            try
            {
                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot send Email to Address: {to}" +
                    $"\nException Message: {ex.Message}");
            }

            return false;
        }
    }
}
