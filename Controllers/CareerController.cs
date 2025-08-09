using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZhooNotification.API.Data;
using ZhooNotification.API.Helper;
using ZhooNotification.API.Models;
using ZhooNotification.API.Services;

namespace ZhooNotification.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CareerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICloudflareUploader _uploader;
        private readonly IEmailService _emailService;

        public CareerController(AppDbContext context, ICloudflareUploader uploader, IEmailService emailService)
        {
            _context = context;
            _uploader = uploader;
            _emailService = emailService;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> Apply([FromForm] CareerForm model)
        {
            string resumeUrl = await _uploader.GetPresignedUploadUrlAsync(model.Resume.Name,model.Resume.ContentType);

            var application = new CareerApplication
            {
                Name = model.Name,
                Mail = model.Mail,
                PhoneNo = model.PhoneNo,
                DesiredPosition = model.DesiredPosition,
                CoverLetter = model.CoverLetter,
                ResumeUrl = resumeUrl
            };

            _context.CareerApplications.Add(application);
            await _context.SaveChangesAsync();

            // Confirmation email to applicant
            await _emailService.SendEmailAsync(model.Mail, "Application Received", "Thank you for applying!");

            // Notification email to careers@zhoosoft.com
            string internalMessage = $@"
New career application received:

Name: {model.Name}
Email: {model.Mail}
Phone: {model.PhoneNo}
Desired Position: {model.DesiredPosition}
Cover Letter: {model.CoverLetter}
Resume: {resumeUrl}
";

            await _emailService.SendEmailAsync("careers@zhoosoft.com", "New Career Application", internalMessage);

            return Ok(new { message = "Application submitted successfully." });
        }

    }
}
