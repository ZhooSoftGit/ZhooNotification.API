using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhooNotification.API.Data;
using ZhooNotification.API.DTO;
using ZhooNotification.API.Models;
using ZhooNotification.API.Services;

namespace ZhooNotification.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public ContactController(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send(ContactMessageDto dto)
        {
            var message = new ContactMessage
            {
                Name = dto.Name,
                Mail = dto.Mail,
                Subject = dto.Subject,
                Message = dto.Message,
                QueryType = dto.QueryType
            };

            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();

            // Confirmation mail
            await _emailService.SendEmailAsync(dto.Mail, "Thank you for contacting ZhooSoft", "We will get back to you shortly.");

            // Internal notification
            string internalMessage = $@"
New contact message received:

Name: {dto.Name}
Email: {dto.Mail}
Subject: {dto.Subject}
Query Type: {dto.QueryType ?? "N/A"}
Message: {dto.Message}
";

            await _emailService.SendEmailAsync("contact@zhoosoft.com", "New Contact Message", internalMessage);

            return Ok(new { message = "Message sent successfully." });
        }

    }

}
