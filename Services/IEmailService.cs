using ZhooNotification.API.Models;

namespace ZhooNotification.API.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string message);
        Task SendEmailWithCCAsync(string to, string subject, string message, string cc = null);
    }

}
