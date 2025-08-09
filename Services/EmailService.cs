using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using ZhooNotification.API.Models;

namespace ZhooNotification.API.Services
{

    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class SendGridEmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public SendGridEmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string message)
        {
            await SendEmailWithCCAsync(to, subject, message);
        }

        public async Task SendEmailWithCCAsync(string to, string subject, string message, string cc = null)
        {
            var apiKey = _config["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("noreply@zhoosoft.com", "ZhooSoft Notification");
            var toEmail = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, message, message);

            if (!string.IsNullOrEmpty(cc))
            {
                msg.AddCc(new EmailAddress(cc));
            }

            await client.SendEmailAsync(msg);
        }
    }
}
